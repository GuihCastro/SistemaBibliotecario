# 📚 Sistema Bibliotecário

![1747828327479](image/README/1747828327479.png)

Sistema de CRUD para gerenciamento de uma bibliotéca escolar feito como teste para estágio na Qbit Informática.

Desenvolvido em C# (.NET) com Windows Forms e SQL Server Express (LocalDB).

## Utilizando o sistema

### Pré-requisitos

- Windows 10 ou 11;
- .NET Framework 4.8;
- SQL Server LocalDB

### Instalação passo a passo

#### Método 1: Instalador Automático (recomendado)

1. Acesse a [página de release](https://github.com/GuihCastro/SistemaBibliotecario/releases/tag/CRUD);
2. Na versão mais recente, baixe o arquivo `SistemaBibliotecarioInstaller.msi`;
3. Execute o arquivo .msi como administrador (Clique direito > "Executar como administrador");
4. Siga os passos do assistente de instalação.

#### Método 2: Execução Direta (para desenvolvedores)

```
bash
# Clone o repositório
git clone https://github.com/GuihCastro/SistemaBibliotecario.git

# Abra a solução no Visual Studio
cd SistemaBibliotecario
start SistemaBibliotecario.sln

# Compile e execute (F5)
```

### Como usar

#### Cadastro inicial

1. Cadastre Alunos:
   1. Acesse: Cadastros > Alunos;
   2. Preencha RA, Nome, E-mail, Telefone e Data de Nascimento.
2. Cadastre Livros:
   1. Acesse: Cadastros > Livros;
   2. Informe Código, Título, Autor, Gênero (opcional) e Editora (opcional).
3. Registre Empréstimos:
   1. Acesse: Cadastros > Empréstimos;
   2. Informe RA do Aluno e Código do Livro.

#### Funcionalidades principais

- Controle completo de acervo bibliotecário;
- Gerenciamento de usuários (alunos);
- Registro de empréstimos e devoluções;
- Relatório de livros disponíveis e emprestados;
- Notificação de atrasos na devolução.

## Estrutura do projeto

```
SistemaBibliotecario/
├── App_Data/  # Arquivos de Banco de Dados
├── DAL/            # Camada de acesso a dados
├── BLL/            # Lógica de negócios
├── Models/         # Entidades do sistema
├── UI/             # Interface do usuário
├── App.config      # Configurações do aplicativo
├── ScriptsCriacao.sql  # Script SQL para criação do banco
└── Program.cs  # Código principal de entrada no programa
```

## Como contribuir

1. Faça um fork do projeto;
2. Crie uma branch (`git checkout -b feature/nova-funcionalidade`);
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`);
4. Push para a branch (`git push origin feature/nova-funcionalidade`);
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a MIT License.

## Contato

Guilherme Castro - [guilherme.henricastro@gmail.com](https://mailto:guilherme.henricastro@gmail.com/)

[![https://github.com/GuihCastro](https://img.shields.io/badge/GitHub-Reposit%C3%B3rio-blue)](https://github.com/GuihCastro/SistemaBibliotecario)
