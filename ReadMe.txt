Refferd https://dev.to/rramname/api-gateway-and-microservices-using-kong-and-dotnet-core-in-docker-3khh

- Compose Steps by Eren 

	1. docker-compose build
	2. docker-compose up

- Swaggers:

	- http://localhost:7001/swagger/index.html
	- http://localhost:7002/swagger/index.html

- Kong Api Install Steps

	1. (Skip for now) 
	   docker run -d --name kong-database --network=kong-comic-net -p 5432:5432 -e "POSTGRES_USER=kong" -e "POSTGRES_DB=kong"  -e "POSTGRES_PASSWORD=kong" postgres:9.6
	2. 
		docker run --rm --network=kong-comic-net  -e "KONG_DATABASE=postgres" -e "KONG_PG_HOST=kong-database" -e "KONG_PG_USER=kong" -e "KONG_PG_PASSWORD=kong"  kong:latest kong migrations bootstrap
	3. 
		docker run -d --name kong --network=kong-comic-net -e "KONG_DATABASE=postgres" -e "KONG_PG_HOST=kong-database" -e "KONG_PG_USER=kong" -e "KONG_PG_PASSWORD=kong" -e "KONG_PROXY_ACCESS_LOG=/dev/stdout" -e "KONG_ADMIN_ACCESS_LOG=/dev/stdout" -e "KONG_PROXY_ERROR_LOG=/dev/stderr" -e "KONG_ADMIN_ERROR_LOG=/dev/stderr" -e "KONG_ADMIN_LISTEN=0.0.0.0:8001, 0.0.0.0:8444 ssl" -e "KONG_ADMIN_GUI_URL=http://localhost:8002" -p 8000:8000 -p 8443:8443 -p 127.0.0.1:8001:8001 -p 127.0.0.1:8444:8444 -p 8002:8002 kong:latest

	Admin GUI POWERED

		- docker run -d 
			 --name kong-gateway \
			 --network=kong-net \
			 -e "KONG_DATABASE=postgres" \
			 -e "KONG_PG_HOST=kong-database" \
			 -e "KONG_PG_USER=kong" \
			 -e "KONG_PG_PASSWORD=kongpass" \
			 -e "KONG_PROXY_ACCESS_LOG=/dev/stdout" \
			 -e "KONG_ADMIN_ACCESS_LOG=/dev/stdout" \
			 -e "KONG_PROXY_ERROR_LOG=/dev/stderr" \
			 -e "KONG_ADMIN_ERROR_LOG=/dev/stderr" \
			 -e "KONG_ADMIN_LISTEN=0.0.0.0:8001" \
			 -e "KONG_ADMIN_GUI_URL=http://localhost:8002" \
			 -e KONG_LICENSE_DATA \
			 -p 8000:8000 \
			 -p 8443:8443 \
			 -p 8001:8001 \
			 -p 8444:8444 \
			 -p 8002:8002 \
			 -p 8445:8445 \
			 -p 8003:8003 \
			 -p 8004:8004 \
			 kong/kong-gateway:3.3.0.0
	
	- Add APIS

		1. curl -i -X POST http://localhost:8001/services --data name=DC --data url="http://DC:7001/"
		   (if success gets 201)
		2. curl -i -X POST http://localhost:8001/services/DC/routes --data "paths[]=/DC-Web" --data name=DC-Web
		   (if success gets 201)
		3. curl -i -X POST http://localhost:8001/services/DC/routes --data "paths[]=/DC-App" --data name=DC-App
		   (if success gets 201 ready for http://localhost:8000/DC-Web/justiceleague/strongest)
		
		- Same Expectations like the Steps 1,2,3

			1. curl -i -X POST http://localhost:8001/services --data name=Marvel --data url="http://Marvel:7002/"
			2. curl -i -X POST http://localhost:8001/services/Marvel/routes --data "paths[]=/Marvel-Web" --data name=Marvel-Web
			3. curl -i -X POST http://localhost:8001/services/Marvel/routes --data "paths[]=/Marvel-App" --data name=Marvel-App
