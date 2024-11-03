namespace EtiquetasSuprimentosService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBEtiquetas : DbContext
    {
        public DBEtiquetas()
            : base("name=DBEtiquetas")
        {
        }

        public virtual DbSet<vw_EtiquetasSuprimentos> vw_EtiquetasSuprimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vw_EtiquetasSuprimentos>()
                .Property(e => e.suprimento)
                .IsUnicode(false);

            modelBuilder.Entity<vw_EtiquetasSuprimentos>()
                .Property(e => e.serialSuprimento)
                .IsUnicode(false);

            modelBuilder.Entity<vw_EtiquetasSuprimentos>()
                .Property(e => e.etiqueta)
                .IsUnicode(false);

            modelBuilder.Entity<vw_EtiquetasSuprimentos>()
                .Property(e => e.operador)
                .IsUnicode(false);

            modelBuilder.Entity<vw_EtiquetasSuprimentos>()
                .Property(e => e.status)
                .IsUnicode(false);
        }
    }
}
