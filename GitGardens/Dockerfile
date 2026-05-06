# Use the SDK to build the code
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the project file first to save time on "restoring"
COPY ["GitGardens.csproj", "./"]
RUN dotnet restore "GitGardens.csproj"

# Copy everything else and build it
COPY . .
RUN dotnet build "GitGardens.csproj" -c Release -o /app/build
RUN dotnet publish "GitGardens.csproj" -c Release -o /app/publish

# Switch to the smaller "runtime" image to actually run the app
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GitGardens.dll"]