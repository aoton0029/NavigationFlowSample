using Accessibility;
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
    public partial class UcPageNumOfProductsPerBox : UcPageBase
    {
        private readonly RegistData _registData;

        public UcPageNumOfProductsPerBox(ServiceProvider provider, RegistData registData) : base(provider)
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
            _registData.Editing.NumOfProductsPerBox = (int)nudNumOfProductsPerBox.Value;
            _nav.GoNext<UcPageAssignBox>();
        }
    }
}
