using Microsoft.Extensions.DependencyInjection;
using OfferApp.ConsoleApp;
using OfferApp.Core;

var serviceCollection = new ServiceCollection();
// Rejestrujemy zależności z projektu Core
serviceCollection.AddCore()
        // Dodajemy jako Singleton BidInteractionService, ponieważ ten obiekt będzie głównie używany podczas wykonywania operacji.
        // BidInteractionService jest statyczny tzn nie będzie się zmieniać a jedynie będzie odpowiedzialny za wywoałnie odpowiednich widoków IConsoleView.
        .AddSingleton<BidInteractionService>()
        // Możemy tutaj użyć Transient, Scoped jednak powinniśmy przestrzegać się przed wywołaniem jako singleton, a dlaczego?
        // Zauważ że zarejestrowaliśmy serwis jako Scoped a wszystkie widoki IConsoleView mają zależność do tego seriwsu
        .AddScoped<IConsoleView, AddBidView>()
        .AddScoped<IConsoleView, BidUpView>()
        .AddScoped<IConsoleView, DeleteBidView>()
        .AddScoped<IConsoleView, GetAllBidsView>()
        .AddScoped<IConsoleView, GetBidView>()
        .AddScoped<IConsoleView, GetPublishedBidsView>()
        .AddScoped<IConsoleView, SetPublishBidView>()
        .AddScoped<IConsoleView, UpdateBidView>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var bidInteractionService = serviceProvider.GetRequiredService<BidInteractionService>();
bidInteractionService.RunApp();
