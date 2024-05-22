startup: up

up: 
	docker compose up -d --build 

down:
	docker compose down

restart: down startup

tests: 
	cd ./OpenBanking.Tests && dotnet test

deps:
	cd ./OpenBanking.Tests && docker compose up -d --build 

undeps:
	cd ./OpenBanking.Tests && docker compose down