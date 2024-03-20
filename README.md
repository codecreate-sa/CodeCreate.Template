# CodeCreate.Template
Angular &amp; ASP.NET Core REST API with ASP.NET Identity template setup

## Angular Front-end

Installation : 
 1. [nodejs](https://nodejs.org/en/download/current) (v21.7.1)
   
 1. [pnpm](https://pnpm.io/installation) 
    Command `npm install -g pnpm`

Then Go to path `CodeCreate.Template\src\CodeCreate.App\ClientApp` run command 
 1. `pnpm install`
 2. `pnpm start`

Ready to go! Have fun :) 

## Back-end

1. Set up database.
1. Run (`dotnet run`) or start (Visual Studio) `CodeCreate.App` application.

Ready to go!

### Database set up Migrations

**Add migrations**
```c#
dotnet ef migrations add "InitialMigration" --project src\CodeCreate.Data --startup-project src\CodeCreate.App
```

**Update database**
```c#
dotnet ef database update --project src\CodeCreate.Data --startup-project src\CodeCreate.App
```

**Drop database** 
```c#
dotnet ef database drop --project src\CodeCreate.Data --startup-project src\CodeCreate.App
```

