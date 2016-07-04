using CrowdTouring_Projeto.Models;
using CrowdTouring_Projeto.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrowdTouring_Projeto.Controllers
{
    public class SolucoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Solucoes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Solucoes/Details/5
        public ActionResult Details(int id)
        {
            var solucao = db.Solucoes.Where(a => a.SolucaoId == id).First();
            var anexo = db.Anexos.Where(a => a.SolucaoId == id).First();
            SolucaoDesafio Solucao = new SolucaoDesafio();
            Solucao.NomeSolucao = solucao.SolucaoTitulo;
            Solucao.DescricaoSolucao = solucao.Descricao;
            Solucao.FileId = anexo.AnexoId;
            Solucao.FileName = anexo.NomeFicheiro;
            Solucao.FilePath = anexo.Caminho;

            return View(Solucao);
        }

        // GET: Solucoes/Create
        public ActionResult Create(int DesafioId)
        {
            var desafio = db.Desafios.Where(d => d.DesafioId == DesafioId).First();
            var SolucaoDesafio = new SolucaoDesafio();
            SolucaoDesafio.IdDesafio = desafio.DesafioId;
            SolucaoDesafio.NomeDesafio = desafio.TipoTrabalho;
            return View(SolucaoDesafio);
        }

        // POST: Solucoes/Create
        [HttpPost]
        public ActionResult Create(SolucaoDesafio SolucaoDesafio,HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("ErroFicheiro2", "Tem que Submeter pelo menos um ficheiro");
            }

            if (file != null)
            {
                int indexOf = file.ContentType.IndexOf("zip");
                if (indexOf == -1)
                {
                    ModelState.AddModelError("Zip2", "Compacte os ficheiros e envie em formato .Zip");
                }
            }

            if (ModelState.IsValid)
            {
                var Solucao = new Solucao();
                Solucao.DesafioId = SolucaoDesafio.IdDesafio;
                Solucao.Descricao = SolucaoDesafio.DescricaoSolucao;
                Solucao.SolucaoTitulo = SolucaoDesafio.NomeSolucao;
                Solucao.ApplicationUserId = User.Identity.GetUserId();
                Solucao.DataCriacao = DateTime.Now;
                db.Solucoes.Add(Solucao);

                db.SaveChanges();

                #pragma warning disable CS0162 // Unreachable code detected
                if (file.ContentLength > 0)
                {
                    Anexo anexo = new Anexo();

                    string filePath = Path.Combine(HttpContext.Server.MapPath("~/Anexos/"),
                                                   Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);
                    anexo.Caminho = filePath;
                    anexo.SolucaoId = Solucao.SolucaoId;
                    anexo.NomeFicheiro = file.FileName;
                    db.Anexos.Add(anexo);

                    db.SaveChanges();

                    return RedirectToAction("Details", "Desafios", new { id = SolucaoDesafio.IdDesafio });
                }
            }
            return View(SolucaoDesafio);
        }

        // GET: Solucoes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Solucoes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Solucoes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Solucoes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
