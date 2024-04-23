using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCoSo.Models
{
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID động vật")]
        public int IdAnimal { get; set; }

        [DisplayName("Tên động vật")]
        public string Name { get; set; }

        [DisplayName("Giới thiệu động vật")]
        public string GioiThieuText { get; set; }

        [DisplayName("Nơi Sinh Sống")]
        public string NoiSinhSongSongText { get; set; }

        [DisplayName("Ngoại hình")]
        public string NgoaiHinhText { get; set; }

        [DisplayName("Avatar")]
        public string Avatar { get; set; }

        [DisplayName("Danh sách ảnh")]
        public List<AnimalImage> AnimalImages { get; set; }

        [DisplayName("Nơi Sinh Sống")]
        public string NoiSinhSongImage { get; set; }

        [DisplayName("QR CODE")]
        public string ImgQR3D { get; set; }

        [ForeignKey("ClassAnimal")]
        [DisplayName("ID ClassAnimal")]
        public int IdClass { get; set; }

        [DisplayName("ClassAnimal")]
        public ClassAnimal? ClassAnimal { get; set; }

    }

}