namespace WebAPIMicrosservico.Config.ServiceBus
{
    public class AppSettings
    {
        public static readonly string QueueName = "webapimicroqueue";
        public static readonly string AzureServiceBus = "Endpoint=sb://webapimicroqueue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=IDcSIe7o0vdw9qYOBf16LFRz03ST9Mobg+ASbPI3GOc=";
    }
}
