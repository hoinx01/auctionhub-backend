FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app
COPY ./ /app
EXPOSE 5001
ENTRYPOINT ["dotnet", "Startup.dll"]
CMD ["profile=live"]