namespace demo_exam
{
    public class PartnerRepository
    {
        private readonly AppDbContext _context;

        public PartnerRepository()
        {
            _context = new AppDbContext();
        }

        public List<Partner> GetPartners()
        {
            var partners = _context.Partners
                .OrderBy(x => x.Id)
                .ToList();

            return partners;
        }

        public List<string> GetPartnerTypes()
        {
            return _context.Partners
                .Select(p => p.Type)
                .Distinct()
                .ToList();
        }

        public void UpdatePartner(Partner partner, Partner oldPartner)
        {
            partner.Id = oldPartner.Id;
            _context.Entry(oldPartner).CurrentValues.SetValues(partner);
            _context.SaveChanges();
        }

        public void CreatePartner(Partner partner)
        {
            // Получаем максимальный ID
            var maxId = _context.Partners.Max(p => (int?)p.Id) ?? 0;
            partner.Id = maxId + 1;

            _context.Partners.Add(partner);
            _context.SaveChanges();
        }

        public int CalculateDiscount(int partnerId)
        {
            var totalCount = _context.PartnerProducts
                .Where(pp => pp.PartnerId == partnerId)
                .Sum(pp => pp.ProductCount);

            if (totalCount < 10_000) return 0;
            if (totalCount < 50_000) return 5;
            if (totalCount < 300_000) return 10;
            return 15;
        }
    }
}
