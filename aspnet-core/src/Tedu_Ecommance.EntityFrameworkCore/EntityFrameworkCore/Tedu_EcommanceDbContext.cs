using Microsoft.EntityFrameworkCore;
using Tedu_Ecommance.ProductAttributes;
using Tedu_Ecommance.Inventories;
using Tedu_Ecommance.InventoryTickets;
using Tedu_Ecommance.ManuFacturers;
using Tedu_Ecommance.Orders;
using Tedu_Ecommance.ProductCategories;
using Tedu_Ecommance.Products;
using Tedu_Ecommance.Promotions;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Tedu_Ecommance.Configurations.ProductAttributes;
using Tedu_Ecommance.Configurations.Orders;
using Tedu_Ecommance.Configurations.Products;

namespace Tedu_Ecommance.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class Tedu_EcommanceDbContext :
    AbpDbContext<Tedu_EcommanceDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }


    // Ecomance-------------------------------------------------------
    public DbSet<ProductAttribute> ProductAttribute { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryTicket> InventoryTickets { get; set; }
    public DbSet<InventoryTicketItems> InventoryTicketItems { get; set; }
    public DbSet<ManuFacturer> ManuFacturers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<OrderTransactions> orderTransactions { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAttributeDateTime> ProductAttributeDateTimes { get; set; }
    public DbSet<ProductAttributeDecimal> ProductAttributeDecimals { get; set; }
    public DbSet<ProductAttributeInt> productAttributeInts { get; set; }
    public DbSet<ProductAttributeText> productAttributeTexts { get; set; }
    public DbSet<ProductAttributeVarchar> ProductAttributeVarchars { get; set; }
    public DbSet<ProductLinks> ProductLinks { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<ProductTags> productTags { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionCategories> promotionCategories { get; set; }
    public DbSet<PromotionManufactures> promotionManufactures { get; set; }
    public DbSet<PromotionProducts> promotionProducts { get; set; }
    public DbSet<PromotionUsageHistories> promotionUsageHistories { get; set; }

    #endregion


    public Tedu_EcommanceDbContext(DbContextOptions<Tedu_EcommanceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.ApplyConfiguration(new InventoryConfiguration());
        builder.ApplyConfiguration(new ProductAttributeConfiguration());
        builder.ApplyConfiguration(new InventoryTicketConfiguration());
        builder.ApplyConfiguration(new InventoryTicketItemConfiguration());
        builder.ApplyConfiguration(new ManuFacturerConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderItemConfiguration());
        builder.ApplyConfiguration(new OrderTransactionConfiguration());
        builder.ApplyConfiguration(new ProductCategoryConfiguration());
        builder.ApplyConfiguration(new ProductAttributeDateTimeConfiguration());
        builder.ApplyConfiguration(new ProductAttributeDecimalConfiguration());
        builder.ApplyConfiguration(new ProductAttributeIntConfiguration());
        builder.ApplyConfiguration(new ProductAttributeTextConfiguration());
        builder.ApplyConfiguration(new ProductAttributeVarcharConfiguration());
        builder.ApplyConfiguration(new ProductLinkConfiguration());
        builder.ApplyConfiguration(new ProductReviewConfiguration());
        builder.ApplyConfiguration(new ProductTagConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new PromotionCategoryConfiguration());
        builder.ApplyConfiguration(new PromotionManuFacturerConfiguration());
        builder.ApplyConfiguration(new PromotionProductConfiguration());
        builder.ApplyConfiguration(new PromotionUsageHistoryConfiguration());
    }
}