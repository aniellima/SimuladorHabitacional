# Obter o diretório do script atual
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

# Construir o caminho absoluto para packages.txt
$packagesPath = Join-Path -Path $scriptDir -ChildPath "../packages.txt"

# Ler o arquivo packages.txt e instalar cada pacote listado
Get-Content -Path $packagesPath | ForEach-Object {
    $package, $version = $_ -split ","
    if ($package -and $version) {
        Write-Output "Instalando pacote $package versão $version"
        dotnet add package $package --version $version
    }
}
# Restaurar as dependências especificando o arquivo de projeto
dotnet restore ../SimulacaoEmprestimo.csproj
