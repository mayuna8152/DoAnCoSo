using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo.Models
{
    public class AnimalImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAnimal { get; set; }

        public string Url { get; set; }

        [ForeignKey("Animal")]
        public int? AnimalId { get; set; }

        public Animal Animal { get; set; }
    }
}