# Projeto ASP.NET Core - Empréstimo de Livros

## Visão Geral

Este projeto é uma aplicação web desenvolvida em ASP.NET Core com o objetivo de gerenciar empréstimos de livros. Ele permite o cadastro de usuários, livros e controle dos empréstimos realizados entre os usuários.

## Funcionalidades

- **Gerenciamento de Usuários**: Cadastro, edição, listagem e exclusão de usuários.
- **Gerenciamento de Livros**: Cadastro, edição, listagem e exclusão de livros.
- **Controle de Empréstimos**: Registro de novos empréstimos associando usuários e livros, garantindo que o fornecedor seja diferente do recebedor e que o livro pertença ao fornecedor.
- **Regras de Negócio**:
  - Não permitir a exclusão de um usuário que possua livros cadastrados.
  - Não permitir a exclusão de um usuário envolvido em empréstimos, seja como fornecedor ou recebedor.
  - Não permitir a exclusão de um livro que esteja associado a um empréstimo ativo.

## Tecnologias Utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap (para interface responsiva)

