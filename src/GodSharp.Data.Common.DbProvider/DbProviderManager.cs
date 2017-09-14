using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace GodSharp.Data.Common.DbProvider
{
    /// <summary>
    /// DbProviderManager
    /// </summary>
    public class DbProviderManager
    {
        /// <summary>
        /// The providers
        /// </summary>
        internal static IDictionary<string, DbProviderSetting> Providers;

        /// <summary>
        /// The connections
        /// </summary>
        internal static IDictionary<string, DbConnectionStringSetting> Connections;

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void LoadConfiguration(IConfiguration configuration)
        {
            List<DbProviderSetting> po = new List<DbProviderSetting>();
            configuration.GetSection("DbProviderFactories").Bind(po);

            if (po.Count > 0)
            {
                Providers = po.ToDictionary(m => m.Invariant, m => m);
            }

            List<DbConnectionStringSetting> co = new List<DbConnectionStringSetting>();
            configuration.GetSection("DbConnectionStrings").Bind(co);

            if (co.Count > 0)
            {
                Connections = co.ToDictionary(m => m.Name, m => m);
            }
        }

        /// <summary>
        /// Gets the DbProvider.
        /// </summary>
        /// <param name="providerInvariantName">Name of the provider invariant.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Provider not found</exception>
        internal static DbProviderSetting GetProvider(string providerInvariantName)
        {
            if (Providers == null || !Providers.Keys.Contains(providerInvariantName))
            {
                throw new KeyNotFoundException("Provider not found");
            }

            return Providers[providerInvariantName];
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">ConnectionString not found</exception>
        internal static DbConnectionStringSetting GetConnectionString(string connectionStringName)
        {
            if (Connections == null || !Connections.Keys.Contains(connectionStringName))
            {
                throw new KeyNotFoundException("ConnectionString not found");
            }

            return Connections[connectionStringName];
        }

        /// <summary>
        /// Gets the provider keys.
        /// </summary>
        /// <returns></returns>
        internal static ICollection<string> GetProviderKeys()
        {
            return Providers?.Keys;
        }

        /// <summary>
        /// Gets the connection string keys.
        /// </summary>
        /// <returns></returns>
        internal static ICollection<string> GetConnectionStringKeys()
        {
            return Connections?.Keys;
        }

        /// <summary>
        /// provider invariant if exist
        /// </summary>
        /// <param name="providerInvariantName">provider invariant name.</param>
        /// <returns></returns>
        internal static bool GetProviderExist(string providerInvariantName) => Providers.ContainsKey(providerInvariantName);
    }
}
