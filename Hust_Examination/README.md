# HUST Exam Project

## Application URLs:
- Identity STS: https://localhost:5001
- Exam API: https://localhost:5002
- Identity API: https://localhost:5003
- Admin: https://localhost:6001
- Portal: https://localhost:6002
- Identity Admin: https://localhost:6003

## Drop database 
USE master;
GO

ALTER DATABASE [Identity] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
DROP DATABASE [Identity];
GO

## Packages References
- https://github.com/serilog/serilog/wiki/Getting-Started
- https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
- https://mudblazor.com/
- https://github.com/Garderoben/MudBlazor.Templates



## Exam API

{
  "DatabaseSettings": {
    "Server": "localhost:27017",
    "DatabaseName": "ExamDb",
    "User": "admin",
    "Password": "Admin%40123%24"
  },
  "IdentityUrl": "
  
  
  
  ://localhost:5001"
}

## Identity.Admin
{
  "ConnectionStrings": {
    "ConfigurationDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "PersistedGrantDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "IdentityDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AdminLogDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AdminAuditLogDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "DataProtectionDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "AdminConfiguration": {
    "IdentityAdminRedirectUri": "https://localhost:6003/signin-oidc",
    "IdentityServerBaseUrl": "https://localhost:5001",
  }
}

## Identity.STS
{
  "ConnectionStrings": {
    "ConfigurationDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "PersistedGrantDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "IdentityDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "DataProtectionDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "AdminConfiguration": {
    "IdentityAdminBaseUrl": "https://localhost:6003",
  }
}

# Admin API
{
  "ConnectionStrings": {
    "ConfigurationDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "PersistedGrantDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "IdentityDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AdminLogDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AdminAuditLogDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "DataProtectionDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "AdminApiConfiguration": {
    "ApiBaseUrl": "https://localhost:5003",
    "IdentityServerBaseUrl": "https://localhost:5001",
  }
}




### Deploy
1. Upgrade MudBlazor
+ exam-api.local
+ identity-sts.local
+ identity-admin.local
+ admin.local
+ portal.local



