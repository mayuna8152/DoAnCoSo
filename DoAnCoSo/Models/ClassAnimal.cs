using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class ClassAnimal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClass { get; set; }

        [DisplayName("Tên động vật")]
        public string Name { get; set; }

        [DisplayName("Thông Tin")]
        public string Info { get; set; }

        public string BackgroundVideo { get; set; }
        public List<Animal>? Animals { get; set; }

    }
}
