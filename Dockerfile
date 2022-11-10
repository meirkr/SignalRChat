FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY SignalRChat.csproj .
RUN dotnet restore "SignalRChat.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "SignalRChat.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SignalRChat.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SignalRChat.dll"]
