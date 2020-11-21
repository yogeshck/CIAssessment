using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIAssessment.Models.CutomModel
{
    public class NodeExtension
    {
        public long _id { get; set; }
        public long Quantity { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsSuppressed { get; set; }

        public Node ChildNode { get; set; }
    }
}
