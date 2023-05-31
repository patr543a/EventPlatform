using EventPlatform.Entities.ECP;
using Microsoft.EntityFrameworkCore;

namespace EventPlatform.Entities.Contexts;

public partial class EventContext : DbContext
{
    public EventContext()
    {
    }

    public EventContext(DbContextOptions<EventContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<ECP.Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ECP;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C810D8307BD7");

            entity.ToTable("Events");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.OrganizerIdFk)
                .HasMaxLength(20)
                .HasColumnName("OrganizerId_FK");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__Organizer__276EDEB3");
        });

        modelBuilder.Entity<ECP.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B1E51575AB");

            entity.Property(e => e.EventIdFk).HasColumnName("EventId_FK");

            entity.HasOne(d => d.Event).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.EventIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__EventId_F__2A4B4B5E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C2F0E1EB3");

            entity.Property(e => e.UserId).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(20);

            entity.HasMany(d => d.TaskIdAssignment).WithMany(p => p.VolunteerIdAssignment)
                .UsingEntity<Dictionary<string, object>>(
                    "Assignment",
                    r => r.HasOne<ECP.Task>().WithMany()
                        .HasForeignKey("TaskIdFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Assignmen__TaskI__31EC6D26"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("VolunteerIdFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Assignmen__Volun__30F848ED"),
                    j =>
                    {
                        j.HasKey("VolunteerIdFk", "TaskIdFk").HasName("PK__Assignme__A18C411FDEE264E4");
                        j.ToTable("Assignments");
                        j.IndexerProperty<string>("VolunteerIdFk")
                            .HasMaxLength(20)
                            .HasColumnName("VolunteerId_FK");
                        j.IndexerProperty<int>("TaskIdFk").HasColumnName("TaskId_FK");
                    });

            entity.HasMany(d => d.TaskIdCandidate).WithMany(p => p.VolunteerIdCandidate)
                .UsingEntity<Dictionary<string, object>>(
                    "Candidate",
                    r => r.HasOne<ECP.Task>().WithMany()
                        .HasForeignKey("TaskIdFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Candidate__TaskI__2E1BDC42"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("VolunteerIdFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Candidate__Volun__2D27B809"),
                    j =>
                    {
                        j.HasKey("VolunteerIdFk", "TaskIdFk").HasName("PK__Candidat__A18C411F8251F017");
                        j.ToTable("Candidates");
                        j.IndexerProperty<string>("VolunteerIdFk")
                            .HasMaxLength(20)
                            .HasColumnName("VolunteerId_FK");
                        j.IndexerProperty<int>("TaskIdFk").HasColumnName("TaskId_FK");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
