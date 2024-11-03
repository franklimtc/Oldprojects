namespace MonitorTrocasSuprimentosII
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbModelTrocas : DbContext
    {
        public dbModelTrocas()
            : base("name=dnaPrint")
        {
        }

        public virtual DbSet<vw_UltimasTrocasSuprimentos> vw_UltimasTrocasSuprimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vw_UltimasTrocasSuprimentos>()
                .Property(e => e.serie)
                .IsUnicode(false);

            modelBuilder.Entity<vw_UltimasTrocasSuprimentos>()
                .Property(e => e.serial)
                .IsUnicode(false);

            modelBuilder.Entity<vw_UltimasTrocasSuprimentos>()
                .Property(e => e.Suprimento)
                .IsUnicode(false);
        }
    }
}
