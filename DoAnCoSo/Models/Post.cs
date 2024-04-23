using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã post")]
        public int IdPost { get; set; }
        
        [DisplayName("Title")]
        public string Title { get; set; }
       
        [DisplayName("Ngày tháng năm")]
        public DateTime Date { get; set; } 
        
        [DisplayName("Ảnh QR và video")]
        public string ImageQRVideo { get; set; }

		public List<Comment>? Comments { get; set; }
    }
}
