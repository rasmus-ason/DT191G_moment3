using System.ComponentModel.DataAnnotations;

namespace Dt191G_moment3.Models{
    public class Music {
        public int Id {get; set;}

        [Required(ErrorMessage = "Ange titel på album")]
        [Display(Name = "Titel på album")]
        public string? Title {get; set;}

        [Required(ErrorMessage = "Ange artist")]
        [Display(Name = "Ange artist/grupp")]        
        public string? Artist {get; set;}

        [Required(ErrorMessage = "Ange spår på album")]
        [Display(Name = "Antal spår")]
        public int? Tracks {get; set;}

        [Display(Name = "Status")]
        public bool? IsBorrowed {get; set;}

       

    }
}