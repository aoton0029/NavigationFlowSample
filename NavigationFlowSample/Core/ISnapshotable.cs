using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationFlowSample.Core
{
    /// <summary>
    /// スナップショットの作成と復元が可能なオブジェクトを定義するインターフェース
    /// </summary>
    public interface ISnapshotable<T>
    {
        /// <summary>
        /// 現在の状態のスナップショットを作成します
        /// </summary>
        /// <returns>オブジェクトの現在の状態</returns>
        T CreateSnapshot();

        /// <summary>
        /// スナップショットから状態を復元します
        /// </summary>
        /// <param name="snapshot">復元するスナップショット</param>
        void RestoreSnapshot(T snapshot);
    }
}
