FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN apt update -y && \
    apt install nodejs npm -y && \
    apt -y clean && \
    apt -y autoclean
