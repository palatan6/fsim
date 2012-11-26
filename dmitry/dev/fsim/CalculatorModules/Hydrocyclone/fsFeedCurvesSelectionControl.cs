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

        private readonly Dictionary<fsYAxisParameter.fsYParameterKind, Color> m_kindToColor =
                new Dictionary<fsYAxisParameter.fsYParameterKind, Color>
            {
                {fsYAxisParameter.fsYParameterKind.InputParameter, Color.Blue},
                {fsYAxisParameter.fsYParameterKind.CalculatedConstantParameter, Color.Gray},
                {fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter, Color.Black}
            };

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
            materialVariablesListView.Items.Clear();
            otherVariablesListView.Items.Clear();

            AddParametersToLists(fsYAxisParameter.fsYParameterKind.InputParameter, internalParameters, internalItemToParameter);
            AddParametersToLists(fsYAxisParameter.fsYParameterKind.CalculatedConstantParameter, internalParameters, internalItemToParameter);
            AddParametersToLists(fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter, internalParameters, internalItemToParameter);

            CheckShowConstantsCheckBoxIfNeeded(internalParameters);
        }

        private void CheckShowConstantsCheckBoxIfNeeded(IEnumerable<fsYAxisParameterWithChecking> internalParameters)
        {
            if (internalParameters.Any(parameterWithChecking => parameterWithChecking.IsChecked
                    && parameterWithChecking.Kind != fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter))
            {
                ShowConstantsCheckBox.Checked = true;
            }
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
            var lists = new[]
                            {
                                materialVariablesListView,
                                materialConstantsListView,
                                otherVariablesListView,
                                otherConstantsListView
                            };
            foreach (ListView listView in lists)
            {
                foreach (ListViewItem item in listView.Items)
                {
                    itemToParameter[item].IsChecked = item.Checked;
                }
            }
        }

        private void AddParametersToLists(
            fsYAxisParameter.fsYParameterKind kindToAdd,
            IEnumerable<fsYAxisParameterWithChecking> yAxisParameters,
            Dictionary<ListViewItem, fsYAxisParameterWithChecking> itemToYParameter)
        {
            var materialParameters = new[]
                                             {
                                                 fsParameterIdentifier.MotherLiquidDensity,
                                                 fsParameterIdentifier.MotherLiquidDensity,
                                                 fsParameterIdentifier.LiquidDensity,
                                                 fsParameterIdentifier.SolidsDensity,
                                                 fsParameterIdentifier.SuspensionDensity,
                                                 fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                                                 fsParameterIdentifier.SuspensionSolidsMassFraction,
                                                 fsParameterIdentifier.SuspensionSolidsConcentration,
                                                 fsParameterIdentifier.CakePorosity0,
                                                 fsParameterIdentifier.CakePorosity,
                                                 fsParameterIdentifier.Kappa0,
                                                 fsParameterIdentifier.Kappa,
                                                 fsParameterIdentifier.Ne,
                                                 fsParameterIdentifier.CakePermeability0,
                                                 fsParameterIdentifier.CakePermeability,
                                                 fsParameterIdentifier.CakeResistance0,
                                                 fsParameterIdentifier.CakeResistance,
                                                 fsParameterIdentifier.CakeResistanceAlpha0,
                                                 fsParameterIdentifier.CakeResistanceAlpha,
                                                 fsParameterIdentifier.CakeCompressibility,
                                                 fsParameterIdentifier.FilterMediumResistanceHce0,
                                                 fsParameterIdentifier.DryCakeDensity0,
                                                 fsParameterIdentifier.DryCakeDensity,
                                                 fsParameterIdentifier.CakeWetDensity0,
                                                 fsParameterIdentifier.CakeWetDensity,
                                                 fsParameterIdentifier.CakeWetMassSolidsFractionRs0,
                                                 fsParameterIdentifier.CakeWetMassSolidsFractionRs,
                                                 fsParameterIdentifier.CakeMoistureContentRf0,
                                                 fsParameterIdentifier.CakeMoistureContentRf,

                                             };

            foreach (fsYAxisParameterWithChecking selectionParameter in yAxisParameters)
            {
                if (selectionParameter.Kind == kindToAdd)
                {
                    var newItem = new ListViewItem(selectionParameter.Identifier.Name)
                    {
                        Checked = selectionParameter.IsChecked,
                        ForeColor = m_kindToColor[selectionParameter.Kind]
                    };
                    ListView listView;
                    if (materialParameters.Contains(selectionParameter.Identifier))
                    {
                        listView = selectionParameter.Kind ==
                                   fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter
                                       ? materialVariablesListView
                                       : materialConstantsListView;
                    }
                    else
                    {
                        listView = selectionParameter.Kind ==
                                   fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter
                                       ? otherVariablesListView
                                       : otherConstantsListView;
                    }

                    listView.Items.Add(newItem);
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

        private void CheckBox1CheckedChanged(object sender, System.EventArgs e)
        {
            ShowHideConstantParameters();
        }

        private void ShowHideConstantParameters()
        {
            MachineSettingsSplitContainer.Panel2Collapsed = !ShowConstantsCheckBox.Checked;
            MaterialParametersSplitContainer.Panel2Collapsed = !ShowConstantsCheckBox.Checked;
        }

        private void TablesAndChartsParametersSelectionControlLoad(object sender, System.EventArgs e)
        {
            ShowHideConstantParameters();
        }

    }
}
