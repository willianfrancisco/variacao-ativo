FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development
# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["../src/VariacaoAtivo.Application/VariacaoAtivo.Application.csproj", "src/VariacaoAtivo.Application/"]
COPY ["../src/VariacaoAtivo.Domain/VariacaoAtivo.Domain.csproj", "src/VariacaoAtivo.Domain/"]
COPY ["../src/VariacaoAtivo.Infra.Data/VariacaoAtivo.Infra.Data.csproj", "src/VariacaoAtivo.Infra.Data/"]
COPY ["../src/VariacaoAtivo.Infra.Ioc/VariacaoAtivo.Infra.Ioc.csproj", "src/VariacaoAtivo.Infra.Ioc/"]
COPY ["src/VariacaoAtivo.UI/VariacaoAtivo.UI.csproj", "src/VariacaoAtivo.UI/"]
RUN dotnet restore "src/VariacaoAtivo.UI/VariacaoAtivo.UI.csproj"
COPY . .
WORKDIR "/src/src/VariacaoAtivo.UI"
RUN dotnet build "VariacaoAtivo.UI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "VariacaoAtivo.UI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VariacaoAtivo.UI.dll"]
