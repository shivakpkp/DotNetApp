# MyTestWebApp (.NET 8)

A minimal ASP.NET Core (.NET 8) web app to test Azure App Service.

## Run locally
```bash
dotnet run
# then open the URL printed to the console (usually http://localhost:5000)
```

Endpoints:
- `/` home page (HTML)
- `/health` JSON health endpoint
- `/hello/{name}` dynamic route
- `/env` safe environment info (machine, framework, region, etc.)

## Build for release
```bash
dotnet publish -c Release
```

## Quick deploy to Azure via Azure CLI
PowerShell (Windows):
```powershell
./deploy/azure-deploy.ps1 -AppName mytestwebapp12345 -ResourceGroup rg-test-webapp -Location canadacentral
```

Bash (macOS/Linux):
```bash
bash ./deploy/azure-deploy.sh mytestwebapp12345 rg-test-webapp canadacentral
```
