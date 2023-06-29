using OfferApp.Core.DTO;
using OfferApp.Core.Services;

namespace OfferApp.ConsoleApp
{
    internal sealed class AddBidView : IConsoleView
    {
        private readonly IBidService _bidService;

        public string KeyProvider => "1";

        public AddBidView(IBidService bidService)
        {
            _bidService = bidService;
        }

        public void GenerateView()
        {
            var bid = CreateBid();

            if (bid is null)
            {
                return;
            }

            bid = _bidService.AddBid(bid);
            Console.WriteLine($"Added Bid {bid}");
        }

        private static BidDto? CreateBid()
        {
            var bid = new BidDto();
            if (!SetTitle(bid))
            {
                return null;
            }
            if (!SetDescription(bid))
            {
                return null;
            }
            if (!SetFirstPrice(bid))
            {
                return null;
            }
            return bid;
        }


        private static bool SetTitle(BidDto bid)
        {
            Console.WriteLine("Enter Bid name");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Bid cannot be empty");
                return false;
            }
            bid.Name = name;
            return true;
        }

        private static bool SetDescription(BidDto bid)
        {
            Console.WriteLine("Enter Bid description");
            var description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Bid cannot be empty");
                return false;
            }
            bid.Description = description;
            return true;
        }

        private static bool SetFirstPrice(BidDto bid)
        {
            Console.WriteLine("Enter Bid first price");
            var description = Console.ReadLine();
            if (!decimal.TryParse(description, out var price))
            {
                Console.WriteLine("Invalid Bid price");
                return false;
            }
            bid.FirstPrice = price;
            return true;
        }
    }
}
