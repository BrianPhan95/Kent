using Kent.Libary.Enums;

namespace Kent.Business.Core.Models.Media
{
    public class MediaBrowserSetupModel
    {
        public MediaBrowserSetupModel()
        {
            FileTreeAttribute = new FileTreeAttribute();
        }

        #region Public Properties

        public FileTreeAttribute FileTreeAttribute { get; set; }

        public string RootFolder { get; set; }

        public MediaBrowserSelectMode Mode { get; set; }

        #endregion
    }
}
