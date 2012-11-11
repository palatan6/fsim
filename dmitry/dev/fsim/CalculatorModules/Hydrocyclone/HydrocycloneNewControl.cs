using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using Parameters;
using ParametersIdentifiers;
using StepCalculators;
using StepCalculators.Simulation_Calculators;
using Value;
using fmDataGrid;
using fsUIControls;
using Units;

namespace CalculatorModules.Hydrocyclone
{
    public partial class fsHydrocycloneNewControl : fsOptionsAndCommentsCalculatorControl
    {
        #region Data for suit managing Adaptation & Machine Geometry group

        private static fsParameterIdentifier[] adaptationParameters = new[]
                                                                           {
                                                                               fsParameterIdentifier.Alpha1,
                                                                               fsParameterIdentifier.Alpha2,
                                                                               fsParameterIdentifier.Alpha3,
                                                                               fsParameterIdentifier.Beta1,
                                                                               fsParameterIdentifier.Beta2,
                                                                               fsParameterIdentifier.Beta3,
                                                                               fsParameterIdentifier.Gamma1,
                                                                               fsParameterIdentifier.Gamma2,
                                                                               fsParameterIdentifier.Gamma3
                                                                           };   

        private static fsParameterIdentifier[] machineGeometryParameters = new[]
                                                                                {
                                                                                    fsParameterIdentifier.bigLOverD,
                                                                                    fsParameterIdentifier.smallLOverD,
                                                                                    fsParameterIdentifier.DiOverD,
                                                                                    fsParameterIdentifier.DoOverD
                                                                                };
 
        #endregion

        #region String Data

        private static string[][] tooltipsAddsUOFlows = new string[][]
                                                                      {
                                                                          new string[]{ "Q = ", "Qu = ", "Qo = " },
                                                                          new string[]{ "Qm = ", "Qmu = ", "Qmo = " },
                                                                          new string[]{ "Qms = ", "Qmsu = ", "Qmso = " },
                                                                          new string[]{ "x50 = ", "x50u = ", "x50o = " },
                                                                          new string[]{ "cm = ", "cmu = ", "cmo = " },
                                                                          new string[]{ "cv = ", "cvu = ", "cvo = " },
                                                                          new string[]{ "C = ", "Cu = ", "Co = " }
                                                                      };

        private static string[] namesUOFlows = new string[] { "Q", "Qm", "Qms", "x50", "cm", "cv", "C" };

        private static string[] tooltipsUOFlows0 = new string[] 
                                                              { 
                                                                  "Volume Flow Rate triple (Q, Qu, Qo)",
                                                                  "Mass Flow Rate triple (Qm, Qmu, Qmo)",
                                                                  "Solids Mass Flow Rate triple (Qms, Qmsu, Qmso)",
                                                                  "Median Particle Size triple (x50, x50u, x50o)",
                                                                  "Suspension Solids Mass Fraction triple (cm, cmu, cmo)",
                                                                  "Suspension Solids Volume Fraction triple (cv, cvu, cvo)",
                                                                  "Suspension Solids Concentration triple (C, Cu, Co)"
                                                              };

        private static string[] tooltipsAdaptation = new string[]
                                                                {
                                                                    "Alpha Adaptation Parameters (" + "\u03B1" + "1, " + "\u03B1" + "2, " + "\u03B1" + "3)",
                                                                    "Beta Adaptation Parameters (" + "\u03B2" + "1, " + "\u03B2" + "2, " + "\u03B2" + "3)",
                                                                    "Gamma Adaptation Parameters (" + "\u03B3" + "1, " + "\u03B3" + "2, " + "\u03B3" + "3)"
                                                                };

