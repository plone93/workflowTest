# -----------------------------
# Build  (.NET SDK )
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# csproj 복사 및 복원
COPY *.csproj ./
RUN dotnet restore

# 전체 소스 복사 및 빌드
COPY . ./
RUN dotnet publish -c Release -o /app/out

# -----------------------------
# Runtime  (.NET Runtime)
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# 빌드 결과 복사
COPY --from=build /app/out ./

# 앱 실행
ENTRYPOINT ["dotnet", "razorWebApplication1.dll"]

# 외부 접근 포트
EXPOSE 5000
