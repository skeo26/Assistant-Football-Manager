using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public class TransferRequest
    {
        public Club InterestedClub { get; private set; }
        public Player TargetPlayer { get; private set; }
        public double OfferAmount { get; private set; }
        public double SalaryOffer { get; private set; }
        public bool? IsApproved { get; set; }

        public TransferRequest(Club interestedClub, Player targetPlayer, double offerAmount, double salaryOffer)
        {
            InterestedClub = interestedClub;
            TargetPlayer = targetPlayer;
            OfferAmount = offerAmount;
            SalaryOffer = salaryOffer;
            IsApproved = null;
        }

        public void SetApprovedStatus(bool status)
        {
            IsApproved = status;    
        }
    }
}
