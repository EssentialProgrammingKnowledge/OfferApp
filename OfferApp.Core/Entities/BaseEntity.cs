namespace OfferApp.Core.Entities
{
    public abstract class BaseEntity
    {
        // Nie chcemy aby ktokolwiek mógł zmieniać wartość Id
        // Mamy do tego kilka możliwości ustawić jako readonly tak jak w tym przypadku, zostawić jako private set lub jako protected i np poprzez konstruktor ustawiać tą wartość.
        // Jednak będzie to pozwalać na zmianę Id w klasie pochodnej.
        // Wybrałem tutaj podmianę wartości Id tylko i wyłącznie poprzez refleksję, aby uniemożliwić ustawienie wartości Id innej niż ta która pochodzi z jednego źródła prawdy jak np baza danych, plik
        public int Id { get; }

        public BaseEntity(int id)
        {
            Id = id;
        }
    }
}
