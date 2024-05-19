# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

ADD . /build

RUN dotnet restore /build/main/JacksonVeroneze.NET.GRPCServer.Api/JacksonVeroneze.NET.GRPCServer.Api.csproj

RUN dotnet publish /build/main/JacksonVeroneze.NET.GRPCServer.Api/JacksonVeroneze.NET.GRPCServer.Api.csproj \
    --configuration Release \
    --output /build/out \
    --no-restore

#RUN dotnet restore /build/main/JacksonVeroneze.NET.GRPCServer.Api/JacksonVeroneze.NET.GRPCServer.Api.csproj \
#    --runtime linux-x64 \
#    /p:PublishReadyToRun=true

#RUN dotnet publish /build/main/JacksonVeroneze.NET.GRPCServer.Api/JacksonVeroneze.NET.GRPCServer.Api.csproj \
#    --configuration Release \
#    --output /build/out \
#    --no-restore \
#    --runtime linux-x64 \
#    --self-contained \
#    #/p:PublishTrimmed=true \ skipped - prometheus metrics
#    /p:PublishReadyToRun=true \
#    /p:PublishSingleFile=false

FROM jacksonveroneze/aspnet:8.0-debian-1.1.0

COPY --from=build-env /build/out /home/app
