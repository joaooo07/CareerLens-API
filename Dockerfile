# ================================
# 1) Build stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo para dentro do container
COPY . .

# Restaura dependências
RUN dotnet restore

# Publica o projeto principal (CareerLens)
RUN dotnet publish CareerLens/CareerLens.csproj -c Release -o /app/publish

# ================================
# 2) Runtime stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia arquivos publicados
COPY --from=build /app/publish .

# Expõe porta usada no Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Comando de execução
ENTRYPOINT ["dotnet", "CareerLens.dll"]
