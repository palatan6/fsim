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

        private Dictionary<ListViewItem, fsYAxisParameterWithChecking> m_itemToYParameter =
            new Dictionary<ListViewItem, fsYAxisParameterWithChecking>();

        #endregion

        public fsFeedCurvesSelectionControl()
        {
            InitializeComponent();
        }

        #region Public Methods

        public void AssignYAxisParameters(List<fsYAxisParameterWithChecking> parameters)
        {
            AssignYAxisParameters(parameters, ref m_yAxisParameters, ref m_itemToYParameter);
        }

        private void AssignYAxisParameters(
            IEnumerable<fsYAxisParameterWithChecking> externalParameters,
            ref List<fsYAxisParameterWithChecking> internalParameters,
            ref Dictionary<ListViewItem, fsYAxisParameterWithChecking> internalItemToParameter)
        {
            internalParameters = new List<fsYAxisParameterWithChecking>();
            foreach (var selectionParameter in externalParameters)
            {
                internalParameters.Add(new fsYAxisParameterWithChecking(selectionParameter));
            }

            internalItemToParameter = new Dictionary<ListViewItem, fsYAxisParameterWithChecking>();
            otherVariablesListView.Items.Clear();

            AddParametersToLists(fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter, internalParameters, internalItemToParameter);
        }

        internal List<fsParameterIdentifier> GetCheckedYAxisParameters()
        {
            ApplyChecks(m_itemToYParameter);
            return (from yParameter in m_yAxisParameters where yParameter.IsChecked select yParameter.Identifier).ToList();
        }

        #endregion

        #region Internal Routines

        internal void ApplyChecks(Dictionary<ListViewItem, fsYAxisParameterWithChecking> itemToParameter)
        {
            foreach (ListViewItem item in otherVariablesListView.Items)
            {
                itemToParameter[item].IsChecked = item.Checked;
            }
        }

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
            foreach (ListViewItem item in m_itemToYParameter.Keys)
            {
                item.Checked = false;
            }
        }
    }
}
