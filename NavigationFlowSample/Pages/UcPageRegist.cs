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

        public UcPageRegist(RegistData registData)
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

        private void btnComplete_Click(object sender, EventArgs e)
        {
            _nav.Complete();
        }
    }
}
