using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bau302TodoList.Models
{
    public class Media : BaseEntity
    {
        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("Uzantı")]
        [StringLength(200)]
        public string Extension { get; set; }
        [DisplayName("Dosya Yolu")]
        [StringLength(200)]
        public string FilePath { get; set; }
        [DisplayName("Dosya Boyutu")]
        public float FileSize { get; set; }
        [DisplayName("Yıl")]
        public int Year { get; set; }
        [DisplayName("Ay")]
        public int Month { get; set; }
        [DisplayName("İçerik Tipi")]
        [StringLength(200)]
        public string ContentType { get; set; }

    }
}