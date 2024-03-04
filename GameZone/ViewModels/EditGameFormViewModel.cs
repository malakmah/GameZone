namespace GameZone.ViewModels
{
    public class EditGameFormViewModel:GameFormViewModel
    {
        public int Id {  get; set; }

        public string? CurrentCover {  get; set; } 
        [AllowedExtensionsAttributes(FileSttings.AllowedExtensions),
            MaxFileSize(FileSttings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
