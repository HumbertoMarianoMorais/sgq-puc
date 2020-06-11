using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcSgq.Sessao;

namespace WebMvcSgq.Controllers
{
    public class AcessoController : Controller
    {
        // GET: Acesso
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

            return View();
        }

        public ActionResult SemAcesso()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session["Usuario"] = null;

            SessaoUsuario.SessaoUsuarios = null;

            return RedirectToAction("Login", "Login");

        }

    }
}