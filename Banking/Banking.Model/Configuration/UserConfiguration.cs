using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Banking.Model.Entities;
using Banking.Model.Extensions;

namespace Banking.Model.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            var tableName = nameof(User);
            var schemaName = SchemaNames.Banking;

            ToTable(tableName, schemaName);

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200);
            
            Property(c => c.Username)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200)
                .HasUniqueKey(schemaName, tableName, nameof(User.Username));

            Property(c => c.Password)
                .IsRequired()
                .IsUnicode(false);
        }
    }
}