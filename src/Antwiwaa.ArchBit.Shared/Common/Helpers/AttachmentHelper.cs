using System;
using Antwiwaa.ArchBit.Shared.Common.Enums;

namespace Antwiwaa.ArchBit.Shared.Common.Helpers
{
    public static class AttachmentHelper
    {
        public static string GetAsPhoto(string base64String, PhotoFormat photoFormat)
        {
            var dataContentType = photoFormat switch
            {
                PhotoFormat.Jpeg => "data:image/jpg;base64,",
                PhotoFormat.Png => "data:image/png;base64,",
                _ => throw new ArgumentOutOfRangeException(nameof(photoFormat), photoFormat, null)
            };

            return $"{dataContentType}{base64String}";
        }


        public static string GetAsDocument(string base64String, DocumentFormat documentFormat)
        {
            var dataContentType = documentFormat switch
            {
                DocumentFormat.Png => "data:image/png;base64,",
                DocumentFormat.Jpg => "data:image/jpg;base64,",

                DocumentFormat.Pdf => "data:application/pdf;base64,",
                DocumentFormat.Doc => "data:application/msword,base64",
                DocumentFormat.Docx =>
                    "data:application/vnd.openxmlformats-officedocument.wordprocessingml.document,base64",
                _ => throw new ArgumentOutOfRangeException(nameof(documentFormat), documentFormat, null)
            };

            return $"{dataContentType}{base64String}";
        }

        public static PhotoFormat GetPhotoFormat(this string format)
        {
            return format.Equals(".png", StringComparison.OrdinalIgnoreCase) ? PhotoFormat.Png : PhotoFormat.Jpeg;
        }


        public static DocumentFormat GetDocumentFormat(this string format)
        {
            switch (format)
            {
                case ".pdf":
                    return DocumentFormat.Pdf;

                case ".png":
                    return DocumentFormat.Png;
                case ".jpeg":
                case ".jpg":
                case ".jfif":
                    return DocumentFormat.Jpg;
                case ".tif":
                case ".tiff":
                    return DocumentFormat.Tif;
                case ".gif":
                    return DocumentFormat.Gif;
                case ".webp":
                    return DocumentFormat.Webp;
                case ".bmp":
                    return DocumentFormat.Bmp;

                case ".doc":
                    return DocumentFormat.Doc;
                case ".docx":
                    return DocumentFormat.Docx;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }


        public static string GetDocumentPreviewContentType(this string format)
        {
            var dataContentType = format.ToLower() switch
            {
                ".png" => "image/png",
                ".jpeg" => "image/jpg",
                ".jpg" => "image/jpg",
                ".jfif" => "image/jpg",
                ".gif" => "image/gif",
                ".tiff" => "image/tiff",
                ".tif" => "image/tiff",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",

                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
            };

            return dataContentType;
        }


        public static string GetFile(string base64String)
        {
            return $"{base64String}";
        }
    }
}