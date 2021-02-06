# Read me

See this guide for an overview of migrations in EF Core: 

https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

To add a new migration (add one every time you change the schema), use any of these commands:

- .NET Core CLI: 
	
	```dotnet ef migrations add <Migration Name>```

- Visual Studio: 
	
	```AddMigration <Migration Name>```


To update your database, use any of these commands:

- .NET Core CLI: 

	```dotnet ef database update```

- Visual Studio: 

	```Update-Database```