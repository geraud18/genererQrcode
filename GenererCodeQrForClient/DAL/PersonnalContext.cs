using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenererCodeQrForClient.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GenererCodeQrForClient.DAL
{
    public class PersonnalContext : DbContext
    {
        public PersonnalContext() : base("PersonnalContext")
        {
        }

        public DbSet<PersonnelModel> Personnel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}