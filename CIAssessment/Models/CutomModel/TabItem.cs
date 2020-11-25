using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIAssessment.Models.CutomModel
{
    /// <summary>
    /// Custom entity to represent tab item
    /// </summary>
    public class TabItem
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public Node Content { get; set; }
    }
}
