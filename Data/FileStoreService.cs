using DocumentStore.Enums;
using DocumentStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentStore.Data
{
    public class FileStoreService
    {
        private IDbContextFactory<FileStoreDBContext> _contextFactory;

        public FileStoreService(IDbContextFactory<FileStoreDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<FileDescription> GetSingleFileDescriptionAsync(Guid streamId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.FileDescriptions
                    .Where(x => x.StreamId == streamId)
                    .SingleOrDefaultAsync();
            }
        }

        public async Task<List<FileDescriptionShort>> GetFileDescriptionsAsync(Sections section)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.FileDescriptions
                .Where(x => x.SectionId == section)
                .Select(s => new FileDescriptionShort
                {
                    StreamId = s.StreamId,
                    FileName = s.FileName,
                    Description = s.Description
                }).ToListAsync();
            }
        }

        public async Task<FileStore> GetFileAsync(Guid streamId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.FileRepository.SingleOrDefaultAsync(s => s.StreamId == streamId);
            }
        }

        public async Task<FileStore> GetFileByNameAsync(string fileName)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.FileRepository.SingleOrDefaultAsync(s => s.Name == fileName);
            }
        }

        public async Task<int> DeleteFileAsync(Guid streamId)
        {
            FileStore file = new FileStore() { StreamId = streamId };
            using (var context = _contextFactory.CreateDbContext())
            {
                context.FileRepository.Remove(file);
                return await context.SaveChangesAsync();
            }
        }

        public async Task InsertFileDescriptionAsync(FileDescription fileDescription)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.FileDescriptions.Add(fileDescription);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateFileDescriptionAsync(FileDescription newFileDescription)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.FileDescriptions.Update(newFileDescription);

                await context.SaveChangesAsync();
            }
        }
    }
}
