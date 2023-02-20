FROM mcr.microsoft.com/dotnet/sdk:7.0 AS Build

# Data project
RUN mkdir -p /src/RubbergodService.Data
COPY "RubbergodService.Data/RubbergodService.Data.csproj" /src/RubbergodService.Data
RUN dotnet restore "src/RubbergodService.Data/RubbergodService.Data.csproj" -r linux-x64

# App
RUN mkdir -p /src/RubbergodService
COPY "RubbergodService/RubbergodService.csproj" /src/RubbergodService
RUN dotnet restore "src/RubbergodService/RubbergodService.csproj" -r linux-x64

COPY "RubbergodService.Data/" /src/RubbergodService.Data
COPY "RubbergodService/" /src/RubbergodService
RUN mkdir -p /publish
RUN dotnet publish /src/RubbergodService -c Release -o /publish --no-restore -r linux-x64 --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as FinalImage
LABEL org.opencontainers.image.source https://github.com/grillbot/rubbergodService

WORKDIR /app
EXPOSE 5031
ENV TZ=Europe/Prague
ENV ASPNETCORE_URLS 'http://+:5031'
ENV DOTNET_PRINT_TELEMETRY_MESSAGE 'false'

RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
RUN apt update && apt install -y --no-install-recommends tzdata libc6-dev
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

COPY --from=Build /publish .
ENTRYPOINT [ "dotnet", "RubbergodService.dll" ]
