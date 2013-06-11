
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CallCenterManager.View;

namespace CallCenterManager.Util
{
    class Delegates
    {
		public delegate Series SeriesResultChartStringParams(Chart chart, string str);

		public delegate TreeNode TreeNodeResultTreeNodeObjectParams(TreeNode parent_node, object tag);
		public delegate TreeNode TreeNodeResultStringParams(string str);

		public delegate void VoidResultChartSeriesParams(Chart chart, Series series);
		public delegate void VoidResultChartStringUlongUlongParams(Chart chart, string str, ulong x, ulong y);
		public delegate void VoidResultLoginStateParams(LoginState state);
		public delegate void VoidResultTreeViewParams(TreeView tree_view);
		public delegate void VoidResultTreeNodeParams(TreeNode node);
		public delegate void VoidResultTreeNodeStringIntParams(TreeNode node, string str, int integer);
		public delegate void VoidResultTreeNodeStringParams(TreeNode node, string str);
		public delegate void VoidResultTreeNodeTreeNodeParams(TreeNode parent_node, TreeNode node);
 
		public delegate void VoidResultVoidParams();
	}
}
