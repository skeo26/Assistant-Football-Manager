using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace TMHWPF.Business
{
    public class TransferMarket : ITransferMarket
    {
        private static readonly Lazy<TransferMarket> instance = new Lazy<TransferMarket>(() => new TransferMarket());
        public static TransferMarket Instance => instance.Value;

        private const int mandatoryBudgetClub = 150000000;
        private const int allowsPlayersClub = 28;

        private List<Player> availablePlayers = new List<Player>();
        public List<Player> AvailablePlayers { get { return availablePlayers; } set { availablePlayers = value; } }


        private TransferMarket() { }


        public bool AttemptToSignPlayer(TransferRequest transferRequest)
        {
            if (!transferRequest.TargetPlayer.IsReadyToTransfer ||
                transferRequest.OfferAmount < transferRequest.TargetPlayer.MarketValue)
            {
                return false;
            }

            return new Random().NextDouble() <= transferRequest.OfferAmount / transferRequest.TargetPlayer.MarketValue * 1.1;
        }

        private void TransferToClub(Club buyingClub, Club sellingClub, Player player, double offerAmount, double salaryOffer)
        {
            player.ChangeTeam(buyingClub);
            player.ChangeSalary(salaryOffer);
            player.SetReadyToTransfer(false);

            buyingClub.ChangeBalance(-offerAmount);
            sellingClub.ChangeBalance(offerAmount);
            buyingClub.ChangeCountPlayers(1);
            sellingClub.ChangeCountPlayers(-1);
        }

        public TransferRequest ProcessTransferRequest(TransferRequest request)
        {
            bool isSuccess = AttemptToSignPlayer(request);

            Club sellingClub = request.TargetPlayer.Team;
            if (sellingClub == null)
            {
                isSuccess = false;
            }
            if (NumberOfPlayersNotAllowsTransfer(request.InterestedClub))
            {
                isSuccess = false;
            }
            if (!IsEnoughBalance(request.InterestedClub, request.OfferAmount))
            {
                isSuccess = false;
            }

            request.SetApprovedStatus(isSuccess);

            return request;
        }

        public bool IsEnoughBalance(Club club, double offerAmount)
        {
            return club.Balance > (offerAmount + mandatoryBudgetClub);
        }

        public bool IsResponsePositive(TransferRequest response)
        {
            if (response.IsApproved == true)
            {
                TransferToClub(response.InterestedClub, response.TargetPlayer.Team, response.TargetPlayer, response.OfferAmount, response.SalaryOffer);
                return true;
            }
            return false;
        }

        public bool NumberOfPlayersNotAllowsTransfer(Club club)
        {
            return club.CountPlayers >= allowsPlayersClub;
        }
    }
}
