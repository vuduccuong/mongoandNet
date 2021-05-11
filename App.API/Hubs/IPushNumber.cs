using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Hubs
{
   
        public interface IPushNumber
        {
            Task PushNumberToClient(long number, string groupName);
        }
    
}
