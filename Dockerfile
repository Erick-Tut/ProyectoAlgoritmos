FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ProyectoAlgoritmos.csproj", "./"]
RUN dotnet restore "ProyectoAlgoritmos.csproj"

COPY . .
RUN dotnet publish "ProyectoAlgoritmos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ProyectoAlgoritmos.dll"]
