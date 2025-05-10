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
    public partial class UcPageRegist : UcPageBase
    {
        private readonly RegistData _registData;

        public UcPageRegist(ServiceProvider provider, RegistData registData) : base(provider)
        {
            InitializeComponent();
            _registData = registData;
        }

        public override void OnPageShown(NavigationParameters parameter)
        {
            
        }

        public override void OnPageLeave(NavigationParameters parameter)
        {

        }

        public override Type DecideNavigation(NavigationContext context)
        {
            return base.DecideNavigation(context);
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (regist(out string errMsg))
            {
                _nav.Complete();
            }
            else
            {
                MessageBox.Show(errMsg);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _nav.Cancel();
        }

        private bool regist(out string errMsg)
        {
            errMsg = "";
            //RegistDataの内容をデータベースに登録
            return true;
        }
    }
}
