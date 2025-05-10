using NavigationFlowSample.Core;
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
    public partial class UcPageBase : UserControl, IPage, IDecideNavigation
    {
        protected readonly ServiceProvider _provider;
        protected NavigationFlowService _nav => _provider.GetService<NavigationFlowService>();

        public UcPageBase(ServiceProvider provider)
        {
            InitializeComponent();
            _provider = provider;
        }

        public virtual Type DecideNavigation(NavigationContext context)
        {
            return context.DefaultNextPage;
        }

        public virtual void OnPageLeave(NavigationParameters parameter)
        {

        }

        public virtual void OnPageShown(NavigationParameters parameter)
        {

        }
    }
}
