using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Entities
{
    public class HelpFriend : BaseEntity
    {
        public long PersonId { get; set; }
        public long UserId { get; set; }
        public string WhyHelp { get; set; } = string.Empty;

        public Users Users { get; set; }
        public Person Person { get; set; }
    }
}
