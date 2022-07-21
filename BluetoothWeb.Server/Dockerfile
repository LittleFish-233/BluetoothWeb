#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BluetoothWeb.Server/BluetoothWeb.Server.csproj", "BluetoothWeb.Server/"]
COPY ["BluetoothWeb.Shared/BluetoothWeb.Shared.csproj", "BluetoothWeb.Shared/"]
COPY ["BluetoothWeb.DataModel/BluetoothWeb.DataModel.csproj", "BluetoothWeb.DataModel/"]
RUN dotnet restore "BluetoothWeb.Server/BluetoothWeb.Server.csproj"
COPY . .
WORKDIR "/src/BluetoothWeb.Server"
RUN dotnet build "BluetoothWeb.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BluetoothWeb.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BluetoothWeb.Server.dll"]