﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_sgqEntities : DbContext
    {
        public db_sgqEntities()
            : base("name=db_sgqEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_Processo> tbl_Processo { get; set; }
        public virtual DbSet<tbl_atividades> tbl_atividades { get; set; }
        public virtual DbSet<tbl_etapa> tbl_etapa { get; set; }
        public virtual DbSet<tbl_funcao> tbl_funcao { get; set; }
        public virtual DbSet<tbl_Funcionario> tbl_Funcionario { get; set; }
        public virtual DbSet<tbl_Acessos> tbl_Acessos { get; set; }
        public virtual DbSet<Tbl_Atividade_Diaria> Tbl_Atividade_Diaria { get; set; }
        public virtual DbSet<tbl_NaoConformidade> tbl_NaoConformidade { get; set; }
    }
}
