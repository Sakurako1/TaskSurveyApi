# 1. Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY SurveyApi/ SurveyApi/
RUN dotnet restore "./SurveyApi/SurveyApi.csproj"
WORKDIR "/src/SurveyApi"
RUN dotnet build "./SurveyApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 2. Этап публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SurveyApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 3. Финальная стадия - используем образ aspnet
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Копируем wait-for-it.sh в контейнер
COPY SurveyApi/wait-for-it.sh /usr/local/bin/wait-for-it
RUN chmod +x /usr/local/bin/wait-for-it

# 5. Финальная стадия - запуск приложения
FROM final AS run
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["wait-for-it", "db:5432", "--", "dotnet", "SurveyApi.dll"]
