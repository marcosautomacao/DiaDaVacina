#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./api.csproj", "api/"]
COPY ["../Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "api/api.csproj"
COPY . .
WORKDIR "/src/api"
RUN dotnet build "api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
#WORKDIR /src
#COPY . ./api/
#COPY ../Domain ./Domain/
#RUN dotnet restore 
#RUN dotnet build --no-restore -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish --no-restore -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
## Padrão de container ASP.NET
## ENTRYPOINT ["dotnet", "CarterAPI.dll"]
## Opção utilizada pelo Heroku
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet api.dll

##Use your choice of image as base. Mine is alpine! 
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
#WORKDIR /src
#COPY . .
#
#RUN dotnet restore api/api.csproj
#RUN dotnet build api/api.csproj -c Release -o /app/build
#
#FROM base AS final
#WORKDIR /app
#COPY --from=build /app/build .
#ENV ASPNETCORE_URLS http://*:PORT_NUMBER
#ENTRYPOINT ["dotnet", "Api.dll"]
##CMD ASPNETCORE_URLS=http://*:$PORT dotnet api.dll