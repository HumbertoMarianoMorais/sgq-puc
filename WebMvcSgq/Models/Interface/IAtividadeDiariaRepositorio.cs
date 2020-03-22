using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvcSgq.Models.Interface
{
    interface IAtividadeDiariaRepositorio
    {
        IEnumerable<Tbl_Atividade_Diaria> GetAtividadeDiaria();
        Tbl_Atividade_Diaria GetAtividadePorID(int idAtivDiaria);
        void AdicionaAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria);
        void DeletarAtividadeDiaria(long idAtivDiaria);
        void EditarAtividadeDiaria(Tbl_Atividade_Diaria ativDiaria);
        Tbl_Atividade_Diaria Detalhes(int idAtivDiaria);
    }
}
