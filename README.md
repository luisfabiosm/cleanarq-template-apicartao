Example creating an API and Worker, using some Clean Arq, DDD and P&A. Applying the use of integration with MongoDB and RabbitMQ


Use the Docker folder with docker-compose file or commands to set up the necessary infrastructure for the environment.

version: '3'
services:
  mongodb:
    image: mongo
    ports:
      - '27017:27017'
    container_name: mongodb

  mongo-express:
    image: mongo-express
    ports:
      - '8081:8081'
    links:
      - mongodb:mongo
    container_name: mongo-express

  rabbitmq:
    image: rabbitmq:3-management
    ports:
      - '15672:15672'
      - '5672:5672'
    container_name: rabbitmq
