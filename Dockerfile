FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Install Playwright dependencies
RUN apt-get update && apt-get install -y \
    libnss3 libatk-bridge2.0-0 libxcomposite1 libxdamage1 libxrandr2 libgbm1 \
    libasound2 libpangocairo-1.0-0 libgtk-3-0 libxshmfence1 libx11-xcb1 libdrm2 \
    wget curl gnupg unzip && rm -rf /var/lib/apt/lists/*

# Set working directory
WORKDIR /app

# Copy solution and project
COPY ../AE_extensive_project.sln ./AE_extensive_project.sln
COPY . ./AE_extensive_project/

# Restore and build
WORKDIR /app/AE_extensive_project
RUN dotnet restore ../AE_extensive_project.sln
RUN dotnet build ../AE_extensive_project.sln --no-restore

# Install Playwright CLI and browsers
RUN dotnet tool install --global Microsoft.Playwright.CLI
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN playwright install

CMD ["dotnet", "test", "--logger:trx"]