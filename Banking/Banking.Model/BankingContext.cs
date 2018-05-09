using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Banking.Model.Entities;

namespace Banking.Model
{
    public class BankingContext : DbContext
    {
        public BankingContext() : base($"{SchemaNames.Banking}Context")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new NullDatabaseInitializer<BankingContext>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaNames.Banking);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.AddFromAssembly(typeof(BankingContext).Assembly);
        }
    }
}