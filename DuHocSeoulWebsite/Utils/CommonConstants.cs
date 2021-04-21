namespace DuHocSeoulWebsite.Utils
{
    public static class CommonConstants
    {
        public static string ApiToSendRequestUrl => AppSettings.Instance.GetByKey("AppSettings:ApiToSendRequestUrl");
    }
}
