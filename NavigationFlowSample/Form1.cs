using NavigationFlowSample.Core;
using NavigationFlowSample.Models;
using NavigationFlowSample.Pages;

namespace NavigationFlowSample
{
    public partial class Form1 : Form
    {
        ServiceProvider _provider;
        RegistData _registData;
        NavigationFlowService _nav;
        SnapshotManager<RegistData> _snapshotManager;

        public Form1()
        {
            InitializeComponent();
            _provider = new ServiceProvider();
            _registData = new RegistData();
            _snapshotManager = new SnapshotManager<RegistData>(_registData);
            _nav = new NavigationFlowService(_provider, this, null, OnComplete, OnCancel, OnTerminate);
            _nav.PreNavigatedEvent += PreNavigatedEvent;
            _nav.PostNavigatedEvent += PostNavigatedEvent;
            _provider.RegisterSingleton(_provider);
            _provider.RegisterSingleton(_nav);
            _provider.RegisterSingleton(new UcPageLogin(_provider, _registData));
            _provider.RegisterSingleton(new UcPageNumOfAllBoxes(_provider, _registData));
            _provider.RegisterSingleton(new UcPageOrderInfo(_provider, _registData));
            _provider.RegisterSingleton(new UcPageNumOfProductsPerBox(_provider, _registData));
            _provider.RegisterSingleton(new UcPageAssignBox(_provider, _registData));
            _provider.RegisterSingleton(new UcConfirmIncludePackage(_provider, _registData));
            _provider.RegisterSingleton(new UcPageRegist(_provider, _registData));
        }

        private void PreNavigatedEvent(object? sender, NavigationService.NavigationEventArgs e)
        {
            try
            {
                switch (e.ActionType)
                {
                    case NavigateActionType.Next:
                        // 次画面に進む際にスナップショットを保存
                        _snapshotManager.SaveSnapshot();
                        System.Diagnostics.Debug.WriteLine($"スナップショット保存: {e.FromPageType?.Name ?? "不明"} → {e.ToPageType?.Name ?? "不明"}");
                        break;

                    case NavigateActionType.Back:
                        // 前の画面に戻る場合、直前のスナップショットを復元
                        if (_snapshotManager.CanUndo)
                        {
                            bool result = _snapshotManager.Undo();
                            System.Diagnostics.Debug.WriteLine($"スナップショット復元: {e.FromPageType?.Name ?? "不明"} → {e.ToPageType?.Name ?? "不明"}, 結果: {(result ? "成功" : "失敗")}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("警告: 戻る操作が要求されましたが、復元可能なスナップショットがありません");
                        }
                        break;

                    case NavigateActionType.Cancel:
                    case NavigateActionType.Complete:
                    case NavigateActionType.Terminate:
                        // これらのアクションでは特別な処理は不要（OnComplete/OnCancelでクリア処理を実施）
                        System.Diagnostics.Debug.WriteLine($"ワークフロー操作: {e.ActionType}");
                        break;

                    default:
                        // デフォルトでは何もしない
                        System.Diagnostics.Debug.WriteLine($"その他のナビゲーション: {e.ActionType}");
                        break;
                }
            }
            catch (Exception ex)
            {
                // スナップショット操作中のエラーをログに記録
                System.Diagnostics.Debug.WriteLine($"スナップショット操作エラー: {ex.Message}");
                MessageBox.Show($"データの保存/復元中にエラーが発生しました: {ex.Message}",
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PostNavigatedEvent(object? sender, NavigationService.NavigationEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"画面遷移完了: {e.ToPageType?.Name ?? "不明"} (Undo可能: {_snapshotManager.CanUndo}, Redo可能: {_snapshotManager.CanRedo})");

                // 必要に応じてUIの更新などを行う
                // 例: 戻るボタンの有効/無効を_snapshotManager.CanUndoに合わせて設定するなど
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"画面遷移後処理でエラー: {ex.Message}");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            _nav.Start<UcPageLogin>();
        }

        private NavigationResult OnComplete(NavigationContext context)
        {
            _snapshotManager.Clear();
            return NavigationResult.Complete();
        }

        private NavigationResult OnCancel(NavigationContext context)
        {
            if (MessageBox.Show("編集中のデータがありますが終了しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _snapshotManager.Clear();
                return NavigationResult.Cancel();
            }
            return NavigationResult.None();
        }

        private NavigationResult OnTerminate(NavigationContext context)
        {
            _snapshotManager.Clear();
            return NavigationResult.Cancel();
        }

    }
}
