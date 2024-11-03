namespace MonitorEtqSuprimentos
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBTrocas : DbContext
    {
        public DBTrocas()
            : base("name=DBTrocas")
        {
        }

        public virtual DbSet<ControleTrocaCilindro> ControleTrocaCilindro { get; set; }
        public virtual DbSet<ControleTrocaTonner> ControleTrocaTonner { get; set; }
        public virtual DbSet<vw_relatorio_suprimentos> vw_relatorio_suprimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ControleTrocaCilindro>()
                .Property(e => e.serie)
                .IsUnicode(false);

            modelBuilder.Entity<ControleTrocaCilindro>()
                .Property(e => e.serial)
                .IsUnicode(false);

            modelBuilder.Entity<ControleTrocaTonner>()
                .Property(e => e.serie)
                .IsUnicode(false);

            modelBuilder.Entity<ControleTrocaTonner>()
                .Property(e => e.serial)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.UF)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.Cidade)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.Unidade)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.Ambiente)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.Fila)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.serie)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.tpEnvioToner)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.PostagemToner)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.tpEnvioCilindro)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.PostagemCilindro)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.serialToner)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.serialFoto)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.SugestaoToner)
                .IsUnicode(false);

            modelBuilder.Entity<vw_relatorio_suprimentos>()
                .Property(e => e.SugestaoCilindro)
                .IsUnicode(false);
        }
    }
}
