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
        public String SaveAndCreatePath(FormFile file, string baseUrl)
        {
            try
            {
                FileInfo fi = new FileInfo(file.FileName);
                var newFileName = "Image_" + DateTime.Now.Millisecond + fi.Extension;
                var path = Path.Combine(env.WebRootPath, "uploads", newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return baseUrl + "/uploads/" + newFileName;
            }
            catch (Exception)
            {
                throw new Exception("Ne mogu da sacuvam sliku!");
            }
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
