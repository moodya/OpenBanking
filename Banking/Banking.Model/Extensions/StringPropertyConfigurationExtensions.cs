using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Banking.Model.Extensions
{
    public static class StringPropertyConfigurationExtensions
    {
        public static StringPropertyConfiguration HasUniqueKey(this StringPropertyConfiguration configuration, string schemaName, string tableName, string columnName, int order = 1)
        {
            return configuration.HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute($"IX_{schemaName}.{tableName}_{columnName}", order)
                    {
                        IsUnique = true
                    }));
        }
    }
}