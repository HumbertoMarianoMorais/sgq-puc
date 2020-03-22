using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcSgq.Models;
using WebMvcSgq.Models.Interface;
using WebMvcSgq.Sessao;

namespace WebMvcSgq.Controllers
{
    public class AtividadeDiariaController : Controller
    {
        private IAtividadeDiariaRepositorio rep = null;
        public AtividadeDiariaController()
        {
            this.rep = new AtividadeDiariaRepositorio();
        }

        public ActionResult Login()
        {
            Session["Usuario"] = null;

            SessaoUsuario.SessaoUsuarios = null;

            return RedirectToAction("Login", "Login");

            return View();
        }

        // GET: AtividadeDiaria
        public ActionResult Index()
        {
            Acessos acesso = new Acessos();

            if (SessaoUsuario.VerificarLogin())
            {
                Session["Usuario"] = null;

                return RedirectToAction("Login", "Login");
            }

            if (!acesso.ConsultaAcesso(HttpContext.Request.Path))
            {
                return RedirectToAction("SemAcesso", "Acesso");
            }

            var listaAtivDiaria = from ativDiaria in rep.GetAtividadeDiaria()
                                 select ativDiaria;
            return View(listaAtivDiaria);
        }


        public ActionResult AdicionaAtividadeDiaria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionaAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria)
        {
            rep.AdicionaAtividadeDiaria(ativDiaria);
            return RedirectToAction("Index");
        }

        public ActionResult EditarAtividadeDiaria(int IdAtividade = 0)
        {
            Tbl_Atividade_Diaria ativDiaria = rep.GetAtividadePorID(IdAtividade);

            if (ativDiaria == null)
                return HttpNotFound();

            return View(ativDiaria);
        }

        [HttpPost]
        public ActionResult EditarAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria)
        {
            rep.EditarAtividadeDiaria(ativDiaria);
            return RedirectToAction("Index");
        }

        public ActionResult DeletarAtividadeDiaria(int IdAtividade = 0)
        {
            Tbl_Atividade_Diaria ativDiaria = rep.GetAtividadePorID(IdAtividade);

            if (ativDiaria == null)
                return HttpNotFound();

            return View(ativDiaria);
        }

        [HttpPost]
        public ActionResult DeletarAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria)
        {
            rep.DeletarAtividadeDiaria(ativDiaria.IdAtividade);
            return RedirectToAction("Index");
        }

        public ActionResult Detalhes(int IdAtividade = 0)
        {
            Tbl_Atividade_Diaria ativDiaria = rep.Detalhes(IdAtividade);
            return View(ativDiaria);
        }
    }
}