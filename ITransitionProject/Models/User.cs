using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class User : IdentityUser
    {
        public int intId { get; set; }

        public string NotLoginName { get; set; }
        public List<Collection> Collections { get; set; }
    }
}
