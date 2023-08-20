##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DC.csproj", "."]
RUN dotnet restore "./DC.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DC.dll"]

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
#
#WORKDIR /app
#EXPOSE 80
##EXPOSE 443
##EXPOSE 7001
#
#COPY ./ ./
#RUN dotnet restore DC.csproj
#
#COPY . ./
#RUN dotnet publish -c Release -o out
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /app
#COPY --from=build-env /app/out .
##ENV ASPNETCORE_URLS=http://+:7001  
#ENTRYPOINT ["dotnet", "DC.dll"]