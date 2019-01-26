using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    [Table("Product")]
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> prcatId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CatgoryName { get; set; }
        public string Images { get; set; }

        public virtual Category Category { get; set; }
    }
}