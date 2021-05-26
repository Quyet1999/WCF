using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TraSua.ViewModel
{
    public class ControlBarVM : BaseViewModel
    {
        #region
        public ICommand ClodeWindowCommand { get; set; }

        public ControlBarVM()
        {
            ClodeWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) => { GetWindowParent(p); });

        }

        void GetWindowParent(UserControl p)
        {
            FrameworkElement t = p.Parent as FrameworkElement;
        }
        #endregion
    }
}
