using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Banking.Model.Entities;
using Banking.Model.Extensions;

namespace Banking.Model.Configuration
{
    public class BankConfiguration : EntityTypeConfiguration<Bank>
    {
        public BankConfiguration()
        {
            var tableName = nameof(Bank);
            var schemaName = SchemaNames.Banking;

            ToTable(tableName, schemaName);

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200)
                .HasUniqueKey(schemaName, tableName, nameof(Bank.Name));

            Property(c => c.ClientBaseAddress)
                .IsRequired()
                .IsUnicode(false);

            Property(c => c.ClientName)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200);
        }
    }
}