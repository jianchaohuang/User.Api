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

FROM ccr.ccs.tencentyun.com/dotnet-core/sdk as build-env
WORKDIR /code
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env  /code/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "User.Api.dll"]

## 添加基础镜像
#FROM microsoft/dotnet:2.1-aspnetcore-runtime
##容器中系统的工作空间
#WORKDIR /app
##拷贝当前文件夹下的文件到容器中系统的工作空间
#COPY . /app 
##设置Docker容器对外暴露的端口
#EXPOSE 80
##容器中使用 ["dotnet","系统启动的dll"] 来运行应用程序
##使用ENTRYPOINT ["dotnet","系统启动的dll"]
##或使用 CMD ["dotnet","系统启动的dll"]
#ENTRYPOINT ["dotnet", "User.Api.dll"]