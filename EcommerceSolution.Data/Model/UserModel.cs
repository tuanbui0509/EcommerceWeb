using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Data.Model
{
    public class UserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Avatar { get; set; }

        //Date of birth
        public DateTime Dob { get; set; }
    }
}
