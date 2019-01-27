using DarkUI.Controls;
using DarkUI.Docking;

namespace Example.Forms.Docking
{
    public partial class DockProject : DarkToolWindow
    {
        #region Constructor Region

        public DockProject()
        {
            InitializeComponent();

            // Build dummy nodes
            var childCount = 0;
            for (var i = 0; i < 1; i++)
            {
                var node = new DarkTreeNode($"Root node #{i}");
                node.ExpandedIcon = Icons.folder_open;
                node.Icon = Icons.folder_closed;
                node.IsFolder = true;
                treeProject.Nodes.Add(node);

                for (var j = 0; j < 5; j++)
                {
                    var node2 = new DarkTreeNode($"Root node #{j}");
                    node2.ExpandedIcon = Icons.folder_open;
                    node2.Icon = Icons.folder_closed;
                    node2.IsFolder = true;
                    node2.SuffixText = "fuck";

                    node.Nodes.Add(node2);
                    for (var x = 0; x < 5; x++)
                    {
                        var childNode = new DarkTreeNode($"Child node #{childCount}");
                        childNode.Icon = Icons.files;

                        childCount++;
                        node2.Nodes.Add(childNode);
                    }

                   
                }


                
            }
        }

        #endregion

        private void TreeProject_Click(object sender, System.EventArgs e)
        {

        }
    }
}
