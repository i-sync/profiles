namespace Profiles.DAL
{
    public class SqlConnectionSetting
    {
        public string ReadConnection { get; set; }

        public string WriteConnection { get; set; }

        public static SqlConnectionSetting Default { get; set; }
    }
}