using Aspose.Imaging.FileFormats.Png;
using Aspose.Imaging.ImageOptions;
using Aspose.Imaging.Sources;
using Aspose.Imaging;
using Newtonsoft.Json.Linq;

namespace Aspose.Core
{
    public class ImageWizard
    {
        #region Events

        public Action<int> OnImageProcessed;

        #endregion
 
        #region Methods

        public bool DirExistsOrCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                return false;
            }

            return true;
        }

        public void CreateFileIfNotExists(string path)
        {
            if (!File.Exists(path))
            { 
                var fs = File.Create(path);

                fs.Close();
                fs.Dispose();
            }
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void Resize(int imgWidth, int imgHeight, Image img)
        {
            img.Resize(imgWidth, imgHeight);
        }
       
        public Size GetImageSize(Image image)
        { 
            if(image is null)
                throw new ArgumentNullException(nameof(image));

            return image.Size;
        }

        private Size CalculateCanvasSize(Size columnsRows, Size imgSize, Size offset)
        {
            Size size = new Size()
            { 
                Width = (imgSize.Width + offset.Width) * columnsRows.Width,
                Height = (imgSize.Height + offset.Height) * columnsRows.Height
            };

            return size;
        }

        public void CreateSpriteSheet(
            Size ColumnsRows,
            Size Offset,            
            string[] pathToInputImages,  
            string pathToOutput,
            string outputName,
            bool resize = false, 
            Size newSize = default
            )
        {
            Size imgSize;

            Size CanvasSize;

            var options = new PngOptions() { ColorType = PngColorType.TruecolorWithAlpha };

            var source = CreatePNG(pathToOutput, outputName, false);

            options.Source = source;
            
            if (resize)
                imgSize = newSize;
            else
            {
                Image first = Image.Load(pathToInputImages[0]);

                imgSize = GetImageSize(first);
            }                

            CanvasSize = CalculateCanvasSize(ColumnsRows, imgSize, Offset);

            Image canvas = Image.Create(options, CanvasSize.Width, CanvasSize.Height + 50);

            canvas.BackgroundColor = Color.Transparent;

            Graphics g = new Graphics(canvas);

            int count = pathToInputImages.Length;

            float x = 0, y = 50;

            var loadOptions = new LoadOptions();

            for (int i = 0; (i < count) && y <= CanvasSize.Height; i++)
            {
                Image img = Image.Load(pathToInputImages[i], loadOptions);

                if(!resize)
                    imgSize = GetImageSize(img);

                img.BackgroundColor = Color.Transparent;

                if (resize)
                {
                    Resize(imgSize.Width, imgSize.Height, img);
                }

                if (x <= CanvasSize.Width)
                {
                    g.DrawImage(img, new PointF(x, y));

                    x += imgSize.Width + Offset.Width;
                }

                if (x >= CanvasSize.Width)
                {
                    x = 0;
                    y += imgSize.Height + Offset.Height;

                    g.DrawImage(img, new PointF(x, y));

                    x += imgSize.Width + Offset.Width;
                }

                OnImageProcessed?.Invoke(i);
            }

            canvas.Save();
            
            canvas.Dispose();

            CreateMetaDataFile(ColumnsRows,
                Offset.IsEmpty ? false : true, 
                Offset, 
                resize, 
                imgSize, 
                pathToInputImages.Length, 
                pathToOutput, 
                outputName);
        }

        public FileCreateSource CreatePNG(string pathToOutput, string name, bool temporal)
        {
            return new FileCreateSource(pathToOutput + Path.DirectorySeparatorChar + name + ".png", temporal);
        }

        private void CreateMetaDataFile(
            Size ColumnsRows, 
            bool useOffsets,
            Size Offset, 
            bool useResize,
            Size imageSize, 
            int imageCount, 
            string pathToOutput, 
            string outputFileName)
        {
            string path = pathToOutput + Path.DirectorySeparatorChar + string.Concat("md_", outputFileName, ".json");

            CreateFileIfNotExists(path);

            JObject root = new JObject();

            root["Animation File Name"] = outputFileName;
            root["Full Path"] = pathToOutput;
            root["Image Count"] = imageCount;
            root["Columns / Rows"] = new { Columns = ColumnsRows.Width, Rows = ColumnsRows.Height }.ToString();
            root["Use Offsets"] = useOffsets;
            root["Offset"] = new { Horizontal = useOffsets? Offset.Width : 0, Vertical = useOffsets? Offset.Height : 0 }.ToString();
            root["Use Resize"] = useResize;
            root["Image Size"] = new { imageSize.Width, imageSize.Height }.ToString();

            File.WriteAllText(path, root.ToString());
        }

        #endregion
    }
}
