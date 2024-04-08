FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FitByBitService/FitByBitService.csproj", "FitByBitService/"]
RUN dotnet restore "FitByBitService/FitByBitService.csproj"
COPY . .
WORKDIR "/src/FitByBitService"
RUN dotnet build "FitByBitService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FitByBitService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitByBitService.dll"]
