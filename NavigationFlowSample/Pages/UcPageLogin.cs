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
    public partial class UcPageLogin : UcPageBase
    {
        private readonly RegistData _registData;
        private ErrorProvider _errorProvider;

        public UcPageLogin(ServiceProvider provider, RegistData registData) : base(provider)
        {
            InitializeComponent();
            _registData = registData;
        }

        public override void OnPageShown(NavigationParameters parameter)
        {
            // ユーザーIDに初期フォーカスを設定
            txtUserId.Focus();
        }

        public override void OnPageLeave(NavigationParameters parameter)
        {
            base.OnPageLeave(parameter);
        }

        public override Type DecideNavigation(NavigationContext context)
        {
            return base.DecideNavigation(context);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _nav.Cancel();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _nav.GoNext<UcPageNumOfAllBoxes>();
        }


        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                e.Handled = true; // キー入力イベントを消費
                e.SuppressKeyPress = true; // ビープ音を抑制

                string name =  GetUserName(txtUserId.Text);
                if (!string.IsNullOrEmpty(name))
                {
                    _registData.UserId = txtUserId.Text;
                    _registData.UserName = name;
                    lblUserName.Text = name;
                }
            }
        }

        private string GetUserName(string userId)
        {
            return $"Sample {userId}";
        }
    }
}
