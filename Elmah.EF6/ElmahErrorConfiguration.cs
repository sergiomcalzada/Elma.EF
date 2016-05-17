using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Elmah.EF;

namespace Elmah.EF6
{
    public class ElmahErrorConfiguration : EntityTypeConfiguration<ElmahError>
    {
        public ElmahErrorConfiguration()
        {
            this.ToTable("ElmahError");
            this.HasKey(x => x.ErrorId);
            this.Property(x => x.Sequence).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.AllXml).HasColumnType("ntext");
        }
    }
}