namespace CManager.Integration.AWS.Settings
{
    public class AwsSettings
    {
        public Account Account { get; set; }
    }

    public class Account
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
    }
}
