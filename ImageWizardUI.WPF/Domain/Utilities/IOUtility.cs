using System.IO;

namespace Domain.Utilities
{
    public static class IOUtility
    {
        public static string[] GetFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            return Directory.GetFiles(path);            
        }
    }
}
