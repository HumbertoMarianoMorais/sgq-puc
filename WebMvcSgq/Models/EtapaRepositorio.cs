using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvcSgq.Models.Interface;

namespace WebMvcSgq.Models
{
    public class EtapaRepositorio : IEtapaRepositorio
    {
        db_sgqEntities db = new db_sgqEntities();

        IEnumerable<tbl_etapa> IEtapaRepositorio.GetEtapas(long processoId)
        {
            try
            {
                IEnumerable<tbl_etapa> lista;
                lista = db.tbl_etapa.Include("tbl_processo").Where(p=> p.IdProcesso == processoId).ToList();
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

        tbl_etapa IEtapaRepositorio.GetEtapaPorID(long processoId)
        {
            try
            {
                return db.tbl_etapa.Include("tbl_Processo").SingleOrDefault(x => x.IdProcesso == processoId);
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