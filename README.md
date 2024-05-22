# open-banking
## Tecnology used
This project was made using:
- UI: React JS
- API: ASP.NET .NET Core 8
- Database: Mongo DB
- NGINX
- makefile

## How to run the project 
### Startup
To startup project run: make startup

### UI
- The ui responses on: [http://localhost:10000/](http://localhost:10000/)

### Documentation
The docs are placed in this [location](./docs/docs.md). 

### API Swagger
- The swagger api response on [http://localhost:10000/api/index.html](http://localhost:10000/api/index.html)

## Tests
This project has integration tests in order to run tests properly please run
`make deps` before running the tests then run `make tests`

### Unload dependencies
To unload test dependencies run `make undeps`
