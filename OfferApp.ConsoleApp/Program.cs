using OfferApp.ConsoleApp;
using OfferApp.Core;

// Wykorzystałem wzorzec strategię.
// Każdy widok implementuje interfejs IConsoleView.
// Następnie wewnątrz BidInteractionService tworzę co wywołanie kolekcję IConsoleView.
// Szczegóły w klasie BidInteractionService

var menuService = Extensions.CreateMenuService();
var bidService = Extensions.CreateBidService();
var bidInteractionService = new BidInteractionService(menuService, bidService);
bidInteractionService.RunApp();
