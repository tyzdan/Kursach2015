using SmokeControl.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SmokeControl
{
    public static class DataAccess
    {
        private static List<Solution> solutions = new List<Solution>();

        private const string ServerName = "http://192.168.1.187:49408/";

        public static string IsLogin
        {
            get
            {
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
                return settings["login"] as string;
            }
        }

        public static async Task<bool> Login(string login, string password)
        {
            var responce = await Send(ServerName + "Api/Login", "login", login, "password", password);
            if (responce != string.Empty)
            {
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
                settings["login"] = login;
                settings["token"] = responce;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> Sync()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
            var responce = await Send(
                ServerName + "Api/Sync",
                "login",
                settings["login"].ToString(),
                "token",
                settings["token"].ToString(),
                "data",
                Serialize(GetUnsyncedSolutions(), typeof(List<Solution>)));
            var success = bool.Parse(responce);
            if (success)
            {
                MarkSynced();
            }

            return success;
        }

        private static void MarkSynced()
        {
            foreach (var i in solutions)
                i.Synchronized = true;
        }

        private static object GetUnsyncedSolutions()
        {
            return solutions.Where(i=>!i.Synchronized).ToList();
        }

        public static async Task<bool> Register(string login, string password)
        {
            var responce = await Send(ServerName + "Api/Register", "login", login, "password", password);
            if (responce != string.Empty)
            {
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
                settings["login"] = login;
                settings["token"] = responce;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Logout()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
            settings["login"] = null;
            settings["token"] = null;
        }

        public static void AddSolution(Solution s)
        {
            solutions.Add(s);

            if (SolutionAdded != null)
            {
                SolutionAdded(null, s);
            }
        }

        public static List<Solution> GetSolutions()
        {
            return solutions;
        }

        public static event EventHandler<Solution> SolutionAdded;

        public static async Task<string> Send(string url, params string[] args)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i += 2)
            {
                pairs.Add(args[i], args[i + 1]);
            }

            HttpClient clientOb = new HttpClient();
            HttpFormUrlEncodedContent formContent = new HttpFormUrlEncodedContent(pairs);
            try
            {
                HttpResponseMessage response = await clientOb.PostAsync(new Uri(url), formContent);
                return response.Content.ToString();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private static string Serialize(object o, Type type)
        {
            var dcjs = new DataContractJsonSerializer(type);
            string data;
            Stream stream = new MemoryStream();
            dcjs.WriteObject(stream, o);
            stream.Position = 0;
            using (StreamReader sr = new StreamReader(stream))
            {
                data = sr.ReadToEnd();
            }

            return data;
        }
    }
}