        private static string[][] tooltipsAddsAdaptation = new string[][]
                                                                        { new string[]{"\u03B1" + "1 = ", "\u03B1" + "2 = ", "\u03B1" + "3 = "},
                                                                          new string[]{"\u03B2" + "1 = ", "\u03B2" + "2 = ", "\u03B2" + "3 = "},
                                                                          new string[]{"\u03B3" + "1 = ", "\u03B3" + "2 = ", "\u03B3" + "3 = "}
                                                                        };

        private static string[] tooltipsMachGeom = new string[]
                                                              {
                                                                  "Cyclon Length to Diameter (L/D)",
                                                                  "Cylindrical Part Length to Diameter (l/D)",
                                                                  "Inlet Diameter to Diameter (Di/D)",
                                                                  "Outlet Diameter to Diameter (Do/D)"
                                                              };

        #endregion
        
        public event DataGridViewCellEventHandler CellValueChangedByUser;

        private void ProcessThisValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            if (CellValueChangedByUser != null)
            {
                CellValueChangedByUser(sender, e);
            }
        }

        private void FillUnderOverFlowsTable()
        {
            fsSimulationModuleParameter Q = Values[fsParameterIdentifier.FeedVolumeFlowRate];
            fsSimulationModuleParameter Qo = Values[fsParameterIdentifier.OverflowVolumeFlowRate];
            fsSimulationModuleParameter Qu = Values[fsParameterIdentifier.UnderflowVolumeFlowRate];
            fsSimulationModuleParameter Qm = Values[fsParameterIdentifier.FeedSolidsMassFlowRate];
            fsSimulationModuleParameter Qmo = Values[fsParameterIdentifier.OverflowMassFlowRate];
            fsSimulationModuleParameter Qmu = Values[fsParameterIdentifier.UnderflowMassFlowRate];
            fsSimulationModuleParameter Qms = Values[fsParameterIdentifier.Qms];
            fsSimulationModuleParameter Qmso = Values[fsParameterIdentifier.OverflowSolidsMassFlowRate];
            fsSimulationModuleParameter Qmsu = Values[fsParameterIdentifier.UnderflowSolidsMassFlowRate];
            fsSimulationModuleParameter x50 = Values[fsParameterIdentifier.xg];
            fsSimulationModuleParameter xo50 = Values[fsParameterIdentifier.OverflowMeanParticleSize];
            fsSimulationModuleParameter xu50 = Values[fsParameterIdentifier.UnderflowMeanParticleSize];
            fsSimulationModuleParameter cm = Values[fsParameterIdentifier.SuspensionSolidsMassFraction];
            fsSimulationModuleParameter cmo = Values[fsParameterIdentifier.OverflowSolidsMassFraction];
            fsSimulationModuleParameter cmu = Values[fsParameterIdentifier.UnderflowSolidsMassFraction];
            fsSimulationModuleParameter cv = Values[fsParameterIdentifier.SuspensionSolidsVolumeFraction];
            fsSimulationModuleParameter cvo = Values[fsParameterIdentifier.OverflowSolidsVolumeFraction];
            fsSimulationModuleParameter cvu = Values[fsParameterIdentifier.UnderflowSolidsVolumeFraction];
            fsSimulationModuleParameter C = Values[fsParameterIdentifier.SuspensionSolidsConcentration];
            fsSimulationModuleParameter Co = Values[fsParameterIdentifier.OverflowSolidsConcentration];
            fsSimulationModuleParameter Cu = Values[fsParameterIdentifier.UnderflowSolidsConcentration];

            fsSimulationModuleParameter[][] paramGroups = new fsSimulationModuleParameter[][]
                                                              {
                                                                  new fsSimulationModuleParameter[]{ Q, Qu, Qo },
                                                                  new fsSimulationModuleParameter[]{ Qm, Qmu, Qmo },
                                                                  new fsSimulationModuleParameter[]{ Qms, Qmsu, Qmso },
                                                                  new fsSimulationModuleParameter[]{ x50, xu50, xo50 },
                                                                  new fsSimulationModuleParameter[]{ cm, cmu, cmo },
                                                                  new fsSimulationModuleParameter[]{ cv, cvu, cvo },
                                                                  new fsSimulationModuleParameter[]{ C, Cu, Co }
                                                              };          
            for (int i = 0; i < paramGroups.Length; i++)
            {
                dataGridViewUOFlows.Rows[i].Cells[1].Value = paramGroups[i][0].Unit.Name;
                dataGridViewUOFlows.Rows[i].Cells[2].Value = paramGroups[i][0].GetValueInUnits();
                dataGridViewUOFlows.Rows[i].Cells[3].Value = paramGroups[i][1].GetValueInUnits();
                dataGridViewUOFlows.Rows[i].Cells[4].Value = paramGroups[i][2].GetValueInUnits();
                for (int j = 0; j < 3; j++)
                {
                    dataGridViewUOFlows.Rows[i].Cells[j + 2].ToolTipText = tooltipsAddsUOFlows[i][j] + 
                                                     dataGridViewUOFlows.Rows[i].Cells[j + 2].Value.ToString();
                }
            }
        }

