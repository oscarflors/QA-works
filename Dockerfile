# 1. Usamos la imagen oficial de Microsoft preparada para Playwright y .NET 8
FROM mcr.microsoft.com/playwright/dotnet:v1.48.0-noble

# 2. Creamos una carpeta de trabajo dentro del "mini-ordenador"
WORKDIR /app

# 3. Copiamos todos los archivos de tu proyecto al contenedor
COPY . .

# 4. Restauramos las dependencias y construimos el proyecto
RUN dotnet restore
RUN dotnet build --no-restore

# 5. Nos aseguramos de que el contenedor instale los navegadores ocultos
RUN pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps

# 6. La orden final: cuando el contenedor arranque, lanzará los tests automáticamente
CMD ["dotnet", "test"]