using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TraSua
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public bool result = false;
        public MessageBox(string message, string messageType)
        {
            InitializeComponent();
            string duongdan = System.AppDomain.CurrentDomain.BaseDirectory;
            if (messageType == "Success")
            {
                btnYes.Visibility = Visibility.Collapsed;
                btnNo.Visibility = Visibility.Collapsed;
                duongdan = duongdan + "Image/" + "CheckTC.png";
            }
            else if (messageType == "Question")
            {
                btnCancel.Visibility = Visibility.Collapsed;
                duongdan = duongdan + "Image/" + "CauHoi.png";
            }
            else if (messageType == "Warning")
            {
                btnYes.Visibility = Visibility.Collapsed;
                btnNo.Visibility = Visibility.Collapsed;
                duongdan = duongdan + "Image/" + "CanhBao.png";
            }
            ImageWarning.Source = new BitmapImage(new Uri(duongdan));
            Content.Text = message;
        }
        public MessageBox()
        {
            InitializeComponent();
        }
        public void EventYesNo(string message)
        {
            string duongdan = System.AppDomain.CurrentDomain.BaseDirectory;
            btnCancel.Visibility = Visibility.Collapsed;
            duongdan = duongdan + "Image/" + "CauHoi.png";
            ImageWarning.Source = new BitmapImage(new Uri(duongdan));
            Content.Text = message;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            result = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            this.Close();
        }
    }
}
