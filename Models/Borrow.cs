using System.ComponentModel.DataAnnotations;

namespace Dt191G_moment3.Models{
    public class Borrow {
        public int Id {get; set;}

        [Display(Name = "Titel")]
        public string? BorrowedTitle {get; set;}

        [Display(Name = "Artist")]
        public string? BorrowedArtist {get; set;}

        [Required(ErrorMessage = "Ange vem som lånar albumet")]
        [Display(Name = "Utlånas till")]
        public string? BorrowedTo {get; set;}

        public int? AlbumId {get; set;}

        [Display(Name = "Utlåningsdatum")]
        public DateTime? BorrowedDate {get; set;}

        [Display(Name = "Returdatum")]
        public DateTime? ReturnDate {get; set;}

    }
}