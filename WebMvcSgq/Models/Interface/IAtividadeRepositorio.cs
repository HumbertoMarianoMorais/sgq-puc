using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvcSgq.Models.Interface
{
    interface IAtividadeRepositorio
    {
        IEnumerable<tbl_atividades> GetAtividade(long idAtividadeDiaria);

        void SalvarAtividade(tbl_atividades atividade);
    }
}
