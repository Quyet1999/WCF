using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace TraSua.ViewModel
{
    public class LoginVM
    {
        private string _USERNAME;
        private string _PASSWORD;
        public ICommand LoginCommand { get; set; }

        public string USERNAME
        {
            get
            {
                return _USERNAME;
            }
            set
            {
                _USERNAME = value;
                OnPropertyChanged("USERNAME");
            }
        }
        public string PASSWORD
        {
            get
            {
                return _PASSWORD;
            }
            set
            {
                _PASSWORD = value;
                OnPropertyChanged("PASSWORD");
            }
        }
        public LoginVM()
        {
            LoginCommand = new RelayCommand<PasswordBox>((p) => true, (p) =>
             {
                 if (USERNAME == null || USERNAME == "")
                 {
                     MessageBox msg = new MessageBox("Vui lòng nhập vào Username!", "Warning");
                     msg.ShowDialog();
                 }
                 if (p.Password != "")
                 {
                     new MainWindow().ShowDialog();
                     Login.thisLogin.Close();
                 }
             });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
