using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace AppMain
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        public Notification(string message, int delay)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(message, delay) { WindowClose = this.Close };
        }

        internal static void Show(string v, int delay = 2500)
        {
            new Notification(v, delay).Show();
        }
    }

    internal class NotificationViewModel : ObservableObject
    {
        public NotificationViewModel(string message, int delay)
        {
            CloseCommand = new RelayCommand(() => WindowClose());
            Message = message;
            _ = AutoClose(delay);
        }

        private async Task AutoClose(int delay)
        {
            if (delay > 0)
            {
                await Task.Delay(delay);
                WindowClose();
            }
        }

        private string title = "Pemberitahuan";

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        private string message;

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public Action WindowClose { get; internal set; }
        public RelayCommand CloseCommand { get; private set; }
    }
}
