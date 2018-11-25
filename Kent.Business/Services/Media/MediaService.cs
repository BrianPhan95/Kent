using Kent.Business.Core.Models.Media;
using Kent.Libary.Configurations;
using Kent.Libary.Enums;
using Kent.Libary.Logger;
using Kent.Libary.Models;
using Kent.Libary.Utilities;
using Kent.Libary.Utilities.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Kent.Business.Services.Media
{
    public class MediaService : IMediaService
    {
        //private readonly ILogger _logger;
        private const string DefaultFolderName = "New Media";
        //public MediaService(ILogger logger)
        //{
        //    _logger = logger;
        //}

        #region File Manager

        public string MapPath(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
        }

        /// <summary>
        /// Get the ~/App_Data folder
        /// </summary>
        public DirectoryInfo DataDirectory
        {
            get
            {
                var appDataPath = (string)AppDomain.CurrentDomain.GetData("DataDirectory") ??
                                  Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "App_Data");

                return new DirectoryInfo(appDataPath);
            }
        }

        /// <summary>
        /// Detect if a file is image
        /// </summary>
        /// <param name="filename">File name for testing</param>
        /// <returns>true if file is image, otherwise false</returns>
        public Boolean IsImage(string filename)
        {
            return filename.IsImage();
        }

        /// <summary>
        /// Copy directory
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="copySubDirs"></param>
        public void CopyDirectory(string sourcePath, string destinationPath, bool copySubDirs)
        {
            sourcePath.CopyDirectory(destinationPath, copySubDirs);
        }

        /// <summary>
        /// Delete a directory and all of sub-folders/items
        /// </summary>
        /// <param name="path">Path to the directory to delete</param>
        public void DeleteDirectory(string path)
        {
            path.DeleteDirectory();
        }

        /// <summary>
        /// Check if current path is valid
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public ResponseModel CheckPathValid(string path, MediaBrowserSelectMode mode)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new ResponseModel
                {
                    Success = false,
                    //Message = T("Media_Message_EmptyPath")
                };
            }

            var physicalPath = HttpContext.Current.Server.MapPath(path);
            switch (mode)
            {
                case MediaBrowserSelectMode.Folder:
                    if (!Directory.Exists(physicalPath))
                    {
                        //Check if user select file of folder
                        if (File.Exists(physicalPath))
                        {
                            var fileInfo = new FileInfo(physicalPath);
                            path = fileInfo.DirectoryName;
                        }
                        else
                        {
                            return new ResponseModel
                            {
                                Success = false,
                                //Message = T("Media_Message_WrongFolderPath")
                            };
                        }
                    }
                    break;
                case MediaBrowserSelectMode.File:
                case MediaBrowserSelectMode.ComplexFileUpload:
                    if (!File.Exists(physicalPath))
                    {
                        return new ResponseModel
                        {
                            Success = false,
                            //Message = T("Media_Message_WrongFilePath")
                        };
                    }
                    break;
                case MediaBrowserSelectMode.Image:
                case MediaBrowserSelectMode.ComplexImageUpload:
                    if (!File.Exists(physicalPath) || !physicalPath.IsImage())
                    {
                        return new ResponseModel
                        {
                            Success = false,
                            //Message = T("Media_Message_WrongImagePath")
                        };
                    }
                    break;
            }

            return new ResponseModel
            {
                Success = true,
                Data = path
            };
        }

        #endregion

        #region Media

        /// <summary>
        /// Populate master tree
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="node"></param>
        public void PopulateTree(string relativePath, FileTreeModel node)
        {
            var physicalPath = HttpContext.Current.Server.MapPath(relativePath);
            // create if not exists
            if (!Directory.Exists(physicalPath))
                Directory.CreateDirectory(physicalPath);
            if (node.children == null)
            {
                node.children = new List<FileTreeModel>();
            }
            var directory = new DirectoryInfo(physicalPath);

            //Loop through each subdirectory
            foreach (var d in directory.GetDirectories().Where(d => !d.Name.Equals(KentConfiguration.ResizedFolder)))
            {
                var mediaPath = string.Format("{0}/{1}", relativePath, d.Name);
                var t = new FileTreeModel
                {
                    attr = new FileTreeAttribute
                    {
                        Id = mediaPath.ToIdString(),
                        Path = mediaPath,
                        Rel = "folder"
                    },
                    data = d.Name,
                    state = "closed"
                };

                node.children.Add(t);
            }

            //Loop through each file in master directory
            foreach (var f in directory.GetFiles())
            {
                var mediaPath = string.Format("{0}/{1}", relativePath, f.Name);
                var t = new FileTreeModel
                {
                    attr = new FileTreeAttribute
                    {
                        Id = mediaPath.ToIdString(),
                        Path = mediaPath,
                        Class = "jstree-leaf"
                    },
                    data = f.Name,
                    state = "open",
                };
                if (f.FullName.IsImage())
                {
                    t.attr.Rel = "image";
                    t.attr.IsImage = true;
                }
                else
                {
                    t.attr.Rel = "file";
                }
                node.children.Add(t);
            }
        }

        /// <summary>
        /// Get files / folders from directory
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public List<FileTreeModel> PopulateChild(string relativePath)
        {
            var physicalPath = HttpContext.Current.Server.MapPath(relativePath);
            var directory = new DirectoryInfo(physicalPath);

            var children = directory.GetDirectories().Where(d => !d.Name.Equals(KentConfiguration.ResizedFolder))
                .Select(d => new FileTreeModel
                {
                    attr =
                        new FileTreeAttribute
                        {
                            Id = string.Format("{0}/{1}", relativePath, d.Name).ToIdString(),
                            Path = string.Format("{0}/{1}", relativePath, d.Name),
                            Rel = "folder"
                        },
                    data = d.Name,
                    state = "closed"
                }).ToList();

            foreach (var f in directory.GetFiles())
            {
                var t = new FileTreeModel
                {
                    attr = new FileTreeAttribute
                    {
                        Id = string.Format("{0}/{1}", relativePath, f.Name).ToIdString(),
                        Path = string.Format("{0}/{1}", relativePath, f.Name),
                        Class = "jstree-leaf"
                    },
                    data = f.Name,
                    state = "open"
                };
                if (f.FullName.IsImage())
                {
                    t.attr.Rel = "image";
                    t.attr.IsImage = true;
                }
                else
                {
                    t.attr.Rel = "file";
                }
                children.Add(t);
            }
            return children;
        }

        /// <summary>
        /// Move file / folder
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="isCopy"></param>
        /// <returns></returns>
        public ResponseModel MoveData(string source, string target, bool isCopy)
        {
            var response = new ResponseModel
            {
                Success = false
            };
            var status = MoveNodeStatus.Success;
            try
            {
                var sourcePhysicalPath = HttpContext.Current.Server.MapPath(source);
                var targetPhysicalPath = HttpContext.Current.Server.MapPath(target);
                if (Directory.GetParent(sourcePhysicalPath).FullName.Equals(targetPhysicalPath))
                    status = MoveNodeStatus.MoveSameLocation;
                else
                {
                    // get the file attributes for file or directory
                    var attPath = File.GetAttributes(sourcePhysicalPath);
                    var attDestination = File.GetAttributes(targetPhysicalPath);

                    var fi = new FileInfo(sourcePhysicalPath);

                    if (attDestination != FileAttributes.Directory)
                    {
                        status = MoveNodeStatus.MoveNodeToFile;
                    }
                    else if (target.Contains(source))
                    {
                        status = MoveNodeStatus.MoveParentNodeToChild;
                    }
                    else
                    {
                        //detect whether its a directory or file
                        if ((attPath & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            //Move parent folder to children node
                            var targetFolder = string.Format("{0}/{1}", targetPhysicalPath, fi.Name);
                            MoveDirectory(sourcePhysicalPath, targetFolder, isCopy);
                            var relativePath = Path.Combine(target, fi.Name);
                            response.Data = new FileTreeAttribute
                            {
                                Id = relativePath.ToIdString(),
                                Path = relativePath,
                                IsImage = false
                            };
                        }
                        else
                        {
                            var fileName = Path.GetFileName(sourcePhysicalPath) ?? string.Empty;
                            fileName = GetRightFileNameToSave(targetPhysicalPath, fileName);
                            var targetFile = Path.Combine(targetPhysicalPath, fileName);
                            if (isCopy)
                            {
                                File.Copy(sourcePhysicalPath, targetFile);
                            }
                            else File.Move(sourcePhysicalPath, targetFile);

                            response.Data = new FileTreeAttribute
                            {
                                Id = targetFile.ToIdString(),
                                Path = targetFile
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.ErrorException(exception);
                status = MoveNodeStatus.Failure;
                response.Message = exception.Message;
            }
            switch (status)
            {
                case MoveNodeStatus.MoveParentNodeToChild:
                    //response.Message =
                    //    T("Media_Message_MoveData_MoveParentToChild");
                    break;
                case MoveNodeStatus.MoveNodeToFile:
                    //response.Message =
                    //    T("Media_Message_MoveData_MoveItemToFile");
                    break;
                case MoveNodeStatus.MoveSameLocation:
                    //response.Message =
                    //    T("Media_Message_MoveData_MoveSameLocation");
                    break;
                case MoveNodeStatus.Failure:
                    //response.Message = TFormat("Media_Message_MoveData_CreateFolderFailure", response.Message);
                    break;
                case MoveNodeStatus.Success:
                    //response.Message = TFormat("Media_Message_MoveData_MoveSuccessfully", response.Message);
                    response.Success = true;
                    break;
            }
            return response;
        }

        /// <summary>
        /// Move directory
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="isCopy"></param>
        public void MoveDirectory(string source, string target, bool isCopy)
        {
            var stack = new Stack<FolderTransferModel>();
            stack.Push(new FolderTransferModel(source, target));

            while (stack.Count > 0)
            {
                var folders = stack.Pop();
                Directory.CreateDirectory(folders.Target);
                foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
                {
                    var fileName = Path.GetFileName(file) ?? string.Empty;
                    fileName = GetRightFileNameToSave(folders.Target, fileName);
                    var targetFile = Path.Combine(folders.Target, fileName);
                    if (isCopy)
                        File.Copy(file, targetFile);
                    else
                        File.Move(file, targetFile);
                }

                foreach (var folder in Directory.GetDirectories(folders.Source))
                {
                    var folderName = Path.GetFileName(folder) ?? string.Empty;
                    stack.Push(new FolderTransferModel(folder, Path.Combine(folders.Target, folderName)));
                }
            }
            if (!isCopy)
                Directory.Delete(source, true);
        }

        /// <summary>
        /// Create folder
        /// </summary>
        /// <param name="path"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public ResponseModel CreateFolder(string path, string folder)
        {
            try
            {
                var relativePath = Path.Combine(path, folder);
                var physicalPath = HttpContext.Current.Server.MapPath(relativePath);

                if (Directory.Exists(physicalPath))
                {
                    return new ResponseModel
                    {
                        Success = false,
                        //Message = T("Media_Message_CreateFolder_ExistingName")
                    };
                }
                Directory.CreateDirectory(physicalPath);

                return new ResponseModel
                {
                    Success = true,
                    Data = new FileTreeAttribute
                    {
                        Id = relativePath.ToIdString(),
                        Path = relativePath,
                        IsImage = false
                    },
                    //Message = T("Media_Message_CreateFolder_CreateSuccessfully")
                };
            }
            catch (Exception exception)
            {
                Logger.ErrorException(exception);
                return new ResponseModel
                {
                    Success = false,
                    //Message = TFormat("Media_Message_CreateFolder_CreateFailure", exception.Message)
                };
            }
        }

        /// <summary>
        /// Delete path
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public ResponseModel DeletePath(string relativePath)
        {
            try
            {
                var fullPath = HttpContext.Current.Server.MapPath(relativePath);
                var attr = File.GetAttributes(fullPath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Directory.Delete(fullPath, false);
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
                return new ResponseModel
                {
                    Success = true,
                    //Message = T("Media_Message_DeleteItem_DeleteSuccessfully")
                };
            }
            catch (Exception exception)
            {
                Logger.ErrorException(exception);
                return new ResponseModel
                {
                    Success = false,
                    //Message = TFormat("Media_Message_DeleteItem_DeleteFailure", exception.Message)
                };
            }
        }

        /// <summary>
        /// Rename file
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ResponseModel Rename(string relativePath, string name)
        {
            var response = new ResponseModel
            {
                Success = false
            };
            RenameStatus status;
            var physicalPath = HttpContext.Current.Server.MapPath(relativePath);
            var folderPath = Directory.GetParent(physicalPath).FullName;

            try
            {
                var newPath = Path.Combine(folderPath, name);

                // Check if current name and newname is the same
                var attPath = File.GetAttributes(physicalPath);
                if (attPath == FileAttributes.Directory)
                {
                    var diretoryInfo = new DirectoryInfo(physicalPath);
                    if (diretoryInfo.Name.Equals(name))
                    {
                        status = RenameStatus.Success;
                    }
                    else if (Directory.Exists(newPath))
                    {
                        status = RenameStatus.DuplicateName;
                    }
                    else
                    {
                        Directory.Move(physicalPath, newPath);
                        status = RenameStatus.Success;
                    }
                }
                else
                {
                    var currentFileName = Path.GetFileName(physicalPath);
                    if (currentFileName != null && currentFileName.Equals(name))
                    {
                        status = RenameStatus.Success;
                    }
                    else if (File.Exists(newPath))
                    {
                        status = RenameStatus.DuplicateName;
                    }
                    else
                    {
                        //Rename file
                        Directory.Move(physicalPath, newPath);
                        status = RenameStatus.Success;
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.ErrorException(exception);
                status = RenameStatus.Failure;
                response.Message = exception.Message;
            }

            switch (status)
            {
                case RenameStatus.DuplicateName:
                    //response.Message = T("Media_Message_RenameFolder_ExistingName");
                    break;
                case RenameStatus.Failure:
                    //response.Message = TFormat("Media_Message_RenameFolder_RenameFailure", response.Message);
                    break;
                case RenameStatus.Success:
                    //response.Message = T("Media_Message_RenameFolder_RenameSuccessfully");
                    response.Success = true;

                    var position = physicalPath.IndexOf(relativePath.Replace("/", "\\"), StringComparison.Ordinal);
                    if (position > 0)
                    {
                        //Get return result
                        var folder = folderPath.Substring(position).Replace("\\", "/");
                        response.Data = string.Format("{0}/{1}", folder, name);
                    }
                    break;
            }
            return response;
        }

        /// <summary>
        /// Get right file name to save
        /// </summary>
        /// <param name="targetFolder"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetRightFileNameToSave(string targetFolder, string file)
        {
            var thumb = Path.GetFileNameWithoutExtension(file);
            var name = thumb;
            var extension = Path.GetExtension(file);
            var suffix = 1;

            while (File.Exists(Path.Combine(targetFolder, thumb + extension)))
            {
                thumb = string.Format("{0}({1})", name, suffix);
                suffix++;
            }

            return string.Format("{0}{1}", thumb, extension);
        }

        #endregion
    }
}
