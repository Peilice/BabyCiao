using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BabyCiao_Client.Models;

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

    public virtual DbSet<Inquire> Inquires { get; set; }

    public virtual DbSet<Memo> Memos { get; set; }

    public virtual DbSet<NannyOtherCertificate> NannyOtherCertificates { get; set; }

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
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=Babyciao;TrustServerCertificate=True;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC270CABB86E");

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
                .HasConstraintName("FK__Announcem__Accou__66603565");
        });

        modelBuilder.Entity<AnnouncementPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC27AAF10CAA");

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
                .HasConstraintName("FK__Announcem__ID_An__6B24EA82");
        });

        modelBuilder.Entity<AuthGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__AuthGrou__149AF36A7783F426");

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
                .HasConstraintName("FK__AuthGroup__Modif__18EBB532");
        });

        modelBuilder.Entity<BabyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BabyResu__3214EC273D0D3CE7");

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
                .HasConstraintName("FK__BabyResum__Accou__4CA06362");
        });

        modelBuilder.Entity<CompetitionDetail>(entity =>
        {
            entity.HasKey(e => new { e.AccountUserAccount, e.IdOnlineCompetition }).HasName("PK__Competit__21375601FC5E4A74");

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
                .HasConstraintName("FK__Competiti__ID_On__2DE6D218");
        });

        modelBuilder.Entity<CompetitionFavorite>(entity =>
        {
            entity.HasKey(e => new { e.AccountUserAccount, e.IdOnlineCompetition }).HasName("PK__Competit__21375601573DCD1F");

            entity.ToTable("CompetitionFavorite");

            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdOnlineCompetition).HasColumnName("ID_OnlineCompetition");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.HasOne(d => d.IdOnlineCompetitionNavigation).WithMany(p => p.CompetitionFavorites)
                .HasForeignKey(d => d.IdOnlineCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_On__2B0A656D");
        });

        modelBuilder.Entity<CompetitionPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC2742C74ACA");

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
                .HasConstraintName("FK__Competiti__ID_On__2739D489");
        });

        modelBuilder.Entity<CompetitionRecord>(entity =>
        {
            entity.HasKey(e => new { e.VoterAccount, e.IdOnlineCompetition }).HasName("PK__Competit__BAAB54067F58F590");

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
                .HasConstraintName("FK__Competiti__ID_On__31B762FC");
        });

        modelBuilder.Entity<ContactBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContactB__3214EC27CFAF3B79");

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
                .HasConstraintName("FK__ContactBo__Paren__73BA3083");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__5E2E73FAB9663DE3");

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
            entity.Property(e => e.NannySignatureFile).HasMaxLength(500);
            entity.Property(e => e.Statement).HasMaxLength(10);
            entity.Property(e => e.UserSignatureFile).HasMaxLength(500);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.ContractAccountUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__Accoun__534D60F1");

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.ContractNannyAccountUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__NannyA__5165187F");
        });

        modelBuilder.Entity<CustomerService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC27FEAA80BC");

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
                .HasConstraintName("FK__CustomerS__Accou__6CD828CA");
        });

        modelBuilder.Entity<DiaperDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiaperDe__3214EC27AD478A42");

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
                .HasConstraintName("FK__DiaperDet__ID_Co__02FC7413");
        });

        modelBuilder.Entity<Diary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diary__3214EC278C11C27A");

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
                .HasConstraintName("FK__Diary__ID_Contac__114A936A");
        });

        modelBuilder.Entity<DietDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DietDeta__3214EC27DB79E39C");

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
                .HasConstraintName("FK__DietDetai__ID_Co__7E37BEF6");
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluate__3214EC27A837D3DF");

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
                .HasConstraintName("FK__Evaluate__Apprai__619B8048");

            entity.HasOne(d => d.EvaluatorUserAccountNavigation).WithMany(p => p.EvaluateEvaluatorUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.EvaluatorUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__Evalua__60A75C0F");
        });

        modelBuilder.Entity<FunctionSetting>(entity =>
        {
            entity.HasKey(e => new { e.GroupIdAuthGroup, e.FunctionCodeSystemFunction }).HasName("PK__Function__6316C924319FC137");

            entity.ToTable("FunctionSetting");

            entity.Property(e => e.GroupIdAuthGroup).HasColumnName("GroupId_AuthGroup");
            entity.Property(e => e.FunctionCodeSystemFunction).HasColumnName("FunctionCode_SystemFunction");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FunctionCodeSystemFunctionNavigation).WithMany(p => p.FunctionSettings)
                .HasForeignKey(d => d.FunctionCodeSystemFunction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FunctionS__Funct__1F98B2C1");

            entity.HasOne(d => d.GroupIdAuthGroupNavigation).WithMany(p => p.FunctionSettings)
                .HasForeignKey(d => d.GroupIdAuthGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FunctionS__Group__1EA48E88");
        });

        modelBuilder.Entity<GroupBuying>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27613C8098");

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
                .HasConstraintName("FK__GroupBuyi__Accou__498EEC8D");
        });

        modelBuilder.Entity<GroupBuyingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27FFEC0010");

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
                .HasConstraintName("FK__GroupBuyi__Accou__55F4C372");

            entity.HasOne(d => d.GroupBuying).WithMany(p => p.GroupBuyingDetails)
                .HasForeignKey(d => d.GroupBuyingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Group__55009F39");
        });

        modelBuilder.Entity<GroupBuyingDetailFormat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27C650E188");

            entity.ToTable("GroupBuyingDetailFormat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FormatId).HasColumnName("FormatID");

            entity.HasOne(d => d.Format).WithMany(p => p.GroupBuyingDetailFormats)
                .HasForeignKey(d => d.FormatId)
                .HasConstraintName("FK__GroupBuyi__Forma__5AB9788F");

            entity.HasOne(d => d.GroupBuyingDetail).WithMany(p => p.GroupBuyingDetailFormats)
                .HasForeignKey(d => d.GroupBuyingDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Group__59C55456");
        });

        modelBuilder.Entity<GroupBuyingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC277F8C7294");

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
                .HasConstraintName("FK__GroupBuyi__ID_Gr__51300E55");
        });

        modelBuilder.Entity<HealthInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthIn__3214EC27A01A8947");

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
                .HasConstraintName("FK__HealthInf__ID_Co__787EE5A0");
        });

        modelBuilder.Entity<Inquire>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Inquire");

            entity.Property(e => e.UserAccountinquire)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserAccountresponse)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UserAccountinquireNavigation).WithMany()
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.UserAccountinquire)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inquire__UserAcc__5DCAEF64");

            entity.HasOne(d => d.UserAccountresponseNavigation).WithMany()
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.UserAccountresponse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inquire__UserAcc__5CD6CB2B");
        });

        modelBuilder.Entity<Memo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Memo__3214EC278370C1DA");

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
                .HasConstraintName("FK__Memo__ID_Contact__0D7A0286");
        });

        modelBuilder.Entity<NannyOtherCertificate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NannyOtherCertificate");

            entity.Property(e => e.CreateddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModiifiedDateDatetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ModiifiedDateDATETIME");
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.OtherCertificate).HasMaxLength(50);
            entity.Property(e => e.OtherCertificateName).HasMaxLength(50);

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany()
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyOthe__Nanny__59063A47");
        });

        modelBuilder.Entity<NannyRequirment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyReq__3214EC275F71FA16");

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
            entity.Property(e => e.RequirementDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Statement).HasDefaultValue(1);

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.NannyRequirments)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyRequ__Nanny__6FE99F9F");
        });

        modelBuilder.Entity<NannyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyRes__3214EC279A00EA4A");

            entity.ToTable("NannyResume");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.Introduction).HasMaxLength(500);
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .HasDefaultValue("??");
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.Nickname).HasMaxLength(50);
            entity.Property(e => e.ProfessionalPortrait)
                .HasMaxLength(50)
                .HasDefaultValue("???");
            entity.Property(e => e.ServiceCenter).HasMaxLength(50);
            entity.Property(e => e.ServiceType).HasMaxLength(50);
            entity.Property(e => e.TypeOfDaycare).HasMaxLength(50);

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.NannyResumes)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyResu__Nanny__4222D4EF");
        });

        modelBuilder.Entity<NannyResumePhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyRes__3214EC276DA0F4C2");

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
                .HasConstraintName("FK__NannyResu__ID_Na__48CFD27E");
        });

        modelBuilder.Entity<OnlineCompetition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OnlineCo__3214EC27EA7CCA14");

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
                .HasConstraintName("FK__OnlineCom__Accou__236943A5");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC27C0BE8A50");

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
                .HasConstraintName("FK__Platform__Accoun__5D95E53A");
        });

        modelBuilder.Entity<PlatformFavorite>(entity =>
        {
            entity.HasKey(e => new { e.AccountUserAccount, e.IdPlatform }).HasName("PK__Platform__3AF4E25C65A24663");

            entity.ToTable("PlatformFavorite");

            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.IdPlatform).HasColumnName("ID_Platform");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.HasOne(d => d.IdPlatformNavigation).WithMany(p => p.PlatformFavorites)
                .HasForeignKey(d => d.IdPlatform)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlatformF__ID_Pl__69FBBC1F");
        });

        modelBuilder.Entity<PlatformPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC2749F56FE0");

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
                .HasConstraintName("FK__PlatformP__ID_Pl__625A9A57");
        });

        modelBuilder.Entity<PlatformResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC2773E12E7B");

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
                .HasConstraintName("FK__PlatformR__ID_Pl__662B2B3B");
        });

        modelBuilder.Entity<ProductFormat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductF__3214EC278A058F0E");

            entity.ToTable("ProductFormat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FormatName).HasMaxLength(50);
            entity.Property(e => e.FormatType).HasMaxLength(50);
            entity.Property(e => e.IdGroupBuying).HasColumnName("ID_GroupBuying");

            entity.HasOne(d => d.IdGroupBuyingNavigation).WithMany(p => p.ProductFormats)
                .HasForeignKey(d => d.IdGroupBuying)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductFo__ID_Gr__4E53A1AA");
        });

        modelBuilder.Entity<SecondHandExchangeOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecondHa__3214EC270B77213B");

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
                .HasConstraintName("FK__SecondHan__Buyer__3E1D39E1");

            entity.HasOne(d => d.Seller).WithMany(p => p.SecondHandExchangeOrderSellers)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__Selle__3F115E1A");

            entity.HasOne(d => d.WantGet).WithMany(p => p.SecondHandExchangeOrderWantGets)
                .HasForeignKey(d => d.WantGetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__WantG__40058253");

            entity.HasOne(d => d.WantGive).WithMany(p => p.SecondHandExchangeOrderWantGives)
                .HasForeignKey(d => d.WantGiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SecondHan__WantG__41EDCAC5");
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
                .HasConstraintName("FK__SecondHan__ID_Ex__44CA3770");
        });

        modelBuilder.Entity<SecondHandSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecondHa__3214EC2736FEFE77");

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
                .HasConstraintName("FK__SecondHan__Accou__3587F3E0");
        });

        modelBuilder.Entity<SleepDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SleepDet__3214EC27962F4469");

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
                .HasConstraintName("FK__SleepDeta__ID_Co__08B54D69");
        });

        modelBuilder.Entity<SuppliesPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplies__3214EC270DC699B4");

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
                .HasConstraintName("FK__SuppliesP__ID_Se__3A4CA8FD");
        });

        modelBuilder.Entity<SystemFunction>(entity =>
        {
            entity.HasKey(e => e.FunctionId).HasName("PK__SystemFu__31ABFAF81C676BF6");

            entity.ToTable("SystemFunction");

            entity.Property(e => e.FunctionName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CCAC67AF84A4");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Account, "UQ__UserAcco__B0C3AC46CD2AE628").IsUnique();

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
            entity.HasKey(e => e.UserinfoId).HasName("PK__UserInfo__E7D64B315340D344");

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
                .HasConstraintName("FK__UserInfor__Accou__3D5E1FD2");
        });

        modelBuilder.Entity<Vip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VIP__3214EC27502B6E1E");

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
                .HasConstraintName("FK__VIP__Account_Use__160F4887");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
