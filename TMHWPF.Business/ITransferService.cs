using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public interface ITransferService
    {
        bool RequestPlayerTransfer(Club interestedClub, Player targetPlayer, double offerAmount, double salaryOffer);
    }
}
