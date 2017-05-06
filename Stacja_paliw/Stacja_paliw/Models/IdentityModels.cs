using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Stacja_paliw.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here           
            return userIdentity;
        }

        public virtual UserInfo MyUserInfo { get; set; }
        public virtual LoyalityStatus LoyalStatus { get; set; } 
    }

    public class UserInfo
    {
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string Address { get; set; }
        public long NIP_Regon { get; set; }
    }

    public class LoyalityStatus
    {
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int LifetimePts { get; set; }
        public int CurrPts { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<UserInfo> MyUserInfo { get; set; }
        public System.Data.Entity.DbSet<LoyalityStatus> LoyalStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(m => m.MyUserInfo)
                .WithRequired(m => m.ApplicationUser)
                .Map(p => p.MapKey("MyUserInfo_Id"));

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(m => m.LoyalStatus)
                .WithRequired(m => m.ApplicationUser)
                .Map(p => p.MapKey("AspUser_Id"));
        }
    }
}