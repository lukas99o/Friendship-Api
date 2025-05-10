using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Vänskap_Api.Models;

namespace Vänskap_Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
        public DbSet<Interest> Interests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FriendRequest>()
                .HasOne(f => f.Sender)
                .WithMany()
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FriendRequest>()
                .HasOne(f => f.Receiver)
                .WithMany()
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Interests)
                .WithMany(i => i.Users)
                .UsingEntity(j => j.ToTable("UserInterests"));

            builder.Entity<Event>()
                .HasMany(e => e.Interests)
                .WithMany(i => i.Events)
                .UsingEntity(j => j.ToTable("EventInterests"));

            builder.Entity<EventParticipant>()
                .HasOne(ep => ep.User)
                .WithMany(u => u.EventParticipations)
                .HasForeignKey(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.EventParticipants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Interest>().HasData(
                new Interest { Id = 1, Name = "Matlagning" },
                new Interest { Id = 2, Name = "Resor" },
                new Interest { Id = 3, Name = "Fotografi" },
                new Interest { Id = 4, Name = "Träning" },
                new Interest { Id = 5, Name = "Löpning" },
                new Interest { Id = 6, Name = "Vandring" },
                new Interest { Id = 7, Name = "Cykling" },
                new Interest { Id = 8, Name = "Simning" },
                new Interest { Id = 9, Name = "Yoga" },
                new Interest { Id = 10, Name = "Musik" },
                new Interest { Id = 11, Name = "Dans" },
                new Interest { Id = 12, Name = "Måla" },
                new Interest { Id = 13, Name = "Teckna" },
                new Interest { Id = 14, Name = "Skriva" },
                new Interest { Id = 15, Name = "Läsa böcker" },
                new Interest { Id = 16, Name = "Spela gitarr" },
                new Interest { Id = 17, Name = "Spela piano" },
                new Interest { Id = 18, Name = "Programmering" },
                new Interest { Id = 19, Name = "Trädgårdsarbete" },
                new Interest { Id = 20, Name = "Fiske" },
                new Interest { Id = 21, Name = "Jakt" },
                new Interest { Id = 22, Name = "Baka" },
                new Interest { Id = 23, Name = "Mode" },
                new Interest { Id = 24, Name = "Inredning" },
                new Interest { Id = 25, Name = "Filmer" },
                new Interest { Id = 26, Name = "Serier" },
                new Interest { Id = 27, Name = "Podcast" },
                new Interest { Id = 28, Name = "Bilar" },
                new Interest { Id = 29, Name = "Motorcyklar" },
                new Interest { Id = 30, Name = "Djur" },
                new Interest { Id = 31, Name = "Hundar" },
                new Interest { Id = 32, Name = "Katter" },
                new Interest { Id = 33, Name = "Volontärarbete" },
                new Interest { Id = 34, Name = "Aktier" },
                new Interest { Id = 35, Name = "Investeringar" },
                new Interest { Id = 36, Name = "Ekonomi" },
                new Interest { Id = 37, Name = "Historia" },
                new Interest { Id = 38, Name = "Psykologi" },
                new Interest { Id = 39, Name = "Filosofi" },
                new Interest { Id = 40, Name = "Astronomi" },
                new Interest { Id = 41, Name = "Vetenskap" },
                new Interest { Id = 42, Name = "Politik" },
                new Interest { Id = 43, Name = "Miljöfrågor" },
                new Interest { Id = 44, Name = "Debatt" },
                new Interest { Id = 45, Name = "Självutveckling" },
                new Interest { Id = 46, Name = "Meditation" },
                new Interest { Id = 47, Name = "Mindfulness" },
                new Interest { Id = 48, Name = "Skidåkning" },
                new Interest { Id = 49, Name = "Snowboard" },
                new Interest { Id = 50, Name = "Segling" },
                new Interest { Id = 51, Name = "Surfing" },
                new Interest { Id = 52, Name = "Golf" },
                new Interest { Id = 53, Name = "Fotboll" },
                new Interest { Id = 54, Name = "Basket" },
                new Interest { Id = 55, Name = "Tennis" },
                new Interest { Id = 56, Name = "Padel" },
                new Interest { Id = 57, Name = "Baseboll" },
                new Interest { Id = 58, Name = "Esport" },
                new Interest { Id = 59, Name = "Brädspel" },
                new Interest { Id = 60, Name = "Schack" },
                new Interest { Id = 61, Name = "Kortspel" },
                new Interest { Id = 62, Name = "Rollspel" },
                new Interest { Id = 63, Name = "Camping" },
                new Interest { Id = 64, Name = "Roadtrips" },
                new Interest { Id = 65, Name = "Backpacking" },
                new Interest { Id = 66, Name = "Språk" },
                new Interest { Id = 67, Name = "Kultur" },
                new Interest { Id = 68, Name = "Matkultur" },
                new Interest { Id = 69, Name = "Brygga öl" },
                new Interest { Id = 70, Name = "Vinprovning" },
                new Interest { Id = 71, Name = "Cocktails" },
                new Interest { Id = 72, Name = "Kaffe" },
                new Interest { Id = 73, Name = "Teknik" },
                new Interest { Id = 74, Name = "AI" },
                new Interest { Id = 75, Name = "Spelutveckling" },
                new Interest { Id = 76, Name = "Webbutveckling" },
                new Interest { Id = 77, Name = "Mobilappar" },
                new Interest { Id = 78, Name = "Entreprenörskap" },
                new Interest { Id = 79, Name = "Startups" },
                new Interest { Id = 80, Name = "Marknadsföring" },
                new Interest { Id = 81, Name = "Sociala medier" },
                new Interest { Id = 82, Name = "YouTube" },
                new Interest { Id = 83, Name = "Streaming" },
                new Interest { Id = 84, Name = "Standup" },
                new Interest { Id = 85, Name = "Improvisation" },
                new Interest { Id = 86, Name = "Skådespeleri" },
                new Interest { Id = 87, Name = "Teater" },
                new Interest { Id = 88, Name = "Konst" },
                new Interest { Id = 89, Name = "Museer" },
                new Interest { Id = 90, Name = "Arkitektur" },
                new Interest { Id = 91, Name = "Modefotografi" },
                new Interest { Id = 92, Name = "Vintage" },
                new Interest { Id = 93, Name = "Antikviteter" },
                new Interest { Id = 94, Name = "Loppis" },
                new Interest { Id = 95, Name = "Minimalism" },
                new Interest { Id = 96, Name = "Zero waste" },
                new Interest { Id = 97, Name = "DIY-projekt" },
                new Interest { Id = 98, Name = "Snickeri" },
                new Interest { Id = 99, Name = "Keramik" },
                new Interest { Id = 100, Name = "Origami" }
            );

            builder.Entity<ApplicationUser>().HasData(

            );
        }
    }
}
