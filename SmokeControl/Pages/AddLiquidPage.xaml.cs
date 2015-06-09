using SmokeControl.Models;
using System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SmokeControl.Pages
{
    public sealed partial class AddLiquidPage : Page
    {
        private Solution solution;

        public AddLiquidPage()
        {
            this.InitializeComponent();
            Liquids.ItemsSource = new string[] { "Strawberry", "Raspberry", "Blackberry", "Blueberry" };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            solution = new Solution() { Date = DateTime.Now };
        }

        private void AddTasteButton_Click(object sender, RoutedEventArgs e)
        {
            double strength = 0;
            double volume = 0;
            if (Liquids.SelectedItem == null || !double.TryParse(Strength.Text, out strength) || !double.TryParse(Volume.Text, out volume))
            {
                return;
            }

            var item = new Part() { Liquid = Liquids.SelectedItem.ToString(), Strength = strength, Volume = volume };
            AddedList.Items.Add(item);
            solution.Parts.Add(item);
            Strength.Text = string.Empty;
            Volume.Text = string.Empty;
        }

        private async void AddSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (solution.Parts.Count > 0 && int.TryParse(Count.Text, out count))
            {
                DataAccess.AddSolution(solution);
                (Window.Current.Content as Frame).GoBack();
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
                settings["Count"] = count;
            }
            else
            {
                await new MessageDialog("Enter correct information").ShowAsync();
            }
        }
    }
}
