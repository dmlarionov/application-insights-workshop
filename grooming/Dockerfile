#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["grooming/grooming.csproj", "grooming/"]
RUN dotnet restore "grooming/grooming.csproj"
COPY . .
WORKDIR "/src/grooming"
RUN dotnet build "grooming.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "grooming.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "grooming.dll"]