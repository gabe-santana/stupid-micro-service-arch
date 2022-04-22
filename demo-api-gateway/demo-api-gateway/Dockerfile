#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["demo-api-gateway/demo-api-gateway.csproj", "demo-api-gateway/"]
RUN dotnet restore "demo-api-gateway/demo-api-gateway.csproj"
COPY . .
WORKDIR "/src/demo-api-gateway"
RUN dotnet build "demo-api-gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "demo-api-gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "demo-api-gateway.dll"]