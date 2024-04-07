using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID commet")]
        public int IdCmt { get; set; }
        
        [DisplayName("ChatDATA")]
        public string ChatData { get; set; }
        
        [DisplayName("Time")]
        public DateTime DateTime { get; set; }

        [ForeignKey("Post")]
        public int IdPost { get; set; }

        public Post? Post { get; set; }  


    }
}
