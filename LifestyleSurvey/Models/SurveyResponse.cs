using System;
using System.ComponentModel.DataAnnotations;

namespace LifestyleSurvey.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Names")]
        public string FullNames { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Favorite Foods")]
        public string FavoriteFoods { get; set; }

        [Range(1, 5)]
        [Display(Name = "Movies Rating")]
        public int MoviesRating { get; set; }

        [Range(1, 5)]
        [Display(Name = "Radio Rating")]
        public int RadioRating { get; set; }

        [Range(1, 5)]
        [Display(Name = "Eat Out Rating")]
        public int EatOutRating { get; set; }

        [Range(1, 5)]
        [Display(Name = "TV Rating")]
        public int TVRating { get; set; }

        public DateTime SubmissionDate { get; set; }
    }
}