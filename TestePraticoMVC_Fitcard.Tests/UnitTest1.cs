using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestePraticoMVC_Fitcard.Models;

namespace TestePraticoMVC_Fitcard.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TesteDeGravacaoDeDados()
        {
            var e = new Estabelecimento();
            e.CategoriaID = 1;
            e.dataCad = DateTime.Parse("10/10/2018");
            e.razao = "Teste Estabelecimento";
            e.fantasia = "Teste Estabelecimento";
            e.CNPJ = "55.235.935/0001-49";
            e.email = "teste@teste.com";
            e.endereco = "Rua 1";
            e.cidade = "Campinas";
            e.estado = "SP";
            e.telefone = "(19) 1919-1919";
            e.agencia = "123-4";
            e.conta = "56.789.0";
            e.status = bool.Parse("True");

            using (DatabaseEntities dc = new DatabaseEntities())
            {
                dc.Estabelecimento.Add(e);
                dc.SaveChanges();
            }
        }

        [TestMethod]
        public void TesteDeBuscaDeCategoria()
        {
            using (DatabaseEntities dc = new DatabaseEntities())
            {
                var categorias = dc.Categoria.ToList();
                if (categorias.Count > 0)
                {
                    throw new Exception("OK");
                }
                else
                {
                    throw new Exception("Sem carga");
                }
            }
        }
    }
}

