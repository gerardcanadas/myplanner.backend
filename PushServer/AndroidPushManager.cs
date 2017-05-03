using System;
using System.Collections.Generic;
using FCM.Net;

namespace devices.PushServer 
{
    public class AndroidPushManager
    {
        private const string FCM_API_KEY = "AIzaSyCMYHiN9N85xofEZvzsRE13J0DNGh5SWRg";

        private static AndroidPushManager instance = null;
        public static AndroidPushManager getInstance()
        {
            if (instance == null)
                instance = new AndroidPushManager();
            return instance;
        }

        protected AndroidPushManager() 
        {
            
        }

        public async void SendMessage(List<string> devicesId, string message)
        {
            //TODO: get deviceId by userName from db
            //TODO: send msg payload to device
            using(var sender = new Sender(FCM_API_KEY))
            {
                var fcmMessage = new Message
                {
                    RegistrationIds = devicesId,
                    Notification = new Notification
                    {
                        Title = "Test from FCM.Net",
                        Body = $"Hello World@!{DateTime.Now.ToString()}"
                    }
                };
                var result = await sender.SendAsync(message);
                Console.WriteLine($"Success: {result.MessageResponse.Success}");
            }
        }
    }
}