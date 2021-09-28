#!/bin/bash
docker stop $(docker ps -qa)
docker rm $(docker ps -qa)
docker rmi $(docker images -q)
docker pull $1
#docker run $1
docker-compose -f "/bin/docker_config_bash/docker-compose.yml" -f "/bin/docker_config_bash/docker-compose.override.yml" up
#docker-compose.vs.release.g.yml

# 1>docker-compose  -f "D:\Intership\tenant-application\WebApplication\docker-compose.yml" -f "D:\Intership\tenant-application\WebApplication\docker-compose.override.yml" -p dockercompose17129638450449943462 --no-ansi build
# docker-compose  -f "D:\Intership\tenant-application\WebApplication\docker-compose.yml" -f "D:\Intership\tenant-application\WebApplication\docker-compose.override.yml" -f "D:\Intership\tenant-application\WebApplication\obj\Docker\docker-compose.vs.release.g.yml" -p dockercompose17129638450449943462 --no-ansi up -d --no-build