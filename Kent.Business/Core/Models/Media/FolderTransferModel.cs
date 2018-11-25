namespace Kent.Business.Core.Models.Media
{
    public class FolderTransferModel
    {
        public string Source { get; private set; }

        public string Target { get; private set; }

        public FolderTransferModel(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }
}