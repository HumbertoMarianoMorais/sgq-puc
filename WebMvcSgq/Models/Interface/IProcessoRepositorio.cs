﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvcSgq.Models.Interface
{
    interface IProcessoRepositorio
    {
        IEnumerable<tbl_Processo> GetProcessos();
        tbl_Processo GetProcessoPorID(int processoId);
        void AdicionaProcesso(tbl_Processo processo);
        void DeletaProcesso(long processoId);
        void AtualizaProcesso(tbl_Processo processo);
        tbl_Processo Detalhes(int processoId);
    }
}