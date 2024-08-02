using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BabyCiao.Models;

public partial class BabyciaoContext : DbContext
{
    public BabyciaoContext()
    {
    }

    public BabyciaoContext(DbContextOptions<BabyciaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AnnouncementPhoto> AnnouncementPhotos { get; set; }

    public virtual DbSet<AuthGroup> AuthGroups { get; set; }

    public virtual DbSet<BabyResume> BabyResumes { get; set; }

    public virtual DbSet<CompetitionDetail> CompetitionDetails { get; set; }

    public virtual DbSet<CompetitionFavorite> CompetitionFavorites { get; set; }

    public virtual DbSet<CompetitionPhoto> CompetitionPhotos { get; set; }

    public virtual DbSet<CompetitionRecord> CompetitionRecords { get; set; }

    public virtual DbSet<ContactBook> ContactBooks { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<CustomerService> CustomerServices { get; set; }

    public virtual DbSet<DiaperDetail> DiaperDetails { get; set; }

    public virtual DbSet<Diary> Diaries { get; set; }

    public virtual DbSet<DietDetail> DietDetails { get; set; }

    public virtual DbSet<Evaluate> Evaluates { get; set; }

    public virtual DbSet<FunctionSetting> FunctionSettings { get; set; }

    public virtual DbSet<GroupBuying> GroupBuyings { get; set; }

    public virtual DbSet<GroupBuyingDetail> GroupBuyingDetails { get; set; }

    public virtual DbSet<GroupBuyingDetailFormat> GroupBuyingDetailFormats { get; set; }

    public virtual DbSet<GroupBuyingPhoto> GroupBuyingPhotos { get; set; }

    public virtual DbSet<HealthInformation> HealthInformations { get; set; }

    public virtual DbSet<Memo> Memos { get; set; }

    public virtual DbSet<NannyRequirment> NannyRequirments { get; set; }

    public virtual DbSet<NannyResume> NannyResumes { get; set; }

    public virtual DbSet<NannyResumePhoto> NannyResumePhotos { get; set; }

    public virtual DbSet<OnlineCompetition> OnlineCompetitions { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<PlatformFavorite> PlatformFavorites { get; set; }

    public virtual DbSet<PlatformPhoto> PlatformPhotos { get; set; }

    public virtual DbSet<PlatformResponse> PlatformResponses { get; set; }

    public virtual DbSet<ProductFormat> ProductFormats { get; set; }

    public virtual DbSet<SecondHandExchangeOrder> SecondHandExchangeOrders { get; set; }

    public virtual DbSet<SecondHandExchangeOrderDetail> SecondHandExchangeOrderDetails { get; set; }

    public virtual DbSet<SecondHandSupply> SecondHandSupplies { get; set; }

    public virtual DbSet<SleepDetail> SleepDetails { get; set; }

    public virtual DbSet<SuppliesPhoto> SuppliesPhotos { get; set; }

    public virtual DbSet<SystemFunction> SystemFunctions { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    public virtual DbSet<Vip> Vips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=Babyciao;Integrated Security=true;TrustServerCertificate=true;Encrypt=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC275A2566B7");

            entity.ToTable("Announcement");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.PublishTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReferenceName).HasMaxLength(500);
            entity.Property(e => e.ReferenceRoute).HasMaxLength(500);
            entity.Property(e => e.Tittle).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(10);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.Announcements)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Announcem__Accou__4CA06362");
        });

        modelBuilder.Entity<AnnouncementPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC2761C619AB");

            entity.ToTable("AnnouncementPhoto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BuiledTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdAnnouncement).HasColumnName("ID_Announcement");
            entity.Property(e => e.PhotoName).HasMaxLength(500);

            entity.HasOne(d => d.IdAnnouncementNavigation).WithMany(p => p.AnnouncementPhotos)
                .HasForeignKey(d => d.IdAnnouncement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Announcem__ID_An__5165187F");
        });

        modelBuilder.Entity<AuthGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__AuthGrou__149AF36A0E56606D");

            entity.ToTable("AuthGroup");

            entity.Property(e => e.GroupDescription).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedPersonUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ModifiedPerson_UserAccount");

