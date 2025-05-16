using SistemaBibliotecario.BLL;
using SistemaBibliotecario.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SistemaBibliotecario.Testes
{
    [TestClass]
    public class AlunoBLLTestes
    {
        private Aluno _alunoValido;

        [TestInitialize]
        public void Setup()
        {
            // Configurando um aluno v�lido para testes
            _alunoValido = new Aluno()
            {
                RA = 123456,
                Nome = "Guilherme Henrique de Castro",
                Email = "guilherme.henricastro@gmail.com",
                Telefone = "11965757486",
                DataNascimento = new DateTime(1992, 08, 10)
            };
        }

        [TestMethod]
        public void Inserir_AlunoValido_DeveInserirComSucesso()
        {
            // N�o deve lan�ar exce��o
            try
            {
                AlunoBLL.Inserir(_alunoValido);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail("N�o deveria lan�ar exce��o: " + ex.Message);
            }
            finally
            {
                // Limpa o banco ap�s o teste
                AlunoBLL.Excluir(_alunoValido.RA);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Inserir_RAInvalido_DeveLancarExcecao()
        {
            Aluno alunoInvalido = new Aluno()
            {
                RA = -1, // RA inv�lido
                Nome = "Nome V�lido",
                Email = "email.valido@gmail.com",
                Telefone = "1112345678",
                DataNascimento = new DateTime(2000, 01, 01)
            };

            // Deve lan�ar exce��o
            AlunoBLL.Inserir(alunoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Inserir_RADuplicado_DeveLancarExcecao()
        {
            // Insere o aluno v�lido
            AlunoBLL.Inserir(_alunoValido);
            try
            {
                // Tenta inserir o mesmo aluno novamente
                AlunoBLL.Inserir(_alunoValido);
            }
            finally
            {
                // Limpa o banco ap�s o teste
                AlunoBLL.Excluir(_alunoValido.RA);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Inserir_NomeInvalido_DeveLancarExcecao()
        {
            Aluno alunoInvalido = new Aluno()
            {
                RA = 123,
                Nome = "",
                Email = "email.valido@gmail.com",
                Telefone = "1112345678",
                DataNascimento = new DateTime(2000, 01, 01)
            };

            // Deve lan�ar exce��o
            AlunoBLL.Inserir(alunoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Inserir_EmailInvalido_DeveLancarExcecao()
        {
            Aluno alunoInvalido = new Aluno()
            {
                RA = 123,
                Nome = "Nome V�lido",
                Email = "email.invalido.gmail.com",
                Telefone = "1112345678",
                DataNascimento = new DateTime(2000, 01, 01)
            };

            // Deve lan�ar exce��o
            AlunoBLL.Inserir(alunoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Inserir_TelefoneInvalido_DeveLancarExcecao()
        {
            Aluno alunoInvalido = new Aluno()
            {
                RA = 123,
                Nome = "Nome V�lido",
                Email = "email.valido@gmail.com",
                Telefone = "",
                DataNascimento = new DateTime(2000, 01, 01)
            };

            // Deve lan�ar exce��o
            AlunoBLL.Inserir(alunoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Inserir_NascimentoInvalido_DeveLancarExcecao()
        {
            Aluno alunoInvalido = new Aluno()
            {
                RA = 123,
                Nome = "Nome V�lido",
                Email = "email.valido@gmail.com",
                Telefone = "1112345678",
                DataNascimento = new DateTime(2020, 01, 01)
            };
            // Deve lan�ar exce��o
            AlunoBLL.Inserir(alunoInvalido);
        }

        [TestMethod]
        public void Atualizar_AlunoValido_DeveAtualizarComSucesso()
        {
            // Insere o aluno v�lido
            AlunoBLL.Inserir(_alunoValido);
            try
            {
                // Atualiza o nome do aluno
                _alunoValido.Nome = "Nome Atualizado V�lido";
                AlunoBLL.Atualizar(_alunoValido);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail("N�o deveria lan�ar exce��o: " + ex.Message);
            }
            finally
            {
                // Limpa o banco ap�s o teste
                AlunoBLL.Excluir(_alunoValido.RA);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Atualizar_AlunoInvalido_DeveLancarExcecao()
        {
            AlunoBLL.Atualizar(_alunoValido); // Aluno n�o existe no banco
        }

        [TestMethod]
        public void Excluir_AlunoValido_DeveExcluirComSucesso()
        {
            // Insere o aluno v�lido
            AlunoBLL.Inserir(_alunoValido);
            try
            {
                // Exclui o aluno
                AlunoBLL.Excluir(_alunoValido.RA);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail("N�o deveria lan�ar exce��o: " + ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Excluir_AlunoInvalido_DeveLancarExcecao()
        {
            AlunoBLL.Excluir(_alunoValido.RA); // Aluno n�o existe no banco
        }


    }
}
