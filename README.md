# AnticiPay API
## Sobre o projeto
API desenvolvida em .NET 8 adotando princípios de **Domain-Driven Design (DDD)** e **SOLID** para uma solução estruturada e eficaz no gerenciamento de notas fiscais, carrinhos de antecipação e armazenamento seguro de dados em um banco de dados **MySQL**.

Além disso, a API oferece um serviço onde uma empresa pode solicitar a antecipação de recebíveis, calculando o valor a ser antecipado com base nas notas fiscais cadastradas e no limite de crédito, que varia de acordo com o faturamento mensal e o ramo da empresa.

A API segue os padrões **REST**, utilizando métodos **HTTP** adequados para garantir comunicação simples e eficiente. Para facilitar a exploração e o teste dos endpoints, a API conta com uma **documentação Swagger** interativa.

## Funcionalidades
- **Cadastro de empresas**: Registre uma nova empresa com todas as informações necessárias.
- **Login de empresas**: Autentique uma empresa utilizando suas credenciais.
- **Cadastro de notas fiscais**: Registre uma nova nota fiscal com todos os detalhes necessários.
- **Manipulação de carrinhos**: Adicione ou remova notas fiscais de um carrinho.
- **Checkout de carrinhos**: Finalize o carrinho e calcule o valor líquido das notas fiscais.
- **Solicitação de antecipação de recebíveis**: Calcule o valor a ser antecipado com base nas notas fiscais cadastradas e no limite de crédito da empresa.
- **Execução automática de migrações**: As migrações são aplicadas automaticamente ao iniciar o projeto.

## Tecnologias Utilizadas
- **.NET 8**
- **Entity Framework Core**
- **MySQL**
- **AutoMapper**
- **Swagger**

### Construído com

![Windows-Badge]
![.Net-Badge]
![MySQL]
![Visual-Studio]
![Swagger-Badge]

## Configuração do Projeto
### Requisitos
- Visual Studio 2022+ ou Visual Studio Code
- Windows 10+ ou Linux/MacOS com [.NET SDK 8.0][net-sdk-link] instalado
- MySQL Server

### Instalação e Execução
1. Clone o repositório:
 ```sh
    git clone https://github.com/brenonPerez/AnticiPay.git
 ```
2. adicione a string de conexão com o MySQL no arquivo `appsettings.json`.
```
"ConnectionStrings": {
    "Connection": "Server=localhost;Database=anticipaydb;Uid=root;Pwd=master"
}
```
3. adicione as configurações de JWT e a taxa mensal a ser cobrada no arquivo `appsettings.json`.
```
"Settings": {
    "Jwt": {
        "SigningKey": "W5U?B*Peuaf6s%?72)f1<htGl$+Q|kR/",
        "ExpiresMinutes": 1000
    },
    "Tax": {
        "MonthlyRate": 0.0465
    }
}
```
4. Execute a API dentro da pasta `src/AnticiPay.Api`:
```sh
   dotnet run
```
6. Verifique a porta onde está rodando a aplicação e acesse a documentação Swagger para testar os endpoints:
```
   https://localhost:5001/swagger/index.html
```

## Estrutura de Diretórios
```
AnticiPay/
│── AnticiPay.sln                     # Solução do projeto
│── README.md                         # Arquivo de documentação
│── src/                              # Diretório principal do código-fonte
│   ├── AnticiPay.Api/                # Camada de API
│   │   ├── Controllers/              # Controladores da API
│   │   ├── Filters/                  # Filtros globais para exceções
│   │   ├── Program.cs                # Configuração principal da API
│   │
│   ├── AnticiPay.Application/        # Camada de Aplicação
│   │   ├── AutoMapper/               # Configurações do AutoMapper
│   │   ├── UseCases/                 # Casos de uso
│   │
│   ├── AnticiPay.Communication/      # Comunicação de Requests e Responses
│   │   ├── Requests/                 # Arquivo de requests formato JSON
│   │   ├── Responses/                # Arquivo de responses formato JSON
│   │
│   ├── AnticiPay.Domain/             # Domínio da aplicação
│   │   ├── Entities/                 # Entidades do domínio
│   │   ├── Repositories/             # Interfaces para repositórios
│   │
│   ├── AnticiPay.Exception/          # Exceções e Recursos
│   │   ├── Exceptions/               # Exceções personalizadas
│   │   ├── Resources/                # Arquivos de Recurso
│   │
│   ├── AnticiPay.Infrastructure/     # Infraestrutura e persistência
│       ├── DataAccess/               # Configuração do EF Core
│       ├── Migrations/               # Migrações do banco de dados
```

## Arquitetura do Projeto
O projeto está dividido em três camadas principais:

1. **Camada de Infraestrutura (AnticiPay.Infrastructure)**
   - Responsável pelo acesso aos dados.
   - Implementa o Entity Framework Core para interagir com o MySQL.
   - Contém repositórios abstratos para manipulação de entidades.

2. **Camada de Aplicação (AnticiPay.Application)**
   - Contém os casos de uso (UseCases) que representam as regras de negócio.
   - Utiliza AutoMapper para transformar entidades em DTOs.

3. **Camada de API (AnticiPay.Api)**
   - Exponibiliza os endpoints REST para interação com faturas e carrinhos.
   - Implementa filtros globais para tratamento de exceções.


## Melhorias que poderiam ser feitas no projeto
- **Concluir Endpoints**: Implementar funcionalidades para alterar empresa, excluir a conta da empresa, alterar notas fiscais e excluir notas fiscais.
- **Implementar Usuários**: Permitir que, ao invés da empresa logar na aplicação, a empresa tenha usuários cadastrados.
- **Implementar Testes Automatizados**: Adicionar testes unitários e testes de integração para garantir a qualidade do código e facilitar a manutenção do projeto.
- **Otimização de Consultas**: Revisar e otimizar as consultas ao banco de dados para melhorar a performance.
- **Implementar CI/CD**: Configurar pipelines de integração contínua e entrega contínua para automatizar o processo de build, testes e deploy.
- **Monitoramento e Logging**: Implementar soluções de monitoramento e logging para acompanhar a saúde e a performance da aplicação em produção.

<!-- LINKS -->
[net-sdk-link]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- BADGES -->
[Windows-Badge]: https://img.shields.io/badge/Windows-blue?style=for-the-badge&logo=windows
[.Net-Badge]: https://img.shields.io/badge/.NET-8.0-blue?style=for-the-badge&logo=dotnet
[MySQL]: https://img.shields.io/badge/MySQL-8.0-blue?style=for-the-badge&logo=mysql
[Visual-Studio]: https://img.shields.io/badge/Visual%20Studio-2022-blue?style=for-the-badge&logo=visual-studio
[Swagger-Badge]: https://img.shields.io/badge/Swagger-UI-blue?style=for-the-badge&logo=swagger
