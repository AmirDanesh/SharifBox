@ECHO OFF
ECHO --------------------------------UserApi Building--------------------------------
CD %CD%\Services\UserApi
CALL dotnet build
ECHO --------------------------------TeamApi Building--------------------------------
CD %CD%\Services\TeamApi
CALL dotnet build
ECHO --------------------------------SpaceApi Building--------------------------------
CD %CD%\Services\SpaceApi
CALL dotnet build
ECHO --------------------------------NewsApi Building--------------------------------
CD %CD%\Services\News.Api
CALL dotnet build
ECHO --------------------------------FileApi Building--------------------------------
CD %CD%\Services\FileApi
CALL dotnet build
ECHO --------------------------------IdentityApi Building--------------------------------
CD %CD%\Services\IdentityApi
CALL dotnet build
ECHO --------------------------------ApiGateway Building--------------------------------
CD %CD%\ApiGateway
CALL dotnet build

CALL ./RunServices.bat
PAUSE