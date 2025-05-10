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
    public partial class UcPageOrderInfo : UcPageBase
    {
        private readonly RegistData _registData;
        private ErrorProvider _errorProvider;
        private bool _isDataLoading = false;
        private bool _isOrderInfoValid = false;

        public UcPageOrderInfo(ServiceProvider provider, RegistData registData) : base(provider)
        {
            InitializeComponent();
            _registData = registData;

            // エラープロバイダーの初期化
            _errorProvider = new ErrorProvider();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

        }

        public override void OnPageShown(NavigationParameters parameter)
        {
            try
            {
                _isDataLoading = true;

                Debug.WriteLine($"[UcPageOrderInfo] 画面表示: パッケージ数={_registData.Packages?.Count ?? 0}");

                // 既存データがある場合は表示
                if (_registData.Editing != null)
                {
                    if (!string.IsNullOrEmpty(_registData.Editing.OrderNo))
                    {
                        txtOrderSheetNo.Text = _registData.Editing.OrderSheetNo;
                        lblProductName.Text = _registData.Editing.ProductName;
                        _isOrderInfoValid = true;
                        btnNext.Enabled = true;
                    }
                }

                _isDataLoading = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UcPageOrderInfo] 画面表示エラー: {ex.Message}");
                MessageBox.Show($"注文情報の読み込み中にエラーが発生しました: {ex.Message}",
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isDataLoading = false;
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
            SaveOrderInfo();
            _nav.GoBack();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ValidateOrderInfo())
            {
                SaveOrderInfo();
                _nav.GoNext<UcPageNumOfProductsPerBox>();
            }
        }

        private void txtOrderSheetNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                try
                {
                    if (txtOrderSheetNo.Text.Contains('-'))
                    {
                        var spl = txtOrderSheetNo.Text.Split('-');
                        string product = GetProductName(spl[0], int.Parse(spl[1]));
                        if (!string.IsNullOrEmpty(product))
                        {
                            _registData.Editing.OrderNo = spl[0];
                            _registData.Editing.GroupNo = int.Parse(spl[1]);
                            _registData.Editing.ProductName = product;
                            lblProductName.Text = product;
                            _isOrderInfoValid = true;
                            btnNext.Enabled = true;
                        }
                        else
                        {
                            _errorProvider.SetError(txtOrderSheetNo, "指定された注文情報が見つかりません");
                            MessageBox.Show("有効な注文情報ではありません", "エラー",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _isOrderInfoValid = false;
                            btnNext.Enabled = false;
                            lblProductName.Text = "";
                        }
                    }
                    else
                    {
                        _errorProvider.SetError(txtOrderSheetNo, "正しい形式で入力してください (例: A12345-1)");
                        _isOrderInfoValid = false;
                        btnNext.Enabled = false;
                        lblProductName.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[UcPageOrderInfo] 注文情報検索エラー: {ex.Message}");
                    _errorProvider.SetError(txtOrderSheetNo, "注文情報の処理中にエラーが発生しました");
                    _isOrderInfoValid = false;
                    btnNext.Enabled = false;
                    lblProductName.Text = "";
                }
            }
        }

        public string GetProductName(string orderNo, int groupNo)
        {
            return "Sample Product";
        }

        // 注文情報を保存するメソッド
        private void SaveOrderInfo()
        {
            if (_registData.Editing != null && _isOrderInfoValid)
            {
                // すでに基本情報はKeyDownで設定済みなので追加のプロパティがあれば設定
                // 例：_registData.Editing.AdditionalInfo = "...";
                Debug.WriteLine($"[UcPageOrderInfo] 注文情報保存: OrderNo={_registData.Editing.OrderNo}, GroupNo={_registData.Editing.GroupNo}");
            }
        }

        // 注文情報が有効かどうかを検証するメソッド
        private bool ValidateOrderInfo()
        {
            if (!_isOrderInfoValid)
            {
                _errorProvider.SetError(txtOrderSheetNo, "有効な注文情報を入力してください");
                return false;
            }

            return true;
        }

        // 入力欄をクリアするメソッド
        public void ClearForm()
        {
            txtOrderSheetNo.Text = "";
            lblProductName.Text = "";
            _errorProvider.Clear();
            _isOrderInfoValid = false;
            btnNext.Enabled = false;
        }
    }
}
