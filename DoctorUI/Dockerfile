FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DoctorUI/DoctorUI.csproj", "DoctorUI/"]
RUN dotnet restore "DoctorUI/DoctorUI.csproj"
COPY . .
WORKDIR "/src/DoctorUI"
RUN dotnet build "DoctorUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DoctorUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DoctorUI.dll"]
