FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base-backend
RUN apt-get update && apt-get install -y --allow-unauthenticated libgdiplus
ENV ASPNETCORE_ENVIRONMENT=Test
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-backend
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Test
COPY ["SAM.Api/SAM.Api.csproj", "SAM.Api/"]
COPY ["SAM.BLL/SAM.Application.csproj", "SAM.BLL/"]
COPY ["SAM.DAL/SAM.Infrastructure.csproj", "SAM.DAL/"]
COPY ["SAM.Domain/SAM.Domain.csproj", "SAM.Domain/"]
RUN dotnet restore "SAM.Api/SAM.Api.csproj"
COPY . .
WORKDIR "/src/SAM.Api"
RUN dotnet build "SAM.Api.csproj" -c Release -o /app/build

FROM build-backend AS publish
RUN dotnet publish "SAM.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet tool install --version 6.4.0 Swashbuckle.AspNetCore.Cli --tool-path /
RUN /swagger tofile --output /app/swagger.json /app/publish/SAM.Api.dll v1

FROM node:14.21.3-alpine AS frontend-build
WORKDIR /app
COPY SAM.Frontend/package*.json ./
RUN npm install
COPY SAM.Frontend .
COPY --from=publish /app/swagger.json ./swagger.json
RUN npm install -g ng-openapi-gen
RUN ng-openapi-gen --input ./swagger.json --output ./src/app/api
RUN npm run build --prod

FROM nginx:alpine AS final-frontend
COPY SAM.Frontend/nginx/default.conf /etc/nginx/conf.d/
COPY --from=frontend-build /app/dist/SAM.frontend /usr/share/nginx/html
EXPOSE 80
EXPOSE 443

FROM base-backend AS final-backend
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "SAM.Api.dll"]