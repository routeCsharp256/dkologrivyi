FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/merchandise-service/merchandise-service.csproj","src/merchandise-service/"]
RUN dotnet restore "src/merchandise-service/merchandise-service.csproj"
WORKDIR /
COPY . .
WORKDIR /src/merchandise-service/
RUN dotnet build "merchandise-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "merchandise-service.csproj" -c Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

EXPOSE 80

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .

#ENTRYPOINT [ "dotnet", "merchandise-service.dll"]
#ENTRYPOINT [ "dotnet", "MerchandaiseMigrator.dll"]
RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh