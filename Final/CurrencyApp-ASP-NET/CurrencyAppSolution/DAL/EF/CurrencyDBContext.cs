namespace DAL.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CurrencyDBContext : DbContext
    {
        public CurrencyDBContext()
            : base("name=CurrencyDBContext")
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CurrencyChangeLog> CurrencyChangeLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
