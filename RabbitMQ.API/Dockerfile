FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["RabbitMQ.API/RabbitMQ.API.csproj", "RabbitMQ.API/"]
COPY ["RabbitMQ.MessageSenderBus/RabbitMQ.MessageSenderBus.csproj", "RabbitMQ.MessageSenderBus/"]
COPY ["RabbitMQ.MessageReceiverBus/RabbitMQ.MessageReceiverBus.csproj", "RabbitMQ.MessageReceiverBus/"]

RUN dotnet restore "RabbitMQ.API/RabbitMQ.API.csproj"

COPY . .

WORKDIR "/src/RabbitMQ.API"
RUN dotnet build "RabbitMQ.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/RabbitMQ.API"
RUN dotnet publish "RabbitMQ.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ADD https://github.com/jwilder/dockerize/releases/download/v0.6.1/dockerize-linux-amd64-v0.6.1.tar.gz /tmp/
RUN tar -C /usr/local/bin -xzvf /tmp/dockerize-linux-amd64-v0.6.1.tar.gz && rm /tmp/dockerize-linux-amd64-v0.6.1.tar.gz

ENTRYPOINT ["dockerize", "-wait", "tcp://rabbitmq:5672", "-timeout", "60s", "dotnet", "RabbitMQ.API.dll"]
