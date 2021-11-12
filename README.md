<h1 align="center">Votinger</h1>
<div align="center">
<img alt="GitHub Workflow Status" src="https://img.shields.io/github/workflow/status/SeanWoo/Votinger/.NET-Test">
</div>

## Description
__Votinger__ is a web service for creating and conducting polls. In it, you can create polls on various topics and various topics, and users of the service can participate in your voting.

The web service puts your vote in an open feed where everyone can vote

## Technology stack
 - Back-end
    - ASP.NET Core WebAPI (.NET 5)
    - MySQL
    - [gRPC](https://grpc.io/)
 - Front-end
    - [React](https://reactjs.org/) ([TypeScript](https://www.typescriptlang.org/))
    - Redux
 - Other
    - Webpack 
    - Sass
    - Swashbuckle
 
JWT token based authorization using acces and refresh token

## Build and run
```console
docker-compose up --build
```
