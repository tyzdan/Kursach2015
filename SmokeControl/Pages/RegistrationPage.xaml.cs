using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SmokeControl.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox_.Password != PasswordBox__.Password)
            {
                await new MessageDialog("Login or password is invalid").ShowAsync();
                return;
            }

            if (await DataAccess.Register(LoginBox.Text, PasswordBox_.Password))
            {
                (Window.Current.Content as Frame).Navigate(typeof(Pages.MainPage));
            }
            else
            {
                await new MessageDialog("This login has been already taken").ShowAsync();
            }
        }
    }
}
