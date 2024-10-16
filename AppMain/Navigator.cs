using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AppMain
{
    public static class Navigator
    {
        public static NavigationService NavigationService { get; set; }

        private static NavigationService GetService()
        {
            var x = ((MainApp)App.Current.MainWindow).frame.NavigationService;
            return x;
        }

        public static void Navigate(Page page)
        {
            NavigationService.Navigate(page);
        }

        public static void GoBack()
        {
            NavigationService.GoBack();
        }

        public static void GoForward()
        {
            NavigationService.GoForward();
        }
    }
}
