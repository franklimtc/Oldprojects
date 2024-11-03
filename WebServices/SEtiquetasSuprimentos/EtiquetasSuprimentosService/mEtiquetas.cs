namespace EtiquetasSuprimentosService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mEtiquetas : DbContext
    {
        public mEtiquetas()
            : base("name=PPS")
        {
        }

        public virtual DbSet<etiquetasSuprimentos> etiquetasSuprimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<etiquetasSuprimentos>()
                .Property(e => e.suprimento)
                .IsUnicode(false);

            modelBuilder.Entity<etiquetasSuprimentos>()
                .Property(e => e.serialSuprimento)
                .IsUnicode(false);

            modelBuilder.Entity<etiquetasSuprimentos>()
                .Property(e => e.etiqueta)
                .IsUnicode(false);

            modelBuilder.Entity<etiquetasSuprimentos>()
                .Property(e => e.operador)
                .IsUnicode(false);

            modelBuilder.Entity<etiquetasSuprimentos>()
                .Property(e => e.status)
                .IsUnicode(false);
        }
    }
}
