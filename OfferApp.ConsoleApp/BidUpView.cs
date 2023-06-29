using OfferApp.Core.DTO;
using OfferApp.Core.Services;

namespace OfferApp.ConsoleApp
{
    internal sealed class BidUpView : IConsoleView
    {
        private readonly IBidService _bidService;

        public string KeyProvider => "8";

        public BidUpView(IBidService bidService)
        {
            _bidService = bidService;
        }

        public void GenerateView()
        {
            var bid = GetBid();
            if (bid is null)
            {
                return;
            }
            var bidUpValue = BidUp();
            if (bidUpValue is null)
            {
                return;
            }
            _bidService.BidUp(bid.Id, bidUpValue.Value);
        }

        private BidDto? GetBid()
        {
            var id = GetBidId();
            var quest = _bidService.GetBidById(id);

            if (quest is null)
            {
                Console.WriteLine($"Quest with id {id} was not found");
            }
            return quest;
        }

        private static int GetBidId()
        {
            Console.WriteLine("Please enter Bid id");
            int.TryParse(Console.ReadLine(), out var id);
            return id;
        }

        private static decimal? BidUp()
        {
            Console.WriteLine("Enter value to bid up");

            if (!decimal.TryParse(Console.ReadLine(), out var value))
            {
                Console.WriteLine("Entered invalid value");
                return null;
            }

            return value;
        }
    }
}
