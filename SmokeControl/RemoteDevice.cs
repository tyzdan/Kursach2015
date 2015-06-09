using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeControl
{
    public class RemoteDevice
    {
        public static int BatteryLevel
        {
            get
            {
                return 80 - DateTime.Now.Minute;
            }
        }

        public static int MaxPulls
        {
            get
            {
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
                int? count = (int?)settings["Count"];
                return count ?? 0;
            }
        }

        public static int PullsMade
        {
            get
            {
                return (int)(MaxPulls*(DateTime.Now.AddHours(-8).Hour + 1)/25.0);
            }
        }

        public static int FuelLevel
        {
            get
            {
                return (MaxPulls-PullsMade)*100/MaxPulls;
            }
        }
    }
}
