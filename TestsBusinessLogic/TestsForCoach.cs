using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace TestsBusinessLogic
{
    public class TestsForCoach
    {
        [Fact]
        public void TeamManangerBuilder_InitializePropertiesCorrectly()
        {
            //Arrange and Act
            var club = new Club.Builder()
                .SetName("Fulham")
                .SetCountry("England")
                .Build();


            var coach = new TeamMananger.Builder()
                .SetFirstName("Marco")
                .SetLastName("Silva")
                .SetAge(46)
                .SetClub(club)
                .SetPlayersTeam(new List<Player>())
                .Build();

            //Assert
            Assert.Equal("Marco", coach.FirstName);
            Assert.Equal("Silva", coach.LastName);
            Assert.Equal(46, coach.Age);
            Assert.Equal("Fulham", coach.Team.Name);
            Assert.Equal(new List<Player>(), coach.PlayersTeam);
        }
    }
}
