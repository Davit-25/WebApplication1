using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Congiguration
{
    public class FilmListConfig : IEntityTypeConfiguration<FilmsList>
    {
        public void Configure(EntityTypeBuilder<FilmsList> builder)
        {
            builder.HasMany(e => e.sessions);
        }
    }
}
