// to run migration use below commands

**

dotnet ef migrations add InitialCreate --project CustomerApi.DataAccess --startup-project CustomerApi

dotnet ef database update --project=CustomerApi

**