            entity.HasOne(d => d.ModifiedPersonUserAccountNavigation).WithMany(p => p.AuthGroups)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.ModifiedPersonUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuthGroup__Modif__7F2BE32F");
        });

        modelBuilder.Entity<BabyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BabyResu__3214EC271A45AD2B");

            entity.ToTable("BabyResume");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.ApplyDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Babyage).HasMaxLength(10);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Memo).HasMaxLength(500);
            entity.Property(e => e.Photo).HasMaxLength(500);
            entity.Property(e => e.TimeSlot).HasMaxLength(10);
            entity.Property(e => e.TypeOfDaycare).HasMaxLength(10);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.BabyResumes)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BabyResum__Accou__398D8EEE");
        });

        modelBuilder.Entity<CompetitionDetail>(entity =>
        {
            entity.HasKey(e => new { e.AccountUserAccount, e.IdOnlineCompetition }).HasName("PK__Competit__213756017AC7709A");

            entity.ToTable("CompetitionDetail");

            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdOnlineCompetition).HasColumnName("ID_OnlineCompetition");
            entity.Property(e => e.CompetitionPhoto).HasMaxLength(500);
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdOnlineCompetitionNavigation).WithMany(p => p.CompetitionDetails)
                .HasForeignKey(d => d.IdOnlineCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_On__6BE40491");
        });

        modelBuilder.Entity<CompetitionFavorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC27BE5D4A4E");

            entity.ToTable("CompetitionFavorite");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdOnlineCompetition).HasColumnName("ID_OnlineCompetition");

            entity.HasOne(d => d.IdOnlineCompetitionNavigation).WithMany(p => p.CompetitionFavorites)
                .HasForeignKey(d => d.IdOnlineCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_On__625A9A57");
        });

        modelBuilder.Entity<CompetitionPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC2718A9EB09");

            entity.ToTable("CompetitionPhoto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdOnlineCompetition).HasColumnName("ID_OnlineCompetition");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhotoName).HasMaxLength(500);

            entity.HasOne(d => d.IdOnlineCompetitionNavigation).WithMany(p => p.CompetitionPhotos)
                .HasForeignKey(d => d.IdOnlineCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_On__0D7A0286");
        });

        modelBuilder.Entity<CompetitionRecord>(entity =>
        {
            entity.HasKey(e => new { e.VoterAccount, e.IdOnlineCompetition }).HasName("PK__Competit__BAAB5406BA6F01F2");

            entity.ToTable("CompetitionRecord");

            entity.Property(e => e.VoterAccount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdOnlineCompetition).HasColumnName("ID_OnlineCompetition");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IdCompetitionDetail).HasColumnName("ID_CompetitionDetail");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdOnlineCompetitionNavigation).WithMany(p => p.CompetitionRecords)
                .HasForeignKey(d => d.IdOnlineCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_On__6FB49575");
        });

        modelBuilder.Entity<ContactBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContactB__3214EC27016CDF7B");

            entity.ToTable("ContactBook");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BabyName).HasMaxLength(50);
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmergencyContact).HasMaxLength(50);
            entity.Property(e => e.EmergencyContactPhone1).HasMaxLength(20);
            entity.Property(e => e.EmergencyContactPhone2)
                .HasMaxLength(20)
                .HasDefaultValue("?");
            entity.Property(e => e.ParentsIdUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ParentsID_UserAccount");

            entity.HasOne(d => d.ParentsIdUserAccountNavigation).WithMany(p => p.ContactBooks)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.ParentsIdUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContactBo__Paren__59FA5E80");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__5E2E73FA8F13BE4F");

            entity.ToTable("Contract");

            entity.Property(e => e.ContractId).HasColumnName("Contract_Id");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.BuiledTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ContractFile).HasMaxLength(500);
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.Statement).HasMaxLength(10);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.ContractAccountUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__Accoun__403A8C7D");

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.ContractNannyAccountUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__NannyA__3E52440B");
        });

        modelBuilder.Entity<CustomerService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC27B16D38AF");

            entity.ToTable("CustomerService");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Context).HasMaxLength(500);
            entity.Property(e => e.Createddated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModiifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Statement).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(10);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.CustomerServices)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerS__Accou__4D5F7D71");
        });

        modelBuilder.Entity<DiaperDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiaperDe__3214EC27264F4C92");

            entity.ToTable("DiaperDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
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
                .HasConstraintName("FK__DiaperDet__ID_Co__693CA210");
        });

        modelBuilder.Entity<Diary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diary__3214EC2722DBF938");

            entity.ToTable("Diary");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
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
                .HasConstraintName("FK__Diary__ID_Contac__778AC167");
        });

        modelBuilder.Entity<DietDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DietDeta__3214EC271B42FD43");

            entity.ToTable("DietDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.ModifiedTime)
                .HasMaxLength(50)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RecodeTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(10);

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.DietDetails)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DietDetai__ID_Co__6477ECF3");
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluate__3214EC27F63DE6FA");

            entity.ToTable("Evaluate");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppraiseeUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Appraisee_UserAccount");
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.EvaluateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EvaluatorUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Evaluator_UserAccount");
            entity.Property(e => e.Memo).HasMaxLength(500);

            entity.HasOne(d => d.AppraiseeUserAccountNavigation).WithMany(p => p.EvaluateAppraiseeUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AppraiseeUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__Apprai__47DBAE45");

            entity.HasOne(d => d.EvaluatorUserAccountNavigation).WithMany(p => p.EvaluateEvaluatorUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.EvaluatorUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__Evalua__46E78A0C");
        });

        modelBuilder.Entity<FunctionSetting>(entity =>
        {
            entity.HasKey(e => new { e.GroupIdAuthGroup, e.FunctionCodeSystemFunction }).HasName("PK__Function__6316C924FCF16B19");

            entity.ToTable("FunctionSetting");

            entity.Property(e => e.GroupIdAuthGroup).HasColumnName("GroupId_AuthGroup");
            entity.Property(e => e.FunctionCodeSystemFunction).HasColumnName("FunctionCode_SystemFunction");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FunctionCodeSystemFunctionNavigation).WithMany(p => p.FunctionSettings)
                .HasForeignKey(d => d.FunctionCodeSystemFunction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FunctionS__Funct__05D8E0BE");

            entity.HasOne(d => d.GroupIdAuthGroupNavigation).WithMany(p => p.FunctionSettings)
                .HasForeignKey(d => d.GroupIdAuthGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FunctionS__Group__04E4BC85");
        });

        modelBuilder.Entity<GroupBuying>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27B229751D");

            entity.ToTable("GroupBuying");

            entity.Property(e => e.Id).HasColumnName("ID");
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
            entity.Property(e => e.ProductType).HasMaxLength(30);
            entity.Property(e => e.Statement).HasMaxLength(20);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.GroupBuyings)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Accou__078C1F06");
        });

        modelBuilder.Entity<GroupBuyingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27B8BFDD80");

            entity.ToTable("GroupBuyingDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(100);
            entity.Property(e => e.Statement).HasMaxLength(20);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.GroupBuyingDetails)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Accou__13F1F5EB");

            entity.HasOne(d => d.GroupBuying).WithMany(p => p.GroupBuyingDetails)
                .HasForeignKey(d => d.GroupBuyingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Group__12FDD1B2");
        });

        modelBuilder.Entity<GroupBuyingDetailFormat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27A15CD595");

            entity.ToTable("GroupBuyingDetailFormat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FormatId).HasColumnName("FormatID");

            entity.HasOne(d => d.Format).WithMany(p => p.GroupBuyingDetailFormats)
                .HasForeignKey(d => d.FormatId)
                .HasConstraintName("FK__GroupBuyi__Forma__18B6AB08");

            entity.HasOne(d => d.GroupBuyingDetail).WithMany(p => p.GroupBuyingDetailFormats)
                .HasForeignKey(d => d.GroupBuyingDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Group__17C286CF");
        });

        modelBuilder.Entity<GroupBuyingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC276A0541CE");

            entity.ToTable("GroupBuyingPhoto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdGroupBuying).HasColumnName("ID_GroupBuying");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.IdGroupBuyingNavigation).WithMany(p => p.GroupBuyingPhotos)
                .HasForeignKey(d => d.IdGroupBuying)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__ID_Gr__0F2D40CE");
        });

        modelBuilder.Entity<HealthInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthIn__3214EC2756D38949");

            entity.ToTable("HealthInformation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AllergyHistory)
                .HasMaxLength(50)
                .HasDefaultValue("?");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.MedicalHistory)
                .HasMaxLength(50)
                .HasDefaultValue("?");
            entity.Property(e => e.Memo).HasMaxLength(500);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.HealthInformations)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthInf__ID_Co__5EBF139D");
        });

        modelBuilder.Entity<Memo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Memo__3214EC27605C0148");

            entity.ToTable("Memo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(500)
                .IsUnicode(false)
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
                .HasConstraintName("FK__Memo__ID_Contact__73BA3083");
        });

        modelBuilder.Entity<NannyRequirment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyReq__3214EC2719BC20F8");

            entity.ToTable("NannyRequirment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressesOfAgencies).HasMaxLength(500);
            entity.Property(e => e.ChildCareCertificate).HasMaxLength(500);
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.NationalIdentificationCard).HasMaxLength(500);
            entity.Property(e => e.PoliceCriminalRecordCertificate).HasMaxLength(500);
            entity.Property(e => e.RequirementDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Statement).HasDefaultValue(1);

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.NannyRequirments)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyRequ__Nanny__5629CD9C");
        });

        modelBuilder.Entity<NannyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyRes__3214EC27981BF58D");

            entity.ToTable("NannyResume");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.DisplayControl).HasDefaultValue(true);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.Introduction).HasMaxLength(500);
            entity.Property(e => e.Language).HasMaxLength(10);
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.ProfessionalPortrait).HasMaxLength(500);
            entity.Property(e => e.ServiceCenter).HasMaxLength(50);
            entity.Property(e => e.ServiceItems).HasMaxLength(10);
            entity.Property(e => e.TypeOfDaycare).HasMaxLength(30);

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.NannyResumes)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyResu__Nanny__300424B4");
        });

        modelBuilder.Entity<NannyResumePhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyRes__3214EC2732364725");

            entity.ToTable("NannyResumePhoto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdNannyResume).HasColumnName("ID_NannyResume");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhotoName).HasMaxLength(500);

            entity.HasOne(d => d.IdNannyResumeNavigation).WithMany(p => p.NannyResumePhotos)
                .HasForeignKey(d => d.IdNannyResume)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyResu__ID_Na__35BCFE0A");
        });

        modelBuilder.Entity<OnlineCompetition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OnlineCo__3214EC27F8D35826");

            entity.ToTable("OnlineCompetition");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.CompetitionName).HasMaxLength(50);
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.ModifiedTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Statement).HasMaxLength(50);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.OnlineCompetitions)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OnlineCom__Accou__09A971A2");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC278182AFFB");

            entity.ToTable("Platform");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.ModifiedTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.Platforms)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Platform__Accoun__40F9A68C");
        });

        modelBuilder.Entity<PlatformFavorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC2755C98FEA");

            entity.ToTable("PlatformFavorite");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdPlatform).HasColumnName("ID_Platform");

            entity.HasOne(d => d.IdPlatformNavigation).WithMany(p => p.PlatformFavorites)
                .HasForeignKey(d => d.IdPlatform)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlatformF__ID_Pl__65370702");
        });

        modelBuilder.Entity<PlatformPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC275D269C7F");

            entity.ToTable("PlatformPhoto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdPlatform).HasColumnName("ID_Platform");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.IdPlatformNavigation).WithMany(p => p.PlatformPhotos)
                .HasForeignKey(d => d.IdPlatform)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlatformP__ID_Pl__45BE5BA9");
        });

        modelBuilder.Entity<PlatformResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC277B36B43E");

            entity.ToTable("PlatformResponse");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.IdPlatform).HasColumnName("ID_Platform");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdPlatformNavigation).WithMany(p => p.PlatformResponses)
                .HasForeignKey(d => d.IdPlatform)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlatformR__ID_Pl__498EEC8D");
        });

        modelBuilder.Entity<ProductFormat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductF__3214EC27F65DA052");

            entity.ToTable("ProductFormat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FormatName).HasMaxLength(50);
            entity.Property(e => e.FormatType).HasMaxLength(50);
            entity.Property(e => e.IdGroupBuying).HasColumnName("ID_GroupBuying");

            entity.HasOne(d => d.IdGroupBuyingNavigation).WithMany(p => p.ProductFormats)
                .HasForeignKey(d => d.IdGroupBuying)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductFo__ID_Gr__0C50D423");
        });

        modelBuilder.Entity<SecondHandExchangeOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecondHa__3214EC27D549A59A");

            entity.ToTable("SecondHandExchangeOrder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GetQuantity).HasDefaultValue(1);
            entity.Property(e => e.GiveQuantity).HasDefaultValue(1);
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");
            entity.Property(e => e.SellerId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Statement).HasMaxLength(20);

            entity.HasOne(d => d.Buyer).WithMany(p => p.SecondHandExchangeOrderBuyers)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__Buyer__7C1A6C5A");

            entity.HasOne(d => d.Seller).WithMany(p => p.SecondHandExchangeOrderSellers)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__Selle__7D0E9093");

            entity.HasOne(d => d.WantGet).WithMany(p => p.SecondHandExchangeOrderWantGets)
                .HasForeignKey(d => d.WantGetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__WantG__7E02B4CC");

            entity.HasOne(d => d.WantGive).WithMany(p => p.SecondHandExchangeOrderWantGives)
                .HasForeignKey(d => d.WantGiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__WantG__7FEAFD3E");
        });

        modelBuilder.Entity<SecondHandExchangeOrderDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SecondHandExchangeOrderDetail");

            entity.Property(e => e.IdExchangeOrder).HasColumnName("ID_ExchangeOrder");

            entity.HasOne(d => d.IdExchangeOrderNavigation).WithMany()
                .HasForeignKey(d => d.IdExchangeOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__ID_Ex__02C769E9");
        });

        modelBuilder.Entity<SecondHandSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecondHa__3214EC2775118D3A");

            entity.Property(e => e.Id).HasColumnName("ID");
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
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.SecondHandSupplies)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__Accou__73852659");
        });

        modelBuilder.Entity<SleepDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SleepDet__3214EC27390A47B7");

            entity.ToTable("SleepDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasDefaultValue("?");
            entity.Property(e => e.IdContactBook).HasColumnName("ID_ContactBook");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SleepState).HasMaxLength(10);
            entity.Property(e => e.SleepTime).HasColumnType("datetime");
            entity.Property(e => e.WakeUpTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdContactBookNavigation).WithMany(p => p.SleepDetails)
                .HasForeignKey(d => d.IdContactBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SleepDeta__ID_Co__6EF57B66");
        });

        modelBuilder.Entity<SuppliesPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplies__3214EC274ADEAB32");

            entity.ToTable("SuppliesPhoto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdSecondHandSupplies).HasColumnName("ID_SecondHandSupplies");
            entity.Property(e => e.ModifiedTime)
                .HasMaxLength(50)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.IdSecondHandSuppliesNavigation).WithMany(p => p.SuppliesPhotos)
                .HasForeignKey(d => d.IdSecondHandSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SuppliesP__ID_Se__7849DB76");
        });

        modelBuilder.Entity<SystemFunction>(entity =>
        {
            entity.HasKey(e => e.FunctionId).HasName("PK__SystemFu__31ABFAF85C95DF6A");

            entity.ToTable("SystemFunction");

            entity.Property(e => e.FunctionName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CCACE6E3996C");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Account, "UQ__UserAcco__B0C3AC46DEA88053").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModiifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Permissions).HasDefaultValue(1);
            entity.Property(e => e.Vip).HasColumnName("VIP");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.UserinfoId).HasName("PK__UserInfo__E7D64B317F176153");

            entity.ToTable("UserInformation");

            entity.Property(e => e.UserinfoId).HasColumnName("UserinfoID");
            entity.Property(e => e.AccountUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_User");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CreateddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModiifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nickname).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserFirstName).HasMaxLength(50);
            entity.Property(e => e.UserLastName).HasMaxLength(50);
            entity.Property(e => e.UserPhoto).HasMaxLength(500);

            entity.HasOne(d => d.AccountUserNavigation).WithMany(p => p.UserInformations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInfor__Accou__2B3F6F97");
        });

        modelBuilder.Entity<Vip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VIP__3214EC27EFA8C11C");

            entity.ToTable("VIP");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.Vips)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VIP__Account_Use__7C4F7684");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
