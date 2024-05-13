using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace TestsBusinessLogic
{
    public class TransferServiceTests
    {
        [Fact]
        public void RequestPlayerTransfer_SuccessfulTransfer_ReturnsTrue()
        {
            // Arrange
            var transferMarket = TransferMarket.Instance;
            var transferService = new TransferService(transferMarket);

            var interestedClub = new Club.Builder()
                .SetBalance(1000000000)
                .SetCountPlayers(24)
                .Build();

            var club = new Club.Builder()
               .SetName("Fulham")
               .SetCountry("England")
               .SetBalance(250000000)
               .SetCountPlayers(24)
               .SetTeamPlayers(new List<Player>())
               .Build();

            var targetPlayer = new Player.Builder()
                .SetReadyToTransfer(true)
                .SetMarketValue(650000)
                .SetTeam(club)
                .Build();

            var offerAmount = 1000000;
            var salaryOffer = 50000;

            // Act
            var result = transferService.RequestPlayerTransfer(interestedClub, targetPlayer, offerAmount, salaryOffer);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RequestPlayerTransfer_UnsuccessfulTransfer_ReturnsFalse()
        {
            // Arrange
            var transferMarket = TransferMarket.Instance;
            var transferService = new TransferService(transferMarket);
            var interestedClub = new Club.Builder().Build();
            var targetPlayer = new Player.Builder().Build();
            var offerAmount = 10000; 
            var salaryOffer = 50000; 

            // Act
            var result = transferService.RequestPlayerTransfer(interestedClub, targetPlayer, offerAmount, salaryOffer);

            // Assert
            Assert.False(result);
        }
    }
}
