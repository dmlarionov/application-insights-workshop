FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish ./cli/cli.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime
COPY --from=build /out /app
WORKDIR /app
ENTRYPOINT [ "dotnet", "cli.dll" ]