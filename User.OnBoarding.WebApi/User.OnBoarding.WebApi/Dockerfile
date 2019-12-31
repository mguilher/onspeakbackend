FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["User.OnBoarding.WebApi/User.OnBoarding.WebApi.csproj", "User.OnBoarding.WebApi/"]
RUN dotnet restore "User.OnBoarding.WebApi/User.OnBoarding.WebApi.csproj"
COPY . .
WORKDIR "/src/User.OnBoarding.WebApi"
RUN dotnet build "User.OnBoarding.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.OnBoarding.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.OnBoarding.WebApi.dll"]