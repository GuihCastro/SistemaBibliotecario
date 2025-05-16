using SistemaBibliotecario.BLL;
using SistemaBibliotecario.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SistemaBibliotecario.Testes
{
    [TestClass]
    public class AlunoIntegrationTestes
    {
        [TestMethod]
        public void FluxoCompleto_CRUD_DeveFuncionar()
        {
            Aluno aluno = new Aluno()
            {
                RA = 123456,
                Nome = "Teste de Integra��o",
                Email = "email_integracao@teste.com",
                Telefone = "11987654321",
                DataNascimento = new DateTime(1992, 03, 20)
            };

            try
            {
                // Inserir
                AlunoBLL.Inserir(aluno);
                Aluno alunoInserido = AlunoBLL.BuscarPorRA(aluno.RA); // Buscar
                Assert.IsNotNull(alunoInserido, "Aluno n�o encontrado ap�s inser��o!");

                // Atualizar
                alunoInserido.Nome = "Teste de Integra��o Atualizado";
                AlunoBLL.Atualizar(alunoInserido);
                Aluno alunoAtualizado = AlunoBLL.BuscarPorRA(aluno.RA);
                Assert.AreEqual("Teste de Integra��o Atualizado", alunoAtualizado.Nome, "Nome do aluno n�o foi atualizado corretamente!");

                // Excluir
                AlunoBLL.Excluir(aluno.RA);
                Aluno alunoExcluido = AlunoBLL.BuscarPorRA(aluno.RA); // Deve falhar
                Assert.IsNull(alunoExcluido, "Aluno ainda encontrado ap�s exclus�o!");
            }
            finally
            {
                // Limpeza final, caso o teste falhe antes da exclus�o
                try { AlunoBLL.Excluir(aluno.RA); }
                catch (Exception) { } // Ignorar exce��o se o aluno j� foi exclu�do
            }
        }
    }
}

