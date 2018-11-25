using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Enums
{
    public enum MoveNodeStatus
    {
        Success = 1,
        Failure = 2,
        MoveParentNodeToChild = 3,
        MoveNodeToFile = 4,
        MoveSameLocation = 5
    }

    public enum RenameStatus
    {
        Success = 1,
        Failure = 2,
        DuplicateName = 3
    }

    public enum EditImageStatus
    {
        SaveFail = 0,
        SaveSuccess = 1,
        OverWriteConfirm = 2
    }

    public enum MediaBrowserSelectMode
    {
        All = 1,
        File = 2,
        SimpleFileUpload = 3,
        ComplexFileUpload = 4,
        Image = 5,
        SimpleImageUpload = 6,
        ComplexImageUpload = 7,
        Folder = 8
    }
}
