FROM microsoft/dotnet:2.1-sdk as build
WORKDIR /app

# copy sln and csproj to restore as a distinct layer
COPY src/*.sln .
COPY src/AperturePi.Controller/*.csproj ./AperturePi.Controller/
RUN dotnet restore

# copy everything else and build
COPY src/. .
WORKDIR /app/AperturePi.Controller
RUN dotnet build

FROM build as publish
WORKDIR /app/AperturePi.Controller
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime as runtime
WORKDIR /app
COPY --from=publish /app/AperturePi.Controller/out ./
ENTRYPOINT ["dotnet", "AperturePi.Controller.dll"]
