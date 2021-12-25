using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Application.Helper
{
    public static class FileHelper
    {
        public static string ProfilePictureAddress(Guid userId, string extention)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Startup.Configuration["DefaultRoutes:profilePicture"]);
            string file = Path.Combine(path, $"{userId}.{extention}");
            return file;
        }

        public static string TeamLogoAddress(Guid teamId, string extention)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Startup.Configuration["DefaultRoutes:teamLogo"]);
            string file = Path.Combine(path, $"{teamId}.{extention}");
            return file;
        }

        public static string EventBannerAddress(Guid eventId, string extention)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Startup.Configuration["DefaultRoutes:eventBanner"]);
            string file = Path.Combine(path, $"{eventId}.{extention}");
            return file;
        }

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToUpperInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string> {
                {".AAC", "audio/aac"},
                {".ABW", "application/x-abiword"},
                {".ARC", "application/x-freearc"},
                {".AVI", "video/x-msvideo"},
                {".AZW", "application/vnd.amazon.ebook"},
                {".BIN", "application/octet-stream"},
                {".BMP", "image/bmp"},
                {".BZ", "application/x-bzip"},
                {".BZ2", "application/x-bzip2"},
                {".CSH", "application/x-csh"},
                {".CSS", "text/css"},
                {".CSV", "text/csv"},
                {".DOC", "application/msword"},
                {".DOCX", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".EOT", "application/vnd.ms-fontobject"},
                {".EPUB", "application/epub+zip"},
                {".EXE", "application/octet-stream"},
                {".GZ", "application/gzip"},
                {".GIF", "image/gif"},
                {".HTM", "text/html"},
                {".HTML", "text/html"},
                {".ICO", "image/vnd.microsoft.icon"},
                {".ICS", "text/calendar"},
                {".JAR", "application/java-archive"},
                {".JPEG", "image/jpeg"},
                {".JPG", "image/jpeg"},
                {".JS", "text/javascript"},
                {".JSON", "application/json"},
                {".JSONLD", "application/ld+json"},
                {".MID", "audio/x-midi"},
                {".MIDI", "audio/midi"},
                {".MJS", "text/javascript"},
                {".MP3", "audio/mpeg"},
                {".MPEG", "video/mpeg"},
                {".MPKG", "application/vnd.apple.installer+xml"},
                {".ODP", "application/vnd.oasis.opendocument.presentation"},
                {".ODS", "application/vnd.oasis.opendocument.spreadsheet"},
                {".ODT", "application/vnd.oasis.opendocument.text"},
                {".OGA", "audio/ogg"},
                {".OGV", "video/ogg"},
                {".OGX", "application/ogg"},
                {".OPUS", "audio/opus"},
                {".OTF", "font/otf"},
                {".PNG", "image/png"},
                {".PDF", "application/pdf"},
                {".PHP", "application/x-httpd-php"},
                {".PPT", "application/vnd.ms-powerpoint"},
                {".PPTX", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                {".RAR", "application/vnd.rar"},
                {".RTF", "application/rtf"},
                {".SH", "application/x-sh"},
                {".SVG", "image/svg+xml"},
                {".SWF", "application/x-shockwave-flash"},
                {".TAR", "application/x-tar"},
                {".TIF", "image/tiff"},
                {".TIFF", "image/tiff"},
                {".TS", "video/mp2t"},
                {".TTF", "font/ttf"},
                {".TXT", "text/plain"},
                {".VSD", "application/vnd.visio"},
                {".WAV", "audio/wav"},
                {".WEBA", "audio/webm"},
                {".WEBM", "video/webm"},
                {".WEBP", "image/webp"},
                {".WOFF", "font/woff"},
                {".WOFF2", "font/woff2"},
                {".XHTML", "application/xhtml+xml"},
                {".XLS", "application/vnd.ms-excel"},
                {".XLSX", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".XML", "application/xml"}, // if readable from casual users { ".XML", "text/xml"},
                {".XUL", "application/vnd.mozilla.xul+xml"},
                {".ZIP", "application/zip"},
                {".3GP", "video/3gpp"}, //if it doesn't contain video { ".3GP", "audio/3gpp"},
                {".3G2", "video/3gpp2"}, //if it doesn't contain video { ".3G2", "audio/3gpp2"},
                {".7Z", "application/x-7z-compressed"}
            };
        }
    }
}