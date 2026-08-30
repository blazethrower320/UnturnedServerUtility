using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnturnedServerUtility.Models
{
    public class ServerFilesModel
    {
        public string fileName { get; set; }
        public bool isFolder { get; set; } = false;
    }
}
