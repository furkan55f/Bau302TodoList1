using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bau302TodoList.Models
{
    public class Contact:BaseEntity
    {

        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Ad")]
        public string FirstName { get; set; }

        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Soyad")]
        public string LastName { get; set; }

        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("E-mail")]
        public string Email { get; set; }

        [StringLength(20), DisplayName("Telefon")]
        public string Phone { get; set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}