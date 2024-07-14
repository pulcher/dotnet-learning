Certainly! Here's a step-by-step guide to adding user-secrets to an existing .NET 8.0 web application without using top-level statements. This example will include an endpoint to demonstrate accessing secrets from the `secrets.json` file.

### Step-by-Step Guide

#### 1. Create or Open Your .NET 8.0 Web Application
If you don't have an existing project, create a new one:
```bash
dotnet new webapp -n MyWebApp
cd MyWebApp
```

#### 2. Initialize User-Secrets
Initialize user-secrets for your project:
```bash
dotnet user-secrets init
```

#### 3. Add Secrets to User-Secrets
Add a secret to the user-secrets store:
```bash
dotnet user-secrets set "MySecret" "ThisIsASecretValue"
```

#### 4. Update `MyWebApp.csproj`
Ensure your project file includes the `UserSecretsId` property:
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <UserSecretsId>aspnet-MyWebApp-12345678-1234-1234-1234-1234567890ab</UserSecretsId>
  </PropertyGroup>
</Project>
```

#### 5. Modify `Program.cs`
Update `Program.cs` to include user-secrets in the configuration:
```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add user-secrets to the configuration
            builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();

            // Add an endpoint to demonstrate accessing secrets
            app.MapGet("/secrets", (IConfiguration config) =>
            {
                var secretValue = config["MySecret"];
                return Results.Ok($"Secret Value: {secretValue}");
            });

            app.Run();
        }
    }
}
```

#### 6. Run Your Application
Run your application to see the secret in action:
```bash
dotnet run
```

#### 7. Access the Secrets Endpoint
Navigate to `https://localhost:5001/secrets` in your browser. You should see the secret value displayed:
```
Secret Value: ThisIsASecretValue
```

### Summary
This example demonstrates how to add user-secrets to an existing .NET 8.0 web application, configure the application to use these secrets, and create an endpoint to access and display the secret value.

Would you like more details on any specific part of this process?

Source: Conversation with Copilot, 7/14/2024
(1) Safe storage of app secrets in development in ASP.NET Core. https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0.
(2) How to add User Secrets in a .NET Core console app | makolyte. https://makolyte.com/how-to-add-user-secrets-in-a-dotnetcore-console-app/.
(3) Safe storage of app secrets during development. https://aspnetcore.readthedocs.io/en/stable/security/app-secrets.html.
(4) How to get "Manage User Secrets" in a .NET Core console-application?. https://stackoverflow.com/questions/42268265/how-to-get-manage-user-secrets-in-a-net-core-console-application.
(5) Create an ASP.NET Core web app with user data protected by authorization. https://learn.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-8.0.