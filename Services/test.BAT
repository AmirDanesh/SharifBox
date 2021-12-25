@ECHO OFF
PRINT Starting Services...
CD %CD%\Services\UserApi
CALL start /d "." dotnet run args
CD %CD%\Services\TeamApi
CALL start /d "." dotnet run args
CD %CD%\Services\SpaceApi
CALL start /d "." dotnet run args
CD %CD%\Services\News.Api
CALL start /d "." dotnet run args
CD %CD%\Services\FileApi
CALL start /d "." dotnet run args
CD %CD%\Services\IdentityApi
CALL start /d "." dotnet run args
CD %CD%\ApiGateway
CALL start /d "." dotnet run args
PAUSE