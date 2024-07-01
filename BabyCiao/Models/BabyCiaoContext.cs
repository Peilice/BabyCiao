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

    public virtual DbSet<AuthGroup> AuthGroups { get; set; }

    public virtual DbSet<BabyResume> BabyResumes { get; set; }

    public virtual DbSet<CompetitionDetail> CompetitionDetails { get; set; }

    public virtual DbSet<CompetitionPhoto> CompetitionPhotos { get; set; }

    public virtual DbSet<CompetitionRecord> CompetitionRecords { get; set; }

    public virtual DbSet<ContactBook> ContactBooks { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<DiaperDetail> DiaperDetails { get; set; }

    public virtual DbSet<Diary> Diaries { get; set; }

    public virtual DbSet<DietDetail> DietDetails { get; set; }

    public virtual DbSet<Evaluate> Evaluates { get; set; }

    public virtual DbSet<ExchangeOrder> ExchangeOrders { get; set; }

    public virtual DbSet<ExchangeOrderDetail> ExchangeOrderDetails { get; set; }

    public virtual DbSet<FunctionSetting> FunctionSettings { get; set; }

    public virtual DbSet<GroupBuying> GroupBuyings { get; set; }

    public virtual DbSet<GroupBuyingDetail> GroupBuyingDetails { get; set; }

    public virtual DbSet<GroupBuyingPhoto> GroupBuyingPhotos { get; set; }

    public virtual DbSet<HealthInformation> HealthInformations { get; set; }

    public virtual DbSet<Memo> Memos { get; set; }

    public virtual DbSet<NannyRequirment> NannyRequirments { get; set; }

    public virtual DbSet<NannyResume> NannyResumes { get; set; }

    public virtual DbSet<OnlineCompetition> OnlineCompetitions { get; set; }

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

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=BabyCiao;Integrated Security=true;TrustServerCertificate=true;Encrypt=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC27C5545393");

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
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.Announcements)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Announcem__Accou__44FF419A");
        });

        modelBuilder.Entity<AnnouncementPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC276B6993FB");

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
                .HasConstraintName("FK__Announcem__ID_An__49C3F6B7");
        });

        modelBuilder.Entity<AuthGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__AuthGrou__149AF36A0A918665");

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
                .HasConstraintName("FK__AuthGroup__Modif__778AC167");
        });

        modelBuilder.Entity<BabyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BabyResu__3214EC2783367EFE");

            entity.ToTable("BabyResume");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.ApplyDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.Display).HasDefaultValue(true);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Memo).HasMaxLength(500);
            entity.Property(e => e.Photo).HasMaxLength(200);
            entity.Property(e => e.TimeSlot).HasMaxLength(10);
            entity.Property(e => e.TypeOfDaycare).HasMaxLength(10);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.BabyResumes)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BabyResum__Accou__31EC6D26");
        });

        modelBuilder.Entity<CompetitionDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC27F3980427");

            entity.ToTable("CompetitionDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.CompetitionPhoto).HasMaxLength(500);
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.IdOnlineCompetition).HasColumnName("ID_OnlineCompetition");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdOnlineCompetitionNavigation).WithMany(p => p.CompetitionDetails)
                .HasForeignKey(d => d.IdOnlineCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__ID_On__09A971A2");
        });

        modelBuilder.Entity<CompetitionPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC277D4E794A");

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
                .HasConstraintName("FK__Competiti__ID_On__05D8E0BE");
        });

        modelBuilder.Entity<CompetitionRecord>(entity =>
        {
            entity.HasKey(e => new { e.VoterAccount, e.IdCompetitionDetail }).HasName("PK__Competit__9202720EBCB6FF5F");

            entity.ToTable("CompetitionRecord");

            entity.Property(e => e.VoterAccount)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.HasKey(e => e.Id).HasName("PK__ContactB__3214EC2738A9B6DE");

            entity.ToTable("ContactBook");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BabyName).HasMaxLength(50);
            entity.Property(e => e.BloodType)
                .HasMaxLength(5)
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
                .HasConstraintName("FK__ContactBo__Paren__52593CB8");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__5E2E73FA94D1649A");

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
                .HasConstraintName("FK__Contract__Accoun__38996AB5");

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.ContractNannyAccountUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__NannyA__36B12243");
        });

        modelBuilder.Entity<DiaperDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiaperDe__3214EC271A2BDF0C");

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
                .HasConstraintName("FK__DiaperDet__ID_Co__619B8048");
        });

        modelBuilder.Entity<Diary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diary__3214EC2717E4FEF8");

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
                .HasConstraintName("FK__Diary__ID_Contac__6FE99F9F");
        });

        modelBuilder.Entity<DietDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DietDeta__3214EC27E276DC70");

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
                .HasConstraintName("FK__DietDetai__ID_Co__5CD6CB2B");
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluate__3214EC272CAC4AB6");

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
                .HasConstraintName("FK__Evaluate__Apprai__403A8C7D");

            entity.HasOne(d => d.EvaluatorUserAccountNavigation).WithMany(p => p.EvaluateEvaluatorUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.EvaluatorUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__Evalua__3F466844");
        });

        modelBuilder.Entity<ExchangeOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exchange__3214EC279FE7747D");

            entity.ToTable("ExchangeOrder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountAUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AccountA_UserAccount");
            entity.Property(e => e.AccountBUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AccountB_UserAccount");
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");
            entity.Property(e => e.Statement).HasMaxLength(20);

            entity.HasOne(d => d.AccountAUserAccountNavigation).WithMany(p => p.ExchangeOrderAccountAUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountAUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeO__Accou__19DFD96B");

            entity.HasOne(d => d.AccountBUserAccountNavigation).WithMany(p => p.ExchangeOrderAccountBUserAccountNavigations)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountBUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeO__Accou__1AD3FDA4");
        });

        modelBuilder.Entity<ExchangeOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.IdExchangeOrder, e.IdSecondHandSupplies }).HasName("PK__Exchange__4C80FCD507C79D16");

            entity.ToTable("ExchangeOrderDetail");

            entity.Property(e => e.IdExchangeOrder).HasColumnName("ID_ExchangeOrder");
            entity.Property(e => e.IdSecondHandSupplies).HasColumnName("ID_SecondHandSupplies");

            entity.HasOne(d => d.IdExchangeOrderNavigation).WithMany(p => p.ExchangeOrderDetails)
                .HasForeignKey(d => d.IdExchangeOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeO__ID_Ex__1DB06A4F");

            entity.HasOne(d => d.IdSecondHandSuppliesNavigation).WithMany(p => p.ExchangeOrderDetails)
                .HasForeignKey(d => d.IdSecondHandSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeO__ID_Se__1EA48E88");
        });

        modelBuilder.Entity<FunctionSetting>(entity =>
        {
            entity.HasKey(e => new { e.GroupIdAuthGroup, e.FunctionCodeSystemFunction }).HasName("PK__Function__6316C924A0433A9B");

            entity.ToTable("FunctionSetting");

            entity.Property(e => e.GroupIdAuthGroup).HasColumnName("GroupId_AuthGroup");
            entity.Property(e => e.FunctionCodeSystemFunction).HasColumnName("FunctionCode_SystemFunction");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FunctionCodeSystemFunctionNavigation).WithMany(p => p.FunctionSettings)
                .HasForeignKey(d => d.FunctionCodeSystemFunction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FunctionS__Funct__7E37BEF6");

            entity.HasOne(d => d.GroupIdAuthGroupNavigation).WithMany(p => p.FunctionSettings)
                .HasForeignKey(d => d.GroupIdAuthGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FunctionS__Group__7D439ABD");
        });

        modelBuilder.Entity<GroupBuying>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27AAEA4F83");

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
            entity.Property(e => e.Statement).HasMaxLength(20);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.GroupBuyings)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Accou__2180FB33");
        });

        modelBuilder.Entity<GroupBuyingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27743FA319");

            entity.ToTable("GroupBuyingDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_UserAccount");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Statement).HasMaxLength(20);

            entity.HasOne(d => d.AccountUserAccountNavigation).WithMany(p => p.GroupBuyingDetails)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.AccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Accou__3D2915A8");

            entity.HasOne(d => d.GroupBuying).WithMany(p => p.GroupBuyingDetails)
                .HasForeignKey(d => d.GroupBuyingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBuyi__Group__3C34F16F");
        });

        modelBuilder.Entity<GroupBuyingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupBuy__3214EC27E38A4DB9");

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
                .HasConstraintName("FK__GroupBuyi__ID_Gr__2645B050");
        });

        modelBuilder.Entity<HealthInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthIn__3214EC279A18E802");

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
                .HasConstraintName("FK__HealthInf__ID_Co__571DF1D5");
        });

        modelBuilder.Entity<Memo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Memo__3214EC27EDFF9256");

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
                .HasConstraintName("FK__Memo__ID_Contact__6C190EBB");
        });

        modelBuilder.Entity<NannyRequirment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyReq__3214EC27EDCF259D");

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
                .HasConstraintName("FK__NannyRequ__Nanny__4E88ABD4");
        });

        modelBuilder.Entity<NannyResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NannyRes__3214EC2749733391");

            entity.ToTable("NannyResume");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.DisplayControl).HasDefaultValue(true);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.InternalPhoto1).HasMaxLength(200);
            entity.Property(e => e.InternalPhoto2).HasMaxLength(200);
            entity.Property(e => e.InternalPhoto3).HasMaxLength(200);
            entity.Property(e => e.InternalPhoto4).HasMaxLength(200);
            entity.Property(e => e.InternalPhoto5).HasMaxLength(200);
            entity.Property(e => e.Introduction).HasMaxLength(500);
            entity.Property(e => e.Language).HasMaxLength(10);
            entity.Property(e => e.NannyAccountUserAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NannyAccount_UserAccount");
            entity.Property(e => e.ProfessionalPortrait).HasMaxLength(200);
            entity.Property(e => e.ServiceCenter).HasMaxLength(50);
            entity.Property(e => e.ServiceItems).HasMaxLength(10);
            entity.Property(e => e.TypeOfDaycare).HasMaxLength(10);

            entity.HasOne(d => d.NannyAccountUserAccountNavigation).WithMany(p => p.NannyResumes)
                .HasPrincipalKey(p => p.Account)
                .HasForeignKey(d => d.NannyAccountUserAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NannyResu__Nanny__2C3393D0");
        });

        modelBuilder.Entity<OnlineCompetition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OnlineCo__3214EC273FD36F55");

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
                .HasConstraintName("FK__OnlineCom__Accou__02084FDA");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC27B2612B82");

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
                .HasConstraintName("FK__Platform__Accoun__2DE6D218");
        });

        modelBuilder.Entity<PlatformPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC278B1A2BBE");

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
                .HasConstraintName("FK__PlatformP__ID_Pl__32AB8735");
        });

        modelBuilder.Entity<PlatformResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC2704C467FD");

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
                .HasConstraintName("FK__PlatformR__ID_Pl__367C1819");
        });

        modelBuilder.Entity<SecondHandSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecondHa__3214EC2715C51358");

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
                .HasConstraintName("FK__SecondHan__Accou__114A936A");
        });

        modelBuilder.Entity<SleepDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SleepDet__3214EC275DDFBB79");

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
                .HasConstraintName("FK__SleepDeta__ID_Co__6754599E");
        });

        modelBuilder.Entity<SuppliesPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplies__3214EC277F0ABA91");

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
                .HasConstraintName("FK__SuppliesP__ID_Se__160F4887");
        });

        modelBuilder.Entity<SystemFunction>(entity =>
        {
            entity.HasKey(e => e.FunctionId).HasName("PK__SystemFu__31ABFAF8E1109EAF");

            entity.ToTable("SystemFunction");

            entity.Property(e => e.FunctionName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CCAC2559091D");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Account, "UQ__UserAcco__B0C3AC46FE219342").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
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
            entity.HasKey(e => e.UserinfoId).HasName("PK__UserInfo__E7D64B319D389F85");

            entity.ToTable("UserInformation");

            entity.Property(e => e.UserinfoId).HasColumnName("UserinfoID");
            entity.Property(e => e.AccountUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Account_User");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
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
            entity.HasKey(e => e.Id).HasName("PK__VIP__3214EC27D2627FD2");

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
                .HasConstraintName("FK__VIP__Account_Use__74AE54BC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
