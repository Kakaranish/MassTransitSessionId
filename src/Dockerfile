FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MassTransitSessionId.csproj", "./"]
RUN dotnet restore "MassTransitSessionId.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "MassTransitSessionId.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransitSessionId.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransitSessionId.dll"]
