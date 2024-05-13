

using TMHWPF.Business;

namespace TestsBusinessLogic
{
    public class TestsForTransferMarket
    {
        [Fact]
        public void AttemptToSignPlayer_WhenPlayerIsNotReadyToTransfer()
        {
            //Arrange
            var player = new Player.Builder()
                .SetMarketValue(1000000)
                .SetSalary(25000)
                .SetReadyToTransfer(false) 
                .Build();

            var club = new Club.Builder()
                .Build();


            var transferRequest = new TransferRequest(club, player, 1500000, 30000);

            //Act
            bool result = TransferMarket.Instance.AttemptToSignPlayer(transferRequest);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AttemptToSignPlayer_WhenPriceBelowTheMarket()
        {
            //Arrange
            var player = new Player.Builder()
                .SetMarketValue(1000000)
                .SetSalary(25000)
                .SetReadyToTransfer(true)
                .Build();

            var club = new Club.Builder()
                .Build();


            var transferRequest = new TransferRequest(club, player, 900000, 30000);

            //Act
            bool result = TransferMarket.Instance.AttemptToSignPlayer(transferRequest);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AttemptToSignPlayer_WhenSalaryBelowTheMarket()
        {
            //Arrange
            var player = new Player.Builder()
                .SetMarketValue(1000000)
                .SetSalary(25000)
                .SetReadyToTransfer(true)
                .Build();

            var club = new Club.Builder()
                .Build();


            var transferRequest = new TransferRequest(club, player, 1500000, 20000);

            //Act
            bool result = TransferMarket.Instance.AttemptToSignPlayer(transferRequest);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AttemptToSignPlayer_ReturnTrue()
        {
            //Arrange
            var player = new Player.Builder()
                .SetMarketValue(1000000)
                .SetSalary(25000)
                .SetReadyToTransfer(true)
                .Build();

            var club = new Club.Builder()
                .Build();


            var transferRequest = new TransferRequest(club, player, 1500000, 30000);

            //Act
            bool result = TransferMarket.Instance.AttemptToSignPlayer(transferRequest);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ProcessTransferRequest_WithValidConditions()
        {
            // Arrange
            var sellingClub = new Club.Builder()
                .SetBalance(100000000)
                .Build();
            var buyingClub = new Club.Builder()
                .SetBalance(550000000)
                .Build();
            var player = new Player.Builder()
                .SetTeam(sellingClub)
                .SetMarketValue(25000000)
                .SetSalary(250000)
                .SetReadyToTransfer(true)
                .Build();

            var request = new TransferRequest(buyingClub, player, 30000000, 400000);

            // Act
            var response = TransferMarket.Instance.ProcessTransferRequest(request);

            // Assert
            Assert.True(response.IsApproved);
        }

        [Fact]
        public void ProcessTransferRequest_WithInValidConditions()
        {
            // Arrange
            var sellingClub = new Club.Builder()
                .SetBalance(100000000)
                .Build();
            var buyingClub = new Club.Builder()
                .SetBalance(550000000)
                .Build();
            var player = new Player.Builder()
                .SetTeam(sellingClub)
                .SetMarketValue(25000000)
                .SetSalary(250000)
                .SetReadyToTransfer(true)
                .Build();

            var request = new TransferRequest(buyingClub, player, 550000000, 250000);

            // Act
            var response = TransferMarket.Instance.ProcessTransferRequest(request);

            // Assert
            Assert.False(response.IsApproved);
        }

        [Fact]
        public void IsEnoughBalance_WhenClubHasEnoughMoney()
        {
            // Arrange
            var club = new Club.Builder()
                .SetBalance(170000000)
                .Build();

            // Act
            bool result = TransferMarket.Instance.IsEnoughBalance(club, 10000000);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEnoughBalance_WhenClubHasNotEnoughMoney()
        {
            // Arrange
            var club = new Club.Builder()
                .SetBalance(160000000)
                .Build();

            // Act
            bool result = TransferMarket.Instance.IsEnoughBalance(club, 13000000);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void NumberOfPlayersAllowsTransfer_WhenClubHasMaximumPlayers()
        {
            // Arrange
            var club = new Club.Builder()
                .SetCountPlayers(28)
                .Build();

            // Act
            bool result = TransferMarket.Instance.NumberOfPlayersNotAllowsTransfer(club);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void NumberOfPlayersAllowsTransfer_WhenClubHasLessThanMaximumPlayers()
        {
            // Arrange
            var club = new Club.Builder()
                .SetCountPlayers(24)
                .Build();

            // Act
            bool result = TransferMarket.Instance.NumberOfPlayersNotAllowsTransfer(club);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void NumberOfPlayersAllowsTransfer_WhenClubHasMoreThanMaximumPlayers()
        {
            // Arrange
            var club = new Club.Builder()
                .SetCountPlayers(28)
                .Build();

            // Act
            bool result = TransferMarket.Instance.NumberOfPlayersNotAllowsTransfer(club);

            // Assert
            Assert.True(result);
        }
    }
}
