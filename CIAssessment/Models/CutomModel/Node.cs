using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIAssessment.Models.CutomModel
{
    public class Node
    {
        public Node()
        {
            if(ChildNodes == null)
                ChildNodes = new List<Node>();
        }

        public long Id { get; set;}
        public bool IsRoot { get; set;}
        public bool IsActive { get; set;}
        public string FileName { get; set; }
        public string AbsolutePath { get; set; }
        public string PartNumber { get; set; }
        public string BomPartNumber { get; set; }
        public string Revision { get; set; }
        public string Description { get; set; }
        public byte[] Preview { get; set; } 

        public Node ParentNode { get; set; }
        public List<Node> ChildNodes { get; set; }
    }
}
