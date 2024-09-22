#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BSS.DishDepot.Api/BSS.DishDepot.Api.csproj", "src/BSS.DishDepot.Api/"]
COPY ["src/BSS.DishDepot.Application/BSS.DishDepot.Application.csproj", "src/BSS.DishDepot.Application/"]
COPY ["src/BSS.DishDepot.Domain/BSS.DishDepot.Domain.csproj", "src/BSS.DishDepot.Domain/"]
COPY ["src/BSS.DishDepot.Presentation/BSS.DishDepot.Presentation.csproj", "src/BSS.DishDepot.Presentation/"]
RUN dotnet restore "./src/BSS.DishDepot.Api/BSS.DishDepot.Api.csproj"
COPY . .
WORKDIR "/src/src/BSS.DishDepot.Api"
RUN dotnet build "./BSS.DishDepot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BSS.DishDepot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BSS.DishDepot.Api.dll"]