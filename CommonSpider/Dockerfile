FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY CommonSpider/CommonSpider.csproj CommonSpider/
COPY CommonSpider.JVideo/CommonSpider.JVideo.csproj CommonSpider.JVideo/
COPY CommonSpider.DBHelper/CommonSpider.DBHelper.csproj CommonSpider.DBHelper/
COPY CommonSpider.JVideoSinglePage/CommonSpider.JVideoSinglePage.csproj CommonSpider.JVideoSinglePage/
RUN dotnet restore
COPY . .
WORKDIR /src/CommonSpider
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CommonSpider.dll"]
