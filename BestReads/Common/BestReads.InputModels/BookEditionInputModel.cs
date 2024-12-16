using System.ComponentModel.DataAnnotations;

namespace BestReads.InputModels
{
    public class BookEditionInputModel
    {
        [Required]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "Заглавието трябва да е между {0} и {1} символа.")]
        public string Title { get; set; }

        [Required]
        [StringLength(700, MinimumLength = 5, ErrorMessage = "Описанието трябва да е между {0} и {1} символа.")]
        public string Description { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN трябва да е {0} символа.")]
        public string ISBN { get; set; }

        public string? CoverImageUrl { get; set; }

        [Required]
        // [EnumDataType()]
        public string CoverType { get; set; }

        [Required]
        [Range(1, 5000, ErrorMessage = "Страниците трябва да имат стойност между 1 и 5000.")]
        public int Pages { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Невалидна дата.")]
        public DateTime DatePublished { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}