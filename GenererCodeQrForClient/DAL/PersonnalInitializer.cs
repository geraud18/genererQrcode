using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GenererCodeQrForClient.Models;

namespace GenererCodeQrForClient.DAL
{
    public class PersonnalInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PersonnalContext>
    {
        protected override void Seed(PersonnalContext context)
        {
            var personnel = new List<PersonnelModel>
            {
            new PersonnelModel{Id=12,FirstName="Alexander",LastName="Carson",Email="carson@gmail.com",Number="1234"},
            new PersonnelModel{Id=44,FirstName="Anand",LastName="Arturo",Email="arturo@gmail.com",Number="1265334"},
            new PersonnelModel{Id=66,FirstName="Barzdukas",LastName="Gytis",Email="gytis@gmail.com",Number="1009234"},
            new PersonnelModel{Id=55,FirstName="Li",LastName="Yan",Email="yan@gmail.com",Number="671234"},
            new PersonnelModel{Id=67,FirstName="Justice",LastName="Peggy",Email="peggy@gmail.com",Number="127634"},
           
            };

            personnel.ForEach(s => context.Personnel.Add(s));
            context.SaveChanges();
            
        }
    }
}