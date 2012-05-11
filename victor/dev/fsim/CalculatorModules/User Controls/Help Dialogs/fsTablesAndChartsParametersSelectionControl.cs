using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Parameters;

namespace CalculatorModules.User_Controls.Help_Dialogs
{
    public partial class fsTablesAndChartsParametersSelectionControl : UserControl
    {
        #region Data

        private List<fsYAxisParameterWithChecking> m_parameters;

        private Dictionary<ListViewItem, fsYAxisParameterWithChecking> m_itemToParameter =
            new Dictionary<ListViewItem, fsYAxisParameterWithChecking>();

        private readonly Dictionary<fsYAxisParameter.fsYParameterKind, Color> m_kindToColor =
                new Dictionary<fsYAxisParameter.fsYParameterKind, Color>
            {
                {fsYAxisParameter.fsYParameterKind.InputParameter, Color.Blue},
                {fsYAxisParameter.fsYParameterKind.CalculatedConstantParameter, Color.Gray},
                {fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter, Color.Black}
            };

        #endregion

        public fsTablesAndChartsParametersSelectionControl()
        {
            InitializeComponent();
        }

        #region Public Methods

        public void AssignParameters(List<fsYAxisParameterWithChecking> parameters)
        {
            m_parameters = new List<fsYAxisParameterWithChecking>();
            foreach (var selectionParameter in parameters)
            {
                m_parameters.Add(new fsYAxisParameterWithChecking(selectionParameter));
            }

            m_itemToParameter = new Dictionary<ListViewItem, fsYAxisParameterWithChecking>();
            materialVariablesListView.Items.Clear();
            otherVariablesListView.Items.Clear();

            AddParametersToLists(fsYAxisParameter.fsYParameterKind.InputParameter);
            AddParametersToLists(fsYAxisParameter.fsYParameterKind.CalculatedConstantParameter);
            AddParametersToLists(fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter);
        }

        internal List<fsParameterIdentifier> GetCheckedParameters()
        {
            ApplyChecks(m_itemToParameter);
            return (from yParameter in m_parameters where yParameter.IsChecked select yParameter.Identifier).ToList();
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

        private void AddParametersToLists(fsYAxisParameter.fsYParameterKind kindToAdd)
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

            foreach (fsYAxisParameterWithChecking selectionParameter in m_parameters)
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
                    m_itemToParameter[newItem] = selectionParameter;
                }
            }
        }

        #endregion

    }
}
