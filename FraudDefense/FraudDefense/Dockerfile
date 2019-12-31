FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["FraudDefense/FraudDefense.csproj", "FraudDefense/"]
RUN dotnet restore "FraudDefense/FraudDefense.csproj"
COPY . .
WORKDIR "/src/FraudDefense"
RUN dotnet build "FraudDefense.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FraudDefense.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FraudDefense.dll"]