#!/bin/bash
docker stop $(docker ps -qa)
docker rm $(docker ps -qa)
docker rmi $(docker images -q)
docker pull $1
docker run $1
