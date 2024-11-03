namespace dnaPrint.Config
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.SQLite.EF6;


    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<config> config { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<config>()
                .Property(e => e.configuracao)
                .IsUnicode(false);

            modelBuilder.Entity<config>()
                .Property(e => e.valor)
                .IsUnicode(false);
        }
    }
}
