using OfferApp.Core.DTO;
using OfferApp.Core.Services;

namespace OfferApp.ConsoleApp
{
    internal sealed class UpdateBidView : IConsoleView
    {
        private readonly IBidService _bidService;

        public string KeyProvider => "3";

        public UpdateBidView(IBidService bidService)
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
            ModifiedBid(bid);
            bid = _bidService.UpdateBid(bid);
            Console.WriteLine($"Updated Bid {bid}");
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
        private static BidDto? ModifiedBid(BidDto bid)
        {
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
