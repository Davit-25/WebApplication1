using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Congiguration
{
    public class SessionsConfig : IEntityTypeConfiguration<Sessions>
    {
        public void Configure(EntityTypeBuilder<Sessions> builder)
        {
            ;
            builder.HasKey(e=>e.id); 
        }
    }
}
