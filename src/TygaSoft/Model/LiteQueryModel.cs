using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public class LiteQueryModel
    {
        public Guid ApplicationId { get; set; }
        public Guid CategoryId { get; set; }

        public Guid MgrDepmtId { get; set; }

        public Guid UseDepmtId { get; set; }

        public Guid StoragePlaceId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserRule { get; set; }

        public string Keyword { get; set; }
    }
}
