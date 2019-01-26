using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    [Table("Category")]
    public class CatogryModel
    { [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }
    }
}