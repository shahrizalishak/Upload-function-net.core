using System.ComponentModel.DataAnnotations;

namespace eForm.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
