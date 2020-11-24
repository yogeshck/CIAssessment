using CIAssessment.Helpers;
using CIAssessment.Models;
using CIAssessment.Models.CutomModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIAssessment.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private DataEntities dataContext;

        public RepositoryService(DataEntities datacontext = null)
        {
            if (datacontext == null)
                dataContext = new DataEntities();
            else
                dataContext = datacontext;
        }

        public List<TabItem> GetRootAssemblies(bool isPart=true)
        {
            var items = new List<TabItem>();

            var RootConfigs =  (from c in dataContext.Configurations
                                    where c.IsRoot.HasValue && c.IsRoot.Value && !string.IsNullOrEmpty(c.ConfigurationName)
                                    orderby c.Id descending
                                    select c).ToList();

            foreach(var rootConfig in RootConfigs)
            {
                //if(rootConfig.Id == 1)
                //{
                    var rootNode = ConvertEntities.GetNodeFromEntity(rootConfig,null,isPart);
                    rootNode.Level = 1;
                    List<Node> display = new List<Node>();
                    LoadData(display, rootNode);
                    var tabitem = new TabItem()
                    {
                        Header = rootConfig.ConfigurationName,
                        Id = rootConfig.Id.Value,
                        Content = LoadData(rootNode,isPart),
                    };
                    items.Add(tabitem);
               // }
            }
            return items;
        }

        public void UpdateTabItemVisibilty(Node node, bool isPart = true,bool isFile=true)
        {
            if (node == null)
                return;

            if (node.IsParent)
            {
                foreach (Node n in node.ChildNodes)
                    UpdateTabItemVisibilty(n, isPart, isFile);
            }

            ConvertEntities.UpdateNodeVisibility(node, isPart, isFile);
        }

        private Node LoadData(Node parent, bool isPart = true)
        {
            if (parent == null)
                return null;

            var boms = (from b in dataContext.Boms
                          where b.ParentConfigIndex == parent.Id
                          select b).ToList();

            if (boms == null || boms.Count == 0)
                return null;

            foreach(var bom in boms)
             {
                var config = (from c in dataContext.Configurations
                              where (!c.IsRoot.HasValue || !c.IsRoot.Value) && c.Id == bom.ChildConfigIndex
                              select c).FirstOrDefault();

                if(config != null)
                 {
                    var node = ConvertEntities.GetNodeFromEntity(config,bom,isPart);

                    if(node != null)
                    {
                        node.Level = parent.Level + 1;
                        LoadData(node);
                        ConvertEntities.AddChildNode(parent, node);
                    }
                }
            }

            return parent;
        }

        private void LoadData(List<Node> children, Node parent,bool isPart = true)
        {
            if (children == null)
                return;

            if (parent == null)
                return;

            var boms = (from b in dataContext.Boms
                        where b.ParentConfigIndex == parent.Id
                        select b).ToList();

            if (boms == null || boms.Count == 0)
                children.Add(parent);

            foreach (var bom in boms)
            {
                var config = (from c in dataContext.Configurations
                              where (!c.IsRoot.HasValue || !c.IsRoot.Value) && c.Id == bom.ChildConfigIndex
                              select c).FirstOrDefault();

                if (config != null)
                {
                    var node = ConvertEntities.GetNodeFromEntity(config, bom, isPart);

                    if (node != null)
                    {
                        node.Level = parent.Level + 1;
                        node.ParentNode = parent;
                        LoadData(children,node);
                        children.Add(node);
                    }
                }
            }
        }
    }
}
