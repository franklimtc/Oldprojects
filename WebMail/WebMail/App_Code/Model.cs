using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

public partial class Model : DbContext
{
    public Model()
        : base("name=pecasSigep")
    {
    }

    public virtual DbSet<Emails> Emails { get; set; }
    public virtual DbSet<LogsErros> LogsErros { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Emails>()
            .Property(e => e.emailPara)
            .IsUnicode(false);

        modelBuilder.Entity<Emails>()
            .Property(e => e.emailCopia)
            .IsUnicode(false);

        modelBuilder.Entity<Emails>()
            .Property(e => e.emailCopiaOculta)
            .IsUnicode(false);

        modelBuilder.Entity<Emails>()
            .Property(e => e.titulo)
            .IsUnicode(false);

        modelBuilder.Entity<Emails>()
            .Property(e => e.mensagem)
            .IsUnicode(false);

        modelBuilder.Entity<LogsErros>()
            .Property(e => e.sistema)
            .IsUnicode(false);

        modelBuilder.Entity<LogsErros>()
            .Property(e => e.erro)
            .IsUnicode(false);
    }
}
