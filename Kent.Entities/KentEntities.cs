using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities
{
    public class KentEntities : DbContext
    {
        public KentEntities() : base("name=KentDatabase")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<HeaderTemplate> HeaderTemplates { get; set; }
        public DbSet<FooterTemplate> FooterTemplates { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Menu> Menus { get; set; }

        #region English Testing
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionSection> QuestionSections { get; set; }
        public DbSet<QuestionKit> QuestionKits { get; set; }
        public DbSet<QuestionTemplate> QuestionTemplates { get; set; }
        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<HistoryRow>().ToTable(tableName: "MigrationHistory", schemaName: "admin");
            //modelBuilder.Entity<HistoryRow>().Property(p => p.MigrationId).HasColumnName("Migration_ID");
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}
