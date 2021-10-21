#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Votinger.PollServer/Votinger.PollServer.Web/Votinger.PollServer.Web.csproj", "Votinger.PollServer/Votinger.PollServer.Web/"]
COPY ["Votinger.PollServer/Votinger.PollServer.Infrastructure/Votinger.PollServer.Infrastructure.csproj", "Votinger.PollServer/Votinger.PollServer.Infrastructure/"]
COPY ["Votinger.PollServer/Votinger.PollServer.Core/Votinger.PollServer.Core.csproj", "Votinger.PollServer/Votinger.PollServer.Core/"]
COPY ["Votinger.PollServer/Votinger.PollServer.Services/Votinger.PollServer.Services.csproj", "Votinger.PollServer/Votinger.PollServer.Services/"]
RUN dotnet restore "Votinger.PollServer/Votinger.PollServer.Web/Votinger.PollServer.Web.csproj"
COPY . .
WORKDIR "/src/Votinger.PollServer/Votinger.PollServer.Web"
RUN dotnet build "Votinger.PollServer.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Votinger.PollServer.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Votinger.PollServer.Web.dll"]