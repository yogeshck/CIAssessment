using CIAssessment.Models;
using CIAssessment.Models.CutomModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIAssessment.Helpers
{
    public class ConvertEntities
    {
        public static Node GetNodeFromEntity(Configuration config)
        {
            return new Node()
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
            };
        }

        public static void AddChildNode(Node parent, Node Child)
        {
            parent.ChildNodes.Add(Child);
            Child.ParentNode = parent;
        }
    }
}
