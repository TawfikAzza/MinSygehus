FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PatientUI/PatientUI.csproj", "PatientUI/"]
RUN dotnet restore "PatientUI/PatientUI.csproj"
COPY . .
WORKDIR "/src/PatientUI"
RUN dotnet build "PatientUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PatientUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientUI.dll"]
