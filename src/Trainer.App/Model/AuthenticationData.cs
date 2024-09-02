using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainer.App.Model
{
    internal class AuthenticationData
    {
        public required string Token { get; set; }
        public DateTime ExpireOn { get; set; }
    }
}
