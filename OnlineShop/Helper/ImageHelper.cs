using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace OnlineShop.Helper
{
    public static class ImageHelper
    {
        public static string SaveImage(HttpPostedFileBase sourceImage, string ImagePath)
        {
            #region build image name
            string _FileName = Path.GetFileName(sourceImage.FileName);
            var index = _FileName.LastIndexOf('.');
            var filename = Guid.NewGuid().ToString() + _FileName.Substring(index, _FileName.Count() - index);
            #endregion
            var newImage = new WebImage(sourceImage.InputStream);

            var width = newImage.Width;
            var height = newImage.Height;

            if (width > height)
            {
                var leftRightCrop = (width - height) / 2;
                newImage.Crop(0, leftRightCrop, 0, leftRightCrop);
            }
            else if (height > width)
            {
                var topBottomCrop = (height - width) / 2;
                newImage.Crop(topBottomCrop, 0, topBottomCrop, 0);
            }
            newImage.FileName = filename;
            string _path = Path.Combine(ImagePath, filename);
            newImage.Save(_path);
            return filename;
            //do something with cropped image...
            //newImage.GetBytes();
        }
    }
}