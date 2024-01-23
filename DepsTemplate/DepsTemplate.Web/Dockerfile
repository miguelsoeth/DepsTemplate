#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.4-focal-amd64 AS base
RUN apt update && apt install --allow-unauthenticated -y tzdata locales curl libgdiplus libc6-dev ca-certificates
ENV TIME_ZONE="America/Sao_Paulo"
ENV TZ="America/Sao_Paulo"
ENV LANG="pt_BR.UTF-8"
ENV LANGUAGE="pt_BR:pt"
ENV LC_ALL="pt_BR.UTF-8"
RUN sed -i -e 's/# pt_BR.UTF-8 UTF-8/pt_BR.UTF-8 UTF-8/' /etc/locale.gen
RUN dpkg-reconfigure --frontend=noninteractive locales
RUN locale-gen
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /DepsTemplate
COPY ["DepsTemplate/DepsTemplate.Web/DepsTemplate.Web.csproj", "src/DepsTemplate.Web/"]
COPY ["DepsTemplate/DepsTemplate.Infrastructure/DepsTemplate.Infrastructure.csproj", "src/DepsTemplate.Infrastructure/"]
COPY ["DepsTemplate/DepsTemplate.SharedKernel/DepsTemplate.SharedKernel.csproj", "src/DepsTemplate.SharedKernel/"]
COPY ["DepsTemplate/DepsTemplate.Core/DepsTemplate.Core.csproj", "src/DepsTemplate.Core/"]
RUN dotnet restore "src/DepsTemplate.Web/DepsTemplate.Web.csproj"
COPY . .
WORKDIR "/DepsTemplate/DepsTemplate/DepsTemplate.Web"
RUN dotnet build "DepsTemplate.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DepsTemplate.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DepsTemplate.Web.dll"]