using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NavigationFlowSample.Core
{
    public interface IPage
    {
        void OnPageShown(NavigationParameters parameter);
        void OnPageLeave(NavigationParameters parameter);
    }

    public interface IDecideNavigation
    {
        Type DecideNavigation(NavigationContext context);
    }

    public enum NavigateActionType
    {
        None,
        Next,
        Back,
        Cancel,
        Complete,
        Terminate
    }

    public class NavigationContext
    {
        public NavigateActionType ActionType { get; set; }
        public Type PrevPage { get; set; }
        public Type CurrentPage { get; set; }
        public Type DefaultNextPage { get; set; }
        public Type NextPage { get; set; }
        public NavigationParameters Parameter { get; set; }
    }

    public class NavigationParameters : Dictionary<string, object>
    {
        public T GetValue<T>(string key)
        {
            if (TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return default;
        }

        public void AddParameter<T>(string key, T value)
        {
            this[key] = value;
        }
    }

    public class NavigationResult
    {
        public bool IsClosed { get; set; }
        public Type RedirectPage { get; set; }
        public NavigationParameters Parameter { get; set; }

        public NavigationResult(bool isClosed, Type redirectPage = null, NavigationParameters parameters = null)
        {
            IsClosed = isClosed;
            RedirectPage = redirectPage;
            Parameter = parameters;
        }

        public static NavigationResult None() => new(false);
        public static NavigationResult Complete() => new(true);
        public static NavigationResult Cancel() => new(true);
        public static NavigationResult Terminate() => new(true);
    }

    public class NavigationService : IDisposable
    {
        protected readonly ServiceProvider _serviceProvider;
        protected readonly Control _container;
        protected UserControl _currentPage;

        public NavigationContext Context { get; set; }

        // イベント引数クラスの定義
        public class NavigationEventArgs : EventArgs
        {
            public Type FromPageType { get; }
            public Type ToPageType { get; }
            public UserControl FromPage { get; }
            public UserControl ToPage { get; }
            public NavigationParameters Parameters { get; }
            public NavigateActionType ActionType { get; }

            public NavigationEventArgs(
                Type fromPageType,
                Type toPageType,
                UserControl fromPage,
                UserControl toPage,
                NavigationParameters parameters,
                NavigateActionType actionType)
            {
                FromPageType = fromPageType;
                ToPageType = toPageType;
                FromPage = fromPage;
                ToPage = toPage;
                Parameters = parameters;
                ActionType = actionType;
            }
        }

        // 遷移前イベントの定義
        public event EventHandler<NavigationEventArgs> PreNavigatedEvent;

        // 遷移後イベントの定義
        public event EventHandler<NavigationEventArgs> PostNavigatedEvent;

        public NavigationService(ServiceProvider serviceProvider, Control container, NavigationContext context = null)
        {
            _serviceProvider = serviceProvider;
            _container = container;
            Context = context ?? new NavigationContext();
        }

        public void NavigateTo<T>(NavigationParameters parameters = null)
        {
            NavigateTo(typeof(T), parameters);
        }

        public void NavigateTo(Type type, NavigationParameters parameters = null)
        {
            Type prevPageType = Context.PrevPage;
            Type currentPageType = Context.CurrentPage;
            UserControl currentPageControl = _currentPage;

            // 新しいページを作成
            UserControl newPage = (UserControl)_serviceProvider.GetService(type);

            // Contextを更新
            Context.PrevPage = currentPageType;
            Context.CurrentPage = type;
            Context.Parameter = parameters ?? new NavigationParameters();

            InternalNavigateTo(
                prevPageType,
                currentPageType,
                type,
                currentPageControl,
                newPage,
                parameters,
                NavigateActionType.None);
        }

        protected virtual void InternalNavigateTo(
            Type prev,
            Type from,
            Type to,
            UserControl ucfrom,
            UserControl ucto,
            NavigationParameters parameters,
            NavigateActionType actionType)
        {
            // 現在のページのOnPageLeaveを呼び出し
            if (ucfrom is IPage fromPage)
            {
                fromPage.OnPageLeave(parameters);
            }

            // コンテナから古いページを削除
            if (ucfrom != null && _container.Controls.Contains(ucfrom))
            {
                _container.Controls.Remove(ucfrom);
            }

            // 遷移前イベントを発火
            PreNavigatedEvent?.Invoke(this, new NavigationEventArgs(from, to, ucfrom, ucto, parameters, actionType));

            // 新しいページを追加
            if (ucto != null)
            {
                ucto.Dock = DockStyle.Fill;
                _container.Controls.Add(ucto);
                _currentPage = ucto;

                // 新しいページのOnPageShownを呼び出し
                if (ucto is IPage toPage)
                {
                    toPage.OnPageShown(parameters);
                }
            }

            // 遷移後イベントを発火
            PostNavigatedEvent?.Invoke(this, new NavigationEventArgs(from, to, ucfrom, ucto, parameters, actionType));
        }

        public virtual void Dispose()
        {
            // 外部で使用されているインスタンスは破棄せず、参照のみ解放する
            _currentPage = null;
            Context = null;

            // イベントハンドラーのクリア
            PreNavigatedEvent = null;
            PostNavigatedEvent = null;

            // GC.SuppressFinalize は不要なオブジェクトをGCが追跡しないようにするため呼び出す
            GC.SuppressFinalize(this);
        }
    }

    public class NavigationFlowService : NavigationService
    {
        private Stack<Type> _pageStack = new Stack<Type>();

        public Func<NavigationContext, NavigationResult> OnComplete;
        public Func<NavigationContext, NavigationResult> OnCancelled;
        public Func<NavigationContext, NavigationResult> OnTerminated;

        public NavigationFlowService(
            ServiceProvider serviceProvider, 
            Control container,
            NavigationContext context = null,
            Func<NavigationContext, NavigationResult> complete = null,
            Func<NavigationContext, NavigationResult> cancel = null,
            Func<NavigationContext, NavigationResult> teminate = null) : base(serviceProvider, container, context)
        {
            OnComplete = complete;
            OnCancelled = cancel;
            OnTerminated = teminate;
        }

        /// <summary>
        /// ナビゲーションフローを開始します
        /// </summary>
        /// <typeparam name="T">開始ページの型</typeparam>
        /// <param name="parameters">開始パラメータ</param>
        public void Start<T>(NavigationParameters parameters = null) where T : UserControl
        {
            Start(typeof(T), parameters);
        }

        /// <summary>
        /// ナビゲーションフローを開始します
        /// </summary>
        /// <param name="startPageType">開始ページの型</param>
        /// <param name="parameters">開始パラメータ</param>
        public void Start(Type startPageType, NavigationParameters parameters = null)
        {
            // フローをリセット
            _pageStack.Clear();

            // コンテキスト初期化
            Context.ActionType = NavigateActionType.None;
            Context.PrevPage = null;
            Context.DefaultNextPage = null;
            Context.NextPage = null;
            Context.Parameter = parameters ?? new NavigationParameters();

            // 最初のページに遷移
            NavigateTo(startPageType, parameters);
        }

        public void GoNext<T>(NavigationParameters tempdata = null)
        {
            GoNext(typeof(T), tempdata);
        }

        public void GoNext(Type type, NavigationParameters tempdata = null)
        {
            if (Context.CurrentPage != null)
            {
                _pageStack.Push(Context.CurrentPage);
            }

            Context.ActionType = NavigateActionType.Next;
            Context.DefaultNextPage = type;
            Context.NextPage = type;

            // IDecideNavigationインターフェースを実装している場合、次のページを決定
            if (_serviceProvider.GetService(Context.CurrentPage) is IDecideNavigation decider)
            {
                Type decidedType = decider.DecideNavigation(Context);
                if (decidedType != null)
                {
                    type = decidedType;
                }
            }

            NavigateTo(type, tempdata);
        }

        public void GoBack()
        {
            if (_pageStack.Count > 0)
            {
                Type prevPageType = _pageStack.Pop();
                Context.ActionType = NavigateActionType.Back;

                NavigateTo(prevPageType, Context.Parameter);
            }
        }

        public void TryJumpBack<T>()
        {
            TryJumpBack(typeof(T));
        }

        public void TryJumpBack(Type type)
        {
            // スタック内に指定されたタイプが存在するか確認
            if (_pageStack.Contains(type))
            {
                // 指定されたタイプに到達するまでスタックからポップ
                Stack<Type> tempStack = new Stack<Type>();
                while (_pageStack.Count > 0)
                {
                    Type pageType = _pageStack.Pop();
                    if (pageType == type)
                    {
                        // 目的のページが見つかった
                        Context.ActionType = NavigateActionType.Back;
                        NavigateTo(type, Context.Parameter);
                        return;
                    }
                    tempStack.Push(pageType);
                }

                // 見つからなかった場合、スタックを元に戻す
                while (tempStack.Count > 0)
                {
                    _pageStack.Push(tempStack.Pop());
                }
            }
        }

        public void JumpBuck<T>()
        {
            JumpBuck(typeof(T));
        }

        public void JumpBuck(Type type)
        {
            // 指定された型のページに直接戻る（中間ページのイベントは発生しない）
            if (_pageStack.Contains(type))
            {
                Stack<Type> tempStack = new Stack<Type>();
                bool found = false;

                // 目的のページを見つけるまでポップ
                while (_pageStack.Count > 0 && !found)
                {
                    Type pageType = _pageStack.Pop();
                    if (pageType == type)
                    {
                        found = true;
                    }
                    else
                    {
                        tempStack.Push(pageType);
                    }
                }

                if (found)
                {
                    Context.ActionType = NavigateActionType.Back;
                    UserControl currentPageControl = _currentPage;
                    UserControl newPage = (UserControl)_serviceProvider.GetService(type);

                    // Context を更新
                    Type currentPageType = Context.CurrentPage;
                    Context.PrevPage = currentPageType;
                    Context.CurrentPage = type;

                    InternalNavigateTo(
                        Context.PrevPage,
                        currentPageType,
                        type,
                        currentPageControl,
                        newPage,
                        Context.Parameter,
                        NavigateActionType.Back);
                }

                // 見つからなかった場合、スタックを元に戻す
                while (tempStack.Count > 0)
                {
                    _pageStack.Push(tempStack.Pop());
                }
            }
        }

        public void GoBackTo<T>()
        {
            GoBackTo(typeof(T));
        }

        public void GoBackTo(Type type)
        {
            // 指定された型のページまで順番に戻り、最終的に表示するのは指定された型のページのみ
            if (_pageStack.Contains(type))
            {
                Stack<Type> tempStack = new Stack<Type>();
                bool found = false;

                // 目的のページを見つけるまでポップ
                while (_pageStack.Count > 0 && !found)
                {
                    Type pageType = _pageStack.Pop();
                    if (pageType == type)
                    {
                        found = true;
                    }
                    else
                    {
                        tempStack.Push(pageType);
                    }
                }

                if (found)
                {
                    Context.ActionType = NavigateActionType.Back;
                    NavigateTo(type, Context.Parameter);
                }

                // 見つからなかった場合、スタックを元に戻す
                while (tempStack.Count > 0)
                {
                    _pageStack.Push(tempStack.Pop());
                }
            }
        }

        public void Cancel(NavigationParameters tempdatas = null)
        {
            var result = OnCancelled?.Invoke(Context) ?? NavigationResult.Cancel();
            HandleNavigationResult(result, tempdatas);
        }

        public void Complete(NavigationParameters tempdatas = null)
        {
            var result = OnComplete?.Invoke(Context) ?? NavigationResult.Complete();
            HandleNavigationResult(result, tempdatas);
        }

        public void Terminate(NavigationParameters tempdatas = null)
        {
            var result = OnTerminated?.Invoke(Context) ?? NavigationResult.Terminate();
            HandleNavigationResult(result, tempdatas);
        }

        private void HandleNavigationResult(NavigationResult result, NavigationParameters tempdatas)
        {
            if (result.IsClosed)
            {
                // フローが終了
                _pageStack.Clear();
            }

            if (result.RedirectPage != null)
            {
                // リダイレクト先が指定されている場合
                NavigateTo(result.RedirectPage, tempdatas);
            }

        }

        public override void Dispose()
        {
            // 独自のリソースの解放
            _pageStack.Clear();
            _pageStack = null;

            // イベントハンドラをクリア
            OnComplete = null;
            OnCancelled = null;
            OnTerminated = null;

            base.Dispose();
        }
    }
}
