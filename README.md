# AssetFlow

## Two DB contexts 

### IdentityContext
Add-Migration InitIdentity -Project AssetFlow.Persistence -StartupProject AssetFlow.API -Context IdentityContext -OutputDir Migrations/Identity
Update-Database -Project AssetFlow.Persistence -StartupProject AssetFlow.API -Context IdentityContext



### AppContext
Add-Migration InitIdentity -Project AssetFlow.Persistence -StartupProject AssetFlow.API -Context IdentityContext -OutputDir Migrations/Identity