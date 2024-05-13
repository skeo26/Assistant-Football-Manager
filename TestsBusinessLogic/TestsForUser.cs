using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace TestsBusinessLogic
{
    public class TestsForUser
    {
        [Fact]
        public void UserBuilder_InitializePropertiesCorrectly()
        {
            //Arrange and Act
            var user = new User.Builder()
                .SetLogin("harry")
                .SetPassword("maguire")
                .SetFirstName("Eric")
                .SetLastName("tenHag")
                .Build();

            //Assert
            Assert.Equal("harry", user.Login);
            Assert.Equal("maguire", user.Password);
            Assert.Equal("Eric", user.FirstName);
            Assert.Equal("tenHag", user.LastName);
        }
    }
}
