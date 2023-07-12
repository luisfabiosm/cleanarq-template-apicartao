#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["api.cartao/api.cartao.csproj", "api.cartao/"]
RUN dotnet restore "api.cartao/api.cartao.csproj"
COPY . .
WORKDIR "/src/api.cartao"
RUN dotnet build "api.cartao.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api.cartao.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.cartao.dll"]