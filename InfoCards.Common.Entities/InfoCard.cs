using System.ComponentModel.DataAnnotations;

namespace InfoCards.Common.Entities
{
    public class InfoCard
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public string ImageBase64 { get; set; }
    }
}