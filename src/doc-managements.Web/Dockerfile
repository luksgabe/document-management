FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
RUN apt-get update && apt-get install -y locales tzdata
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["doc-managements.Web/DocManagements.Web.csproj", "doc-managements.Web/"]
COPY ["DocManagements.Infra.Data/DocManagements.Infra.Data.csproj", "DocManagements.Infra.Data/"]
COPY ["DocManagements.Infra.CrossCutting.IoT/DocManagements.Infra.CrossCutting.IoT.csproj", "DocManagements.Infra.CrossCutting.IoT/"]
COPY ["DocManagement.Core/DocManagement.Core.csproj", "DocManagement.Core/"]
COPY ["DocManagements.AppServices/DocManagements.AppServices.csproj", "DocManagements.AppServices/"]
COPY . .

# Restore as distinct layers
RUN dotnet restore "doc-managements.Web/DocManagements.Web.csproj"
COPY . .
WORKDIR "/src/doc-managements.Web"
# Build and publish a release
RUN dotnet build "DocManagements.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocManagements.Web.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final 
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocManagements.Web.dll"]