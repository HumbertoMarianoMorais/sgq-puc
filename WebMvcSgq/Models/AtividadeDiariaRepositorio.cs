using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvcSgq.Models.Interface;

namespace WebMvcSgq.Models
{
    public class AtividadeDiariaRepositorio : IAtividadeDiariaRepositorio
    {

        db_sgqEntities db = new db_sgqEntities();

        void IAtividadeDiariaRepositorio.AdicionaAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria)
        {
            try
            {
                ativDiaria.Dt_Cadastro = DateTime.Now;

                db.Tbl_Atividade_Diaria.Add(ativDiaria);
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
        void IAtividadeDiariaRepositorio.EditarAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria)
        {
            try
            {
                var novaAtividade = db.Tbl_Atividade_Diaria.Where(x => x.IdAtividade == ativDiaria.IdAtividade).FirstOrDefault();
                novaAtividade.Descricao = ativDiaria.Descricao;
                novaAtividade.Dt_Alteracao = DateTime.Now;
                novaAtividade.Dt_Alteracao = ativDiaria.Dt_Alteracao;
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
                obj = db.Tbl_Atividade_Diaria.SingleOrDefault(s => s.IdAtividade == idAtivDiaria);
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
                lista = db.Tbl_Atividade_Diaria.ToList();
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

        Tbl_Atividade_Diaria IAtividadeDiariaRepositorio.GetAtividadePorID(int idAtivDiaria)
        {
            try
            {
                return db.Tbl_Atividade_Diaria.SingleOrDefault(x => x.IdAtividade == idAtivDiaria);
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