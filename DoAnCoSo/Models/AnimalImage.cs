using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo.Models
{
    [Table("AnimalImage")]
    public class AnimalImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Url { get; set; }

        [ForeignKey("Animal")]
        public int IdAnimal { get; set; }

        public Animal Animal { get; set; }
    }
}