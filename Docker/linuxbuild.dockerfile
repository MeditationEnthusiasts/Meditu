FROM mcr.microsoft.com/dotnet/aspnet:6.0

RUN apt update -y && \
    apt install nodejs npm -y && \
    apt -y clean && \
    apt -y autoclean
