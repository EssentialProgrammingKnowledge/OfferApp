using Microsoft.Extensions.DependencyInjection;
using OfferApp.ConsoleApp;
using OfferApp.Core;
using System.Reflection;

// Setup kontenera IoC
IServiceCollection Setup()
{
    // Tworzymy obiekt ServiceCollection
    var serviceCollection = new ServiceCollection();
    // Rejestrujemy zależności z projektu Core
    serviceCollection.AddCore()
            // Dodajemy jako Singleton BidInteractionService, ponieważ ten obiekt będzie głównie używany podczas wykonywania operacji.
            // BidInteractionService jest statyczny tzn nie będzie się zmieniać a jedynie będzie odpowiedzialny za wywoałnie odpowiednich widoków IConsoleView.
            .AddSingleton<BidInteractionService>();

    // Sposród złożenia wybieramy złożenie, które aktualnie jest używane i pobieramy typy.
    // Następnie zrównoleglamy proces wyszukiwania aby przyspieszyć przeszukiwanie i szukamy takich typów które dziedziczą po IConsoleView (IsAssignableFrom) i są różne od interfejsu IConsoleView, bo nie chcemy wyszukać interfejsu a jedynie jego implementację.
    // Konwertujemy na listę a póżniej dla każdego elementu z listy rejestrujemy w kontenerze IoC jako Scoped.
    // Możemy tutaj użyć Transient, Scoped jednak powinniśmy przestrzegać się przed wywołaniem jako singleton, a dlaczego?
    // Zauważ że zarejestrowaliśmy serwis jako Scoped a wszystkie widoki IConsoleView mają zależność do tego seriwsu
    Assembly.GetExecutingAssembly().GetTypes()
        .AsParallel()
        .Where(t => typeof(IConsoleView).IsAssignableFrom(t) && t != typeof(IConsoleView))
        .ToList()
        .ForEach(t =>
        {
            serviceCollection.AddScoped(typeof(IConsoleView), t);
        });
    return serviceCollection;
}


var serviceCollection = Setup();
var serviceProvider = serviceCollection.BuildServiceProvider();

var bidInteractionService = serviceProvider.GetRequiredService<BidInteractionService>();
bidInteractionService.RunApp();
