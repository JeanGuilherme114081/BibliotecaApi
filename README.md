# Sistema de Gerenciamento de Biblioteca Universitária

## Integrantes

| Nome | RGM |

| Jean Guilherme | 114.081 |
| Thiago Mendes | 114.093 |

**Curso:** Engenharia de Software  
**Disciplina:** Programação Orientada a Objetos  
**Atividade:** Prática 02 — P2

## Sobre o projeto

Projeto desenvolvido para a disciplina de Programação Orientada a Objetos.

O sistema consiste em uma Web API desenvolvida em C# com ASP.NET Core, responsável pelo gerenciamento de materiais de uma biblioteca universitária.

A aplicação permite cadastrar, consultar, atualizar e excluir livros e revistas, utilizando conceitos de Programação Orientada a Objetos.

## Tecnologias utilizadas

- C#
- .NET 10
- ASP.NET Core Web API
- Swagger (Swashbuckle)
- Visual Studio Code
- Git e GitHub

## Conceitos implementados

**Herança e abstração:** a classe abstrata `MaterialBiblioteca` reúne as propriedades comuns aos livros e revistas.

**Polimorfismo:** as classes `Livro` e `Revista` implementam seus próprios métodos de descrição e cálculo do prazo de empréstimo.

**Interfaces:** a interface `IEmprestimo` define a capacidade de realizar empréstimos, sendo implementada pela classe `Livro`.

**Repository Pattern:** o acesso aos materiais é organizado por meio de `IMaterialRepository` e `MaterialRepository`.

**Singleton:** o repositório é registrado como Singleton para manter os dados durante a execução da aplicação.

**Dictionary:** os materiais são armazenados em memória, utilizando IDs gerados automaticamente.

## Como executar o projeto

**Pré-requisitos:**
- .NET SDK 10 instalado
- Visual Studio Code ou outra IDE compatível

**Passos:**

1. Clone o repositório:

   `git clone https://github.com/JeanGuilherme114081/BibliotecaApi.git`

2. Entre na pasta:

   `cd BibliotecaApi`

3. Restaure as dependências:

   `dotnet restore`

4. Execute a aplicação:

   `dotnet run`

5. Abra o Swagger no navegador:

   `http://localhost:PORTA/swagger`

Substitua `PORTA` pela porta exibida no terminal.

## Principais endpoints

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/Materiais` | Lista todos os materiais |
| GET | `/api/Materiais/{id}` | Busca um material pelo ID |
| POST | `/api/Materiais/livro` | Cadastra um livro |
| POST | `/api/Materiais/revista` | Cadastra uma revista |
| PUT | `/api/Materiais/livro/{id}` | Atualiza um livro |
| PUT | `/api/Materiais/revista/{id}` | Atualiza uma revista |
| DELETE | `/api/Materiais/{id}` | Exclui um material |
| GET | `/api/Materiais/{id}/descricao` | Consulta descrição e prazo |
| POST | `/api/Materiais/{id}/emprestar` | Verifica a capacidade de empréstimo |

## Exemplos de testes no Swagger

### Cadastro de livro

**POST `/api/Materiais/livro`**

Exemplo de JSON:

`{"titulo":"O Programador Pragmático","anoPublicacao":1999,"autor":"Andrew Hunt","paginas":352}`

Resultado esperado: `201 Created`.

### Cadastro de revista

**POST `/api/Materiais/revista`**

Exemplo de JSON:

`{"titulo":"Revista de Tecnologia","anoPublicacao":2026,"edicao":20,"mesPublicacao":11}`

Resultado esperado: `201 Created`.

### Consulta de material inexistente

**GET `/api/Materiais/999`**

Resultado esperado: `404 Not Found`.

### Validação de livro inválido

Cadastrar um livro com `paginas` igual a `0`.

Resultado esperado: `400 Bad Request`.

### Teste de polimorfismo

Consultar a descrição de um livro e de uma revista.

Resultados esperados:
- Livro: prazo de 14 dias.
- Revista: prazo de 7 dias.

### Teste da interface

Realizar uma tentativa de empréstimo:

- Livro: `200 OK`.
- Revista: `400 Bad Request`.

## Testes e evidências

Os testes funcionais serão executados pelo Swagger, conforme o enunciado da atividade.

**Status:** testes obrigatórios e capturas de tela pendentes de confirmação.

As evidências serão adicionadas ao repositório após a execução dos testes.

## Observações

O sistema utiliza armazenamento em memória, sem banco de dados. Ao reiniciar a aplicação, os materiais cadastrados durante a execução não são preservados.

A aplicação não possui frontend, autenticação ou sistema de login, conforme os requisitos da atividade.