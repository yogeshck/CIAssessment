using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIAssessment.Models.CutomModel
{
    /// <summary>
    /// Custom Entity to represent the Data Model.
    /// </summary>
    public class Node
    {
        #region Constructor
        public Node()
        {
            if (ChildNodes == null)
                ChildNodes = new ObservableCollection<Node>();
        }

        public Node(Node parent) : this()
        {
            ParentNode = parent;
        }

        public Node(Node parent,List<Node> children)
        {
            ParentNode = parent;
            ChildNodes = new ObservableCollection<Node>(children);
        }
        #endregion

        #region View Component
        [JsonIgnore]
        public Visibility IsPart { get; set; }
        [JsonIgnore]
        public Visibility IsFile { get; set; }

        #endregion

        #region Entity Properties
        public long Id { get; set;}
        public bool IsRoot { get; set;}
        public bool IsActive { get; set;}
        public string FileName { get; set; }
        public string AbsolutePath { get; set; }
        public string PartNumber { get; set; }
        public string BomPartNumber { get; set; }
        public string Revision { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public byte[] Preview { get; set; }
        public double Quantity { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsSuppressed { get; set; }
        #endregion

        #region User Interface Properties
        [JsonIgnore]
        public int Level { get; set; }
        [JsonIgnore]
        public bool IsParent { get { return ChildNodes != null && ChildNodes.Count > 0; } }
        [JsonIgnore]
        public bool HasParent { get { return ParentNode != null; } }
        #endregion

        #region Tree Relationship
        public ObservableCollection<Node> ChildNodes { get; set; }
        [JsonIgnore]
        public Node ParentNode { get; set; }
        #endregion
    }
}