        public override void RecalculateAndRedraw()
        {
            base.RecalculateAndRedraw();
            if (IsCalculatorControlInitialized)
                FillUnderOverFlowsTable();            
        }

        protected internal override void StopGridsEdit()
        {
            dataGridViewAdapt.EndEdit();
            dataGridViewMachGeom.EndEdit();
            dataGridViewUOFlows.EndEdit();
            fsParametersWithValuesTable1.EndEdit();
            fsParametersWithValuesTable2.EndEdit();
            fsParametersWithValuesTableAdaptation.EndEdit();
            fsParametersWithValuesTableMachine.EndEdit();
            fsParametersWithValuesTableMachineGeometry.EndEdit();
            fsParametersWithValuesTableMaterial.EndEdit();
            fsParametersWithValuesTableOverUnderFlows.EndEdit();
        }
        
        protected override void InitializeCalculatorControl()
        {
            if (!IsCalculatorControlInitialized)
            {
                base.InitializeCalculatorControl();

                const int count = 2; // count = {column count in fsParametersWithValuesTable} - 1
 
                //--------------- Loading Adaptition Parameters -----------------------------\\
                // Adding rows
                dataGridViewAdapt.Rows.Add(new[] { "\u03B1", "", "", "" }); // unicode alpha U+03B1
                dataGridViewAdapt.Rows.Add(new[] { "\u03B2", "", "", "" }); // unicode beta U+03B2
                dataGridViewAdapt.Rows.Add(new[] { "\u03B3", "", "", "" }); // unicode gamma U+03B3
                // Defining colors
                for (int i = 0; i < 3; i++)
                {
                    foreach (DataGridViewCell cell in dataGridViewAdapt.Rows[i].Cells)
                    {
                        cell.Style.BackColor = Color.FromArgb(255, 255, 230);
                        if (cell.ColumnIndex > 0)
                            cell.Style.ForeColor = Color.Blue;
                    }
                }
                // Defining tooltips
                for (int i = 0; i < 3; i++)
                {
                    dataGridViewAdapt.Rows[i].Cells[0].ToolTipText = tooltipsAdaptation[i];
                }                
                // Mapping cells between invisible fsParametersWithValuesTableAdaptation and it's visible represenrative dataGridViewAdapt
                for (int i = 0; i < 9; i++)
                {
                    DataGridViewCell cell = dataGridViewAdapt.Rows[i / 3].Cells[i % 3 + 1];
                    cell.Value = fsParametersWithValuesTableAdaptation.Rows[i].Cells[count].Value;
                    cell.ToolTipText = tooltipsAddsAdaptation[i / 3][i % 3] + cell.Value.ToString();
                }
                //---------------------------------------------------------------------------\\

                //-------------- Loading Machine Geometry Parameters ------------------------\\
                // Adding row
                dataGridViewMachGeom.Rows.Add(new[] { "", "", "", "" });
                // Defining colors
                foreach (DataGridViewCell cell in dataGridViewMachGeom.Rows[0].Cells)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 230, 255);
                    cell.Style.ForeColor = Color.Blue;
                }
                // Defining tooltips
                for (int i = 0; i < 4; i++)
                {
                    dataGridViewMachGeom.Columns[i].HeaderCell.ToolTipText = tooltipsMachGeom[i];
                } 
                // Mapping cells between invisible fsParametersWithValuesTableMachineGeometry and it's visible represenrative dataGridViewMachGeom
                for (int i = 0; i < 4; i++)
                {
                    DataGridViewCell cell = dataGridViewMachGeom.Rows[0].Cells[i];
                    cell.Value = fsParametersWithValuesTableMachineGeometry.Rows[i].Cells[count].Value;
                }
                //---------------------------------------------------------------------------\\ 

