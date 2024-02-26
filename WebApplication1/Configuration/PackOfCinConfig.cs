using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Congiguration
{
    public class PackOfCinConfig : IEntityTypeConfiguration<PackageOfCinema>
    {
        public void Configure( EntityTypeBuilder<PackageOfCinema> builder)
        {
           builder.HasOne(e => e.Films).WithMany(e => e.sessions).HasForeignKey(e => e.idFilms);
        }
    }
}
