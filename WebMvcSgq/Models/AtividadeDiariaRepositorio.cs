using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvcSgq.Models.Classe;
using WebMvcSgq.Models.Interface;

namespace WebMvcSgq.Models
{
    public class AtividadeDiariaRepositorio : IAtividadeDiariaRepositorio
    {

        db_sgqEntities db = new db_sgqEntities();
        private IAtividadeDiariaRepositorio repAtiv = null;
        private IProcessoRepositorio repProcesso = null;

        void IAtividadeDiariaRepositorio.AdicionaAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria)
        {
            try
            {
                ativDiaria.Dt_Cadastro = DateTime.Now;
                db.Tbl_Atividade_Diaria.Add(ativDiaria);
                db.SaveChanges();

                repAtiv = new AtividadeDiariaRepositorio();
                repProcesso = new ProcessoRepositorio();

                ativDiaria = db.Tbl_Atividade_Diaria.Where(p => p.IdAtividade == ativDiaria.IdAtividade).FirstOrDefault();

                tbl_Processo processo = repProcesso.GetProcessoPorID(ativDiaria.IdProcesso.Value);

                foreach (var item in processo.tbl_etapa)
                {
                    tbl_atividades atv = null;
                    atv = ativDiaria.tbl_atividades.Where(p => p.IdAtividadeDiaria == ativDiaria.IdAtividade && p.IdEtapa == item.IdEtapa).FirstOrDefault();

                    if (atv == null)
                    {
                        atv = new tbl_atividades();
                        atv.IdEtapa = item.IdEtapa;
                        atv.IdAtividadeDiaria = ativDiaria.IdAtividade;
                        atv.DsSelecionado = 0;
                        ativDiaria.tbl_atividades.Add(atv);
                    }
                    else
                    {
                        ativDiaria.tbl_atividades.Where(p => p.IdAtividadeDiaria == ativDiaria.IdAtividade && p.IdEtapa == item.IdEtapa).FirstOrDefault().DsSelecionado = 1;
                    }
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((db == null))
                    db.Dispose();
            }
        }
        void IAtividadeDiariaRepositorio.EditarAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria, List<tbl_atividades> listaAtividade)
        {
            try
            {
                var novaAtividade = db.Tbl_Atividade_Diaria.Where(x => x.IdAtividade == ativDiaria.IdAtividade).FirstOrDefault();
                novaAtividade.Descricao = ativDiaria.Descricao;
                novaAtividade.Dt_Alteracao = DateTime.Now;

                foreach (var item in listaAtividade)
                {
                    tbl_atividades atv = null;
                    atv = novaAtividade.tbl_atividades.Where(p => p.IdAtividadeDiaria == novaAtividade.IdAtividade && p.IdEtapa == item.IdEtapa).FirstOrDefault();

                    if (atv == null)
                    {
                        atv = new tbl_atividades();
                        atv.IdEtapa = item.IdEtapa;
                        atv.IdAtividadeDiaria = novaAtividade.IdAtividade;
                        atv.DsSelecionado = 0;
                        novaAtividade.tbl_atividades.Add(atv);
                    }
                    else
                    {
                        novaAtividade.tbl_atividades.Where(p => p.IdAtividadeDiaria == novaAtividade.IdAtividade && p.IdEtapa == item.IdEtapa).FirstOrDefault().DsSelecionado = 1;
                    }
                }

                db.SaveChanges();
                novaAtividade = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }

        void IAtividadeDiariaRepositorio.DeletarAtividadeDiaria(long idAtivDiaria)
        {
            try
            {
                Tbl_Atividade_Diaria _atividadeDiaria = db.Tbl_Atividade_Diaria.SingleOrDefault(x => x.IdAtividade == idAtivDiaria);
                db.Tbl_Atividade_Diaria.Remove(_atividadeDiaria);
                db.SaveChanges();
                _atividadeDiaria = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }

        Tbl_Atividade_Diaria IAtividadeDiariaRepositorio.Detalhes(int idAtivDiaria)
        {
            try
            {
                Tbl_Atividade_Diaria obj = new Tbl_Atividade_Diaria();
                obj = db.Tbl_Atividade_Diaria.Include("tbl_Processo").Include("tbl_Atividades").SingleOrDefault(s => s.IdAtividade == idAtivDiaria);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }

        IEnumerable<Tbl_Atividade_Diaria> IAtividadeDiariaRepositorio.GetAtividadeDiaria()
        {
            try
            {
                IEnumerable<Tbl_Atividade_Diaria> lista;
                lista = db.Tbl_Atividade_Diaria.Include("tbl_Processo").ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }

        Tbl_Atividade_Diaria IAtividadeDiariaRepositorio.GetAtividadePorID(long idAtivDiaria)
        {
            try
            {
                return db.Tbl_Atividade_Diaria.Include("tbl_Processo").Include("tbl_Atividades").SingleOrDefault(x => x.IdAtividade == idAtivDiaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }


        #region Atividade Diaria Repositorio Novo

        AtiviModelView IAtividadeDiariaRepositorio.GetAtividadeDiariaPorID(long idAtivDiaria)
        {
            AtiviModelView amv = new AtiviModelView();
            try
            {

                Tbl_Atividade_Diaria tbAd = db.Tbl_Atividade_Diaria.Include("tbl_Processo").Include("tbl_Atividades").SingleOrDefault(x => x.IdAtividade == idAtivDiaria);
                AtividadeDiariaClass adc = new AtividadeDiariaClass();
                adc.Descricao = tbAd.Descricao;
                adc.NomeProcesso = tbAd.tbl_Processo.Nome;
                adc.Dt_Alteracao = tbAd.Dt_Alteracao;
                adc.Dt_Cadastro = tbAd.Dt_Cadastro;
                adc.IdAtividade = tbAd.IdAtividade;
                adc.IdProcesso = tbAd.IdProcesso.Value;

                List<AtividadeClass> listac  = new List<AtividadeClass>();
                
                foreach (var ativid in tbAd.tbl_atividades.ToList())
                {
                    AtividadeClass acv = new AtividadeClass();
                    acv.IdAtividade = ativid.IdAtividade;
                    acv.IdAtividadeDiaria = ativid.IdAtividadeDiaria.Value;
                    acv.IdEtapa = ativid.IdEtapa.Value;
                    acv.DsEtapa = ativid.tbl_etapa.Descricao;
                    acv.DsSelecionado = ativid.DsSelecionado.Value;
                    listac.Add(acv);
                }

                amv.atividadeDiaCla = adc;
                amv.atividadeCla = listac;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }

            return amv;
        }

        void IAtividadeDiariaRepositorio.EditarAtividadeDiaria(AtiviModelView amv)
        {
            try
            {
                Tbl_Atividade_Diaria ad = db.Tbl_Atividade_Diaria.Where(x => x.IdAtividade == amv.atividadeDiaCla.IdAtividade).FirstOrDefault();
                ad.Descricao = amv.atividadeDiaCla.Descricao;
                ad.Dt_Alteracao = amv.atividadeDiaCla.Dt_Alteracao;

                foreach (var item in amv.atividadeCla)
                {
                    tbl_atividades atv = null;
                    atv = ad.tbl_atividades.Where(p => p.IdAtividadeDiaria == ad.IdAtividade && p.IdEtapa == item.IdEtapa).FirstOrDefault();

                    if (atv == null)
                    {
                        atv = new tbl_atividades();
                        atv.IdEtapa = item.IdEtapa;
                        atv.IdAtividadeDiaria = ad.IdAtividade;
                        atv.DsSelecionado = item.DsSelecionado;
                        ad.tbl_atividades.Add(atv);
                    }
                    else
                    {
                        ad.tbl_atividades.Where(p => p.IdAtividadeDiaria == ad.IdAtividade && p.IdEtapa == item.IdEtapa).FirstOrDefault().DsSelecionado = item.DsSelecionado;
                    }
                }

                db.SaveChanges();
                ad = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }

        #endregion

    }
}