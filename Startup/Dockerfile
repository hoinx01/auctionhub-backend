FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app
COPY ./ /app
EXPOSE 5001
ENTRYPOINT ["dotnet", "Application.Startup.dll"]
CMD ["profile=live"]