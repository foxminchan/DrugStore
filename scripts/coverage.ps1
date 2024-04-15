dotnet test --collect:"XPlat Code Coverage"
"%UserProfile%\.nuget\packages\reportgenerator\5.2.4\tools\net8.0\ReportGenerator.exe" -reports:*\TestResults\*\coverage.cobertura.xml -targetdir:coveragereport
