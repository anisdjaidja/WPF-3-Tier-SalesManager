using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_N_Tier_Test.ViewModel
{
    public interface INavigationViewModel
    {
        public void NavigateTo(int idx);
        public void NavigateToTab(int idx, int tabIdx);
    }
}
