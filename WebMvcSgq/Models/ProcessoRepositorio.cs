using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvcSgq.Models.Interface;

namespace WebMvcSgq.Models
{
    public class ProcessoRepositorio : IProcessoRepositorio
    {

        db_sgqEntities db = new db_sgqEntities();

        void IProcessoRepositorio.AdicionaProcesso(tbl_Processo processo)
        {
            try
            {
                processo.Dt_Cadastro = DateTime.Now;
                
                db.tbl_Processo.Add(processo);
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
        void IProcessoRepositorio.AtualizaProcesso(tbl_Processo processo)
        {
            try
            {
                var novoProcesso = db.tbl_Processo.Where(x => x.IdProcesso == processo.IdProcesso).FirstOrDefault();
                novoProcesso.Nome = processo.Nome;
                processo.Dt_Alteracao = DateTime.Now;
                novoProcesso.Dt_Alteracao = processo.Dt_Alteracao;
                db.SaveChanges();
                novoProcesso = null;
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

        void IProcessoRepositorio.DeletaProcesso(long processoId)
        {
            try
            {
                tbl_Processo _processo = db.tbl_Processo.SingleOrDefault(x => x.IdProcesso == processoId);
                db.tbl_Processo.Remove(_processo);
                db.SaveChanges();
                _processo = null/* TODO Change to default(_) if this is not a reference type */;
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

        tbl_Processo IProcessoRepositorio.Detalhes(long processoId)
        {
            try
            {
                tbl_Processo obj = new tbl_Processo();
                obj = db.tbl_Processo.SingleOrDefault(s => s.IdProcesso == processoId);
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

        IEnumerable<tbl_Processo> IProcessoRepositorio.GetProcessos()
        {
            try
            {
                IEnumerable<tbl_Processo> lista;
                lista = db.tbl_Processo.ToList();
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

        tbl_Processo IProcessoRepositorio.GetProcessoPorID(long processoId)
        {
            try
            {
                return db.tbl_Processo.Include("tbl_etapa").SingleOrDefault(x => x.IdProcesso == processoId);
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