#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Votinger.AuthServer/Votinger.AuthServer.Web/Votinger.AuthServer.Web.csproj", "Votinger.AuthServer/Votinger.AuthServer.Web/"]
RUN dotnet restore "Votinger.AuthServer/Votinger.AuthServer.Web/Votinger.AuthServer.Web.csproj"
COPY . .
WORKDIR "/src/Votinger.AuthServer/Votinger.AuthServer.Web"
RUN dotnet build "Votinger.AuthServer.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Votinger.AuthServer.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Votinger.AuthServer.Web.dll"]