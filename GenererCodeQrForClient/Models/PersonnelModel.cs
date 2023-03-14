using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GenererCodeQrForClient.Models
{
    public class PersonnelModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Number { get; set; }

    }
}