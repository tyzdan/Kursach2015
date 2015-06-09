using System;
using System.Collections.Generic;
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
    public sealed partial class InformationPage : Page
    {
        public InformationPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public int PullsLeft
        {
            get
            {
                return RemoteDevice.MaxPulls - RemoteDevice.PullsMade;
            }
        }

        public int MaxPulls
        {
            get
            {
                return RemoteDevice.MaxPulls;
            }
        }

        public int BatteryLevel
        {
            get
            {
                return RemoteDevice.BatteryLevel;
            }
        }

        public int FuelLevel
        {
            get
            {
                return RemoteDevice.FuelLevel;
            }
        }

        public double BatteryLevelIcon
        {
            get
            {
                return RemoteDevice.BatteryLevel / 100.0;
            }
        }

        public double FuelLevelIcon
        {
            get
            {
                return RemoteDevice.FuelLevel / 100.0;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
    }
}
