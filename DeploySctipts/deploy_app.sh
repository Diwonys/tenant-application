#!/bin/bash

cd tenant-application
git pull
docker stop $(docker ps -qa)
docker rm $(docker ps -qa)
docker rmi $(docker images -q)

cd WebApplication
docker-compose -f "docker-compose.yml" -f "docker-compose.prod.yml" up -d