                //----------------- Loading underflow/overflow Parameters -------------------\\
                // Adding rows
                for (int i = 0; i < namesUOFlows.Length; i++)
                {
                    dataGridViewUOFlows.Rows.Add(namesUOFlows[i], "", "", "", "");
                    dataGridViewUOFlows.Rows[i].Cells[0].ToolTipText = tooltipsUOFlows0[i];
                }
                // Defining colors
                for (int i = 0; i < namesUOFlows.Length; i++)
                {
                    foreach (DataGridViewCell cell in dataGridViewUOFlows.Rows[i].Cells)
                    {
                        cell.Style.BackColor = Color.FromArgb(255, 255, 230);
                        cell.Style.ForeColor = Color.Black;
                    } 
                }
                // Populating cells
                FillUnderOverFlowsTable();
                //---------------------------------------------------------------------------\\
            }
        }
        
        public fsHydrocycloneNewControl()
        {
            InitializeComponent();           
            this.dataGridViewAdapt.CellValueChangedByUser += ProcessThisValueChangedByUser;
        }

        #region Calculation Option

        public enum fsCalculationOption
        {
            [Description("\u0394" + "p")] // Delta p
            Dp,
            [Description("Q")]
            Q,
            [Description("n")]
            n
        }

        #endregion

        protected override void InitializeCalculators()
        {
            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsHydrocycloneCalculator());
        }

        protected override void InitializeGroups()
        {
            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            fsParametersGroup etaGroup = AddGroup(fsParameterIdentifier.MotherLiquidViscosity); //eta

            fsParametersGroup rhoGroup = AddGroup(fsParameterIdentifier.MotherLiquidDensity); //rho

            fsParametersGroup densitiesGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity, //rho_s
                fsParameterIdentifier.SuspensionDensity); //rho_sus

            fsParametersGroup cGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction, //cm
                fsParameterIdentifier.SuspensionSolidsVolumeFraction, //cv
                fsParameterIdentifier.SuspensionSolidsConcentration); //C

            fsParametersGroup xgGroup = AddGroup(fsParameterIdentifier.xg); //xg

            fsParametersGroup sigma_gGroup = AddGroup(fsParameterIdentifier.sigma_g); //sigma_g

            fsParametersGroup sigma_sGroup = AddGroup(fsParameterIdentifier.sigma_s); //sigma_s

            fsParametersGroup underFlowGroup = AddGroup(
                fsParameterIdentifier.rf, //rf
                fsParameterIdentifier.DuOverD, // Du/D
                fsParameterIdentifier.UnderflowSolidsMassFraction); //cmu

            fsParametersGroup iGroup = AddGroup(fsParameterIdentifier.PercentageOfParticles); // i

            fsParametersGroup cxdGroup = AddGroup(
                fsParameterIdentifier.OverflowSolidsMassFraction, //cmo
                fsParameterIdentifier.OverflowParticleSize, //xio
                fsParameterIdentifier.UnderflowParticleSize, //xiu
                fsParameterIdentifier.ReducedCutSize, //x’50
                fsParameterIdentifier.MachineDiameter); //D

            fsParametersGroup numCyclonesGroup = AddGroup(fsParameterIdentifier.NumberOfCyclones); //n

            fsParametersGroup pressureGroup = AddGroup(fsParameterIdentifier.PressureDifference); //Dp

            fsParametersGroup qGroup = AddGroup(
                fsParameterIdentifier.FeedVolumeFlowRate, //Q
                fsParameterIdentifier.FeedSolidsMassFlowRate, //Qm
                fsParameterIdentifier.Qms); //Qms

            fsParametersGroup alpha1Group = AddGroup(fsParameterIdentifier.Alpha1);
            fsParametersGroup alpha2Group = AddGroup(fsParameterIdentifier.Alpha2);
            fsParametersGroup alpha3Group = AddGroup(fsParameterIdentifier.Alpha3);

            fsParametersGroup beta1Group = AddGroup(fsParameterIdentifier.Beta1);
            fsParametersGroup beta2Group = AddGroup(fsParameterIdentifier.Beta2);
            fsParametersGroup beta3Group = AddGroup(fsParameterIdentifier.Beta3);

            fsParametersGroup gamma1Group = AddGroup(fsParameterIdentifier.Gamma1);
            fsParametersGroup gamma2Group = AddGroup(fsParameterIdentifier.Gamma2);
            fsParametersGroup gamma3Group = AddGroup(fsParameterIdentifier.Gamma3);

            fsParametersGroup bigLOverDGroup = AddGroup(fsParameterIdentifier.bigLOverD); // L/D

            fsParametersGroup smallLOverDGroup = AddGroup(fsParameterIdentifier.smallLOverD); // l/D

            fsParametersGroup diOverDGroup = AddGroup(fsParameterIdentifier.DiOverD); // Di/D

            fsParametersGroup doOverDGroup = AddGroup(fsParameterIdentifier.DoOverD); // Do/D           

            fsParametersGroup onlyCalculatedMaterialParametersGroup = AddGroup(
                fsParameterIdentifier.StokesNumber,  // Stk
                fsParameterIdentifier.EulerNumber,  // Eu
                fsParameterIdentifier.ReynoldsNumber, // Re
                fsParameterIdentifier.AverageVelocity, // v
                fsParameterIdentifier.TotalEfficiency, // ET
                fsParameterIdentifier.ReducedTotalEfficiency); //E'T

            fsParametersGroup onlyCalculatedMachineParametersGroup = AddGroup(
                fsHydrocycloneCalculator.NumberOfCyclClone, // n (precisely, clone of fsParameterIdentifier.NumberOfCyclones)
                fsParameterIdentifier.CycloneLength,  // L
                fsParameterIdentifier.LengthOfCylindricalPart,  // l
                fsParameterIdentifier.InletDiameter,  // Di
                fsParameterIdentifier.OutletDiameter, // Do
                fsParameterIdentifier.UnderflowDiameter); // Du

            fsParametersGroup onlyCalculatedUnderOverFlowsParametersGroup = AddGroup(
                fsParameterIdentifier.OverflowVolumeFlowRate, // Qo
                fsParameterIdentifier.UnderflowVolumeFlowRate, // Qu
                fsParameterIdentifier.OverflowMassFlowRate, // Qmo
                fsParameterIdentifier.UnderflowMassFlowRate, // Qmu
                fsParameterIdentifier.OverflowSolidsMassFlowRate, // Qmso
                fsParameterIdentifier.UnderflowSolidsMassFlowRate, // Qmsu
                fsParameterIdentifier.OverflowMeanParticleSize, // xo50
                fsParameterIdentifier.UnderflowMeanParticleSize, // xu50
                fsParameterIdentifier.OverflowSolidsVolumeFraction, // cvo 
                fsParameterIdentifier.UnderflowSolidsVolumeFraction, // cvu
                fsParameterIdentifier.OverflowSolidsConcentration, // Co
                fsParameterIdentifier.UnderflowSolidsConcentration); // Cu

            var groupsMaterial = new[]
                                     {etaGroup,
                                      rhoGroup,
                                      densitiesGroup,
                                      cGroup,
                                      xgGroup,
                                      sigma_gGroup,
                                      sigma_sGroup
                                     };

            var groupsMachine = new[]
                                    {underFlowGroup,
                                     iGroup,
                                     cxdGroup,
                                     numCyclonesGroup,
                                     pressureGroup,
                                     qGroup
                                    };

            var groupsAdaptation = new[] 
                                       { alpha1Group,
                                         alpha2Group,
                                         alpha3Group,
                                         beta1Group,
                                         beta2Group,
                                         beta3Group,
                                         gamma1Group,
                                         gamma2Group,
                                         gamma3Group
                                       };

            var groupsMachineGeometry = new[]
                                            { bigLOverDGroup,
                                              smallLOverDGroup,
                                              diOverDGroup,
                                              doOverDGroup
                                            };

            // Loading Adaptation only input Parameters
            for (int i = 0; i < groupsAdaptation.Length; ++i)
            {
                AddGroupToUI(fsParametersWithValuesTableAdaptation, groupsAdaptation[i], colors[i % colors.Length]);
                SetGroupInput(groupsAdaptation[i], true);
            }

            // Loading Machine Geometry only input Parameters
            for (int i = 0; i < groupsMachineGeometry.Length; ++i)
            {
                AddGroupToUI(fsParametersWithValuesTableMachineGeometry, groupsMachineGeometry[i], colors[i % colors.Length]);
                SetGroupInput(groupsMachineGeometry[i], true);
            }

            // Loading Material input/calculated Parameters
            for (int i = 0; i < groupsMaterial.Length; ++i)
            {
                AddGroupToUI(fsParametersWithValuesTableMaterial, groupsMaterial[i], colors[i % colors.Length]);
                SetGroupInput(groupsMaterial[i], true);
            }

            // Loading Machine input/calculated Parameters
            for (int i = 0; i < groupsMachine.Length; ++i)
            {
                AddGroupToUI(fsParametersWithValuesTableMachine, groupsMachine[i], colors[i % colors.Length]);
                SetGroupInput(groupsMachine[i], true);
            }            

            // Loading Material  only calculated Parameters 
            AddGroupToUI(fsParametersWithValuesTable1, onlyCalculatedMaterialParametersGroup, colors[0]);
            SetGroupInput(onlyCalculatedMaterialParametersGroup, false);

            // Loading Machine  only calculated Parameters
            AddGroupToUI(fsParametersWithValuesTable2, onlyCalculatedMachineParametersGroup, colors[0]);
            SetGroupInput(onlyCalculatedMachineParametersGroup, false);

            // Loading underflow/overflow  only calculated Parameters
            AddGroupToUI(fsParametersWithValuesTableOverUnderFlows, onlyCalculatedUnderOverFlowsParametersGroup, colors[0]);
            SetGroupInput(onlyCalculatedUnderOverFlowsParametersGroup, false);
        }

        protected void AdaptationCellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
            if (cell == null)
                return;
            fsParameterIdentifier parameter = adaptationParameters[3 * cell.RowIndex + cell.ColumnIndex - 1];
            UpdateInputInGroup(parameter);
            ReadEnteredValue(cell, parameter);
            RecalculateAndRedraw();
        }

        protected void MachineGeometryCellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
            if (cell == null)
                return;
            fsParameterIdentifier parameter = machineGeometryParameters[cell.ColumnIndex];
            UpdateInputInGroup(parameter);
            ReadEnteredValue(cell, parameter);
            RecalculateAndRedraw();
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(comboBoxCalculationOption.Items, typeof (fsCalculationOption));
            EstablishCalculationOption(fsCalculationOption.Dp);
            AssignCalculationOptionAndControl(typeof (fsCalculationOption), comboBoxCalculationOption);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { comboBoxCalculationOption, 
                                   fsParametersWithValuesTableMachine,
                                   fsParametersWithValuesTableMaterial,
                                   fsParametersWithValuesTableAdaptation,
                                   fsParametersWithValuesTableMachineGeometry
                                 };
        }

        protected override void InitializeParametersValues()
        {
            Values[fsParameterIdentifier.MotherLiquidViscosity].Value = new fsValue(1e-3);
            Values[fsParameterIdentifier.MotherLiquidDensity].Value = new fsValue(1000);
            Values[fsParameterIdentifier.SolidsDensity].Value = new fsValue(2000);
            Values[fsParameterIdentifier.SuspensionSolidsMassFraction].Value = new fsValue(5e-2);

            Values[fsParameterIdentifier.PercentageOfParticles].Value = new fsValue(0.9);

            Values[fsParameterIdentifier.xg].Value = new fsValue(100e-6);
            Values[fsParameterIdentifier.sigma_g].Value = new fsValue(3);
            Values[fsParameterIdentifier.sigma_s].Value = new fsValue(2);
            ParameterToGroup[fsParameterIdentifier.ReducedCutSize].Representator = fsParameterIdentifier.ReducedCutSize;
            Values[fsParameterIdentifier.rf].Value = new fsValue(5e-2);
            Values[fsParameterIdentifier.ReducedCutSize].Value = new fsValue(100e-6);
            Values[fsParameterIdentifier.NumberOfCyclones].Value = new fsValue(3);
            Values[fsParameterIdentifier.FeedVolumeFlowRate].Value = new fsValue(16900/3600.0);


            Values[fsParameterIdentifier.Alpha1].Value = new fsValue(0.0474);
            Values[fsParameterIdentifier.Alpha2].Value = new fsValue(0.742);
            Values[fsParameterIdentifier.Alpha3].Value = new fsValue(8.96);
            Values[fsParameterIdentifier.Beta1].Value = new fsValue(371.5);
            Values[fsParameterIdentifier.Beta2].Value = new fsValue(0.116);
            Values[fsParameterIdentifier.Beta3].Value = new fsValue(2.12);
            Values[fsParameterIdentifier.Gamma1].Value = new fsValue(1218);
            Values[fsParameterIdentifier.Gamma2].Value = new fsValue(4.75);
            Values[fsParameterIdentifier.Gamma3].Value = new fsValue(0.3);

            Values[fsParameterIdentifier.bigLOverD].Value = new fsValue(5);
            Values[fsParameterIdentifier.smallLOverD].Value = new fsValue(3);
            Values[fsParameterIdentifier.DiOverD].Value = new fsValue(0.2);
            Values[fsParameterIdentifier.DoOverD].Value = new fsValue(0.3);
        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            var calculationOption = (fsCalculationOption)CalculationOptions[typeof(fsCalculationOption)];
            fsParametersGroup calculateGroup = null;
            switch (calculationOption)
            {
                case fsCalculationOption.Dp:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.PressureDifference];
                    break;
                case fsCalculationOption.n:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.NumberOfCyclones];
                    break;
                case fsCalculationOption.Q:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.FeedVolumeFlowRate];
                    break;
            }
            var groups = new[]
                {
                    ParameterToGroup[fsParameterIdentifier.PressureDifference],
                    ParameterToGroup[fsParameterIdentifier.NumberOfCyclones],
                    ParameterToGroup[fsParameterIdentifier.FeedVolumeFlowRate]
                };
            foreach (fsParametersGroup group in groups)
            {
                SetGroupInput(group, group != calculateGroup);
            }
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            //override
        }

        protected override void UpdateUIFromData()
        {
            //override

            base.UpdateUIFromData();
        }

        #endregion

    }
}
