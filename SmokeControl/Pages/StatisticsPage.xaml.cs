using SmokeControl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SmokeControl.Pages
{
    public sealed partial class StatisticsPage : Page
    {
        ObservableCollection<Solution> Items = new ObservableCollection<Solution>();


        public StatisticsPage()
        {
            this.InitializeComponent();
            AddedList.ItemsSource = Items;
            foreach (var i in DataAccess.GetSolutions())
            {
                Items.Add(i);
            }

            DataAccess.SolutionAdded += DataAccess_SolutionAdded;
        }

        void DataAccess_SolutionAdded(object sender, Solution e)
        {
            Items.Add(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
