#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["apigw/apigw.csproj", "apigw/"]
RUN dotnet restore "apigw/apigw.csproj"
COPY . .
WORKDIR "/src/apigw"
RUN dotnet build "apigw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apigw.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apigw.dll"]