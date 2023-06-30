using OfferApp.Core.Exceptions;

namespace OfferApp.Core.Entities
{
    public class Bid : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime Created { get; private set; } = DateTime.UtcNow;
        public DateTime? Updated { get; private set; }
        public int Count { get; private set; }
        public decimal FirstPrice { get; private set; }
        public decimal? LastPrice { get; private set; }
        public bool Published { get; private set; }

        public Bid(int id, string name, string description, DateTime created, decimal firstPrice, DateTime? updated = null, int count = 0, decimal? lastPrice = null, bool published = false)
            : base(id)
        {
            ValidName(name);
            ValidDescription(description);
            ValidPrice(firstPrice);
            
            if (lastPrice.HasValue)
            {
                ValidPrice(lastPrice.Value);
            }

            Name = name;
            Description = description;
            Created = created;
            Updated = updated;
            Count = count;
            FirstPrice = firstPrice;
            Updated = updated;
            FirstPrice = firstPrice;
            LastPrice = lastPrice;
            Published = published;
        }

        public static Bid Create(string name, string description, decimal firstPrice)
        {
            return new Bid(0, name, description, DateTime.UtcNow, firstPrice);
        }

        public void ChangeName(string name)
        {
            if (Published)
            {
                throw new OfferException("Cannot change an offer once it has been published");
            }
            ValidName(name);
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            if (Published)
            {
                throw new OfferException("Cannot change an offer once it has been published");
            }
            ValidDescription(description);
            Description = description;
        }

        public void ChangeFirstPrice(decimal firstPrice)
        {
            if (Published)
            {
                throw new OfferException("Cannot change an offer once it has been published");
            }
            ValidPrice(firstPrice);
            FirstPrice = firstPrice;
        }

        public void Publish()
        {
            Published = true;
        }

        public void Unpublish()
        {
            Published = false;
        }

        public void ChangePrice(decimal price)
        {
            if (!Published)
            {
                throw new OfferException("Cannot change an offer once it hasn't been published");
            }

            if (!LastPrice.HasValue)
            {
                if (FirstPrice > price)
                {
                    throw new OfferException($"Price '{price}' cannot be less than {FirstPrice}");
                }

                LastPrice = price;
                Updated = DateTime.UtcNow;
                Count++;
                return;
            }

            if (LastPrice.Value > price)
            {
                throw new OfferException($"Price '{price}' cannot be less than {LastPrice}");
            }

            LastPrice = price;
            Updated = DateTime.UtcNow;
            Count++;
        }

        private static void ValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new OfferException("Name cannot be empty");
            }

            if (name.Length < 4)
            {
                throw new OfferException($"Name: '{name}' should contain at least 4 characters");
            }
        }

        private static void ValidDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new OfferException("Description cannot be empty");
            }

            if (description.Length < 10)
            {
                throw new OfferException($"Description: '{description}' should contain at least 10 characters");
            }
        }

        private static void ValidPrice(decimal price)
        {
            if (price < 0)
            {
                throw new OfferException($"Price '{price}' cannot be negative");
            }
        }
    }
}
