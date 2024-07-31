// to run migration use below commands

**

dotnet tool install --global dotnet-ef --version 7.0.5

dotnet ef migrations add InitialCreate --project CustomerApi.DataAccess --startup-project CustomerApi

dotnet ef database update --project=CustomerApi

**
