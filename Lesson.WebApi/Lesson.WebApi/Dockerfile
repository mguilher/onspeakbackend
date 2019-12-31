FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Lesson.WebApi/Lesson.WebApi.csproj", "Lesson.WebApi/"]
RUN dotnet restore "Lesson.WebApi/Lesson.WebApi.csproj"
COPY . .
WORKDIR "/src/Lesson.WebApi"
RUN dotnet build "Lesson.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lesson.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lesson.WebApi.dll"]