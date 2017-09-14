namespace GodSharp.Data.Common.DbProvider
{
    /// <summary>
    /// DbConnectionString object.
    /// </summary>
    public class DbConnectionStringSetting
    {
        /// <summary>
        /// the name of DbConnectionString object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the ConnectionString of DbConnectionString object.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// the provider name of DbConnectionString object.
        /// </summary>
        public string ProviderName { get; set; }
    }
}
