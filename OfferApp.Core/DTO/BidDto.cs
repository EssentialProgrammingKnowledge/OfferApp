using FluentValidation;

namespace OfferApp.Core.DTO
{
    public class BidDto : IBaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int Count { get; set; }
        public decimal FirstPrice { get; set; }
        public decimal? LastPrice { get; set; }
        public bool Published { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Created: {Created}, FirstPrice: {FirstPrice}, "
                + $"Published: {Published}, Updated: {Updated}, Count: {Count}, " +
                $"LastPrice {LastPrice}, Description: {Description}";
        }
    }

    public class BidValidator : AbstractValidator<BidDto>
    {
        public BidValidator()
        {
            RuleFor(b => b.Name).MinimumLength(4);
            RuleFor(bid => bid.Description).MinimumLength(10).MaximumLength(3000);
            RuleFor(bid => bid.FirstPrice).GreaterThanOrEqualTo(0);

            When(b => !string.IsNullOrWhiteSpace(b.Description), () =>
            {
                RuleFor(bid => bid.LastPrice).GreaterThanOrEqualTo(0);
            });
        }
    }
}
