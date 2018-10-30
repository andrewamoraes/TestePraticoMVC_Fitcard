using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePraticoMVC_Fitcard.Models;

namespace TestePraticoMVC_Fitcard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Melhorar para buscar pelo relacionamento do Entity
        public ActionResult Estabelecimentos()
        {
            using (DatabaseEntities dc = new DatabaseEntities())
            {
                List<Estabelecimento> lst = new List<Estabelecimento>();

                var estabelecimentos = dc.Estabelecimento.OrderByDescending(a => a.estabelecimentoID).ToList();
                foreach (var item in estabelecimentos)
                {
                    Estabelecimento e = new Estabelecimento();
                    e.estabelecimentoID = item.estabelecimentoID;
                    e.CategoriaID = item.CategoriaID;
                    e.labelCategoria = ListarCategoriasID(item.CategoriaID);
                    e.dataCad = item.dataCad;
                    e.razao = item.razao;
                    e.fantasia = item.razao;
                    e.CNPJ = item.razao;
                    e.email = item.email;
                    e.endereco = item.endereco;
                    e.cidade = item.cidade;
                    e.estado = item.estado;
                    e.telefone = item.telefone;
                    e.agencia = item.agencia;
                    e.conta = item.conta;
                    e.status = item.status;
                    e.labelStatus = (item.status == true) ? "Ativo" : "Inativo";
                    lst.Add(e);
                }

                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Salvar(int id = 0)
        {
            using (DatabaseEntities dc = new DatabaseEntities())
            {
                var e = dc.Estabelecimento.Where(x => x.estabelecimentoID == id).FirstOrDefault();
                ListarCategorias();
                return View(e);
            }
        }

        [HttpPost]
        public ActionResult Validar(Estabelecimento estabelecimento)
        {
            bool status = false;
            int erros = 0;

            if (estabelecimento.CategoriaID == 5 && estabelecimento.telefone == null) erros += 0;

            if (erros == 0)
            { status = SalvarRegistro(estabelecimento); }

            return new JsonResult { Data = new { status = status } };
        }

        public bool SalvarRegistro(Estabelecimento estabelecimento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DatabaseEntities dc = new DatabaseEntities())
                    {
                        if (estabelecimento.estabelecimentoID > 0)
                        {
                            //Editar
                            var e = dc.Estabelecimento.Where(x => x.estabelecimentoID == estabelecimento.estabelecimentoID).FirstOrDefault();
                            if (e != null)
                            {
                                e.CategoriaID = estabelecimento.CategoriaID;
                                e.dataCad = estabelecimento.dataCad;
                                e.razao = estabelecimento.razao;
                                e.fantasia = estabelecimento.fantasia;
                                e.CNPJ = estabelecimento.CNPJ;
                                e.email = estabelecimento.email;
                                e.endereco = estabelecimento.endereco;
                                e.cidade = estabelecimento.cidade;
                                e.estado = estabelecimento.estado;
                                e.telefone = estabelecimento.telefone;
                                e.agencia = estabelecimento.agencia;
                                e.conta = estabelecimento.conta;
                                e.status = estabelecimento.status;
                            }
                        }
                        else
                        {
                            //Salvar
                            dc.Estabelecimento.Add(estabelecimento);
                        }
                        dc.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return false;
        }

        [HttpGet]
        public ActionResult Deletar(int id)
        {
            try
            {
                using (DatabaseEntities dc = new DatabaseEntities())
                {
                    ListarCategorias();
                    var e = dc.Estabelecimento.Where(x => x.estabelecimentoID == id).FirstOrDefault();
                    if (e != null)
                    {
                        e.labelCategoria = ListarCategoriasID(e.CategoriaID);
                        if (e.status) { e.labelStatus = "Ativo"; } else { e.labelStatus = "Inativo"; }
                        return View(e);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ActionName("Deletar")]
        public ActionResult DeletarEstabelecimento(int id)
        {
            bool status = false;
            using (DatabaseEntities dc = new DatabaseEntities())
            {
                var e = dc.Estabelecimento.Where(x => x.estabelecimentoID == id).FirstOrDefault();
                if (e != null)
                {
                    dc.Estabelecimento.Remove(e);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        public IList<Categoria> buscaCategorias()
        {
            using (DatabaseEntities dc = new DatabaseEntities())
            {
                var categorias = dc.Categoria.ToList();
                return categorias;
            }
        }

        public ActionResult ListarCategorias()
        {
            ViewBag.Categorias = buscaCategorias().Select(n => new SelectListItem()
            {
                Value = n.categoriaID.ToString(),
                Text = n.nome
            });
            return View();
        }

        public string ListarCategoriasID(int id)
        {
            string retorno = "";
            using (DatabaseEntities dc = new DatabaseEntities())
            {
                var e = dc.Categoria.Where(x => x.categoriaID == id).FirstOrDefault();
                retorno = e.nome;
            }
            return retorno;
        }
    }
}