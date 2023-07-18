using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MiniProject319.DataModels
{
    public partial class DB_SpecificationContext : DbContext
    {
        public DB_SpecificationContext()
        {
        }

        public DB_SpecificationContext(DbContextOptions<DB_SpecificationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MAdmin> MAdmins { get; set; } = null!;
        public virtual DbSet<MBank> MBanks { get; set; } = null!;
        public virtual DbSet<MBiodataAddress> MBiodataAddresses { get; set; } = null!;
        public virtual DbSet<MBiodataAttachment> MBiodataAttachments { get; set; } = null!;
        public virtual DbSet<MBiodatum> MBiodata { get; set; } = null!;
        public virtual DbSet<MBloodGroup> MBloodGroups { get; set; } = null!;
        public virtual DbSet<MCourier> MCouriers { get; set; } = null!;
        public virtual DbSet<MCourierType> MCourierTypes { get; set; } = null!;
        public virtual DbSet<MCustomer> MCustomers { get; set; } = null!;
        public virtual DbSet<MCustomerMember> MCustomerMembers { get; set; } = null!;
        public virtual DbSet<MCustomerRelation> MCustomerRelations { get; set; } = null!;
        public virtual DbSet<MDoctor> MDoctors { get; set; } = null!;
        public virtual DbSet<MDoctorEducation> MDoctorEducations { get; set; } = null!;
        public virtual DbSet<MEducationLevel> MEducationLevels { get; set; } = null!;
        public virtual DbSet<MLocation> MLocations { get; set; } = null!;
        public virtual DbSet<MLocationLevel> MLocationLevels { get; set; } = null!;
        public virtual DbSet<MMedicalFacility> MMedicalFacilities { get; set; } = null!;
        public virtual DbSet<MMedicalFacilityCategory> MMedicalFacilityCategories { get; set; } = null!;
        public virtual DbSet<MMedicalFacilitySchedule> MMedicalFacilitySchedules { get; set; } = null!;
        public virtual DbSet<MMedicalItem> MMedicalItems { get; set; } = null!;
        public virtual DbSet<MMedicalItemCategory> MMedicalItemCategories { get; set; } = null!;
        public virtual DbSet<MMedicalItemSegmentation> MMedicalItemSegmentations { get; set; } = null!;
        public virtual DbSet<MMenu> MMenus { get; set; } = null!;
        public virtual DbSet<MMenuRole> MMenuRoles { get; set; } = null!;
        public virtual DbSet<MPaymentMethod> MPaymentMethods { get; set; } = null!;
        public virtual DbSet<MRole> MRoles { get; set; } = null!;
        public virtual DbSet<MSpecialization> MSpecializations { get; set; } = null!;
        public virtual DbSet<MUser> MUsers { get; set; } = null!;
        public virtual DbSet<MWalletDefaultNominal> MWalletDefaultNominals { get; set; } = null!;
        public virtual DbSet<TAppointmentCancellation> TAppointmentCancellations { get; set; } = null!;
        public virtual DbSet<TAppointmentDone> TAppointmentDones { get; set; } = null!;
        public virtual DbSet<TAppointmentRescheduleHistory> TAppointmentRescheduleHistories { get; set; } = null!;
        public virtual DbSet<TCourierDiscount> TCourierDiscounts { get; set; } = null!;
        public virtual DbSet<TCurrentDoctorSpecialization> TCurrentDoctorSpecializations { get; set; } = null!;
        public virtual DbSet<TCustomerChat> TCustomerChats { get; set; } = null!;
        public virtual DbSet<TCustomerChatHistory> TCustomerChatHistories { get; set; } = null!;
        public virtual DbSet<TCustomerCustomNominal> TCustomerCustomNominals { get; set; } = null!;
        public virtual DbSet<TCustomerRegisteredCard> TCustomerRegisteredCards { get; set; } = null!;
        public virtual DbSet<TCustomerVa> TCustomerVas { get; set; } = null!;
        public virtual DbSet<TCustomerVaHistory> TCustomerVaHistories { get; set; } = null!;
        public virtual DbSet<TCustomerWallet> TCustomerWallets { get; set; } = null!;
        public virtual DbSet<TCustomerWalletTopUp> TCustomerWalletTopUps { get; set; } = null!;
        public virtual DbSet<TCustomerWalletWithdraw> TCustomerWalletWithdraws { get; set; } = null!;
        public virtual DbSet<TDoctorOffice> TDoctorOffices { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeSchedule> TDoctorOfficeSchedules { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeTreatment> TDoctorOfficeTreatments { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPrices { get; set; } = null!;
        public virtual DbSet<TDoctorTreatment> TDoctorTreatments { get; set; } = null!;
        public virtual DbSet<TMedicalItemPurchase> TMedicalItemPurchases { get; set; } = null!;
        public virtual DbSet<TMedicalItemPurchaseDetail> TMedicalItemPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TPrescription> TPrescriptions { get; set; } = null!;
        public virtual DbSet<TResetPassword> TResetPasswords { get; set; } = null!;
        public virtual DbSet<TToken> TTokens { get; set; } = null!;
        public virtual DbSet<TTreatmentDiscount> TTreatmentDiscounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ETG-XALNV00021-;Initial Catalog=DB_Specification; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
