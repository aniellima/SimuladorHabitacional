FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define diretório no container
WORKDIR /app

# Copia os arquivos do projeto
COPY *.csproj ./
RUN dotnet restore ./SimulacaoEmprestimo.csproj

# Copia os demais arquivos e constrói a aplicação
COPY . ./
RUN dotnet publish ./SimulacaoEmprestimo.csproj -c Release -o /app/publish

# Usa a imagem base do .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Instala bibliotecas de globalização na imagem de runtime
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        locales \
    && rm -rf /var/lib/apt/lists/*

# Gera a locale pt-BR
RUN locale-gen pt_BR.UTF-8

WORKDIR /app
COPY --from=build /app/publish .

# Define variável de ambiente para suportar globalização completa
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=pt_BR.UTF-8
ENV LANGUAGE=pt_BR:pt
ENV LC_ALL=pt_BR.UTF-8

# Listar os arquivos copiados para verificar a estrutura
RUN echo "Conteúdo de /app:" && ls -la /app

ENTRYPOINT ["dotnet", "SimulacaoEmprestimo.dll"]
