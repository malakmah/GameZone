
using GameZone.Settings;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly String _imagesPath;

       public GamesService(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSttings.ImagePath}";
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.category)
                .Include(g => g.Devices)
                .ThenInclude(d =>d.Device)
                .AsNoTracking ()
                .ToList(); 
             
        }
        public Game?  GetById(int id)
        {
            return _context.Games
                .Include(g => g.category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
               .SingleOrDefault(g=>g.Id==id);
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName = await Savecover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                Categoryid = model.Categoryid,
                Cover=CoverName,
                Devices= model.SelectedDevcises.Select(d => new GameDevice { DeviceId = d }).ToList(),
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(g => g.Devices)
               .SingleOrDefault (game => game.Id == model.Id);          
               if(game is null)
                return null;
            var hasNewCovre =model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.Categoryid = model.Categoryid;
            game.Devices=model.SelectedDevcises.Select(d=> new GameDevice { DeviceId =d} ).ToList();

            if(hasNewCovre)
            {
                game.Cover =await Savecover(model.Cover);
            }
          var effectedRows=   _context.SaveChanges();
            if(effectedRows > 0)
            {
                if(hasNewCovre)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }
        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(id);
            if (game is null)
                return isDeleted;
            _context.Games.Remove(game);
            var effectedRows =_context.SaveChanges();
             if (effectedRows > 0) 
            {
                isDeleted = true;
                var cover =Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
              }
            return isDeleted;
        }

        private async Task<string> Savecover(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, CoverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return CoverName;
        }

       
    }
}
    