Запуск тестов с генерацией отчета code coverage формата cobertura:
- зайти в каталог с решением и запустить команду
dotnet test --no-build --collect:"XPlat Code Coverage" --settings ..\..\..\coverlet.runsettings

Запуск генерации отчетов:
reportgenerator.exe -reports:"..\..\CodeCoverageReportsTests\**\*.xml" -targetdir:"result" -reporttypes:"HTMLSummary"
где
-reports - путь до отчетов cide civerage
-targetdir - каталог для генерации отчета
-reporttypes - тип отчета
