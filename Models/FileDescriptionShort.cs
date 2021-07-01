using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentStore.Models
{
    public class FileDescriptionShort
    {
        public Guid StreamId { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
    }
}
