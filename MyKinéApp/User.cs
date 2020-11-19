using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKinéApp
{
    enum UserRole
    {
        Kine,
        Sectretaire
    }

    class User
    {
        public string Username;
        public UserRole Role;
    }
}
