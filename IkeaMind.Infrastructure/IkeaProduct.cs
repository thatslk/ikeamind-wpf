using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media.Imaging;

namespace IkeaMind.Infrastructure
{
    public partial class IkeaProduct
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("article")]
        public string Article { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("category")]
        public string Category { get; set; }
        [Column("measure")]
        public string Measure { get; set; }
        [Column("price")]
        public double? Price { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("imageUrl")]
        public string ImageUrl { get; set; }

        [NotMapped]
        public BitmapImage Image { get; set; }
    }
}
