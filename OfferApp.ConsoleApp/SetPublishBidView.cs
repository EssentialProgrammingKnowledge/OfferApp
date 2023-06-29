using OfferApp.Core.DTO;
using OfferApp.Core.Services;

namespace OfferApp.ConsoleApp
{
    internal sealed class SetPublishBidView : IConsoleView
    {
        private readonly IBidService _bidService;

        public string KeyProvider => "7";

        public SetPublishBidView(IBidService bidService)
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
            var publish = PublishBid(bid);
            if (publish is null)
            {
                return;
            }
            if (publish.Value)
            {
                Console.WriteLine(_bidService.Published(bid.Id) ? $"Published Bid with id '{bid.Id}'" : $"Cannot published Bid with id '{bid.Id}'");
            }
            else
            {
                Console.WriteLine(_bidService.Unpublished(bid.Id) ? $"Unpublished Bid with id '{bid.Id}'" : $"Cannot unpublished Bid with id '{bid.Id}'");
            }
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

        private static bool? PublishBid(BidDto bid)
        {
            Console.WriteLine($"Please choose 0 to unpublish or 1 to publish Bid with id '{bid.Id}' and name '{bid.Name}'");
            if (!int.TryParse(Console.ReadLine(), out var state))
            {
                Console.WriteLine("Entered invalid value");
                return null;
            }

            if (state < 0 || state > 1)
            {
                return null;
            }

            return state == 1;
        }
    }
}
