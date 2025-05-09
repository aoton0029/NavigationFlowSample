using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationFlowSample.Core
{
    /// <summary>
    /// ISnapshotableインターフェースを実装するオブジェクトのundo/redo操作を管理するクラス
    /// </summary>
    public class SnapshotManager<T> where T : ISnapshotable<T>
    {
        private readonly T _target;
        private readonly Stack<T> _undoStack = new Stack<T>();
        private readonly Stack<T> _redoStack = new Stack<T>();
        private bool _isBusy = false;

        /// <summary>
        /// UndoRedoManagerを初期化します
        /// </summary>
        /// <param name="target">管理対象のISnapshotableオブジェクト</param>
        public SnapshotManager(T target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Undo操作が可能かどうかを取得します
        /// </summary>
        public bool CanUndo => _undoStack.Count > 0;

        /// <summary>
        /// Redo操作が可能かどうかを取得します
        /// </summary>
        public bool CanRedo => _redoStack.Count > 0;

        /// <summary>
        /// 現在の状態をスナップショットとして記録します
        /// </summary>
        public void SaveSnapshot()
        {
            if (_isBusy) return;

            var snapshot = _target.CreateSnapshot();
            _undoStack.Push(snapshot);
            _redoStack.Clear();
        }

        /// <summary>
        /// Undo操作を実行します
        /// </summary>
        /// <returns>操作が成功したかどうか</returns>
        public bool Undo()
        {
            if (!CanUndo || _isBusy) return false;

            try
            {
                _isBusy = true;

                // 現在の状態をRedoスタックに保存
                var currentSnapshot = _target.CreateSnapshot();
                _redoStack.Push(currentSnapshot);

                // 前の状態を復元
                var previousSnapshot = _undoStack.Pop();
                _target.RestoreSnapshot(previousSnapshot);

                return true;
            }
            finally
            {
                _isBusy = false;
            }
        }

        /// <summary>
        /// Redo操作を実行します
        /// </summary>
        /// <returns>操作が成功したかどうか</returns>
        public bool Redo()
        {
            if (!CanRedo || _isBusy) return false;

            try
            {
                _isBusy = true;

                // 現在の状態をUndoスタックに保存
                var currentSnapshot = _target.CreateSnapshot();
                _undoStack.Push(currentSnapshot);

                // 次の状態を復元
                var nextSnapshot = _redoStack.Pop();
                _target.RestoreSnapshot(nextSnapshot);

                return true;
            }
            finally
            {
                _isBusy = false;
            }
        }

        /// <summary>
        /// すべての履歴をクリアします
        /// </summary>
        public void Clear()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }
    }
}
