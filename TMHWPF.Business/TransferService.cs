using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public class TransferService : ITransferService
    {
        private readonly TransferMarket transferMarket;

        public TransferService(TransferMarket transferMarket)
        {
            this.transferMarket = transferMarket;
        }

        public bool RequestPlayerTransfer(Club interestedClub, Player targetPlayer, double offerAmount, double salaryOffer)
        {
            TransferRequest transferRequest = new TransferRequest(interestedClub, targetPlayer, offerAmount, salaryOffer);
            TransferRequest response = transferMarket.ProcessTransferRequest(transferRequest);

            if (!response.IsApproved.HasValue || !response.IsApproved.Value)
            {
                return false;
            }
            return transferMarket.IsResponsePositive(response); 
        }
    }
}
