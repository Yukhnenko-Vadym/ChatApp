using ChatApp.Features.Chat.Models;
using ChatApp.Features.UserAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;

public class ChatContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Chat> Chats { get; set; }
    
    public ChatContext(DbContextOptions<ChatContext> opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Username)
                .IsRequired().HasMaxLength(40);

            builder.Property(u => u.Email)
                .IsRequired().HasMaxLength(40);
        });
        
        /*mb.Entity<Message>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.ChatId);

            builder.HasIndex(m => m.SenderId);
            
            builder.Property(m => m.Content)
                .IsRequired().HasMaxLength(250);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne<Chat>()
                .WithMany()
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        mb.Entity<Chat>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.CreatorId);

            builder.HasOne(c => c.Creator) 
                .WithOne() 
                .HasForeignKey<Chat>(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        mb.Entity<ChatUser>(builder =>
        {
            builder.HasKey(cu => new { cu.ChatId, cu.UserId });

            builder.HasOne(cu => cu.User)
                .WithOne(u => u.ChatUser)
                .HasForeignKey<ChatUser>(cu => cu.UserId);

            builder.HasMany(u => u.Chats)
                .WithOne(c => c.ChatUser);
        });*/
        
    }
}