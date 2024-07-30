namespace CManager.API.Common
{
    public class ApiSettings
    {
        public Cache Cache { get; set; }
        public string ConnectionStringDB { get; set; }
        public ElasticSearch ElasticSearch { get; set; }
    }
}
