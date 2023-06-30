using OfferApp.Core.DTO;
using OfferApp.Core.Entities;

namespace OfferApp.Core.Mappings
{
    internal static class Extensions
    {
        public static BidDto AsDto(this Bid bid)
        {
            return new BidDto
            {
                Id = bid.Id,
                Name = bid.Name,
                Description = bid.Description,
                Created = bid.Created,
                Updated = bid.Updated,
                Count = bid.Count,
                FirstPrice = bid.FirstPrice,
                LastPrice = bid.LastPrice,
                Published = bid.Published,
            };
        }

        public static BidPublishedDto AsPublishedDto(this Bid bid)
        {
            return new BidPublishedDto
            {
                Id = bid.Id,
                Name = bid.Name,
                Description = bid.Description,
                Created = bid.Created,
                Updated = bid.Updated,
                Count = bid.Count,
                FirstPrice = bid.FirstPrice,
                LastPrice = bid.LastPrice,
            };
        }

        public static MenuDto AsDto(this Menu menu)
        {
            return new MenuDto
            {
                Id = menu.Id,
                Name = menu.Name
            };
        }

        public static Menu AsEntity(this MenuDto menuDto)
        {
            return new Menu(menuDto.Id, menuDto.Name);
        }

    }
}
