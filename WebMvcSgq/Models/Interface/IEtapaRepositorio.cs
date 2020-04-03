using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvcSgq.Models.Interface
{
    interface IEtapaRepositorio
    {
        IEnumerable<tbl_etapa> GetEtapas(long processoId);
        tbl_etapa GetEtapaPorID(long idEtapa);
    }
}
