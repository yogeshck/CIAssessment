using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIAssessment.Models.CutomModel
{
    public class TabItem
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public Node Content { get; set; }

        public ObservableCollection<Node> Children { 
            get
            {
                return IterateTree();
            }
        }

        private ObservableCollection<Node> IterateTree()
        {
            var list = new List<Node>();

            /*var childs = Content.ChildNodes.OrderBy(d => d.Id).ToList();
            foreach (var child in childs)
                IterateTree(list, child);*/
            IterateTree(list,Content);

            return new ObservableCollection<Node>(list);
        }

        private void IterateTree(List<Node> children,Node parent)
        {
            if (parent.Level > 0)
                children.Add(parent);
            else
                return;

            if (parent.ChildNodes.Count == 0)
                return;

            var chilenodes = parent.ChildNodes.OrderBy(d => d.Id).ToList();
            foreach (var child in chilenodes)
                IterateTree(children, child);
        }
    }
}
