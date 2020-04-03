using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvcSgq.Models.Classe
{

    public class AtiviModelView
    {
        public AtividadeDiariaClass atividadeDiaCla { get; set; }
        public List<AtividadeClass> atividadeCla {get;set;}
    }

    public class AtividadeDiariaClass
    {
        [Required]
        public long IdAtividade { get; set; }
        [Required]
        public long IdProcesso { get; set; }

        public string NomeProcesso { get; set; }
        public string Descricao { get; set; }
        public DateTime? Dt_Cadastro { get; set; }
        public DateTime? Dt_Alteracao { get; set; }
    }

    public class AtividadeClass
    {
        [Required]
        public long IdAtividade { get; set; }
        [Required]
        public long IdAtividadeDiaria { get; set; }
        [Required]
        public long IdEtapa { get; set; }

        public string DsEtapa { get; set; }
        public int DsSelecionado { get; set; }
    }
}