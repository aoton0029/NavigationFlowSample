using NavigationFlowSample.Core;
using NavigationFlowSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavigationFlowSample.Pages
{
    public partial class UcPageNumOfAllBoxes : UcPageBase
    {
        private readonly RegistData _registData;
        private ErrorProvider _errorProvider;
        private const int MAX_BOXES = 100; // 最大箱数の制限
        private const int MIN_BOXES = 1;   // 最小箱数の制限
        private bool _isInitializing = false;

        public UcPageNumOfAllBoxes(ServiceProvider provider, RegistData registData) : base(provider)
        {
            InitializeComponent();
            _registData = registData;

            // エラープロバイダーの初期化
            _errorProvider = new ErrorProvider();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            // NumericUpDownコントロールの設定
            SetupNumericUpDown();
        }

        public override void OnPageShown(NavigationParameters parameter)
        {
            try
            {
                // RegistDataの状態をUIに反映
                SyncRegistDataToUI();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UcPageNumOfAllBoxes] 画面表示エラー: {ex.Message}");
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            _nav.GoBack();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _nav.GoNext<UcPageOrderInfo>();
        }

        private void nudNumOfAllBoxes_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // 入力値の検証
                int numBoxes = (int)nudNumOfAllBoxes.Value;

                if (numBoxes < MIN_BOXES || numBoxes > MAX_BOXES)
                {
                    _errorProvider.SetError(nudNumOfAllBoxes, $"箱数は{MIN_BOXES}から{MAX_BOXES}の間で指定してください");
                    return;
                }

                // エラーをクリア
                _errorProvider.SetError(nudNumOfAllBoxes, "");

                // 値を保存
                _registData.NumOfAllBoxes = numBoxes;

                // 箱数に応じたPackagesの初期化
                UpdatePackagesBasedOnBoxCount(numBoxes);

                UpdateButtonState();
                Debug.WriteLine($"[UcPageNumOfAllBoxes] 箱数変更: {numBoxes}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UcPageNumOfAllBoxes] エラー: {ex.Message}");
                _errorProvider.SetError(nudNumOfAllBoxes, "値の設定中にエラーが発生しました");
            }
        }

        private void SyncRegistDataToUI()
        {
            _isInitializing = true;
            try
            {
                // RegistDataの値をUIに反映
                if (_registData.NumOfAllBoxes > 0)
                {
                    nudNumOfAllBoxes.Value = Math.Min(_registData.NumOfAllBoxes, MAX_BOXES);
                }
                else
                {
                    nudNumOfAllBoxes.Value = 1;
                    _registData.NumOfAllBoxes = 1;
                }

                UpdateButtonState();
            }
            finally
            {
                _isInitializing = false;
            }
        }

        private void UpdatePackagesBasedOnBoxCount(int numBoxes)
        {
            // 現在のPackages数と必要な数の差分を計算
            int currentCount = _registData.Packages?.Count ?? 0;

            // 既存のPackagesが少なすぎる場合は追加
            while (currentCount < numBoxes && currentCount < MAX_BOXES)
            {
                _registData.AddPackage();
                currentCount++;
            }

            // 既存のPackagesが多すぎる場合は調整
            // 注: この実装は要件によって異なる場合があります
            if (currentCount > numBoxes && _registData.Packages != null)
            {
                if (MessageBox.Show(
                    $"箱数を{currentCount}から{numBoxes}に減らすと、一部のデータが失われる可能性があります。続行しますか？",
                    "確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // 最後の要素から削除（通常は逆順に処理）
                    while (_registData.Packages.Count > numBoxes)
                    {
                        _registData.Packages.RemoveAt(_registData.Packages.Count - 1);
                    }
                }
                else
                {
                    // キャンセルされた場合は元の箱数に戻す
                    _isInitializing = true;
                    nudNumOfAllBoxes.Value = currentCount;
                    _registData.NumOfAllBoxes = currentCount;
                    _isInitializing = false;
                }
            }
        }

        private void UpdateButtonState()
        {
            // 次へボタンは、箱数が有効な範囲内であれば有効にする
            btnNext.Enabled = _registData.NumOfAllBoxes >= MIN_BOXES && _registData.NumOfAllBoxes <= MAX_BOXES;
        }

        private void ResetBoxCount()
        {
            _registData.NumOfAllBoxes = MIN_BOXES;
            if (_registData.Packages != null)
            {
                _registData.Packages.Clear();
            }
            _registData.AddPackage(); // 最低1つのパッケージを追加
        }

        private void SetupNumericUpDown()
        {
            // NumericUpDownの初期設定
            nudNumOfAllBoxes.Minimum = MIN_BOXES;
            nudNumOfAllBoxes.Maximum = MAX_BOXES;
            nudNumOfAllBoxes.DecimalPlaces = 0;
            nudNumOfAllBoxes.Increment = 1;
            nudNumOfAllBoxes.ThousandsSeparator = true;
        }
    }
}
