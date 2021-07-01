using DocumentStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentStore.Data
{
    public class FileStoreDBContext : DbContext
    {
        public FileStoreDBContext(DbContextOptions<FileStoreDBContext> options)
            : base(options)
        {
            // Set command timeout to 1 minute (default is 30s). Increase if longer transactions are expected (executing commands, running queries). 
            Database.SetCommandTimeout(60);
        }

        public DbSet<FileStore> FileRepository { get; set; }

        public DbSet<FileDescription> FileDescriptions { get; set; }
    }
}
