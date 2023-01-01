FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SocialPoster/SocialPoster.csproj", "SocialPoster/"]
RUN dotnet restore "SocialPoster/SocialPoster.csproj"
COPY . .
WORKDIR "/src/SocialPoster"
RUN dotnet build "SocialPoster.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialPoster.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialPoster.dll"]
