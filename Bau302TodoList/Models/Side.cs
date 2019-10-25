using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bau302TodoList.Models
{
    public class Side:BaseEntity
    {
        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Ad")]
        public string Name { get; set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}