# API de Fornecedores

## Objetivo

Esta API tem como objetivo fornecer funcionalidades para a gestão de fornecedores em uma plataforma. O sistema permite realizar operações de **CRUD** (Criar, Ler, Atualizar, Excluir) de fornecedores, além de permitir a vinculação de endereços aos fornecedores.

A API foi desenvolvida utilizando o framework **ASP.NET Core** com **C#** e segue os princípios do desenvolvimento orientado a objetos e padrões de design, com foco em boas práticas de desenvolvimento de software.

## Tecnologias Utilizadas

- **ASP.NET Core** (para criação da API)
- **C#** (linguagem principal de desenvolvimento)
- **Entity Framework Core** (para manipulação de dados e ORM)
- **AutoMapper** (para conversão de entidades e DTOs)
- **JWT Bearer Authentication** (para autenticação)
- **Swagger** (para documentação e teste da API)

## Modelagem de Dados

A modelagem de dados foi realizada utilizando a abordagem **Code First** do **Entity Framework Core**. A estrutura de dados foi definida por meio de classes, sendo que o modelo foi desenhado de acordo com as necessidades da aplicação.

### Entidades

- **Fornecedor**: Representa os fornecedores cadastrados no sistema, contendo informações como `Nome`, `Email`, `Telefone` e uma lista de `Endereços`.

- **EnderecoFornecedor**: Representa os endereços associados a cada fornecedor, contendo informações como `Logradouro`, `Cidade`, `Bairro`, e `CEP`.

As classes são mapeadas para o banco de dados automaticamente, e o relacionamento entre `Fornecedor` e `EnderecoFornecedor` é configurado para garantir que cada fornecedor possa ter vários endereços associados a ele.

## Padrões de Projeto Utilizados
- **1. Repository Pattern**: O padrão Repository foi utilizado para abstrair a lógica de acesso a dados. Foi criada uma classe Repository genérica que lida com as operações CRUD básicas, e as implementações específicas para cada entidade, como FornecedorRepository e EnderecoFornecedorRepository, herdam desta classe base. Isso facilita a reutilização do código e a manutenção, além de garantir que a lógica de acesso aos dados esteja isolada da lógica de negócios.

Exemplo de implementação de um repositório genérico:
```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext context;
    private DbSet<T> dbSet;

    public Repository(DbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    // Outros métodos para atualização, remoção, etc.
}
````
- **2. Service Layer**:
   A lógica de negócios foi organizada em uma camada de serviços, onde classes como FornecedorService e EnderecoFornecedorService encapsulam as regras de negócios. Essas classes fazem a ponte entre os controladores da API e os repositórios. Esse padrão facilita a separação de responsabilidades, tornando o código mais modular e testável.

## Como Rodar o Projeto

### Requisitos

Antes de rodar o projeto, certifique-se de que você possui os seguintes requisitos:

- **.NET Core 6.0** ou superior
- **MySQL 8.0**

### Passos para Rodar o Projeto

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/fornecedor-api.git
   ```
2. **Navegue até a pasta do projeto**:
   ```bash
   cd fornecedor-api
   ```
   
3. **Restaurar as dependências**: Para garantir que todas as dependências sejam baixadas, execute o comando:
    ```bash
   dotnet restore
   ```
4. **Atualize a string de conexão**:
   (caso necessário): Verifique o arquivo appsettings.json para configurar a string de conexão do seu banco de dados. A configuração padrão está para o SQL Server, mas você pode ajustá-la de acordo com o banco de dados que você está utilizando.


5. **Aplicar as migrações (caso ainda não tenha feito)**:
   ```bash
   dotnet ef database update
   ```

6. **Rodar o projeto**:
   ```bash
   dotnet run
   ```
7. **Acessar A API**:
Após o projeto ser iniciado, a API estará disponível em `https://localhost:5001` ou `http://localhost:5000` (dependendo da configuração). Você pode testar os endpoints da API utilizando o Swagger UI em:
 
    [https://localhost:5001/swagger](https://localhost:5001/swagger)


### Problemas Comuns

- Se o banco de dados não for criado corretamente, verifique as configurações de `appsettings.json` e se a string de conexão está apontando para o banco correto.
- Caso o projeto não inicie, execute o comando `dotnet restore` para garantir que todas as dependências foram instaladas corretamente.