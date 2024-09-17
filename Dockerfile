#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV DEBIAN_FRONTEND=noninteractive

# Instalar ffmpeg
RUN apt-get -y update && apt-get -y upgrade && apt-get install -y --no-install-recommends ffmpeg \
    && rm -rf /var/lib/apt/lists/*

# Criar diretório para ffmpeg se necessário e definir permissões
RUN mkdir -p /MediaToolkit && chmod -R 755 /MediaToolkit

# Mover o ffmpeg para o diretório correto, se necessário
RUN ln -s $(which ffmpeg) /MediaToolkit/ffmpeg.exe

# Criar diretório Downloads e ajustar permissões
RUN mkdir -p /app/Downloads && chown -R app:app /app/Downloads

USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APIDownloadMP3.csproj", "."]
RUN dotnet restore "./APIDownloadMP3.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./APIDownloadMP3.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./APIDownloadMP3.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "APIDownloadMP3.dll"]