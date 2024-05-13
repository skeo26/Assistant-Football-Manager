using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business;

    public class ApplicationState
    {
        private static ApplicationState _instance;
        public static ApplicationState Instance => _instance ?? (_instance = new ApplicationState());

        public string ClubsFilePath { get; set; }
        public string PlayersFilePath { get; set; }
        public string CoachesFilePath { get; set; }
        public string UsersFilePath { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Player> Players { get; set; }

        private ApplicationState() { }
    
    }
