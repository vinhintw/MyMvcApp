# Sử dụng hình ảnh base của .NET Core 8 SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file .csproj và restore các dependencies
COPY *.csproj .
RUN dotnet restore

# Copy toàn bộ mã nguồn và build ứng dụng
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Sử dụng hình ảnh base của .NET Core 8 Runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy các file đã build từ stage build
COPY --from=build /app/publish .

# Expose port 8080 trong container
EXPOSE 8080

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "MyMvcApp.dll"]