namespace DALLayer
{
    using System.Configuration;
    using Utils;

    /// <summary>
    /// Class DBConnection to get the connection string
    /// </summary>
    public static class DBConnection
    {
        /// <summary>
        /// This property is used to get/set the Connstring value.
        /// </summary>
        public static string Connstring
        {
            get
            {
                return AppConfigSetting.GetDBConnection;
            }
        }
    }
}
