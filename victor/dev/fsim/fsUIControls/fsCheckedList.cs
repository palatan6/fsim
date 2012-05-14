using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace fsUIControls
{
    public partial class fsCheckedList : UserControl
    {
        public fsCheckedList()
        {
            InitializeComponent();
        }

        public void AssignList(List<string> list)
        {
            listView1.Items.Clear();
            foreach (string s in list)
            {
                listView1.Items.Add(s);
            }
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int index = 0; index < listView1.Items.Count; ++index)
            {
                listView1.Items[index].Checked = selectAllCheckBox.Checked;
            }
        }

        public ListView.CheckedListViewItemCollection GetCheckedItems()
        {
            return listView1.CheckedItems;
        }

        public void CheckItem(string toCheck)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text == toCheck)
                {
                    item.Checked = true;
                }
            }
        }
    }
}