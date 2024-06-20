using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BabyCiao.Models;

public partial class BabyCiaoContext : DbContext
{
    public BabyCiaoContext()
    {
    }

    public BabyCiaoContext(DbContextOptions<BabyCiaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AnnouncementPhoto> AnnouncementPhotos { get; set; }

    public virtual DbSet<BabyResume> BabyResumes { get; set; }

    public virtual DbSet<CompetitionDetail> CompetitionDetails { get; set; }

    public virtual DbSet<CompetitionRecord> CompetitionRecords { get; set; }

    public virtual DbSet<ContactBook> ContactBooks { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<DiaperDetail> DiaperDetails { get; set; }

    public virtual DbSet<Diary> Diaries { get; set; }

    public virtual DbSet<DietDetail> DietDetails { get; set; }

    public virtual DbSet<Evaluate> Evaluates { get; set; }

    public virtual DbSet<ExchangeOrder> ExchangeOrders { get; set; }

    public virtual DbSet<ExchangeOrderDetail> ExchangeOrderDetails { get; set; }

    public virtual DbSet<GroupBuying> GroupBuyings { get; set; }

    public virtual DbSet<GroupBuyingDetail> GroupBuyingDetails { get; set; }

    public virtual DbSet<GroupBuyingPhoto> GroupBuyingPhotos { get; set; }

    public virtual DbSet<HealthInformation> HealthInformations { get; set; }

    public virtual DbSet<Memo> Memos { get; set; }

    public virtual DbSet<NannyRequirment> NannyRequirments { get; set; }

    public virtual DbSet<NannyResume> NannyResumes { get; set; }

    public virtual DbSet<OnlineCompetition> OnlineCompetitions { get; set; }

    public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<PlatformPhoto> PlatformPhotos { get; set; }

    public virtual DbSet<PlatformResponse> PlatformResponses { get; set; }

    public virtual DbSet<SecondHandSupply> SecondHandSupplies { get; set; }

    public virtual DbSet<SleepDetail> SleepDetails { get; set; }

    public virtual DbSet<SuppliesPhoto> SuppliesPhotos { get; set; }

    public virtual DbSet<SystemFunction> SystemFunctions { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    public virtual DbSet<Vip> Vips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=BabyCiao;TrustServerCertificate=true;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC2749516CD8");

            entity.ToTable("Announcement");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.PublishTime).HasColumnType("datetime");
            entity.Property(e => e.ReferenceName).HasMaxLength(50);
            entity.Property(e => e.ReferenceRoute).HasMaxLength(50);
            entity.Property(e => e.Tittle).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.Announcements)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Announcem__Accou__49C3F6B7");
        });

        modelBuilder.Entity<AnnouncementPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC279A14094F");

            entity.ToTable("AnnouncementPhoto");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("ID");
            entity.Property(e => e.BuiledTime)
                .HasMaxLength(50)
                .HasDefaultValue("NOW");
            entity.Property(e => e.IdAnnouncement).HasColumnName("ID_Announcement");
            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.IdAnnouncementNavigation).WithMany(p => p.AnnouncementPhotos)
                .HasForeignKey(d => d.IdAnnouncement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Announcem__ID_An__4D94879B");
        });

        modelBuilder.Entity<BabyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BabyResu__3214EC2770840FEA");

            entity.ToTable("BabyResume");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.ApplyDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Memo).HasMaxLength(500);
            entity.Property(e => e.TimeSlot).HasMaxLength(50);
            entity.Property(e => e.TypeOfDaycare).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.BabyResumes)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BabyResum__Accou__38996AB5");
        });

        modelBuilder.Entity<CompetitionDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC276CD53920");

            entity.ToTable("CompetitionDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount).HasColumnName("Account_UserAccount");
            entity.Property(e => e.CompetitionPhoto).HasMaxLength(50);
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.IdOnlineCompetition).HasColumnName("ID_OnlineCompetition");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdOnlineCompetitionNavigation).WithMany(p => p.CompetitionDetails)
                .HasForeignKey(d => d.IdOnlineCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_On__09A971A2");
        });

        modelBuilder.Entity<CompetitionRecord>(entity =>
        {
            entity.HasKey(e => new { e.VoterAccount, e.IdCompetitionDetail }).HasName("PK__Competit__9202720EF90EAD40");

            entity.ToTable("CompetitionRecord");

            entity.Property(e => e.IdCompetitionDetail).HasColumnName("ID_CompetitionDetail");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCompetitionDetailNavigation).WithMany(p => p.CompetitionRecords)
                .HasForeignKey(d => d.IdCompetitionDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_Co__0D7A0286");
        });

        modelBuilder.Entity<ContactBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContactB__3214EC27DFB4C440");

            entity.ToTable("ContactBook");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BabyName).HasMaxLength(50);
            entity.Property(e => e.BloodType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmergencyContact).HasMaxLength(50);
            entity.Property(e => e.EmergencyContactPhone1).HasMaxLength(50);
            entity.Property(e => e.EmergencyContactPhone2)
                .HasMaxLength(50)
                .HasDefaultValue("?");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.ParentsIdUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ParentsID_UserAccount");

            entity.HasOne(d => d.ParentsIdUserAccountNavigation).WithMany(p => p.ContactBooks)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.ParentsIdUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContactBo__Paren__5629CD9C");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__5E2E73FA31B912E0");

            entity.ToTable("Contract");

            entity.Property(e => e.ContractId)
                .ValueGeneratedNever()
                .HasColumnName("Contract_Id");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.BuiledTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ContractFile).HasMaxLength(50);
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.Statement).HasDefaultValue(1);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.ContractAccountUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__Accoun__3E52440B");

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.ContractNannyAccountUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__NannyA__3D5E1FD2");
        });

        modelBuilder.Entity<DiaperDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiaperDe__3214EC278B944DD0");

            entity.ToTable("DiaperDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.BowelSituation)
                .HasMaxLength(50)
                .HasDefaultValue("?");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RecodeTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.DiaperDetails)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiaperDet__ID_Co__6477ECF3");
        });

        modelBuilder.Entity<Diary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diary__3214EC27BBAB248F");

            entity.ToTable("Diary");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount).HasColumnName("Account_UserAccount");
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.Memo).HasMaxLength(500);
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Photo).HasMaxLength(50);

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.Diaries)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diary__ID_Contac__74AE54BC");
        });

        modelBuilder.Entity<DietDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DietDeta__3214EC27221DD757");

            entity.ToTable("DietDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount).HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.ModifiedTime)
                .HasMaxLength(50)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RecodeTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Type).HasColumnType("datetime");

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.DietDetails)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DietDetai__ID_Co__5FB337D6");
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluate__3214EC279A716CC7");

            entity.ToTable("Evaluate");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AppraiseeUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Appraisee_UserAccount");
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.EvaluatorUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Evaluator_UserAccount");
            entity.Property(e => e.Memo).HasMaxLength(500);

            entity.HasOne(d => d.AppraiseeUserAccountNavigation).WithMany(p => p.EvaluateAppraiseeUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AppraiseeUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__Apprai__45F365D3");

            entity.HasOne(d => d.EvaluatorUserAccountNavigation).WithMany(p => p.EvaluateEvaluatorUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.EvaluatorUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__Evalua__44FF419A");
        });

        modelBuilder.Entity<ExchangeOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exchange__3214EC2728AFFDE5");

            entity.ToTable("ExchangeOrder");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.ExchangeOrders)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeO__Accou__19DFD96B");
        });

        modelBuilder.Entity<ExchangeOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.IdExchangeOrder, e.IdSecondHandSupplies }).HasName("PK__Exchange__4C80FCD5A28017B5");

            entity.ToTable("ExchangeOrderDetail");

            entity.Property(e => e.IdExchangeOrder).HasColumnName("ID_ExchangeOrder");
            entity.Property(e => e.IdSecondHandSupplies).HasColumnName("ID_SecondHandSupplies");

            entity.HasOne(d => d.IdExchangeOrderNavigation).WithMany(p => p.ExchangeOrderDetails)
                .HasForeignKey(d => d.IdExchangeOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeO__ID_Ex__1CBC4616");

            entity.HasOne(d => d.IdSecondHandSuppliesNavigation).WithMany(p => p.ExchangeOrderDetails)
                .HasForeignKey(d => d.IdSecondHandSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeO__ID_Se__1DB06A4F");
        });

        modelBuilder.Entity<GroupBuying>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27981B8BFD");

            entity.ToTable("GroupBuying");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductDescription).HasMaxLength(500);
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.GroupBuyings)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Accou__208CD6FA");
        });

        modelBuilder.Entity<GroupBuyingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC2758224114");

            entity.ToTable("GroupBuyingDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");
            entity.Property(e => e.Statement).HasDefaultValue(1);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.GroupBuyingDetails)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Accou__29221CFB");
        });

        modelBuilder.Entity<GroupBuyingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC274A3957D0");

            entity.ToTable("GroupBuyingPhoto");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("ID");
            entity.Property(e => e.IdGroupBuying).HasColumnName("ID_GroupBuying");
            entity.Property(e => e.ModifiedTime)
                .HasMaxLength(50)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.IdGroupBuyingNavigation).WithMany(p => p.GroupBuyingPhotos)
                .HasForeignKey(d => d.IdGroupBuying)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__ID_Gr__25518C17");
        });

        modelBuilder.Entity<HealthInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthIn__3214EC27B2065BBD");

            entity.ToTable("HealthInformation");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AllergyHistory)
                .HasMaxLength(50)
                .HasDefaultValue("?");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.MedicalHistory)
                .HasMaxLength(50)
                .HasDefaultValue("?");
            entity.Property(e => e.Memo).HasMaxLength(500);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.HealthInformations)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthInf__ID_Co__5AEE82B9");
        });

        modelBuilder.Entity<Memo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Memo__3214EC2720AA83E6");

            entity.ToTable("Memo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(500)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.Memo1)
                .HasMaxLength(500)
                .HasColumnName("Memo");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RecodeTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.Memos)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Memo__ID_Contact__70DDC3D8");
        });

        modelBuilder.Entity<NannyRequirment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyReq__3214EC273A1142F1");

            entity.ToTable("NannyRequirment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ChildCareCertificate).HasMaxLength(50);
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.NationalIdentificationCard).HasMaxLength(50);
            entity.Property(e => e.PoliceCriminalRecordCertificate).HasMaxLength(50);
            entity.Property(e => e.RequirementDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Statement)
                .HasMaxLength(50)
                .HasDefaultValue("???");

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.NannyRequirments)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyRequ__Nanny__52593CB8");
        });

        modelBuilder.Entity<NannyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyRes__3214EC274EFEAC36");

            entity.ToTable("NannyResume");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.DisplayControl).HasDefaultValue(true);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.InternalPhoto1)
                .HasMaxLength(50)
                .HasDefaultValue("???");
            entity.Property(e => e.InternalPhoto2)
                .HasMaxLength(50)
                .HasDefaultValue("???");
            entity.Property(e => e.InternalPhoto3)
                .HasMaxLength(50)
                .HasDefaultValue("???");
            entity.Property(e => e.InternalPhoto4)
                .HasMaxLength(50)
                .HasDefaultValue("???");
            entity.Property(e => e.InternalPhoto5)
                .HasMaxLength(50)
                .HasDefaultValue("???");
            entity.Property(e => e.Introduction).HasMaxLength(500);
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("??");
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.ProfessionalPortrait)
                .HasMaxLength(50)
                .HasDefaultValue("???");
            entity.Property(e => e.ServiceCenter).HasMaxLength(50);
            entity.Property(e => e.ServiceItems)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfDaycare)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.NannyResumes)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyResu__Nanny__2C3393D0");
        });

        modelBuilder.Entity<OnlineCompetition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OnlineCo__3214EC27B592522A");

            entity.ToTable("OnlineCompetition");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.CompetitionName).HasMaxLength(50);
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.ModifiedTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Statement).HasDefaultValue(1);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.OnlineCompetitions)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OnlineCom__Accou__04E4BC85");
        });

        modelBuilder.Entity<PermissionGroup>(entity =>
        {
            entity.HasKey(e => e.GroupCode).HasName("PK__Permissi__3B974381275C25D5");

            entity.ToTable("PermissionGroup");

            entity.Property(e => e.GroupCode).ValueGeneratedNever();
            entity.Property(e => e.GroupDescription).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedPersonUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ModifiedPerson_UserAccount");

            entity.HasOne(d => d.ModifiedPersonUserAccountNavigation).WithMany(p => p.PermissionGroups)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.ModifiedPersonUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permissio__Modif__7C4F7684");

            entity.HasMany(d => d.FunctionCodeSystemFunctions).WithMany(p => p.GroupCodePermissionGroups)
                .UsingEntity<Dictionary<string, object>>(
                    "FunctionSetting",
                    r => r.HasOne<SystemFunction>().WithMany()
                        .HasForeignKey("FunctionCodeSystemFunction")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FunctionS__Funct__02084FDA"),
                    l => l.HasOne<PermissionGroup>().WithMany()
                        .HasForeignKey("GroupCodePermissionGroup")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FunctionS__Group__01142BA1"),
                    j =>
                    {
                        j.HasKey("GroupCodePermissionGroup", "FunctionCodeSystemFunction").HasName("PK__Function__1A698783182FBCC4");
                        j.ToTable("FunctionSetting");
                        j.IndexerProperty<int>("GroupCodePermissionGroup").HasColumnName("GroupCode_PermissionGroup");
                        j.IndexerProperty<string>("FunctionCodeSystemFunction")
                            .HasMaxLength(50)
                            .HasColumnName("FunctionCode_SystemFunction");
                    });
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC27F75D57A1");

            entity.ToTable("Platform");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.Platforms)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Platform__Accoun__2CF2ADDF");
        });

        modelBuilder.Entity<PlatformPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC27AA8E68AD");

            entity.ToTable("PlatformPhoto");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("ID");
            entity.Property(e => e.IdPlatform).HasColumnName("ID_Platform");
            entity.Property(e => e.ModifiedTime)
                .HasMaxLength(50)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.IdPlatformNavigation).WithMany(p => p.PlatformPhotos)
                .HasForeignKey(d => d.IdPlatform)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlatformP__ID_Pl__30C33EC3");
        });

        modelBuilder.Entity<PlatformResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC2777D761B0");

            entity.ToTable("PlatformResponse");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.IdPlatform).HasColumnName("ID_Platform");
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdPlatformNavigation).WithMany(p => p.PlatformResponses)
                .HasForeignKey(d => d.IdPlatform)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlatformR__ID_Pl__3493CFA7");
        });

        modelBuilder.Entity<SecondHandSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecondHa__3214EC27B0CC528A");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SuppliesDescription).HasMaxLength(500);
            entity.Property(e => e.SuppliesName).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.SecondHandSupplies)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__Accou__114A936A");
        });

        modelBuilder.Entity<SleepDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SleepDet__3214EC27697C72D9");

            entity.ToTable("SleepDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasDefaultValue("?");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SleepTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.WakeUpTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.SleepDetails)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SleepDeta__ID_Co__6A30C649");
        });

        modelBuilder.Entity<SuppliesPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplies__3214EC278DAD9678");

            entity.ToTable("SuppliesPhoto");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("ID");
            entity.Property(e => e.IdSecondHandSupplies).HasColumnName("ID_SecondHandSupplies");
            entity.Property(e => e.ModifiedTime)
                .HasMaxLength(50)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.IdSecondHandSuppliesNavigation).WithMany(p => p.SuppliesPhotos)
                .HasForeignKey(d => d.IdSecondHandSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SuppliesP__ID_Se__160F4887");
        });

        modelBuilder.Entity<SystemFunction>(entity =>
        {
            entity.HasKey(e => e.FunctionCode).HasName("PK__SystemFu__E037746D5F6670A4");

            entity.ToTable("SystemFunction");

            entity.Property(e => e.FunctionCode).HasMaxLength(50);
            entity.Property(e => e.FunctionName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CCACDFF0897F");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Account, "UQ__UserAcco__B0C3AC462129AF49").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Permissions).HasDefaultValue(1);
            entity.Property(e => e.Vip).HasColumnName("VIP");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.UserinfoId).HasName("PK__UserInfo__E7D64B31B2DB845B");

            entity.ToTable("UserInformation");

            entity.Property(e => e.UserinfoId)
                .ValueGeneratedNever()
                .HasColumnName("UserinfoID");
            entity.Property(e => e.AccountUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_User");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserFirstName).HasMaxLength(50);
            entity.Property(e => e.UserLastName).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserNavigation).WithMany(p => p.UserInformations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInfor__Accou__29572725");
        });

        modelBuilder.Entity<Vip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VIP__3214EC2704B1C7E6");

            entity.ToTable("VIP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.Vips)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VIP__Account_Use__797309D9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
