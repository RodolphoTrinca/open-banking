startup: up

up: 
	docker compose up -d --build 

down:
	docker compose down

restart: down startup

tests: 
	cd ./OpenBanking.Tests && dotnet test