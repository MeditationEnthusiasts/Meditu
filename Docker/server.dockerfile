# Docker file for the server
# (NOT the Electron app... that can't be a docker image).
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# First, run "dotnet publish" and ensure the output directory
# is in the bin folder of this directory.
# Then, this will take care of the rest.
COPY bin/ App/

WORKDIR /App
ENTRYPOINT [ "dotnet", "Meditu.Gui.dll" ]
