using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public class TeamMananger
    {
        private string firstName;
        private string lastName;
        private int age;
        private Club team;
        private List<Player> playersTeam;
        

        public string FirstName { get { return firstName; } }
        public string LastName { get { return lastName; } }
        public int Age { get { return age;} }
        public Club Team { get { return team;} }
        public List<Player> PlayersTeam { get { return playersTeam; } } 

        private TeamMananger() { }


        public class Builder
        {
            private readonly TeamMananger teamManager = new TeamMananger();

            public Builder SetFirstName(string firstName) { teamManager.firstName = firstName; return this; }
            public Builder SetLastName(string lastName) { teamManager.lastName = lastName; return this; }
            public Builder SetAge(int age) { teamManager.age = age; return this; }
            public Builder SetPlayersTeam (List<Player> playersTeam) {  teamManager.playersTeam = playersTeam; return this; }
            public Builder SetClub(Club club) { teamManager.team = club; return this; }

            public TeamMananger Build()
            {
                return teamManager;
            }
        }
    }
}
