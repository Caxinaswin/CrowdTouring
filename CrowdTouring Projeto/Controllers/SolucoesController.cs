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
            if(solucao.User.Id != User.Identity.GetUserId())
            {
                solucao.NumeroVisualizacoes++;
                db.SaveChanges();
            }

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

            string extensao = Path.GetExtension(file.FileName);
            if (file == null)
            {
                ModelState.AddModelError("ErroFicheiro2", "Tem que Submeter pelo menos um ficheiro");
            }

            if (file != null)
            {
                int indexOf = file.ContentType.IndexOf("zip");
                if (extensao != ".zip")
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

        public ActionResult Votacao(int id, int avaliacao)
        {
            var solucao = db.Solucoes.Where(d => d.SolucaoId == id).FirstOrDefault();
            solucao.NumeroVotos = solucao.NumeroVotos + avaliacao;
            Voto voto = new Voto();
            voto.solucao = solucao;
            voto.userId = User.Identity.GetUserId();
            db.Votos.Add(voto);
            db.SaveChanges();
            solucao.Votos.Add(voto);
            db.SaveChanges();
            return RedirectToAction("Details", "Desafios",new { @id = solucao.DesafioId});
        }

        // GET: Solucoes/Edit/5
        public ActionResult Edit(int id)
        {
            var solucao = db.Solucoes.Where(s => s.SolucaoId == id).First();
            var anexo = db.Anexos.Where(s => s.SolucaoId == id).FirstOrDefault();
            SolucaoDesafio solucaoDesafio = new SolucaoDesafio();
            solucaoDesafio.NomeSolucao = solucao.SolucaoTitulo;
            solucaoDesafio.DescricaoSolucao = solucao.Descricao;
            solucaoDesafio.FileName = anexo.NomeFicheiro;
            solucaoDesafio.IdSolucao = id;
            solucaoDesafio.IdDesafio = solucao.DesafioId;
            return View(solucaoDesafio);
        }

        // POST: Solucoes/Edit/5
        [HttpPost]
        public ActionResult Edit(SolucaoDesafio solucaoDesafio, HttpPostedFileBase file)
        {
            var solucao = db.Solucoes.Where(s => s.SolucaoId == solucaoDesafio.IdSolucao).First();
            var anexo = db.Anexos.Where(s => s.SolucaoId == solucaoDesafio.IdSolucao).First();
            try
            {
                solucao.SolucaoTitulo = solucaoDesafio.NomeSolucao;
                solucao.Descricao = solucaoDesafio.DescricaoSolucao;              
                if(file.ContentLength > 0)
                {
                    anexo.Caminho = solucaoDesafio.FilePath;
                    anexo.SolucaoId = solucaoDesafio.IdSolucao;
                    anexo.NomeFicheiro = file.FileName;
                }
                db.SaveChanges();
                return RedirectToAction("Details","Desafios", new { id = solucaoDesafio.IdDesafio });
            }
            catch
            {
                return RedirectToAction("Details", "Desafios", new { id = solucaoDesafio.IdDesafio });
            }
        }

        // GET: Solucoes/Delete/5
        public ActionResult Eliminar(int id,int desafio)
        {
            Solucao solucao = db.Solucoes.Find(id);
            var desafioQ = db.Votos.RemoveRange(db.Votos.Where(d => d.solucao.SolucaoId == id));
            db.SaveChanges();
            db.Solucoes.Remove(solucao);
            db.SaveChanges();
            return RedirectToAction("Details", "Desafios", new { id = solucao.DesafioId });
        }

        public ActionResult EstrelasAvaliacao(int estrela,int id)
        {
            var solucao = db.Solucoes.Where(m => m.SolucaoId == id).FirstOrDefault();
            var userId = User.Identity.GetUserId();
            var estrelasDados = db.Estrelas.Where(m => m.User == userId).FirstOrDefault();
            Estrela Estrela = new Estrela();
            Estrela.solucao = solucao;
            Estrela.User = User.Identity.GetUserId();
            Estrela.EstrelaValor = estrela;

            if (estrelasDados == null)
            {
                db.Estrelas.Add(Estrela);
                db.SaveChanges();
                solucao.Estrelas.Add(Estrela);
                db.SaveChanges();
            }
            else
            {
                estrelasDados.EstrelaValor = estrela;
                Estrela novaEstrela = solucao.Estrelas.Where(m => m.User == userId).First();
                novaEstrela.EstrelaValor = estrela;
                db.SaveChanges();
               
            }

            return RedirectToAction("Details", "Desafios", new { @id = solucao.DesafioId });
        }

        public ActionResult Vencedor (int id,int id2)
        {
            var desafio = db.Desafios.Where(d => d.DesafioId == id2).First();
            desafio.IdSolucaoVencedora = id;
            db.SaveChanges();
            return RedirectToAction("Index", "desafios");
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
