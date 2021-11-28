using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Application.Common.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<Result> UploadFile(this FileAttachment file, string repoPath, string content,
            CancellationToken cancellationToken)
        {
            try
            {
                var uploadFolder = $"{repoPath}\\{file.GetPath()}";

                var path = Path.Combine(uploadFolder, $"{file.UniqueCode}{file.Extension}");

                var txtFile = Path.Combine(uploadFolder, $"{file.UniqueCode}.txt");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                await File.WriteAllBytesAsync(path, Convert.FromBase64String(content), cancellationToken);

                await File.WriteAllTextAsync(txtFile, content, cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }
    }
}