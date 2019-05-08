#FROM microsoft/aspnetcore:2.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.0 AS build
#WORKDIR /src
#COPY ["User.Api/User.Api.csproj", "User.Api/"]
#RUN dotnet restore "User.Api/User.Api.csproj"
#COPY . .
#WORKDIR "/src/User.Api"
#RUN dotnet build "User.Api.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "User.Api.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "User.Api.dll"]

FROM microsoft/aspnetcore-build as build-env
WORKDIR /code
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=build-env  /code/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "User.Api.dll"]