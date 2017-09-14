using System.Collections.Generic;

namespace GodSharp.Data.Common.DbProvider
{
    /// <summary>
    /// DbConnectionStringManager
    /// </summary>
    public class DbConnectionStringManager
    {
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        public static DbConnectionStringManager ConnectionStrings { get; protected set; }

        /// <summary>
        /// Initializes the <see cref="DbConnectionStringManager"/> class.
        /// </summary>
        static DbConnectionStringManager()
        {
            if (ConnectionStrings==null)
            {
                lock (_lock)
                {
                    if (ConnectionStrings == null)
                    {
                        ConnectionStrings = new DbConnectionStringManager();
                    }
                } 
            }
        }

        /// <summary>
        /// Gets the <see cref="DbConnectionStringSetting"/> with the specified connection string name.
        /// </summary>
        /// <value>
        /// The <see cref="DbConnectionStringSetting"/>.
        /// </value>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        public DbConnectionStringSetting this[string connectionStringName] => DbProviderManager.GetConnectionString(connectionStringName);

        /// <summary>
        /// Gets the keys.
        /// </summary>
        public static ICollection<string> Keys => DbProviderManager.GetConnectionStringKeys();
    }
}
