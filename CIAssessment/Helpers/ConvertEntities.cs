using CIAssessment.Models;
using CIAssessment.Models.CutomModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIAssessment.Helpers
{
    public class ConvertEntities
    {
        public static Node GetNodeFromEntity(Configuration config,Bom bom =null,bool isPart = true)
        {
            if (config == null)
                return null;

            var node = new Node()
            {
                Id = config.Id.GetValueOrDefault(),
                IsRoot = config.IsRoot.GetValueOrDefault(),
                IsActive = config.IsActive.GetValueOrDefault(),
                Revision = config.Revision,
                BomPartNumber = config.BomPartNumber,
                PartNumber = config.PartNo,
                Preview = config.Preview,
                Description = config.Description,
                FileName = Path.GetFileName(config.FilePath),
                AbsolutePath = config.FilePath,
                IsPart = isPart ? Visibility.Visible : Visibility.Hidden,
                IsFile = isPart ? Visibility.Hidden : Visibility.Visible,
            };

            AddBomInfo(node, bom);

            return node;
        }

        public static void AddBomInfo(Node node,Bom bom)
        {
            node.Quantity = 1;
            node.IsExcluded = false;
            node.IsSuppressed = false;

            if(bom != null)
            {
                node.Quantity = bom.Qty.GetValueOrDefault();
                node.IsExcluded = bom.Excluded.GetValueOrDefault();
                node.IsSuppressed = bom.Suppressed.GetValueOrDefault();
            }

        }

        public static void AddChildNode(Node parent, Node Child)
        {
            parent.ChildNodes.Add(Child);
            Child.ParentNode = parent;
        }

        public static Node UpdateNodeVisibility(Node node,bool isPart = true,bool isFile = false)
        {
            if (node == null)
                return null;

            node.IsPart = GetVisibility(isPart);
            node.IsFile = GetVisibility(isFile);

            return node;
        }

        private static Visibility GetVisibility(bool isVisible)
        {
            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public static string GetJsonForNode(Node node)
        {
            var basedir = Directory.GetCurrentDirectory();
            var fileName = Path.GetFileNameWithoutExtension(node.FileName);

            var basepath = $"{basedir}{Path.DirectorySeparatorChar}Json";
            if (!Directory.Exists(basepath))
                Directory.CreateDirectory(basepath);

            var filepath = $"{basepath}{Path.DirectorySeparatorChar}{fileName}.json";

            using (StreamWriter file = File.CreateText(filepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, node);
            }

            return filepath;
        }
    }
}
