# DevCars.API
Desenvolvida uma API REST completa de gerenciamento de vendas de carros durante o bootcamp ASP.NET Core

## Tecnologias e práticas utilizadas
- ASP.NET Core com .NET 5
- Entity Framework Core
- Dapper
- Swagger
- Injeção de Dependência
## Funcionalidades
- Cadastro, Listagem, Detalhes, Atualização e Remoção de Carros
- Cadastro e Detalhe de Venda de Carro, com items extras
- Cadastro de Cliente
## Observações
  Para executar o projeto, tenha instalado o EF e Dapper. Após isso, dentro do diretório \DevCars.API\, execute o prompt de comando ou git bash e dê os seguintes comandos:

  1. dotnet ef migrations add NomeDaMigration
  2. dotnet ef database update
  
 PS: Tenha instalado o SQL Server ou adapte o projeto ao banco desejado, configure também a connectionstring.
 
## Projeto
![SWCar](https://user-images.githubusercontent.com/60578339/113944746-71559c00-97db-11eb-8b8e-bcd069c58bb3.PNG)

![SWCust](https://user-images.githubusercontent.com/60578339/113944763-774b7d00-97db-11eb-90e1-e15e9926165b.PNG)
