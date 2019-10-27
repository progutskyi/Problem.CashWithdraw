# Cash withdrawal application

Simple cash withdrawal simulator. Returnes you amount request with the minimal amount of notes.

## Dev prerequisites
Visual Studio 2019 with ASP.NET and .NET Core packages

Or

.NET Core SDK 2.2

## Running application
You can either:

1. Open sln file in Visual Studio
2. Ensure Problem.CashWithdraw.Web is selected as start-up project
3. Hit run

Application should startup automatically in your default browser

Or run the following command in the solution directory
```
dotnet run --project .\Problem.CashWithdraw.Web\Problem.CashWithdraw.Web.csproj
```
## Running tests

You can either run all tests from Visual Studio Test explorer

Or

run the following command in the solution directory:
```
dotnet test .\Problem.CashWithdraw.Web.Tests\Problem.CashWithdraw.Web.Tests.csproj
```