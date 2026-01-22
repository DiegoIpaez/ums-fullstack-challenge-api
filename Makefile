# Config
PROJECT_NAME=UmsApi
PROJECT_PATH=UmsApi/UmsApi.csproj
ENV=Development
CONFIGURATION=Release

# Phony targets
.PHONY: help restore build run watch test clean info

help:
	@echo ""
	@echo "Comandos disponibles:"
	@echo "  make restore   -> Restaura dependencias"
	@echo "  make build     -> Compila el proyecto (.NET 8)"
	@echo "  make run       -> Ejecuta la API"
	@echo "  make watch     -> Ejecuta con hot reload"
	@echo "  make clean     -> Limpia bin/obj"
	@echo ""

# Commands
restore:
	dotnet restore $(PROJECT_PATH)

build:
	dotnet build $(PROJECT_PATH) -c $(CONFIGURATION)

run:
	ASPNETCORE_ENVIRONMENT=$(ENV) dotnet run --project $(PROJECT_PATH)

watch:
	ASPNETCORE_ENVIRONMENT=$(ENV) dotnet watch run --project $(PROJECT_PATH)

migrate:
	@if [ -z "$(name)" ]; then \
		echo "❌ Falta el nombre de la migración"; \
		echo "👉 Uso: make migrate name=initial_migration"; \
		exit 1; \
	fi
	ASPNETCORE_ENVIRONMENT=$(ENV) dotnet ef migrations add $(name) --project $(PROJECT_PATH)

update-db:
	ASPNETCORE_ENVIRONMENT=$(ENV) dotnet ef database update --project $(PROJECT_PATH)


clean:
	dotnet clean
	rm -rf **/bin **/obj
