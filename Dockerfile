FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["todo-list/todo-list.csproj", "todo-list/"]
RUN dotnet restore "todo-list/todo-list.csproj"

COPY . .
WORKDIR "/src/todo-list"
RUN dotnet build "todo-list.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "todo-list.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "todo-list.dll"]