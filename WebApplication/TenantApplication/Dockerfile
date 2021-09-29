#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TenantApplication/TenantApplication.csproj", "TenantApplication/"]
RUN dotnet restore "TenantApplication/TenantApplication.csproj"
COPY . .
WORKDIR "/src/TenantApplication"
RUN dotnet build "TenantApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TenantApplication.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS="https://+"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="./webserver.pfx"

WORKDIR /app
COPY --from=publish /app/publish .
COPY webserver.pfx /app
ENTRYPOINT ["dotnet", "TenantApplication.dll"]