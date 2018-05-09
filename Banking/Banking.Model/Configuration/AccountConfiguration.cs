using System.Data.Entity.ModelConfiguration;
using Banking.Model.Entities;
using Banking.Model.Extensions;

namespace Banking.Model.Configuration
{
    public class AccountConfiguration : EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
        {
            var tableName = nameof(Account);
            var schemaName = SchemaNames.Banking;

            ToTable(tableName, schemaName);

            HasKey(c => new
            {
                c.Bank_Id,
                c.User_Id,
                c.Number
            });

            var columnNames = $"{nameof(Account.Bank_Id)}_{nameof(Account.Number)}";

            Property(c => c.Bank_Id)
                .IsRequired()
                .HasUniqueKey(schemaName, tableName, columnNames, 1);

            Property(c => c.Number)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(8)
                .HasUniqueKey(schemaName, tableName, columnNames, 2);

            HasRequired(c => c.Bank)
                .WithMany(c => c.Accounts)
                .HasForeignKey(c => c.Bank_Id);

            HasRequired(c => c.User)
                .WithMany(c => c.Accounts)
                .HasForeignKey(c => c.User_Id);
        }
    }
}