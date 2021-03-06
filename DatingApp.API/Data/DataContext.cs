using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        /*info pour entity lors de la creation de la relation user users many to many
        k.LikerId, k.LikeeId pour evieter d'aimer plusieurs fois on concatene les deux clé primaire
        info pour entity lors de la creation de la relation user et message many to many
        k.MessageSent, k.MessageReceived pour evieter d'aimer plusieurs fois on concatene les deux clé primaire*/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});
            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);
            /* one liker can have many likee */
            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            /* one user can sent to other messages */
            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(u => u.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
            /* one user can receive to other messages */
            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(u => u.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
        }

        /*info pour entity lors de la creation de la relation user et message many to many
        k.MessageSent, k.MessageReceived pour evieter d'aimer plusieurs fois on concatene les deux clé primaire*/
        
    }
}