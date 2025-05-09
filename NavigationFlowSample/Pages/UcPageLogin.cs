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
    public partial class UcPageLogin : UcPageBase
    {
        private readonly RegistData _registData;

        public UcPageLogin(RegistData registData)
        {
            InitializeComponent();
            _registData = registData;
        }

        public override void OnPageShown(NavigationParameters parameter)
        {
            base.OnPageShown(parameter);
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
            _nav.GoNext<UcPagePackageNum>();
        }


        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                _registData.UserId = txtUserId.Text;
                string name = "Sample Test";
                _registData.UserName = name;
                label1.Text = name;
            }
        }
    }
}
