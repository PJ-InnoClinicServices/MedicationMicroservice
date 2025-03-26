FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MedicationMicroservice.WebAPI/MedicationMicroservice.WebAPI.csproj", "MedicationMicroservice.WebAPI/"]
COPY ["MedicationMicroservice.DataAccess/MedicationMicroservice.DataAccess.csproj", "MedicationMicroservice.DataAccess/"]
COPY ["MedicationMicroservice.Shared/MedicationMicroservice.Shared.csproj", "MedicationMicroservice.Shared/"]
COPY ["MedicationMicroservice.BusinessLogic/MedicationMicroservice.BusinessLogic.csproj", "MedicationMicroservice.BusinessLogic/"]
RUN dotnet restore "MedicationMicroservice.WebAPI/MedicationMicroservice.WebAPI.csproj"
COPY . .
WORKDIR "/src/MedicationMicroservice.WebAPI"
RUN dotnet build "MedicationMicroservice.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MedicationMicroservice.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicationMicroservice.WebAPI.dll"]
