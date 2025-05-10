using NavigationFlowSample.Core;
using NavigationFlowSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavigationFlowSample.Pages
{
    public partial class UcPageAssignBox : UcPageBase
    {
        private readonly RegistData _registData;

        public UcPageAssignBox(ServiceProvider provider, RegistData registData) : base(provider)
        {
            InitializeComponent();
            _registData = registData;
        }

        public override void OnPageShown(NavigationParameters parameter)
        {
            // 編集中のパッケージがあるか確認
            if (_registData.Editing != null)
            {
                // 既存のボックス割り当てをクリア
                _registData.Editing.Boxes = new List<Box>();

                // シリアル番号をボックスに分割
                SplitSerialNosToBoxes();

                // データグリッドビューの更新
                UpdateDataGridView();
            }
        }

        public override void OnPageLeave(NavigationParameters parameter)
        {

        }

        public override Type DecideNavigation(NavigationContext context)
        {
            return base.DecideNavigation(context);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _nav.Cancel();
        }

        private void rdbSortAsc_CheckedChanged(object sender, EventArgs e)
        {
            // ソート順が変更されたら再分割
            if (_registData.Editing != null)
            {
                SplitSerialNosToBoxes();
                UpdateDataGridView();
            }
        }

        private void rdbRemaindAtStart_CheckedChanged(object sender, EventArgs e)
        {
            // 余りの配置方法が変更されたら再分割
            if (_registData.Editing != null)
            {
                SplitSerialNosToBoxes();
                UpdateDataGridView();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _nav.GoNext<UcConfirmIncludePackage>();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _nav.GoBack();
        }

        /// <summary>
        /// シリアル番号をボックスに分割する
        /// </summary>
        private void SplitSerialNosToBoxes()
        {
            var package = _registData.Editing;
            if (package == null || package.AllSerialNos == null || package.AllSerialNos.Count == 0)
                return;

            // 既存のボックスをクリア
            package.Boxes = new List<Box>();

            // シリアル番号のリストを取得
            List<SerialNo> serialNos = new List<SerialNo>(package.AllSerialNos);

            // ソート順（昇順/降順）に基づいてシリアル番号をソート
            if (rdbSortAsc.Checked)
            {
                serialNos.Sort(); // 昇順
            }
            else
            {
                serialNos.Sort();
                serialNos.Reverse(); // 降順
            }

            // 箱あたりの製品数
            int numPerBox = package.NumOfProductsPerBox;
            if (numPerBox <= 0) numPerBox = 1;

            // 余りを最初に配置するか最後に配置するか
            bool remaindAtStart = rdbRemaindAtStart.Checked;

            // 必要なボックス数を計算
            int totalSerialNos = serialNos.Count;
            int fullBoxCount = totalSerialNos / numPerBox; // 満杯のボックス数
            int remainder = totalSerialNos % numPerBox; // 余りの製品数
            int totalBoxes = remainder > 0 ? fullBoxCount + 1 : fullBoxCount;

            // インデックスの開始位置
            int currentIndex = 0;

            // ボックスを作成
            for (int boxIndex = 0; boxIndex < totalBoxes; boxIndex++)
            {
                Box box = new Box { BoxNumber = boxIndex + 1, SerialNos = new List<SerialNo>() };

                // 現在のボックスに入れる製品数を決定
                int productsInThisBox;

                if (remainder > 0)
                {
                    if ((remaindAtStart && boxIndex == 0) || (!remaindAtStart && boxIndex == totalBoxes - 1))
                    {
                        // 余りをここに配置
                        productsInThisBox = remainder;
                    }
                    else
                    {
                        productsInThisBox = numPerBox;
                    }
                }
                else
                {
                    productsInThisBox = numPerBox;
                }

                // ボックスにシリアル番号を追加
                for (int i = 0; i < productsInThisBox && currentIndex < totalSerialNos; i++)
                {
                    box.SerialNos.Add(serialNos[currentIndex++]);
                }

                // ボックスをパッケージに追加
                package.Boxes.Add(box);
            }
        }

        /// <summary>
        /// データグリッドビューを更新する
        /// </summary>
        private void UpdateDataGridView()
        {
            var package = _registData.Editing;
            if (package == null || package.Boxes == null)
                return;

            // データグリッドビューのデータソースをクリア
            dataGridView1.DataSource = null;

            // ボックス情報を表示用のリストに変換
            var displayItems = package.Boxes.Select(box => new
            {
                BoxNumber = box.BoxNumber,
                NumOfProducts = box.SerialNos.Count,
                DisplaySerialNo = string.Join(", ", box.SerialNos.Select(s => s.ToString()))
            }).ToList();

            // データグリッドビューにデータをバインド
            dataGridView1.DataSource = displayItems;
        }
    }
}
