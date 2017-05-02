namespace devices.PushServer 
{
    public class AndroidPushManager
    {
        private const string GCM_API_KEY = "";

        public void SendMessage(string userName, string message)
        {
            //TODO: get deviceId by userName from db
            //TODO: send msg payload to device
        }
    }
}