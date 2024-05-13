using TMHWPF.Business;
namespace TestsBusinessLogic
{
    public class TestsForClub
    {
        [Fact]
        public void ClubBuiledr_InitializePropertiesCorrectly()
        {
            //Arrange
            var coach = new TeamMananger.Builder()
                .SetFirstName("Marco")
                .SetLastName("Silva")
                .SetAge(46)
                .Build();

            var club = new Club.Builder()
                .SetName("Fulham")
                .SetCountry("England")
                .SetBalance(250000000)
                .SetCoach(coach)
                .SetCountPlayers(24)
                .SetTeamPlayers(new List<Player>())
                .Build();

            //Act and Assert
            Assert.Equal("Fulham", club.Name);
            Assert.Equal("England", club.Country);
            Assert.Equal(250000000, club.Balance);
            Assert.NotNull(coach);
            Assert.Equal(24, club.CountPlayers);
            Assert.Empty(club.TeamPlayers);
        }

        [Fact]
        public void ChangeBalance_Correcly()
        {
            //Arrange
            var club = new Club.Builder()
                .SetBalance(10000)
                .Build();

            //Act
            club.ChangeBalance(5000);

            //Assert
            Assert.Equal(15000, club.Balance);
        }

        [Fact]
        public void ChangeCountPlayers_Correctly()
        {
            // Arrange
            var club = new Club.Builder()
                        .SetCountPlayers(20)
                        .Build();

            // Act
            club.ChangeCountPlayers(1);

            // Assert
            Assert.Equal(21, club.CountPlayers);
        }

        [Fact]
        public void AddPlayerToList()
        {
            //Arrange
            var club = new Club.Builder()
                .SetTeamPlayers(new List<Player>())
                .Build();

            var player = new Player.Builder()
                .Build();

            //Act
            club.AddPlayer(player);

            //Assert
            Assert.Contains(player, club.TeamPlayers);
            Assert.Single(club.TeamPlayers);
        }

        [Fact]
        public void TryTransferPlayer_ReturnThrowsException()
        {
            //Arrange
            var club = new Club.Builder()
                .Build();
            var player = new Player.Builder()
                .Build();

            //Act
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => club.TryTransferPlayer(player, 100000, 10000));
            Assert.Equal("TransferService не установлен.", ex.Result.Message);
        }
        [Fact]
        public async void TryTransferPlayer_ReturnSucceeds()
        {
            //Arrange
            var club = new Club.Builder()
                .Build();

            var player = new Player.Builder()
                .Build();

            club.TransferService = new StubTransferService(true);

            //Act
            bool result = await club.TryTransferPlayer(player, 100000, 10000);

            //Assert
            Assert.True(result);
        }

        public class StubTransferService : ITransferService
        {
            private bool transferResult;

            public StubTransferService(bool transferResult)
            {
                this.transferResult = transferResult;
            }

            public bool RequestPlayerTransfer(Club club, Player targetPlayer, double offerAmount, double salaryOffer)
            {
                return transferResult;
            }
        }
    }
}