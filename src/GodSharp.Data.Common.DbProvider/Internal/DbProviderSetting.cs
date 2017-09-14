namespace GodSharp.Data.Common.DbProvider
{
    /// <summary>
    /// DbProvider object.
    /// </summary>
    internal class DbProviderSetting
    {
        /// <summary>
        /// the name of DbProvider object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the invariant of DbProvider object.
        /// </summary>
        public string Invariant { get; set; }

        /// <summary>
        /// the description of DbProvider object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// the type of DbProvider object.
        /// </summary>
        public string Type { get; set; }
    }
}
