FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# copy and build everything else
COPY . .

RUN dotnet build


FROM build AS publish
WORKDIR /app
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-runtime AS runtime
WORKDIR /app
COPY --from=publish /app/out ./
ENTRYPOINT ["dotnet", "SignalRChat.dll"]
