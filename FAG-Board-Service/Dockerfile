FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FAG-Board-Service/FAG-Board-Service.csproj", "FAG-Board-Service/"]
RUN dotnet restore "FAG-Board-Service/FAG-Board-Service.csproj"
COPY . .
WORKDIR "/src/FAG-Board-Service"
RUN dotnet build "FAG-Board-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FAG-Board-Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FAG-Board-Service.dll"]
