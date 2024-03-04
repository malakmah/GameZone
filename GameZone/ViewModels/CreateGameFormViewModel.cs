
namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel :GameFormViewModel
    {
     
        [AllowedExtensionsAttributes(FileSttings.AllowedExtensions),
            MaxFileSize(FileSttings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
 