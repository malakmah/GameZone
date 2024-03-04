namespace GameZone.ViewModels
{
    public class GameFormViewModel
    {
        [MaxLength(length: 2500)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Category")]
        public int Categoryid { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supported Devcises")]
        public List<int> SelectedDevcises { get; set; } = default!;
        public IEnumerable<SelectListItem> Devcises { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(length: 500)]
        public string Description { get; set; } = string.Empty;
    }
}
