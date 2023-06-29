using OfferApp.Core.DTO;
using OfferApp.Core.Services;

namespace OfferApp.ConsoleApp
{
    internal sealed class GetBidView : IConsoleView
    {
        private readonly IBidService _bidService;

        public string KeyProvider => "2";

        public GetBidView(IBidService bidService)
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
            Console.WriteLine($"Bid: {bid}");
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
    }
}
