//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMvcSgq.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Funcionario
    {
        public long IdFuncionario { get; set; }
        public Nullable<long> IdFuncao { get; set; }
        public string DsFuncionario { get; set; }
        public string DsUsuario { get; set; }
        public string DsSenha { get; set; }
        public Nullable<System.DateTime> Dt_Cadastro { get; set; }
        public Nullable<System.DateTime> Dt_Alteracao { get; set; }
    
        public virtual tbl_funcao tbl_funcao { get; set; }
    }
}
