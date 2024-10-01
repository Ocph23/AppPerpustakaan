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
        private static NavigationService NavigationService { get; } = (App.Current.MainWindow as MainApp).frame.NavigationService;

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
