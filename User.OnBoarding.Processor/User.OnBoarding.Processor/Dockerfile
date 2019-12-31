FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["User.OnBoarding.Processor/User.OnBoarding.Processor.csproj", "User.OnBoarding.Processor/"]
RUN dotnet restore "User.OnBoarding.Processor/User.OnBoarding.Processor.csproj"
COPY . .
WORKDIR "/src/User.OnBoarding.Processor"
RUN dotnet build "User.OnBoarding.Processor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.OnBoarding.Processor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.OnBoarding.Processor.dll"]