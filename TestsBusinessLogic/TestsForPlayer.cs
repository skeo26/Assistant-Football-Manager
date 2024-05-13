using TMHWPF.Business;

namespace TestsBusinessLogic
{
    public class TestsForPlayer
    {
        [Fact]
        public void PlayerBuilder_InitializePropertiesCorrectly()
        {
            //Arrange and Act
            var player = new Player.Builder()
                .SetFirstName("Alejandro")
                .SetLastName("Garnacho")
                .SetNationality("Argentina")
                .SetAge(19)
                .SetSkills(83)
                .SetPosition("LW")
                .SetSalary(63000)
                .SetMarketValue(40000000)
                .SetReadyToTransfer(false)
                .Build();

            //Assert
            Assert.Equal("Alejandro", player.FirstName);
            Assert.Equal("Garnacho", player.LastName);
            Assert.Equal("Argentina", player.Nationality);
            Assert.Equal(19, player.Age);
            Assert.Equal(83, player.Skills);
            Assert.Equal("LW", player.Position);
            Assert.Equal(63000, player.Salary);
            Assert.Equal(40000000, player.MarketValue);
            Assert.False(player.IsReadyToTransfer);
        }

        [Fact]
        public void ChangeTeamProperty()
        {
            // Arrange
            var player = new Player.Builder()
                .Build();
            var newTeam = new Club.Builder().SetName("West Ham")
                .Build();

            // Act
            player.ChangeTeam(newTeam);

            // Assert
            Assert.Equal(newTeam, player.Team);
        }

        [Fact]
        public void ChangeSalaryProperty()
        {
            // Arrange
            var player = new Player.Builder()
                .SetSalary(300000)
                .Build();

            // Act
            player.ChangeSalary(400000);

            // Assert
            Assert.Equal(400000, player.Salary);
        }

        [Fact]
        public void SetReadyToTransferProperty()
        {
            // Arrange
            var player = new Player.Builder()
                .SetReadyToTransfer(false)
                .Build();

            // Act
            player.SetReadyToTransfer(true);

            // Assert
            Assert.True(player.IsReadyToTransfer);
        }

        [Fact]
        public void ChangeMarketValueProperty()
        {
            // Arrange
            var player = new Player.Builder()
                .SetMarketValue(75000000)
                .Build();

            // Act
            player.ChangeMarketValue(80000000);

            // Assert
            Assert.Equal(80000000, player.MarketValue);
        }
    }
}
