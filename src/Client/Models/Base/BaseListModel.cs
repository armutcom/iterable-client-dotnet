using System.Collections.Generic;

namespace Armut.Iterable.Client.Models.Base
{
    public class BaseListModel
    {
        public int SuccessCount { get; set; }

        public int FailCount { get; set; }
        
        public IEnumerable<string> InvalidEmails { get; set; }
        
        public IEnumerable<string> InvalidUserIds { get; set; }
    }
}