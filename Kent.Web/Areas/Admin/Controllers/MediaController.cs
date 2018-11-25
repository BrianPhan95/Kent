using Kent.Business.Core.Models.Base;
using Kent.Business.Core.Models.Media;
using Kent.Business.Services.Media;
using Kent.Libary.Configurations;
using Kent.Libary.Enums;
using Kent.Libary.Models;
using Kent.Libary.Utilities;
using Kent.Libary.Utilities.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class MediaController : BackendController
    {
        private readonly IMediaFileManager _mediaFileManager;
        private readonly IMediaService _mediaService;

        public MediaController(IMediaFileManager mediaFileManager, IMediaService mediaService)
        {
            _mediaFileManager = mediaFileManager;
            _mediaService = mediaService;
        }

        // GET: Admin/Media
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        #region Media Browser
        [Authorize(Roles = "Admin")]
        public ActionResult MediaBrowser(string rootFolder, string imageUrl, MediaBrowserSelectMode mode = MediaBrowserSelectMode.All)
        {
            if (string.IsNullOrEmpty(rootFolder))
            {
                rootFolder = KentConfiguration.MediaPath;
            }
            var model = new MediaBrowserSetupModel
            {
                RootFolder = rootFolder,
                Mode = mode
            };

            if (!string.IsNullOrEmpty(imageUrl))
            {
                model.FileTreeAttribute = new FileTreeAttribute
                {
                    Id = imageUrl.ToIdString(),
                    Path = imageUrl,
                    IsImage = imageUrl.IsImage(),
                };
            }

            return View(model);
        }

        #endregion

        #region Post

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult FileUpload(string qqfile, string dir)
        {
            var physicalPath = Server.MapPath(dir);
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            string file;
            try
            {
                var stream = Request.InputStream;
                if (String.IsNullOrEmpty(Request["qqfile"]))
                {
                    // IE
                    var postedFile = Request.Files[0];
                    if (postedFile != null)
                    {
                        stream = postedFile.InputStream;
                        file = Path.Combine(physicalPath, Path.GetFileName(postedFile.FileName));
                    }
                    else
                    {
                        return Json(new ResponseModel
                        {
                            Success = false,
                            //Message = T("Media_Message_FileUploadEmpty")
                        });
                    }
                }
                else
                {
                    //Webkit, Mozilla
                    file = Path.Combine(physicalPath, qqfile);
                }

                //Check if path exists or not, if exists then assign new path for image
                file = file.GetRightFilePathToSave();

                if (file.IsImage())
                {
                    using (var img = System.Drawing.Image.FromStream(stream))
                    {
                        //var imageUploadSetting = _siteSettingService.LoadSetting<ImageUploadSetting>();
                        //if (imageUploadSetting != null)
                        //{
                        //    if (imageUploadSetting.MinWidth.HasValue && img.Width < imageUploadSetting.MinWidth)
                        //    {
                        //        return
                        //            Json(
                        //                new
                        //                {
                        //                    Success = false,
                        //                    Message = TFormat("Media_Message_InvalidMinWidth", imageUploadSetting.MinWidth)
                        //                },
                        //                "text/html");
                        //    }
                        //    if (imageUploadSetting.MinHeight.HasValue && img.Height < imageUploadSetting.MinHeight)
                        //    {
                        //        return
                        //            Json(
                        //                new ResponseModel
                        //                {
                        //                    Success = false,
                        //                    Message = TFormat("Media_Message_InvalidMinHeight", imageUploadSetting.MinHeight)
                        //                },
                        //                "text/html");
                        //    }
                        //    if (imageUploadSetting.MaxWidth.HasValue && img.Width > imageUploadSetting.MaxWidth)
                        //    {
                        //        return
                        //            Json(
                        //                new ResponseModel
                        //                {
                        //                    Success = false,
                        //                    Message = TFormat("Media_Message_InvalidMaxWidth", imageUploadSetting.MaxWidth)
                        //                },
                        //                "text/html");
                        //    }
                        //    if (imageUploadSetting.MaxHeight.HasValue && img.Height > imageUploadSetting.MaxHeight)
                        //    {
                        //        return
                        //            Json(
                        //                new ResponseModel
                        //                {
                        //                    Success = false,
                        //                    Message = TFormat("Media_Message_InvalidMaxHeight", imageUploadSetting.MaxHeight)
                        //                },
                        //                "text/html");
                        //    }
                        //}
                        img.Save(file);
                    }
                }
                else
                {
                    FileUtilities.SaveFile(file, stream);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseModel(ex), "text/html");
            }

            var isImage = file.IsImage();

            var location = string.Format("{0}/{1}", dir, Path.GetFileName(file));
            return Json(new
            {
                Success = true,
                //Message = T("Media_Message_UploadSuccessfully"),
                fileLocation = location,
                id = location.ToIdString(),
                isImage
            }, "text/html");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult GetTreeData(string rootFolder, string dir)
        {
            if (string.IsNullOrEmpty(rootFolder))
            {
                rootFolder = KentConfiguration.MediaPath;
            }

            if (string.IsNullOrWhiteSpace(dir))
            {
                var rootNode = new FileTreeModel
                {
                    attr = new FileTreeAttribute
                    {
                        Id = rootFolder.ToIdString(),
                        Rel = "home",
                        Path = rootFolder
                    },
                    state = "open",
                    data = rootFolder.Replace("/", "").CamelFriendly().ToUpper()
                };
                _mediaService.PopulateTree(rootFolder, rootNode);
                return Json(rootNode);
            }
            return Json(_mediaService.PopulateChild(dir));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult MoveData(string path, string destination, bool copy)
        {
            return Json(_mediaService.MoveData(path, destination, copy));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult CreateFolder(string path, string folder)
        {
            return Json(_mediaService.CreateFolder(path, folder));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Delete(string path)
        {
            return Json(_mediaService.DeletePath(path));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Rename(string path, string name)
        {
            return Json(_mediaService.Rename(path, name));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult GetFileInfo(string path)
        {
            try
            {
                path = _mediaService.MapPath(path);

                // get the file attributes for file or directory
                var attr = System.IO.File.GetAttributes(path);
                FileInfoModel model;

                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var info = new DirectoryInfo(path);
                    model = new FileInfoModel
                    {
                        FileName = info.Name,
                        Created = info.CreationTimeUtc.ToLongDateString(),
                        LastUpdate = info.LastWriteTimeUtc.ToLongDateString(),
                        FileSize = string.Empty
                    };
                }
                else
                {
                    var info = new FileInfo(path);
                    model = new FileInfoModel
                    {
                        FileName = info.Name,
                        Created = info.CreationTimeUtc.ToLongDateString(),
                        LastUpdate = info.LastWriteTimeUtc.ToLongDateString(),
                        FileSize = string.Format("{0} Bytes", info.Length),
                    };
                }
                return Json(new ResponseModel
                {
                    Success = true,
                    Data = model
                });

            }
            catch (Exception exception)
            {
                return Json(new ResponseModel
                {
                    Success = true,
                    Message = exception.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult CheckPathValid(string path, MediaBrowserSelectMode mode)
        {
            return Json(_mediaService.CheckPathValid(path, mode));
        }

        #endregion
    }
}