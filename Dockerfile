FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore Planster_ModularMonolith.sln

RUN dotnet publish Planster_ModularMonolith.sln -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

EXPOSE 80

ENTRYPOINT ["dotnet", "Bootstraper.dll"]
