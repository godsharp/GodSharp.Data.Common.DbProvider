using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace GodSharp.Data.Common.DbProvider
{
    /// <summary>
    /// DbProviderFactories
    /// </summary>
    public class DbProviderFactories
    {        
        /// <summary>
        /// Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.
        /// </summary>
        /// <param name="providerInvariantName">Invariant name of a provider.</param>
        /// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified provider name.</returns>
        /// <exception cref="System.ArgumentNullException">providerInvariantName</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Provider not found</exception>
        /// <exception cref="System.InvalidCastException">Provider invalid</exception>
        /// <exception cref="System.NotImplementedException">Provider not installed</exception>
        public static DbProviderFactory GetFactory(string providerInvariantName)
        {
            if (string.IsNullOrWhiteSpace(providerInvariantName))
            {
                throw new ArgumentNullException(nameof(providerInvariantName));
            }

            DbProviderSetting dbProvider = DbProviderManager.GetProvider(providerInvariantName);

            Type type = Type.GetType(dbProvider.Type);

            if (null != type)
            {
                FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
#if NETSTANDARD1_3
                if (null != field && field.FieldType.GetTypeInfo().IsSubclassOf(typeof(DbProviderFactory)))
#elif NETSTANDARD2_0
                    if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
#endif
                {
                    object value = field.GetValue(null);
                    if (value != null)
                    {
                        return (DbProviderFactory)value;
                    }
                }

                throw new InvalidCastException("Provider invalid");
            }

            throw new NotImplementedException("Provider not installed");
        }
        
        /// <summary>
        /// Gets the keys of providers.
        /// </summary>
        public static ICollection<string> Keys => DbProviderManager.GetProviderKeys();
        
        /// <summary>
        /// provider invariant if exist
        /// </summary>
        /// <param name="providerInvariantName">provider invariant name.</param>
        /// <returns></returns>
        public static bool Exist (string providerInvariantName)=> DbProviderManager.GetProviderExist(providerInvariantName);
    }
}
