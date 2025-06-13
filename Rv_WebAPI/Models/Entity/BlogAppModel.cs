using System.ComponentModel.DataAnnotations.Schema;

namespace Rv_WebAPI.Models.Entity
{
    [Table("BlogAppModel")]
    public class BlogAppModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public BlogAppCatagotyModel? Category { get; set; }
    }
}
