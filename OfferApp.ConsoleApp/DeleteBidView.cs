using OfferApp.Core.Services;

namespace OfferApp.ConsoleApp
{
    internal sealed class DeleteBidView : IConsoleView
    {
        private readonly IBidService _bidService;

        public string KeyProvider => "5";

        public DeleteBidView(IBidService bidService)
        {
            _bidService = bidService;
        }

        public void GenerateView()
        { 
            var id = GetBidId();
            _bidService.DeleteBid(id);
            Console.WriteLine($"Bid with id: {id} was deleted");
        }

        private static int GetBidId()
        {
            Console.WriteLine("Please enter Bid id");
            int.TryParse(Console.ReadLine(), out var id);
            return id;
        }
    }
}
