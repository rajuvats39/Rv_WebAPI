using System.ComponentModel.DataAnnotations.Schema;

namespace Rv_WebAPI.Models.Entity
{
    [Table("BlogAppCatagoryModel")]
    public class BlogAppCatagotyModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
