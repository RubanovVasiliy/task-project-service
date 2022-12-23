FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["task-project-service.csproj", "./"]
RUN dotnet restore "task-project-service.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "task-project-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "task-project-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "task-project-service.dll"]
