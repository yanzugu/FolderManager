using Stylet;

namespace FolderManager.Src.Models
{
    public class FolderInfo : PropertyChangedBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsEdit { get; set; }
        public bool IsSpliter { get; set; }
    }
}
