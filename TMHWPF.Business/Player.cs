using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public class Player
    {
        private string firstName;
        private string lastName;
        private string nationality;
        private int age;
        private int skills;
        private string position;
        private double salary;
        private double marketValue;
        private bool readyToTransfer;


        public string FirstName { get { return firstName; } }
        public string LastName { get { return lastName; } }
        public string Nationality { get {  return nationality; } }
        public int Age { get { return age; } }
        public int Skills { get {  return skills; } }
        public string Position { get { return position; } }
        public double Salary { get { return salary; } }
        public double MarketValue { get {  return marketValue; } }
        public Club Team { get; private set; }
        public bool IsReadyToTransfer { get {  return readyToTransfer; } }

        public void ChangeTeam(Club newTeam)
        {
            Team = newTeam;
        }

        public void ChangeSalary(double newSalary)
        {
            if (salary > 0)
                salary = newSalary;
        }

        public void SetReadyToTransfer(bool isReady)
        {
            readyToTransfer = isReady;
        }
        
        public void ChangeMarketValue (double newMarketValue)
        {
            if (newMarketValue > 0) 
                marketValue = newMarketValue;
        }


        private Player() { }

        public class Builder
        {
            private readonly Player player = new Player();

            public Builder SetFirstName(string firstName) { player.firstName = firstName; return this; }
            public Builder SetLastName(string lastName) { player.lastName = lastName; return this; }
            public Builder SetNationality(string nationality) { player.nationality = nationality; return this; }
            public Builder SetAge(int age) { player.age = age; return this; }
            public Builder SetSkills(int skills) { player.skills = skills; return this; }
            public Builder SetPosition(string position) { player.position = position; return this; }
            public Builder SetSalary(double salary) { player.salary = salary; return this; }
            public Builder SetMarketValue(double marketValue) { player.marketValue = marketValue; return this; }
            public Builder SetTeam(Club team) { player.Team = team; return this; }
            public Builder SetReadyToTransfer(bool readyToTransfer) { player.readyToTransfer = readyToTransfer; return this; }

            public Player Build()
            {
                return player;
            }
        }

    }
}
