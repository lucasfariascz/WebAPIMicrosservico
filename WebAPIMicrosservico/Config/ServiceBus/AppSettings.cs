namespace WebAPIMicrosservico.Config.ServiceBus
{
    public class AppSettings
    {
        public static readonly string QueueName = "webapimicroqueue";
        public static readonly string AzureServiceBus = "Endpoint=sb://webapiqueue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=oY9JpImMa+fShpuWLIZGOqwkTVfxoxBxJ+ASbJLpa44=";
    }
}
