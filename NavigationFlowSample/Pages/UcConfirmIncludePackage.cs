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
    public partial class UcConfirmIncludePackage : UcPageBase
    {
        private readonly RegistData _registData;

        public UcConfirmIncludePackage(ServiceProvider provider, RegistData registData) : base(provider) 
        { 
            InitializeComponent();
            _registData = registData;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            _nav.GoNext<UcPageRegist>();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            _nav.GoNext<UcPageOrderInfo>();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _nav.Cancel();
        }
    }
}
