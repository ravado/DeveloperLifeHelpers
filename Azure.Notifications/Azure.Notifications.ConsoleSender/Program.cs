using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.NotificationHubs;

namespace Azure.Notifications.ConsoleSender
{
    class Program
    {
        public static NotificationHubClient NotificationHubClient { get; set; }

        static void Main(string[] args)
        {
            InitNotificationHub();

            var shouldContinue = true;
            while (shouldContinue)
            {
                Console.WriteLine("The notification is going to be sent now. press any key to continue.");
                Console.ReadKey();
                SendNotificationAsync();
                
                Console.WriteLine("Notification has been sent. Press any key to send again, or 'Esc' key to abort.");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    shouldContinue = false;
            }

            Console.WriteLine("Thank you for using this helper :)");
        }

        private static void InitNotificationHub()
        {
            var hubName = ConfigurationManager.AppSettings["HubName"];
            var fullConnectionString = ConfigurationManager.AppSettings["HubFullConnectionString"];
            var listenConnectionString = ConfigurationManager.AppSettings["HubListenConnectionString"];

            NotificationHubClient = NotificationHubClient.CreateClientFromConnectionString(fullConnectionString, hubName);
        }

        private static async void SendNotificationAsync()
        {
            // windows phone & windows notificaiton
            //string toast = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            //    "<wp:Notification xmlns:wp=\"WPNotification\">" +
            //       "<wp:Toast>" +
            //            "<wp:Text1>Hello from a .NET App!</wp:Text1>" +
            //       "</wp:Toast> " +
            //    "</wp:Notification>";
            //await NotificationHubClient.SendMpnsNativeNotificationAsync(toast);

            // apple iOS notifications
            var alert = "{\"aps\":{\"alert\":\"Hello from .NET!\"}}";
            await NotificationHubClient.SendAppleNativeNotificationAsync(alert, "U-2B358485");
        }
    }
}
