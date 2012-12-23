using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Parameters;
using CalculatorModules.User_Controls.Help_Dialogs;

namespace CalculatorModules.Hydrocyclone.Feeds
{
    public partial class fsFeedCurvesSelectionControl : UserControl
    {
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return groupBox1.Text;
            }
            set
            {
                groupBox1.Text = value;
            }
        }

        #region Data

        private List<fsYAxisParameterWithChecking> m_yAxisParameters;
        //public List<fsYAxisParameterWithChecking> m_yAxisParameters;

        //private Dictionary<ListViewItem, fsYAxisParameterWithChecking> m_itemToYParameter =
        //    new Dictionary<ListViewItem, fsYAxisParameterWithChecking>();
        //public Dictionary<ListViewItem, fsYAxisParameterWithChecking> m_itemToYParameter =
        //    new Dictionary<ListViewItem, fsYAxisParameterWithChecking>();
       
        //// --------------- new approach to compare ListViewItems (for suit drag-and-drop) -----
        //public Dictionary<ListViewItem, fsYAxisParameterWithChecking> m_itemToYParameter =
        //    new Dictionary<ListViewItem, fsYAxisParameterWithChecking>(new EqualityComparer());

        //protected class EqualityComparer : IEqualityComparer<ListViewItem>
        //{

        //    public bool Equals(ListViewItem x, ListViewItem y)
        //    {
        //        return x.Text == y.Text;
        //    }

        //    public int GetHashCode(ListViewItem obj)
        //    {
        //        return obj.Text.GetHashCode();
        //    }
        //}
        ////------------------------------------------------------------

        #endregion

        public fsFeedCurvesSelectionControl()
        {
            InitializeComponent();
        }

        //public fsFeedCurvesSelectionControl(FeedCurvesControl fc)
        //{
        //    InitializeComponent();
        //    feedsControl = fc;
        //}

        #region Public Methods

        //public void AssignYAxisParameters(List<fsYAxisParameterWithChecking> parameters)
        //{
        //    AssignYAxisParameters(parameters, ref m_yAxisParameters, ref m_itemToYParameter);
        //}

        //private void AssignYAxisParameters(
        //    IEnumerable<fsYAxisParameterWithChecking> externalParameters,
        //    ref List<fsYAxisParameterWithChecking> internalParameters,
        //    ref Dictionary<ListViewItem, fsYAxisParameterWithChecking> internalItemToParameter)
        //{
        //    internalParameters = new List<fsYAxisParameterWithChecking>();
        //    foreach (var selectionParameter in externalParameters)
        //    {
        //        internalParameters.Add(new fsYAxisParameterWithChecking(selectionParameter));
        //    }

        //    internalItemToParameter = new Dictionary<ListViewItem, fsYAxisParameterWithChecking>();
        //    otherVariablesListView.Items.Clear();

        //    AddParametersToLists(fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter, internalParameters, internalItemToParameter);
        //}

        internal List<fsParameterIdentifier> GetCheckedYAxisParameters()
        {
            //ApplyChecks(m_itemToYParameter);
            //return (from yParameter in m_yAxisParameters where yParameter.IsChecked select yParameter.Identifier).ToList();
            foreach (ListViewItem item in otherVariablesListView.Items)
            {
                feedsControl.nameToParameter[item.Text].IsChecked = item.Checked;
            }
            return (from yParameter in m_yAxisParameters where yParameter.IsChecked select yParameter.Identifier).ToList();
        }

        // ---- for drag-and-drop functionality ------
        public FeedCurvesControl feedsControl;

        public void AssignYAxisWithoutListView(fsYAxisParameterWithChecking[] parameters)
        {
            m_yAxisParameters = new List<fsYAxisParameterWithChecking>();
            foreach (var selectionParameter in parameters)
            {
                m_yAxisParameters.Add(new fsYAxisParameterWithChecking(selectionParameter));
            }
        }

        public void AssignYAxisParametersByOrder(fsYAxisParameterWithChecking[] parameters)
        {
            AssignYAxisParametersByOrder(parameters, ref m_yAxisParameters);
        }

        private void AssignYAxisParametersByOrder(
            fsYAxisParameterWithChecking[] externalParameters,
            ref List<fsYAxisParameterWithChecking> internalParameters)
        {
            internalParameters = new List<fsYAxisParameterWithChecking>();
            foreach (var selectionParameter in externalParameters)
            {
                internalParameters.Add(new fsYAxisParameterWithChecking(selectionParameter));
            }

            otherVariablesListView.Items.Clear();
            for (int i = 0; i < externalParameters.Length; i++)
            {
                var newItem = new ListViewItem(externalParameters[i].Identifier.Name)
                {
                    Checked = externalParameters[i].IsChecked,
                    ForeColor = Color.Black
                };
                otherVariablesListView.Items.Add(newItem);
            }
        }
        // ------------------------------------------

        #endregion

        #region Internal Routines

        //internal void ApplyChecks(Dictionary<ListViewItem, fsYAxisParameterWithChecking> itemToParameter)
        //{
        //    foreach (ListViewItem item in otherVariablesListView.Items)
        //    {
        //        //ListViewItem itemEq = itemToParameter.First(pair => pair.Key.Text == item.Text).Key;
        //        //ListViewItem itemEq = itemToParameter.First(pair => pair.Key.Text.Equals(item.Text)).Key;
        //        itemToParameter[item].IsChecked = item.Checked;
        //        //itemToParameter[itemEq].IsChecked = item.Checked;
        //    }
        //}

        private void AddParametersToLists(
            fsYAxisParameter.fsYParameterKind kindToAdd,
            IEnumerable<fsYAxisParameterWithChecking> yAxisParameters,
            Dictionary<ListViewItem, fsYAxisParameterWithChecking> itemToYParameter)
        {
            foreach (fsYAxisParameterWithChecking selectionParameter in yAxisParameters)
            {
                if (selectionParameter.Kind == kindToAdd)
                {
                    var newItem = new ListViewItem(selectionParameter.Identifier.Name)
                    {
                        Checked = selectionParameter.IsChecked,
                        ForeColor = Color.Black
                    };
                    otherVariablesListView.Items.Add(newItem);
                    itemToYParameter[newItem] = selectionParameter;
                }
            }
        }

        #endregion

        private void Button1Click(object sender, System.EventArgs e)
        {
            //foreach (ListViewItem item in m_itemToYParameter.Keys)
            foreach (ListViewItem item in otherVariablesListView.Items)
            {
                item.Checked = false;
            }
        }
    }
}
