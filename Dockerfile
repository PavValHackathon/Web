#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
COPY out/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "PavValHackathon.Web.API.dll"]