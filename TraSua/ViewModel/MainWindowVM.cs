using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraSua.ViewModel
{
    public class MainWindowVM : BaseViewModel
    {
        private bool IsLoaded = false;
        public MainWindowVM()
        {
            if (!IsLoaded)
            {
                IsLoaded = true;
                Login lg = new Login();
                lg.ShowDialog();
            }
        }
    }
}
