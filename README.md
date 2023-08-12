![maintained-shield]
![Stargazers][stars-shield]
[![LinkedIn][linkedin-shield]][linkedin-url]

<details open="open">
  <summary>Conteudo</summary>
  <ol>
    <li>
      <a href="#sobre-o-projeto">Sobre o projeto</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#features">Features</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#requisitos">Requisitos</a></li>
        <li><a href="#instalação">Instalação</a></li>
      </ul>
    </li>
  
  </ol>
</details>

## Sobre o projeto

Este projeto é uma API implementada em .NET Core 6 que foi construída utilizando a arquitetura Domain Drive Design.

Implementei este projeto com o objetivo de aprender novas tecnologias, metodologias e arquiteturas;

Este projeto é bem simples, ele é pequeno, mas eu abordo aqui varias técnicas de programação, tais como: princípios SOLID, integração com Sonarclound para manter uma boa qualidade de código, testes de unidade para todo o projeto juntamente com Postman collections para cada um dos endpoints implementados.

Esta API permite que o usuário se cadastre e faça controle de suas receitas de cozinha preferidas, podendo criar uma receita com título, ingredientes e modo de preparo. Cada receita recebe uma classificação para tornar fácil o filtro (café da manhã, almoço, sobremesa, jantar), pode ser editada e/ou excluída.

Uma feature legal que eu adicionei foi permitir que os usuários compartilhem suas receitas com outros usuários do App. Este compartilhamento é feito através de um WebSocket para permitir a conexão. O usuário que desejar compartilhar suas receitas vai gerar um QR Code para que um outro usuário leia e seja aceito como uma conexão. A partir dai, os dois usuários vão ver as receitas um do outro :)



### Build Status

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=MeuLivroDeReceitas&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=MeuLivroDeReceitas)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=MeuLivroDeReceitas&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=MeuLivroDeReceitas)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=MeuLivroDeReceitas&metric=bugs)](https://sonarcloud.io/summary/new_code?id=MeuLivroDeReceitas)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=MeuLivroDeReceitas&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=MeuLivroDeReceitas)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=MeuLivroDeReceitas&metric=coverage)](https://sonarcloud.io/summary/new_code?id=MeuLivroDeReceitas)

### Built With

![windows-shield] ![ubuntu-shield] ![visualstudio-shield] ![netcore-shield] ![mysql-shield] ![swagger-shield] ![postman-shield] ![sonarclound-shield]

### Features

- Registrar usuário;
- Criar minhas próprias receitas;
- Adicionar categoria nas receitas para facilitar o filtro;
- Compartilhar minhas receitas com amigos;
- Adicionar amigos usando WebSocket para aceitar conexão em tempo real.

E outras.

## Getting Started

### Requisitos

* Visual Studio 2022+

* MySQL

### Instalação

1. Faça o clone do repositório
   ```sh
   git clone https://github.com/matheusoliveira21tec/api-receitas-csharp.git
   ```
2. Preencha as informações no arquivo `appsettings.Development.json`.
3. Execute a Web API
4. Ótimo teste :)

## Licença

Sinta-se livre para usar este projeto para estudar e aprender. Você não tem permissão para utiliza-lo para distribuição ou comercialização.





<!-- Shields build with -->
[windows-shield]: https://img.shields.io/badge/Windows-00599E?style=for-the-badge&logo=windows&logoColor=white

[ubuntu-shield]: https://img.shields.io/badge/Ubuntu-93300A?style=for-the-badge&logo=ubuntu&logoColor=white


[visualstudio-shield]: https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white

[netcore-shield]: https://img.shields.io/badge/.NET_%20_Core_6.0-5C2D91?style=for-the-badge&logo=.net&logoColor=white

[mysql-shield]: https://img.shields.io/badge/MySQL-00000F?style=for-the-badge&logo=mysql&logoColor=white

[swagger-shield]: https://img.shields.io/badge/Swagger-205E3B?style=for-the-badge&logo=swagger&logoColor=white

[postman-shield]: https://img.shields.io/badge/Postman-AA2E00?style=for-the-badge&logo=postman&logoColor=white

[sonarclound-shield]: https://img.shields.io/badge/Sonarcloud-000000?style=for-the-badge&logo=Sonarcloud&logoColor=white

[buy-me-book]: https://img.shields.io/badge/-buy_me_a_book-gray?logo=buy-me-a-coffee&style=for-the-badge

<!-- Shields about the project -->
[maintained-shield]: https://img.shields.io/badge/Maintained%3F-yes-314100.svg?style=for-the-badge

[stars-shield]: https://img.shields.io/github/stars/welissonArley/Timerom.svg?style=for-the-badge&color=03146F

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555

<!-- Urls -->
[linkedin-url]: https://www.linkedin.com/in/matheus-oliveira-2409/
