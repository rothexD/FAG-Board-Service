# FAG-Board-Service

# What is this for?
This was coded as part of a university homework/project

# How to use?
docker-compose up should run this in default mode
"localhost:swagger" should lead you to the enabled ui

# 12Factors i followed?
i followed these factors from 12Factor.net

## 1 Codebase
This is the one and only Codebase, saved on GitHub

## 3 Config
A default Config is stored in the Environment. Additional Configuration can be done using docker-compose file

## 4 Backing Services
The only 3rd Party Service is a PostgresDb, any PostgresDB can be used changing the connectionstring in the docker-compose file

## 5 Build, release, run
On a commit to master branch this repository will automatically build and release to docker hub. any other use the image on dockerhub to run the latest version

## 6 Processes
The WebApi is the only process to be run locally by the Developer, the database can be run locally or externaly using the connectionstring.

## 7 Port Binding
In the docker-compose the default postgres database is only avaiable to the api. the api exports port 80 and a ui to the user. no other interface is given to the user.

## 9 Disposability
Very fast startup 

## 10 Dev/prod parity
by building and releasing any commit to master to dockerhub we can reach that development goes to production fast

## 11 Logs
i log to console which is stdout
