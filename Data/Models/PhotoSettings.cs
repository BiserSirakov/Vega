namespace Vega.Data.Models
{
    using System.IO;
    using System.Linq;

    public class PhotoSettings
    {
        public int MaxBytes { get; set; }

        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName) =>
            this.AcceptedFileTypes.Any(x => x == Path.GetExtension(fileName).ToLower());
    }
}
