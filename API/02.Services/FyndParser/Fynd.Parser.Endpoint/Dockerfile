#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:80  
EXPOSE 5001
EXPOSE 5000
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["API/02.Services/FyndParser/Fynd.Parser.Endpoint/Fynd.Parser.Endpoint.csproj", "API/02.Services/FyndParser/Fynd.Parser.Endpoint/"]
COPY ["API/01.Framework/Fynd.Framework.Core/Fynd.Framework.Core.csproj", "API/01.Framework/Fynd.Framework.Core/"]
COPY ["API/02.Services/FyndParser/Fynd.Parser.Application/Fynd.Parser.Application.csproj", "API/02.Services/FyndParser/Fynd.Parser.Application/"]
COPY ["API/02.Services/FyndParser/Fynd.Parser.ApplicationContract/Fynd.Parser.ApplicationContract.csproj", "API/02.Services/FyndParser/Fynd.Parser.ApplicationContract/"]
COPY ["API/02.Services/FyndParser/Fynd.Parser.Domain/Fynd.Parser.Domain.csproj", "API/02.Services/FyndParser/Fynd.Parser.Domain/"]
RUN dotnet restore "API/02.Services/FyndParser/Fynd.Parser.Endpoint/Fynd.Parser.Endpoint.csproj"
COPY . .
WORKDIR "/src/API//02.Services/FyndParser/Fynd.Parser.Endpoint"
RUN dotnet build "Fynd.Parser.Endpoint.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fynd.Parser.Endpoint.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fynd.Parser.Endpoint.dll"]