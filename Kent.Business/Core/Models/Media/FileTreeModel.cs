using System.Collections.Generic;

namespace Kent.Business.Core.Models.Media
{
    public class FileTreeModel
    {
        public string data;
        public FileTreeAttribute attr;
        public string state;
        public List<FileTreeModel> children;
    }
}
