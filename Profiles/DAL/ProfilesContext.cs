using Profiles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Profiles.DAL
{
    public class ProfilesContext:DbContext
    {
        public ProfilesContext()
            : base("ProfilesContext")
        {
            Database.SetInitializer<ProfilesContext>(null);
        }

        public DbSet<RPC> RPC { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Skill> Skill { get; set; }

        public DbSet<Experience> Experience { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Living> Living { get; set; }
        public DbSet<Link> Link { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}