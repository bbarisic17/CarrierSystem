This is solution for the Carrier company IT system.

The solution consists of: docker-compose, Api gateway, Accounting microservice, Tickets microservice and TicketsB2B microservice.

Prerequisites:
	- Docker
	
Starting solution:
The solution should be run using docker-compose project. Just download the solution and open it in Visual Studio and start docker-compose project.
Docker-compose will start all microservices, RabbitMQ, Consul, Sql server database, Redis cache, Redis insight.
We used the code-first principle and databases and tables will be generetad automatically.

Urls:
Api gateway: http://localhost:5766/
Accounting microservice: http://localhost:5866/swagger/index.html
Tickets microservice: http://localhost:5966/swagger/index.html

We can access the microservice through api gateway.
Access to the accounting microservice healthcheck via api gateway: http://localhost:5766/gateway/accounting/Healthcheck/healthcheck
Access to the tickets microservice healthcheck via api gateway: http://localhost:5766/gateway/tickets/Healthcheck/healthcheck

RabbitMQ url: http://localhost:15672/ . Username:guest, Password:guest
Consul URL: http://localhost:8500/ 
Redis insight URL: http://localhost:5540/
Seq URL: http://localhost:5341/
Sql server: localhost,1430 . Username:sa, Password:YourStrong!Passw0rd