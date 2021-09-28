#!/bin/bash
docker stop $(docker ps -qa)
docker rm $(docker ps -qa)
docker rmi $(docker images -q)
docker pull $1

docker-compose -f "/bin/docker_config_bash/docker-compose.yml" -f "/bin/docker_config_bash/docker-compose.override.yml" up
#docker-compose -f "/bin/docker_config_bash/docker-compose.yml" -f "/bin/docker_config_bash/docker-compose.vs.release.g.yml" up