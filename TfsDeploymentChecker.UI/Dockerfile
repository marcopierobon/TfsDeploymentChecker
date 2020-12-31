#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TfsDeploymentChecker.UI/Sequel.DeploymentChecker.UI.csproj", "TfsDeploymentChecker.UI/"]
RUN dotnet restore "TfsDeploymentChecker.UI/TfsDeploymentChecker.UI.csproj"
COPY . .
WORKDIR "/src/TfsDeploymentChecker.UI"
RUN dotnet build "TfsDeploymentChecker.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TfsDeploymentChecker.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", TfsDeploymentChecker.UI.dll"]