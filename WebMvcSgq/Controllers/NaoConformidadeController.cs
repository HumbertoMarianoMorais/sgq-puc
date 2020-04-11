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
    public class NaoConformidadeController : Controller
    {

        private INaoConformidadeRepositorio rep = null;
        private IProcessoRepositorio repProcesso = null;
        private IAtividadeDiariaRepositorio repAtiviDiaRep = null;

        public NaoConformidadeController()
        {
            this.rep = new NaoConformidadeRepositorio();
            this.repProcesso = new ProcessoRepositorio();
            this.repAtiviDiaRep = new AtividadeDiariaRepositorio();
        }

        // GET: NaoConformidade
        public ActionResult Index()
        {
            Acessos acesso = new Acessos();

            if (SessaoUsuario.VerificarLogin())
            {
                Session["Usuario"] = null;

                return RedirectToAction("Login", "Login");
            }

            string caminho = HttpContext.Request.Path;
            if (caminho.ToUpper().Contains(acesso.NAOCONFORMIDADE_INDEX))
                caminho = acesso.NAOCONFORMIDADE_INDEX;

            if (!acesso.ConsultaAcesso(caminho))
            {
                return RedirectToAction("SemAcesso", "Acesso");
            }

            IEnumerable<tbl_NaoConformidade> listaNaoConformidade = null;


            listaNaoConformidade = from naoConf in rep.GetNaoConformidade()
                                   select naoConf;

            return View(listaNaoConformidade);
        }

        public ActionResult NovaNaoConformidade()
        {
            tbl_NaoConformidade naoConf = new tbl_NaoConformidade();

            CarregarAtividade();
            CarregarProcesso();

            return View(naoConf);
        }

        [HttpPost]
        public ActionResult NovaNaoConformidade(tbl_NaoConformidade naoConformidade)
        {
            rep.AdicionaNaoConformidade(naoConformidade);
            return RedirectToAction("Index");
        }

        public ActionResult CarregarProcesso()
        {
            tbl_NaoConformidade naoConf = new tbl_NaoConformidade();

            ViewBag.IdProcesso = new SelectList
                (
                    repProcesso.GetProcessos(),
                    "IdProcesso",
                    "Nome"
                );

            return View(naoConf);
        }

        public ActionResult CarregarAtividade()
        {

            tbl_NaoConformidade naoConf = new tbl_NaoConformidade();

            ViewBag.IdAtividadeDiaria = new SelectList
                (
                    repAtiviDiaRep.GetAtividadeDiaria(),
                    "IdAtividadeDiaria",
                    "Descricao"
                );

            return View(naoConf);

        }

        public ActionResult EditarNaoConformidade(int IdNaoConformidade = 0)
        {
            CarregarProcesso();
            CarregarAtividade();

            tbl_NaoConformidade naoConf = new tbl_NaoConformidade();
            naoConf = rep.GetNaoConformidadePorID(IdNaoConformidade);

            return View(naoConf);

        }

        [HttpPost]
        public ActionResult EditarNaoConformidade(tbl_NaoConformidade naoConformidade)
        {
            rep.AtualizaNaoConformidade(naoConformidade);
            return RedirectToAction("Index");

        }

        public ActionResult DeletarNaoConformidade(int IdNaoConformidade = 0)
        {
            tbl_NaoConformidade naoConf = rep.GetNaoConformidadePorID(IdNaoConformidade);

            if (naoConf == null)
                return HttpNotFound();

            return View(naoConf);
        }

        [HttpPost]
        public ActionResult DeletarNaoConformidade(tbl_NaoConformidade naoConf)
        {
            rep.DeletaNaoConformidade(naoConf.IdNaoConformidade);
            return RedirectToAction("Index");
        }

        public ActionResult Detalhes(int IdNaoConformidade = 0)
        {
            tbl_NaoConformidade naoConf = rep.Detalhes(IdNaoConformidade);
            return View(naoConf);
        }

        public ActionResult Login()
        {
            Session["Usuario"] = null;

            SessaoUsuario.SessaoUsuarios = null;

            return RedirectToAction("Login", "Login");

        }


    }
}