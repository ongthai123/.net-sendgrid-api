using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sending_Email_API.ViewModels
{
    public class SendingEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HTMLContent { get; set; }
    }
}
