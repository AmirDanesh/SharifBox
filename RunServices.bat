@ECHO OFF
PRINT Starting Services...
CALL start "UserAPI" /d "%CD%\Services\UserApi" dotnet run --no-build
CALL start "TeamAPI" /d "%CD%\Services\TeamApi" dotnet run --no-build
CALL start "SpaceAPI" /d "%CD%\Services\SpaceApi" dotnet run --no-build
CALL start "NewsAPI" /d "%CD%\Services\News.Api" dotnet run --no-build
CALL start "FileAPI" /d "%CD%\Services\FileApi" dotnet run --no-build
CALL start "IdentityAPI" /d "%CD%\Services\IdentityApi" dotnet run --no-build
CALL start "ApiGateway" /d "%CD%\ApiGateway" dotnet run --no-build