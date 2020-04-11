using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvcSgq.Models.Classe;
using WebMvcSgq.Models.Interface;

namespace WebMvcSgq.Models
{
    public class NaoConformidadeRepositorio : INaoConformidadeRepositorio
    {
        db_sgqEntities db = new db_sgqEntities();

        void INaoConformidadeRepositorio.AdicionaNaoConformidade(tbl_NaoConformidade naoConformidade)
        {
            try
            {
                naoConformidade.Dt_Cadastro = DateTime.Now;
                db.tbl_NaoConformidade.Add(naoConformidade);
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

        void INaoConformidadeRepositorio.AtualizaNaoConformidade(tbl_NaoConformidade naoConformidade)
        {
            try
            {
                tbl_NaoConformidade naoConform = db.tbl_NaoConformidade.Where(x => x.IdNaoConformidade == naoConformidade.IdNaoConformidade).FirstOrDefault();
                naoConform.DsNaoConformidade = naoConformidade.DsNaoConformidade;
                naoConform.Dt_Alteracao = DateTime.Now;
                naoConform.Dstatus = naoConformidade.Dstatus;

                db.SaveChanges();
                naoConform = null;
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

        tbl_NaoConformidade INaoConformidadeRepositorio.Detalhes(long naoConformidadeId)
        {
            try
            {
                tbl_NaoConformidade obj = new tbl_NaoConformidade();
                obj = db.tbl_NaoConformidade.Include("tbl_Processo").Include("tbl_Atividade_Diaria").SingleOrDefault(s => s.IdNaoConformidade == naoConformidadeId);
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

        void INaoConformidadeRepositorio.DeletaNaoConformidade(long naoConformidadeId)
        {
            try
            {
                tbl_NaoConformidade _naoConf = db.tbl_NaoConformidade.SingleOrDefault(x => x.IdNaoConformidade == naoConformidadeId);
                db.tbl_NaoConformidade.Remove(_naoConf);
                db.SaveChanges();
                _naoConf = null;
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

        IEnumerable<tbl_NaoConformidade> INaoConformidadeRepositorio.GetNaoConformidade()
        {
            try
            {
                IEnumerable<tbl_NaoConformidade> lista;
                lista = db.tbl_NaoConformidade.Include("tbl_Processo").Include("tbl_Atividade_Diaria").ToList();
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


        IList<NaoConformidades> INaoConformidadeRepositorio.GetNaoConformidadeRelatorio(ref RelatNaoConformidadeClass relNaoConf)
        {
            try
            {
                IEnumerable<tbl_NaoConformidade> lista;

                long IdProcesso = relNaoConf.IdProcesso;
                long IdAtividadeDiaria = relNaoConf.IdAtividadeDiaria;
                Boolean Dstatus = relNaoConf.status;
                String descrNaoConf = relNaoConf.DsNaoConformidade;

                IList<NaoConformidades> listNc = new List<NaoConformidades>();


                using (var dbs = new db_sgqEntities())
                {
                    lista = db.tbl_NaoConformidade.Include("tbl_Processo").Include("tbl_Atividade_Diaria").Where(p => p.IdAtividadeDiaria == IdAtividadeDiaria && p.IdProcesso == IdProcesso && p.Dstatus == Dstatus).ToList();

                    if (!string.IsNullOrEmpty(descrNaoConf))
                    {
                        lista = lista.Where(p => p.DsNaoConformidade.Contains(descrNaoConf));
                    }

                    if (lista != null)
                    {
                        foreach (var p in lista.ToList())
                        {
                            NaoConformidades nc = new NaoConformidades();
                            nc.DataCadastro = (p.Dt_Cadastro != null ? p.Dt_Cadastro.Value : DateTime.MinValue); ;
                            nc.DsNaoConformidade = p.DsNaoConformidade;
                            nc.NomeAtividadeDiaria = p.Tbl_Atividade_Diaria.Descricao;
                            nc.NomeProcesso = p.tbl_Processo.Nome;
                            nc.Dstatus = p.Dstatus;

                            listNc.Add(nc);
                        }
                    }
                }


                relNaoConf.ListaNaoConformidade = listNc;
                return relNaoConf.ListaNaoConformidade;
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

        tbl_NaoConformidade INaoConformidadeRepositorio.GetNaoConformidadePorID(long naoConformidadeId)
        {
            try
            {
                return db.tbl_NaoConformidade.Include("tbl_Processo").Include("tbl_Atividade_Diaria").SingleOrDefault(x => x.IdNaoConformidade == naoConformidadeId);
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

    }
}