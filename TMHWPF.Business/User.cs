using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public class User
    {
        private string login;
        private string password;
        private string firstName;
        private string lastName;

        public string Login { get { return login; } }
        public string Password { get { return password; } }
        public string FirstName { get { return firstName; } }
        public string LastName { get { return lastName; } }


        private User() { }

        public class Builder
        {
            private readonly User user = new User();

            public Builder SetLogin(string login) { user.login = login; return this; }
            public Builder SetPassword(string password) { user.password = password; return this; }
            public Builder SetFirstName(string firstName) { user.firstName = firstName; return this; }
            public Builder SetLastName(string lastName) { user.lastName = lastName; return this; }

            public User Build()
            {
                return user;
            }
        }
    }
}

