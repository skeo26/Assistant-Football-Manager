using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public class Club
    {
        private string name;
        private string country;
        private double balance;
        private TeamMananger coach;
        private int countPlayers;
        private List<Player> teamPlayers;

        public string Name { get { return name; }  }
    
        public string Country { get { return country; } }

        public double Balance { get { return balance; } }

        public TeamMananger Coach { get { return coach; } }

        public int CountPlayers { get { return countPlayers; } }

        public List<Player> TeamPlayers { get { return teamPlayers; } }

        public ITransferService TransferService { get; set; }

        public void ChangeBalance(double amount)
        {
            balance += amount;
        }

        public void ChangeCountPlayers(int newPlayer)
        {
            countPlayers += newPlayer;
        }

        public void SetPlayers(List<Player> players)
        {
            this.teamPlayers = players;
        }

        public void AddPlayer(Player player)
        {
            teamPlayers.Add(player);
        }
       
        public async Task<bool> TryTransferPlayer(Player targetPlayer, double offerAmount, double salaryOffer)
        {
            if (TransferService == null)
                throw new InvalidOperationException("TransferService не установлен.");

            bool success = TransferService.RequestPlayerTransfer(this, targetPlayer, offerAmount, salaryOffer);
            await Task.Delay(5000);
            return success;
        }

        public override string ToString()
        {
            return Name;
        }

        private Club() { }

        public class Builder
        {
            private readonly Club club = new Club();

            public Builder SetName(string name) { club.name = name; return this; }
            public Builder SetCountry(string country) { club.country = country; return this; }
            public Builder SetBalance(double balance) { club.balance = balance; return this; }
            public Builder SetCoach(TeamMananger coach) { club.coach = coach; return this; }
            public Builder SetCountPlayers(int countPlayers) { club.countPlayers = countPlayers; return this; }
            public Builder SetTeamPlayers(List<Player> teamPlayers) {  club.teamPlayers = teamPlayers; return this; }

            public Club Build()
            {
                return club;
            }
        }
    }
}
