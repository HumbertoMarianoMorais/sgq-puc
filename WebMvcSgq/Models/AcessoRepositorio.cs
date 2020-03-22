using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvcSgq.Models.Interface;

namespace WebMvcSgq.Models
{
    public class AcessoRepositorio : IAcessoRepositorio
    {
        db_sgqEntities db = new db_sgqEntities();


        IList<tbl_Acessos> IAcessoRepositorio.GetAcessosFuncao(long idFuncao)
        {
            try
            {
                List<tbl_Acessos> listAcesso = db.tbl_Acessos.Where(s => s.IdFuncaoAcesso == idFuncao).ToList();

                return listAcesso;
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