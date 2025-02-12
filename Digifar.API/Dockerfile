FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Digifar.API/Digifar.API.csproj", "Digifar.API/"]
COPY ["Digifar.Domain/Digifar.Domain.csproj", "Digifar.Domain/"]
COPY ["Digifar.Application/Digifar.Application.csproj", "Digifar.Application/"]
COPY ["Digifar.Contracts/Digifar.Contracts.csproj", "Digifar.Contracts/"]
COPY ["Digifar.Infrastructure/Digifar.Infrastructure.csproj", "Digifar.Infrastructure/"]
RUN dotnet restore "Digifar.API/Digifar.API.csproj"
COPY . .
WORKDIR "/src/Digifar.API"
RUN dotnet build "Digifar.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Digifar.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Digifar.API.dll"]
