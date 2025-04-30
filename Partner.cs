using System.ComponentModel.DataAnnotations.Schema;

namespace demo_exam
{
    public class Partner
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string DirectorLastName { get; set; } = string.Empty;
        public string DirectorFirstName { get; set; } = string.Empty;
        public string DirectorSurname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long Inn { get; set; }
        public int Rating { get; set; }

        [NotMapped]
        public int Discount { get; set; }

        [NotMapped]
        public string DirectorFullName => $"{DirectorLastName} {DirectorFirstName} {DirectorSurname}";


        public void CalculateDiscount(PartnerRepository repo)
        {
            Discount = repo.CalculateDiscount(Id);
        }
    }

    public class PartnerProduct
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }

        public int ProductCount { get; set; } 
    }
}
