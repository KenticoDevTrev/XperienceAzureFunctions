# XperienceAzureFunctions
Example project of a working Kentico Xperience Azure Function implementation for KX 13. 

# Instructions
## Setup your Azure Functions
1. Clone this repository down
2. Rename `local.settings.json.sample` to `local.settings.json`
3. Add in your Connection string to your Azure SQL environment
4. Update the Xperience.Kentico.Libraries nuget package to whatever hotfix version of KX 13 you have (minimum 13.0.5 for .NET Core)
5. Add any custom classes to the Custom Library, or add your own custom .net Standard / .net Core class libraries (make sure to modify the Startup.cs `RegisterCustomAssemblies();` to include the binaries of any project you are using.
6. Create your own Functions

## Setup Azure Functions on DevOps
In your Azure Portal...
1. Add a new Resource -> Function App.
2. Set the Runtime stack to .NET and version 3.1
3. Host Operating System you can do Linux since this is all .net Core.
4. For the plan type, Consumption (serverless) is the cheapest and you can literally get millions of requests for a single dollar.

Once it is built, on the Overview click “Get Publish Profile” and use this to publish your app from Visual Studio to AF or your preferred means.  

Using the publish profile did fail for me with Visual Studio, so Instead you may want to use the normal Publish wizard:
1. Publish Azure Function Project
2. Select Azure -> Azure Function App (Linux)
3. Select your Subscription and find your Azure Function and publish.

You also may want to add a Custom Domain, such as "api.mydomain.com"

## Add License Key to Xperience
Make sure you have a valid license for whatever domain you do set up for Azure.

## Database Firewall Rules
Your Database needs to be accessible to your Azure Function, this means your database should be hosted in Azure SQL.

To grant access of your Azure Function to your Azure SQL Server, you can try to setup a VNet, or if you have Premium tier Azure Functions you can get a Static IP and whitelist that on the firewall settings.

If you wish to use the non premium tier, there is still a list of possible IP addresses your Azure Function will use.  To get these...
1. Go to https://resources.azure.com 
2. Expand Subscriptions ->  [Expand your Subscription] -> Providers -> Microsoft.Web -> Sites
3. Find your Azure Function Site in the JSON, and locate the `outboundIpAddresses` and `possibleoutboundIpAddresses`, these will contain a list of IP addresses.
4. Add all of them to your SQL server's firewall.
