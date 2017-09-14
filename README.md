# GodSharp.Data.Common.DbProvider
DbProviderFactory Factory libary for .NET Core.

[![AppVeyor build status](https://img.shields.io/appveyor/ci/seayxu/godsharp-data-common-dbprovider.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/seayxu/godsharp-data-common-dbprovider/) [![NuGet](https://img.shields.io/nuget/v/GodSharp.Data.Common.DbProvider.svg?label=nuget&style=flat-square)](https://www.nuget.org/packages/GodSharp.Data.Common.DbProvider/) [![MyGet](https://img.shields.io/myget/seay/v/GodSharp.Data.Common.DbProvider.svg?label=myget&style=flat-square)](https://www.myget.org/Package/Details/seay?packageType=nuget&packageId=GodSharp.Data.Common.DbProvider)

Supported netstandard1.3 and netstandard2.0.

# Getting Started

1. Install Nuget Package.

See [here](https://www.nuget.org/packages/GodSharp.Data.Common.DbProvider/).

2. Add Json Parameters.

You can add to any json file,and then add this file.

Parameters format like below.

``` json
{
  "DbConnectionStrings": [
    {
      "name": "mssql",
      "connectionString": "Data Source=localhost;Initial Catalog=Master;User Id=sa;Password=1234;",
      "providerName": "System.Data.SqlClient"
    },
    {
      "name": "mysql",
      "connectionString": "Data Source=localhost;Initial Catalog=user;User Id=root;Password=1234;",
      "providerName": "Pomelo.Data.MySql"
    },
    {
      "name": "sqlite",
      "connectionString": "Data Source=data.db",
      "providerName": "Microsoft.Data.Sqlite"
    },
    {
      "name": "pgsql",
      "connectionString": "Host=localhost;Database=postgres;Username=postgres;Password=1234;",
      "providerName": "Npgsql"
    }
  ],
  "DbProviderFactories": [
    {
      "name": "SqlClient Data Provider",
      "invariant": "System.Data.SqlClient",
      "description": ".Net Framework Data Provider for SqlServer",
      "type": "System.Data.SqlClient.SqlClientFactory, System.Data.SqlClient"
    },
    {
      "name": "MySQL Data Provider by Pomelo",
      "invariant": "Pomelo.Data.MySql",
      "description": ".Net Framework Data Provider for MySql",
      "type": "Pomelo.Data.MySql.MySqlClientFactory, Pomelo.Data.MySql"
    },
    {
      "name": "SQLite Data Provider",
      "invariant": "Microsoft.Data.Sqlite",
      "description": ".Net Framework Data Provider for SQLite",
      "type": "Microsoft.Data.Sqlite.SqliteFactory, Microsoft.Data.Sqlite"
    },
    {
      "name": "Npgsql Data Provider",
      "invariant": "Npgsql",
      "description": ".Net Framework Data Provider for PostgreSql",
      "type": "Npgsql.NpgsqlFactory, Npgsql"
    }
  ]
}
```

*Tips:`DbConnectionStrings` section is not required,but I strongly recommend you add this.*


3. Load DbProviders and DbConnectionStrings[option].

```
var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
// add json file
.AddJsonFile("db.provider.json");

var config = builder.Build();

// Load DbProviders and DbConnectionStrings
DbProviderManager.LoadConfiguration(config);
```

4. Get DbConnectionString.

```
DbConnectionStringSetting conn = DbConnectionStringManager.ConnectionStrings["sqlite"];
```


5. Get DbConnectionFactory.

```
DbProviderFactory factory = DbProviderFactories.GetFactory(conn.ProviderName);
```

6. Use `DbProviderFactory` create `IDbConnection` and other.

Create `IDbConnection`
```
IDbConnection db = factory.CreateConnection();
```

Use `IDbConnection`.

```
db.ConnectionString = conn.ConnectionString;
db.Open();

var cmd = db.CreateCommand();
cmd.CommandText = "select datetime('now');";
var dt = cmd.ExecuteScalar();
System.Console.WriteLine(dt);

db.Close();
```

You can see the full code in test.