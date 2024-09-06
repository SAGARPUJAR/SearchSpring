using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiTest.Contracts.Model
{
    public class ProductPoco
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double Price { get; set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedOn { get; set; }
    }
}
