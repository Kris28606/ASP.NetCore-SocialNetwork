using System.Net.Http.Headers;

namespace SocialNetwork.PictureUpload
{
    public class Uploader
    {
        private readonly IWebHostEnvironment env;

        public Uploader(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string Upload2(FormFile file)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return dbPath;
                }
                throw new Exception("ne mzoe");
            }
            catch (Exception ex)
            {
                throw new Exception("Ne moze");
            }
        }
    }
}
