using Api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Core.Domain.Models.Entry>
    {
        public override void Configure(EntityTypeBuilder<Api.Core.Domain.Models.Entry> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry", SourDictionaryContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.Entries)
                .HasForeignKey(x => x.CreatedById);
        }
    }
}
