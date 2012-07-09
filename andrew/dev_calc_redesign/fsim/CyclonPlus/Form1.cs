using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Linq;
using StepCalculators;
using Parameters;
using CalculatorModules;
using System.Collections.Generic;
using Units;
using ParametersIdentifiers.Ranges;
using fsUIControls;
using ParametersIdentifiers;
using CalculatorModules.Machine_Ranges;
using Value;
using CalculatorModules.Hydrocyclone;

namespace CyclonPlus
{
    public partial class MainCyclonPlusForm : Form
    {
        public MainCyclonPlusForm()
        {
            InitializeComponent();

            initializeDbComponent();
            connectToCyclonPlusMdb();
            fmDGVDesign();
            hydrocycloneControl1.comboBoxCalculationOption.SelectionChangeCommitted += new EventHandler(comboBoxCalculationOption_SelectionChangeCommitted);
            hydrocycloneControl1.dataGrid.CellValueChangedByUser += new DataGridViewCellEventHandler(dataGrid_CellValueChangedByUser);
        }

        void dataGrid_CellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).CurrentRow.Cells[0];
            fsParameterIdentifier parameter = FindParamIdentifNameFromGroups(cell.Value.ToString());
            writeToOleDb(parameter, e);
        }

        

        void comboBoxCalculationOption_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (fmDataGridSimul.Rows.Count > 0)
            {
                DataRowView drv = (DataRowView)cpCmSimul.Current;
                DataRow dataRowEdit = drv.Row;
                dataRowEdit.BeginEdit();
                dataRowEdit["options"] = hydrocycloneControl1.comboBoxCalculationOption.Text.ToString();
                dataRowEdit.EndEdit();
            }
        }
        private String connectionString = "provider=Microsoft.Jet.OLEDB.4.0; data source=.\\test.mdb";
        private OleDbConnection cpOleDbConnection;
        private OleDbCommand cpOleDbComProj;
        private OleDbCommand cpOleDbComSusp;
        private OleDbCommand cpOleDbComSeries;
        private OleDbCommand cpOleDbComSimul;
        private OleDbCommand cpOleDbComGroups;
        private OleDbCommand cpOleDbComValues;
        private OleDbCommand cpOleDbCommand;
        private DataSet cpDataSet;
        private OleDbDataAdapter cpDaProj;
        private OleDbDataAdapter cpDaSusp;
        private OleDbDataAdapter cpDaSeries;
        private OleDbDataAdapter cpDaSimul;
        private OleDbDataAdapter cpDaGroups;
        private OleDbDataAdapter cpDaValues;
        private DataTable projectsTable;
        private DataTable suspensionsTable;
        private DataTable seriesTable;
        private DataTable simulationsTable;
        private DataTable groupsTable;
        private DataTable valuesTable;
        private OleDbCommandBuilder builderProj;
        private OleDbCommandBuilder builderSusp;
        private OleDbCommandBuilder builderSeries;
        private OleDbCommandBuilder builderSimul;
        private OleDbCommandBuilder builderGroups;
        private OleDbCommandBuilder builderValues;
        private DataRelation drProjSusp;
        private DataRelation drSuspSeries;
        private DataRelation drSeriesSimul;
        private DataRelation drSimulGroups;
        private DataRelation drGroupsValues;
        private BindingSource bsProj;
        private BindingSource bsSusp;
        private BindingSource bsSeries;
        private BindingSource bsSimul;
        private BindingSource bsGroups;
        private BindingSource bsValues;
        private CurrencyManager cpCmProj;
        private CurrencyManager cpCmSusp;
        private CurrencyManager cpCmSeries;
        private CurrencyManager cpCmSimul;

        public struct queriesStruct
        {
            public String project;
            public String suspension;
            public String series;
            public String simulation;
            public String group;
            public String value;
        }

        public struct tablesStruct
        {
            public String projects;
            public String suspensions;
            public String series;
            public String simulations;
            public String groups;
            public String values;
        }

        public tablesStruct tables;
        public queriesStruct queries;

        private void initializeDbComponent()
        {
            //DataTables
            projectsTable = new DataTable("projects");
            suspensionsTable = new DataTable("suspensions");
            seriesTable = new DataTable("series");
            simulationsTable = new DataTable("simulations");
            groupsTable = new DataTable("groups");
            valuesTable = new DataTable("paramValues");
            //queries
            queries.project = "SELECT [projects].[id_project], [projects].[project_name] FROM [projects];";
            queries.suspension = "SELECT [suspensions].[id_suspension], [suspensions].[id_project], [suspensions].[material], [suspensions].[customer], [suspensions].[suspension_name] FROM [suspensions];";
            //queries.series = "SELECT series.id_series, suspensions.id_suspension, series.series_name, projects.project_name , suspensions.material + ' - ' +suspensions.customer + ' - ' + suspensions.suspension_name  AS suspension, series.lastModifDate " +
            //  "FROM (projects INNER JOIN suspensions ON projects.id_project = suspensions.id_project) INNER JOIN series ON suspensions.id_suspension = series.id_suspension";
            queries.series = "SELECT [series].[id_series], [series].[id_suspension], [series].[LastModifDate], [series].[series_name] FROM [series];";
            queries.simulation = "SELECT [simulations].[id_simulation], [simulations].[id_series], [simulations].[simulation_name], [simulations].[options] FROM [simulations];";
            queries.group = "SELECT [groups].[id_group], [groups].[id_simulation], [groups].[representator] FROM [groups];";
            queries.value = "SELECT [paramValues].[id_value], [paramValues].[id_group], [paramValues].[value_name], [paramValues].[defined], [paramValues].[valueOfParam] FROM [paramValues];";
            //tables String
            tables.projects = "projects";
            tables.suspensions = "suspensions";
            tables.series = "series";
            tables.simulations = "simulations";
            tables.groups = "groups";
            tables.values = "paramValues";

            cpDataSet = new DataSet();
            cpOleDbConnection = new OleDbConnection(connectionString);
            //OleDbCommands
            cpOleDbComProj = new OleDbCommand(queries.project, cpOleDbConnection);
            cpOleDbComSusp = new OleDbCommand(queries.suspension, cpOleDbConnection);
            cpOleDbComSeries = new OleDbCommand(queries.series, cpOleDbConnection);
            cpOleDbComSimul = new OleDbCommand(queries.simulation, cpOleDbConnection);
            cpOleDbComGroups = new OleDbCommand(queries.group, cpOleDbConnection);
            cpOleDbComValues = new OleDbCommand(queries.value, cpOleDbConnection);
            cpOleDbCommand = cpOleDbConnection.CreateCommand();
            //OleDbDataAdapter
            cpDaProj = new OleDbDataAdapter(cpOleDbComProj);
            cpDaSusp = new OleDbDataAdapter(cpOleDbComSusp);
            cpDaSeries = new OleDbDataAdapter(cpOleDbComSeries);
            cpDaSimul = new OleDbDataAdapter(cpOleDbComSimul);
            cpDaGroups = new OleDbDataAdapter(cpOleDbComGroups);
            cpDaValues = new OleDbDataAdapter(cpOleDbComValues);
            builderProj = new OleDbCommandBuilder(cpDaProj);
            builderSusp = new OleDbCommandBuilder(cpDaSusp);
            builderSeries = new OleDbCommandBuilder(cpDaSeries);
            builderSimul = new OleDbCommandBuilder(cpDaSimul);
            builderGroups = new OleDbCommandBuilder(cpDaGroups);
            builderValues = new OleDbCommandBuilder(cpDaValues);
        }

        private void connectToCyclonPlusMdb()
        {
            try
            {
                buildTables();
                bsProj = new BindingSource();
                bsSusp = new BindingSource();
                bsSeries = new BindingSource();
                bsSimul = new BindingSource();
                bsGroups = new BindingSource();
                bsValues = new BindingSource();
                /*
                //Add the InsertCommand to retrieve new identity value.
                cpDaSusp.InsertCommand = new OleDbCommand(
                    "INSERT INTO suspensions (id_project, material, customer, suspension_name) " +
                    "VALUES (@@IDENTITY, @material, @customer, @suspension_name)", cpOleDbConnection);

                // Add the parameter for the inserted value.
                cpDaSusp.InsertCommand.Parameters.Add(
                   new OleDbParameter("@material", OleDbType.VarWChar, 40, "material"));
                cpDaSusp.InsertCommand.Parameters.Add(
                   new OleDbParameter("@customer", OleDbType.VarWChar, 40, "customer"));
                cpDaSusp.InsertCommand.Parameters.Add(
                   new OleDbParameter("@suspension_name", OleDbType.VarWChar, 40, "suspension_name"));
                */
                cpOleDbConnection.Open();

                cpDaProj.TableMappings.Add("Table", tables.projects);
                cpDaProj.Fill(cpDataSet);
                cpDaSusp.TableMappings.Add("Table", tables.suspensions);
                cpDaSusp.Fill(cpDataSet);
                cpDaSeries.TableMappings.Add("Table", tables.series);
                cpDaSeries.Fill(cpDataSet);
                cpDaSimul.TableMappings.Add("Table", tables.simulations);
                cpDaSimul.Fill(cpDataSet);
                cpDaGroups.TableMappings.Add("Table", tables.groups);
                cpDaGroups.Fill(cpDataSet);
                cpDaValues.TableMappings.Add("Table", tables.values);
                cpDaValues.Fill(cpDataSet);
                cpOleDbConnection.Close();
                //DataRelations
                drProjSusp = new DataRelation("ProjSusp",
                    cpDataSet.Tables[tables.projects].Columns["id_project"],
                    cpDataSet.Tables[tables.suspensions].Columns["id_project"]);
                cpDataSet.Relations.Add(drProjSusp);
                drSuspSeries = new DataRelation("SuspSeries",
                    cpDataSet.Tables[tables.suspensions].Columns["id_suspension"],
                    cpDataSet.Tables[tables.series].Columns["id_suspension"]);
                cpDataSet.Relations.Add(drSuspSeries);
                drSeriesSimul = new DataRelation("SeriesSimul",
                    cpDataSet.Tables[tables.series].Columns["id_series"],
                    cpDataSet.Tables[tables.simulations].Columns["id_series"]);
                cpDataSet.Relations.Add(drSeriesSimul);
                drSimulGroups = new DataRelation("SimulGroups",
                    cpDataSet.Tables[tables.simulations].Columns["id_simulation"],
                    cpDataSet.Tables[tables.groups].Columns["id_simulation"]);
                cpDataSet.Relations.Add(drSimulGroups);
                drGroupsValues = new DataRelation("GroupsValues",
                    cpDataSet.Tables[tables.groups].Columns["id_group"],
                    cpDataSet.Tables[tables.values].Columns["id_group"]);
                cpDataSet.Relations.Add(drGroupsValues);
                /*DataViewManager dvm = cpDataSet.DefaultViewManager;
                fmDataGridProject.DataSource = dvm;
                fmDataGridProject.DataMember = tables.projects;
                fmDataGridSusp.DataSource = dvm;
                fmDataGridSusp.DataMember = tables.projects + ".ProjSusp";
                fmDataGridSeries.DataSource = dvm;
                fmDataGridSeries.DataMember = tables.suspensions + ".SuspSeries";
                fmDataGridSimul.DataSource = dvm;
                fmDataGridSimul.DataMember = tables.series + ".SeriesSimul";
                dataGridViewGroups.DataSource = dvm;
                dataGridViewGroups.DataMember = tables.simulations + ".SimulGroups";
                dataGridViewValues.DataSource = dvm;
                dataGridViewValues.DataMember = tables.groups + ".GroupsValues";*/

                bsProj.DataSource = cpDataSet;
                bsProj.DataMember = tables.projects;
                bsSusp.DataSource = bsProj;
                bsSusp.DataMember = "ProjSusp";
                bsSeries.DataSource = bsSusp;
                bsSeries.DataMember = "SuspSeries";
                bsSimul.DataSource = bsSeries;
                bsSimul.DataMember = "SeriesSimul";
                bsGroups.DataSource = bsSimul;
                bsGroups.DataMember = "SimulGroups";
                bsValues.DataSource = bsGroups;
                bsValues.DataMember = "GroupsValues";

                fmDataGridProject.DataSource = bsProj;
                fmDataGridSusp.DataSource = bsSusp;
                fmDataGridSeries.DataSource = bsSeries;
                fmDataGridSimul.DataSource = bsSimul;
                /*comboBoxCalculationOption.DataSource = bsSimul;
                comboBoxCalculationOption.DisplayMember = "options";
                comboBoxCalculationOption.ValueMember = "id_simulation";
                comboBox1.DataBindings.Add("options", simulationsTable, "id_simulation");*/
                cpCmProj = (CurrencyManager)BindingContext[fmDataGridProject.DataSource, fmDataGridProject.DataMember];
                cpCmSusp = (CurrencyManager)BindingContext[fmDataGridSusp.DataSource, fmDataGridSusp.DataMember];
                cpCmSeries = (CurrencyManager)BindingContext[fmDataGridSeries.DataSource, fmDataGridSeries.DataMember];
                cpCmSimul = (CurrencyManager)BindingContext[fmDataGridSimul.DataSource, fmDataGridSimul.DataMember];
                //
                //project
                cpDaProj.UpdateCommand = builderProj.GetUpdateCommand();
                cpDaProj.InsertCommand = new OleDbCommand(
                    "INSERT INTO [projects]([project_name]) Values(?)", cpOleDbConnection);
                cpDaProj.InsertCommand.CommandType = CommandType.Text;
                cpDaProj.InsertCommand.Parameters.Add(
                  "@project_name", OleDbType.VarWChar, 40, "project_name");
                cpDaProj.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                cpDaProj.DeleteCommand = builderProj.GetDeleteCommand();
                //suspension
                cpDaSusp.UpdateCommand = builderSusp.GetUpdateCommand();
                cpDaSusp.InsertCommand = new OleDbCommand(
                        "INSERT INTO [suspensions]([suspension_name], [id_project]) Values(?, ?)", cpOleDbConnection);
                cpDaSusp.InsertCommand.CommandType = CommandType.Text;
                cpDaSusp.InsertCommand.Parameters.Add(
                  "@suspension_name", OleDbType.VarWChar, 40, "suspension_name");
                cpDaSusp.InsertCommand.Parameters.Add(
                  "@id_project", OleDbType.Integer, 4, "id_project");
                cpDaSusp.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                cpDaSusp.DeleteCommand = builderSusp.GetDeleteCommand();
                //series
                cpDaSeries.UpdateCommand = builderSeries.GetUpdateCommand();
                cpDaSeries.InsertCommand = new OleDbCommand(
                        "INSERT INTO [series]([series_name], [id_suspension]) Values(?, ?)", cpOleDbConnection);
                cpDaSeries.InsertCommand.CommandType = CommandType.Text;
                cpDaSeries.InsertCommand.Parameters.Add(
                  "@series_name", OleDbType.VarWChar, 40, "series_name");
                cpDaSeries.InsertCommand.Parameters.Add(
                  "@id_suspension", OleDbType.Integer, 4, "id_suspension");
                cpDaSeries.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                cpDaSeries.DeleteCommand = builderSeries.GetDeleteCommand();
                //simulation
                cpDaSimul.UpdateCommand = builderSimul.GetUpdateCommand();
                cpDaSimul.InsertCommand = new OleDbCommand(
                        "INSERT INTO [simulations]([simulation_name], [options], [id_series]) Values(?, ?, ?)", cpOleDbConnection);
                cpDaSimul.InsertCommand.CommandType = CommandType.Text;
                cpDaSimul.InsertCommand.Parameters.Add(
                  "@simulation_name", OleDbType.VarWChar, 40, "simulation_name");
                cpDaSimul.InsertCommand.Parameters.Add(
                 "@options", OleDbType.VarWChar, 40, "options");
                cpDaSimul.InsertCommand.Parameters.Add(
                  "@id_series", OleDbType.Integer, 4, "id_series");
                cpDaSimul.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                cpDaSimul.DeleteCommand = builderSimul.GetDeleteCommand();
                //group
                cpDaGroups.UpdateCommand = builderGroups.GetUpdateCommand();
                cpDaGroups.InsertCommand = new OleDbCommand(
                        "INSERT INTO [groups]([representator], [id_simulation]) Values(?, ?)", cpOleDbConnection);
                cpDaGroups.InsertCommand.CommandType = CommandType.Text;
                cpDaGroups.InsertCommand.Parameters.Add(
                  "@representator", OleDbType.VarWChar, 40, "representator");
                cpDaGroups.InsertCommand.Parameters.Add(
                  "@id_simulation", OleDbType.Integer, 4, "id_simulation");
                cpDaGroups.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                cpDaGroups.DeleteCommand = builderGroups.GetDeleteCommand();
                cpDaValues.UpdateCommand = builderValues.GetUpdateCommand();
                cpDaValues.InsertCommand = new OleDbCommand(
                        "INSERT INTO [paramValues]([defined], [value_name], [valueOfParam], [id_group]) Values(?, ?, ?, ?)", cpOleDbConnection);
                cpDaValues.InsertCommand.CommandType = CommandType.Text;
                cpDaValues.InsertCommand.Parameters.Add(
                  "@defined", OleDbType.Boolean, 1, "defined");
                cpDaValues.InsertCommand.Parameters.Add(
                  "@value_name", OleDbType.VarWChar, 40, "value_name");
                cpDaValues.InsertCommand.Parameters.Add(
                  "@valueOfParam", OleDbType.Double, 10, "valueOfParam");
                cpDaValues.InsertCommand.Parameters.Add(
                  "@id_group", OleDbType.Integer, 4, "id_group");
                cpDaValues.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                cpDaValues.DeleteCommand = builderValues.GetDeleteCommand();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cpOleDbConnection.Close();
            }
        }


        private void buildTables()
        {
            //projects
            projectsTable.Columns.Add("id_project", typeof(Int32));
            projectsTable.Columns["id_project"].AutoIncrement = true;
            projectsTable.Columns["id_project"].AutoIncrementSeed = 0;
            projectsTable.Columns["id_project"].AutoIncrementStep = -1;
            projectsTable.Columns.Add("project_name", typeof(String));
            projectsTable.PrimaryKey = new DataColumn[] { projectsTable.Columns["id_project"] };
            //suspension
            suspensionsTable.Columns.Add("id_suspension", typeof(Int32));
            suspensionsTable.Columns["id_suspension"].Unique = true;
            suspensionsTable.Columns["id_suspension"].AllowDBNull = false;
            suspensionsTable.Columns["id_suspension"].AutoIncrement = true;
            suspensionsTable.Columns["id_suspension"].AutoIncrementSeed = 0;
            suspensionsTable.Columns["id_suspension"].AutoIncrementStep = -1;
            suspensionsTable.Columns.Add("id_project", typeof(Int32));
            suspensionsTable.Columns.Add("suspension_name", typeof(String));
            suspensionsTable.Columns.Add("material", typeof(String));
            suspensionsTable.Columns.Add("customer", typeof(String));
            suspensionsTable.PrimaryKey = new DataColumn[] { suspensionsTable.Columns["id_suspension"] };
            //series
            seriesTable.Columns.Add("id_series", typeof(Int32));
            seriesTable.Columns["id_series"].AutoIncrement = true;
            seriesTable.Columns["id_series"].AutoIncrementSeed = 0;
            seriesTable.Columns["id_series"].AutoIncrementStep = -1;
            seriesTable.Columns.Add("id_suspension", typeof(Int32));
            seriesTable.Columns.Add("series_name", typeof(String));
            seriesTable.Columns.Add("lastModifDate", typeof(DateTime));
            seriesTable.PrimaryKey = new DataColumn[] { seriesTable.Columns["id_series"] };
            //simulations
            simulationsTable.Columns.Add("id_simulation", typeof(Int32));
            simulationsTable.Columns["id_simulation"].AutoIncrement = true;
            simulationsTable.Columns["id_simulation"].AutoIncrementSeed = 0;
            simulationsTable.Columns["id_simulation"].AutoIncrementStep = -1;
            simulationsTable.Columns.Add("id_series", typeof(Int32));
            simulationsTable.Columns.Add("simulation_name", typeof(String));
            simulationsTable.Columns.Add("options", typeof(String));
            simulationsTable.PrimaryKey = new DataColumn[] { simulationsTable.Columns["id_simulation"] };
            //groups
            groupsTable.Columns.Add("id_group", typeof(Int32));
            groupsTable.Columns["id_group"].AutoIncrement = true;
            groupsTable.Columns["id_group"].AutoIncrementSeed = 0;
            groupsTable.Columns["id_group"].AutoIncrementStep = -1;
            groupsTable.Columns.Add("id_simulation", typeof(Int32));
            groupsTable.Columns.Add("representator", typeof(String));
            groupsTable.PrimaryKey = new DataColumn[] { groupsTable.Columns["id_group"] };
            //values
            valuesTable.Columns.Add("id_value", typeof(Int32));
            valuesTable.Columns["id_value"].AutoIncrement = true;
            valuesTable.Columns["id_value"].AutoIncrementSeed = 0;
            valuesTable.Columns["id_value"].AutoIncrementStep = -1;
            valuesTable.Columns.Add("id_group", typeof(Int32));
            valuesTable.Columns.Add("value_name", typeof(String));
            valuesTable.Columns.Add("defined", typeof(Boolean));
            valuesTable.Columns.Add("valueOfParam", typeof(Double));
            valuesTable.PrimaryKey = new DataColumn[] { valuesTable.Columns["id_value"] };
            //add tables to DataSet
            cpDataSet.Tables.Add(projectsTable);
            cpDataSet.Tables.Add(suspensionsTable);
            cpDataSet.Tables.Add(seriesTable);
            cpDataSet.Tables.Add(simulationsTable);
            cpDataSet.Tables.Add(groupsTable);
            cpDataSet.Tables.Add(valuesTable);
        }

        private void fmDGVDesign()
        {
            //projects
            fmDataGridProject.Columns["id_project"].HeaderText = "ID_PROJ";
            fmDataGridProject.Columns["project_name"].HeaderText = "Project name";
            fmDataGridProject.Columns["project_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //fmDataGridProject.Columns["id_project"].Visible = false;
            //suspensions
            fmDataGridSusp.Columns["id_suspension"].HeaderText = "ID_SUSP";
            fmDataGridSusp.Columns["id_project"].HeaderText = "ID_PROJ";
            fmDataGridSusp.Columns["material"].HeaderText = "Material";
            fmDataGridSusp.Columns["customer"].HeaderText = "Customer";
            fmDataGridSusp.Columns["suspension_name"].HeaderText = "Suspension name";
            fmDataGridSusp.Columns["suspension_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fmDataGridSusp.Columns["suspension_name"].DisplayIndex = 5;
            //fmDataGridSusp.Columns["id_suspension"].Visible = false;
            //fmDataGridSusp.Columns["id_project"].Visible = false;
            //series
            fmDataGridSeries.Columns["id_series"].HeaderText = "ID_SERIES";
            fmDataGridSeries.Columns["id_suspension"].HeaderText = "ID_SUSP";
            fmDataGridSeries.Columns["lastModifDate"].HeaderText = "Last Modified Date";
            fmDataGridSeries.Columns["series_name"].HeaderText = "Series name";
            fmDataGridSeries.Columns["lastModifDate"].DisplayIndex = 5;
            fmDataGridSeries.Columns["id_suspension"].Visible = false;
            fmDataGridSeries.Columns["id_series"].Visible = false;
            fmDataGridSeries.Columns["series_name"].ReadOnly = false;
            fmDataGridSeries.Columns["lastModifDate"].ReadOnly = true;
            /*fmDataGridSeries.Columns["project_name"].HeaderText = "Project";
            fmDataGridSeries.Columns["suspension"].HeaderText = "Suspension";
            fmDataGridSeries.Columns["suspension"].ReadOnly = true;
            fmDataGridSeries.Columns["project_name"].ReadOnly = true;*/
            //simulation
            fmDataGridSimul.Columns["id_simulation"].HeaderText = "ID_SIMUL";
            fmDataGridSimul.Columns["id_series"].HeaderText = "ID_SERIES";
            fmDataGridSimul.Columns["simulation_name"].HeaderText = "Simulation name";
            fmDataGridSimul.Columns["options"].HeaderText = "Options";
            fmDataGridSimul.Columns["options"].Visible = false;
            fmDataGridSimul.Columns["id_series"].Visible = false;
            fmDataGridSimul.Columns["id_simulation"].Visible = false;

        }

        private void saveDeleted(DataTable table, OleDbDataAdapter adapter)
        {
            if (table.GetChanges(DataRowState.Deleted) != null)
            {
                DataTable dataChanges = table.GetChanges(DataRowState.Deleted);
                adapter.Update(dataChanges);
                table.Merge(dataChanges);
                //table.AcceptChanges();
            }
        }

        private void saveModified(DataTable table, OleDbDataAdapter adapter)
        {
            if (table.GetChanges(DataRowState.Modified) != null)
            {
                DataTable dataChanges = table.GetChanges(DataRowState.Modified);
                adapter.Update(dataChanges);
                table.Merge(dataChanges);
                //table.AcceptChanges();
            }
        }

        private void saveAdded(DataTable table, OleDbDataAdapter adapter)
        {
            if (table.GetChanges(DataRowState.Added) != null)
            {
                DataTable dataChanges = table.GetChanges(DataRowState.Added);
                adapter.Update(dataChanges);
                table.Merge(dataChanges);
                //table.AcceptChanges();
            }
        }

        private void MainCyclonPlusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet ds = cpDataSet.GetChanges();
            if (ds == null) return;
            DialogResult dr = MessageBox.Show("Save the changes?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    cpDaValues.Update(cpDataSet.Tables[tables.values].Select(null, null, DataViewRowState.Deleted));
                    cpDaGroups.Update(cpDataSet.Tables[tables.groups].Select(null, null, DataViewRowState.Deleted));
                    cpDaSimul.Update(cpDataSet.Tables[tables.simulations].Select(null, null, DataViewRowState.Deleted));
                    cpDaSeries.Update(cpDataSet.Tables[tables.series].Select(null, null, DataViewRowState.Deleted));
                    cpDaSusp.Update(cpDataSet.Tables[tables.suspensions].Select(null, null, DataViewRowState.Deleted));
                    cpDaProj.Update(cpDataSet.Tables[tables.projects].Select(null, null, DataViewRowState.Deleted));
                    cpDaValues.Update(cpDataSet.Tables[tables.values].Select(null, null, DataViewRowState.ModifiedCurrent));
                    cpDaGroups.Update(cpDataSet.Tables[tables.groups].Select(null, null, DataViewRowState.ModifiedCurrent));
                    cpDaSimul.Update(cpDataSet.Tables[tables.simulations].Select(null, null, DataViewRowState.ModifiedCurrent));
                    cpDaSeries.Update(cpDataSet.Tables[tables.series].Select(null, null, DataViewRowState.ModifiedCurrent));
                    cpDaSusp.Update(cpDataSet.Tables[tables.suspensions].Select(null, null, DataViewRowState.ModifiedCurrent));
                    cpDaProj.Update(cpDataSet.Tables[tables.projects].Select(null, null, DataViewRowState.ModifiedCurrent));
                    cpDaProj.Update(cpDataSet.Tables[tables.projects].Select(null, null, DataViewRowState.Added));
                    cpDaSusp.Update(cpDataSet.Tables[tables.suspensions].Select(null, null, DataViewRowState.Added));
                    cpDaSeries.Update(cpDataSet.Tables[tables.series].Select(null, null, DataViewRowState.Added));
                    cpDaSimul.Update(cpDataSet.Tables[tables.simulations].Select(null, null, DataViewRowState.Added));
                    cpDaGroups.Update(cpDataSet.Tables[tables.groups].Select(null, null, DataViewRowState.Added));
                    cpDaValues.Update(cpDataSet.Tables[tables.values].Select(null, null, DataViewRowState.Added));
                    ds.AcceptChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating:\n" + ex.ToString(), "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //if (dr == DialogResult.Cancel)
            //  return;
            /*try
        {
            // Remove all deleted orders from the Projects table.
            if (cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Deleted) != null)
            {
                cpDaProj.Update(cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Deleted));
            }

            // Update the Child tables
            cpDaSusp.Update(cpDataSet, tables.suspensions);
            cpDaSeries.Update(cpDataSet, tables.series);

            // Add new orders to the Orders table.
            if (cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Added) != null)
            {
                cpDaSusp.Update(cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Added));
            }

            // Update all modified Projects.
            if (cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Modified) != null)
            {
                cpDaSusp.Update(cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Modified));
            }

            cpDataSet.AcceptChanges();
        }

        catch (System.Exception ex)
        {
            MessageBox.Show("Update failed\n " + ex.ToString());
        }

        finally
        {
            if (cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Deleted) != null)
            {
                cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Deleted).Dispose();
            }
            if (cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Added) != null)
            {
                cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Added).Dispose();
            }
            if (cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Modified) != null)
            {
                cpDataSet.Tables[tables.projects].GetChanges(DataRowState.Modified).Dispose();
            }
        }
        */
        }

        private void delFromDataSet(CurrencyManager cm, String table)
        {
            if (cm.Count > 0) cm.RemoveAt(cm.Position);
            else
                MessageBox.Show("There are no " + table + " to delete", "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void insertToDataSet(DataTable table, CurrencyManager cmParent)
        {
            DataTable parentTable = table.ParentRelations[0].ParentTable;
            //DataColumn[] primKey = parentTable.PrimaryKey;
            if (cmParent.Count > 0)
            {
                try
                {
                    DataRowView drv = (DataRowView)cmParent.Current;
                    DataRow row = table.NewRow();
                    //row[primKey[0].ToString()] = drv.Row[primKey[0].ToString()];
                    if (table == suspensionsTable)
                    {
                        row["id_project"] = drv.Row["id_project"];
                        row["suspension_name"] = "suspension_" + row["id_suspension"];
                    }
                    if (table == seriesTable)
                    {
                        row["id_suspension"] = drv.Row["id_suspension"];
                        row["series_name"] = "series_" + row["id_series"];
                    }
                    if (table == simulationsTable)
                    {
                        row["id_series"] = drv.Row["id_series"];
                        row["simulation_name"] = "simulation_" + row["id_simulation"];
                    }
                    table.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("You should add a " + parentTable.TableName, "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void saveToDataSet(DataTable table, OleDbDataAdapter dataAdapter)
        {
            if (table.GetChanges() == null) return;
            if (MessageBox.Show("Save the changes to " + table.TableName + "?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dataAdapter.Update(table);
                    cpDataSet.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating " + table.TableName + ":\n" + ex.ToString(), "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }



        private void createChangeStringSuspProj()
        {
            String material = fmDataGridSusp.CurrentRow.Cells["material"].Value.ToString(),
                        customer = fmDataGridSusp.CurrentRow.Cells["customer"].Value.ToString(),
                        name = fmDataGridSusp.CurrentRow.Cells["suspension_name"].Value.ToString();
            for (int i = 0; i < fmDataGridSeries.Rows.Count; i++)
            {
                fmDataGridSeries.Rows[i].Cells["ColumnProject"].Value = fmDataGridProject.CurrentRow.Cells["project_name"].Value;
                fmDataGridSeries.Rows[i].Cells["ColumnSusp"].Value = material + " - " + customer + " - " + name;
            }
        }

        private void createProj()
        {
            throw new Exception("Method commented out.");
//             if (cpCmProj.Count > 0)
//             {
//                 try
//                 {
//                     DataRow row = projectsTable.NewRow();
//                     row["project_name"] = "project_";
//                     projectsTable.Rows.Add(row);
//                     cpDaProj.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaProj_RowUpdated);
//                     fmDataGridProject.CurrentCell = fmDataGridProject.Rows[fmDataGridProject.Rows.Count - 1].Cells[0];
//                 }
//                 catch (Exception ex)
//                 {
//                     MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                 }
//                 finally
//                 {
//                     cpOleDbConnection.Close();
//                 }
//             }
//             else
//             {
//                 try
//                 {
//                     DataRow rowProj = projectsTable.NewRow();
//                     rowProj["project_name"] = "First project";
//                     projectsTable.Rows.Add(rowProj);
//                     cpDaProj.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaProj_RowUpdated);
//                     DataRowView drvPr = (DataRowView)cpCmProj.Current;
//                     DataRow rowSusp = suspensionsTable.NewRow();
//                     rowSusp["id_project"] = (Int32)drvPr.Row["id_project"];
//                     rowSusp["suspension_name"] = "First suspension";
//                     suspensionsTable.Rows.Add(rowSusp);
//                     cpDaSusp.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaSusp_RowUpdated);
//                     DataRowView drvSusp = (DataRowView)cpCmSusp.Current;
//                     DataRow rowSeries = seriesTable.NewRow();
//                     rowSeries["id_suspension"] = (Int32)drvSusp.Row["id_suspension"];
//                     rowSeries["series_name"] = "First series";
//                     seriesTable.Rows.Add(rowSeries);
//                     cpDaSeries.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaSeries_RowUpdated);
//                     DataRowView drvSer = (DataRowView)cpCmSeries.Current;
//                     DataRow rowSimul = simulationsTable.NewRow();
//                     rowSimul["id_series"] = (Int32)drvSer.Row["id_series"];
//                     rowSimul["simulation_name"] = "First simulation";
//                     simulationsTable.Rows.Add(rowSimul);
//                     cpDaSimul.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaSimul_RowUpdated);
//                     for (int c = 0; c < hydrocycloneControl1.Groups.Count; c++)
//                     {
//                         DataRow rowGroup = groupsTable.NewRow();
//                         rowGroup["id_simulation"] = (Int32)rowSimul["id_simulation"];
//                         rowGroup["representator"] = (String)hydrocycloneControl1.Groups[c].Representator.Name;
//                         groupsTable.Rows.Add(rowGroup);
//                         cpDaGroups.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaGroups_RowUpdated);
//                         for (int par = 0; par < hydrocycloneControl1.Groups[c].Parameters.Count; par++)
//                         {
//                             fsValue val = hydrocycloneControl1.GetValue(hydrocycloneControl1.Groups[c].Parameters[par]);
//                             DataRow rowVal = valuesTable.NewRow();
//                             rowVal["value_name"] = (String)hydrocycloneControl1.Groups[c].Parameters[par].Name;
//                             rowVal["defined"] = (Boolean)true;
//                             rowVal["valueOfParam"] = 0;
//                             rowVal["id_group"] = (Int32)rowGroup["id_group"];
//                             valuesTable.Rows.Add(rowVal);
//                             cpDaValues.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaValues_RowUpdated);
//                         }
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                 }
//                 finally
//                 {
//                     cpOleDbConnection.Close();
//                 }
//             }
        }

        private void createSuspension()
        {
            if (cpCmProj.Count > 0)
            {
                try
                {

                    DataRowView drv = (DataRowView)cpCmProj.Current;
                    DataRow row = suspensionsTable.NewRow();
                    row["id_project"] = (Int32)drv.Row["id_project"];
                    row["suspension_name"] = "suspension_";
                    suspensionsTable.Rows.Add(row);
                    cpDaSusp.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaSusp_RowUpdated);
                    fmDataGridSusp.CurrentCell = fmDataGridSusp.Rows[fmDataGridSusp.Rows.Count - 1].Cells[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("You should add a " + tables.projects, "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void createSeries()
        {
            if (cpCmSusp.Count > 0)
            {
                try
                {

                    DataRowView drv = (DataRowView)cpCmSusp.Current;
                    DataRow row = seriesTable.NewRow();
                    row["id_suspension"] = drv.Row["id_suspension"];
                    row["series_name"] = "series_";
                    seriesTable.Rows.Add(row);
                    cpDaSeries.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaSeries_RowUpdated);
                    fmDataGridSeries.CurrentCell = fmDataGridSeries.Rows[fmDataGridSeries.Rows.Count - 1].Cells[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("You should add a " + tables.suspensions, "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void createSimulation()
        {
            throw new Exception("Method commented out.");
//             if (cpCmSeries.Count > 0)
//             {
//                 cpOleDbConnection.Open();
//                 cpOleDbCommand.Transaction = cpOleDbConnection.BeginTransaction();
//                 try
//                 {
//                     DataRowView drv = (DataRowView)cpCmSeries.Current;
//                     DataRow row = simulationsTable.NewRow();
//                     row["id_series"] = drv.Row["id_series"];
//                     row["simulation_name"] = "simulation_";
//                     row["options"] = "Dp";
//                     simulationsTable.Rows.Add(row);
//                     cpDaSimul.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaSimul_RowUpdated);
//                     for (int c = 0; c < hydrocycloneControl1.Groups.Count; c++)
//                     {
//                         DataRow rowGroup = groupsTable.NewRow();
//                         rowGroup["id_simulation"] = (Int32)row["id_simulation"];
//                         rowGroup["representator"] = (String)hydrocycloneControl1.Groups[c].Representator.Name;
//                         groupsTable.Rows.Add(rowGroup);
//                         cpDaGroups.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaGroups_RowUpdated);
//                         for (int par = 0; par < hydrocycloneControl1.Groups[c].Parameters.Count; par++)
//                         {
//                             fsValue val = hydrocycloneControl1.GetValue(hydrocycloneControl1.Groups[c].Parameters[par]);
//                             DataRow rowVal = valuesTable.NewRow();
//                             rowVal["value_name"] = (String)hydrocycloneControl1.Groups[c].Parameters[par].Name;
//                             rowVal["defined"] = (Boolean)true;
//                             rowVal["valueOfParam"] = (Double)val.Value;
//                             rowVal["id_group"] = (Int32)rowGroup["id_group"];
//                             valuesTable.Rows.Add(rowVal);
//                             cpDaValues.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaValues_RowUpdated);
//                         }
//                     }
//                     fmDataGridSimul.CurrentCell = fmDataGridSimul.Rows[fmDataGridSimul.Rows.Count - 1].Cells[2];
//                     cpOleDbCommand.Transaction.Commit();
//                     cpOleDbConnection.Close();
//                     hydrocycloneControl1.dataGrid.Enabled = true;
//                 }
//                 catch (Exception ex)
//                 {
//                     cpOleDbCommand.Transaction.Rollback();
//                     MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                 }
//                 finally
//                 {
//                     cpOleDbConnection.Close();
//                 }
//             }
//             else
//                 MessageBox.Show("You should add a " + tables.series, "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void cloneSimulation()
        {
            throw new Exception("Method commented out.");
//             if (cpCmSimul.Count > 0)
//             {
//                 cpOleDbConnection.Open();
//                 cpOleDbCommand.Transaction = cpOleDbConnection.BeginTransaction();
//                 try
//                 {
//                     DataRowView drv = (DataRowView)cpCmSeries.Current;
//                     DataRow row = simulationsTable.NewRow();
//                     row["id_series"] = drv.Row["id_series"];
//                     row["simulation_name"] = "simulation_";
//                     row["options"] = "Dp";
//                     simulationsTable.Rows.Add(row);
//                     cpDaSimul.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaSimul_RowUpdated);
//                     for (int c = 0; c < hydrocycloneControl1.Groups.Count; c++)
//                     {
//                         DataRow rowGroup = groupsTable.NewRow();
//                         rowGroup["id_simulation"] = (Int32)row["id_simulation"];
//                         rowGroup["representator"] = (String)hydrocycloneControl1.Groups[c].Representator.Name;
//                         groupsTable.Rows.Add(rowGroup);
//                         cpDaGroups.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaGroups_RowUpdated);
//                         for (int par = 0; par < hydrocycloneControl1.Groups[c].Parameters.Count; par++)
//                         {
//                             fsValue val = hydrocycloneControl1.GetValue(hydrocycloneControl1.Groups[c].Parameters[par]);
//                             DataRow rowVal = valuesTable.NewRow();
//                             rowVal["value_name"] = (String)hydrocycloneControl1.Groups[c].Parameters[par].Name;
//                             rowVal["defined"] = (Boolean)val.Defined;
//                             rowVal["valueOfParam"] = (Double)val.Value;
//                             rowVal["id_group"] = (Int32)rowGroup["id_group"];
//                             valuesTable.Rows.Add(rowVal);
//                             cpDaValues.RowUpdated += new OleDbRowUpdatedEventHandler(cpDaValues_RowUpdated);
//                         }
//                     }
//                     fmDataGridSimul.CurrentCell = fmDataGridSimul.Rows[fmDataGridSimul.Rows.Count - 1].Cells[2];
//                     cpOleDbCommand.Transaction.Commit();
//                     cpOleDbConnection.Close();
//                 }
//                 catch (Exception ex)
//                 {
//                     cpOleDbCommand.Transaction.Rollback();
//                     MessageBox.Show("error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                 }
//                 finally
//                 {
//                     cpOleDbConnection.Close();
//                 }
//             }
//             else
//                 MessageBox.Show("You should add a " + simulationsTable.TableName, "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void cpDaValues_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand newValueID = new OleDbCommand("SELECT @@IDENTITY FROM paramValues", cpOleDbConnection);
                e.Row["id_value"] = (int)newValueID.ExecuteScalar();
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        void cpDaGroups_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand newGroupID = new OleDbCommand("SELECT @@IDENTITY FROM groups", cpOleDbConnection);
                e.Row["id_group"] = (int)newGroupID.ExecuteScalar();
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        private void buttonCreateProject_Click(object sender, EventArgs e)
        {
            createProj();
        }

        private void buttonDelProject_Click(object sender, EventArgs e)
        {
            delFromDataSet(cpCmProj, tables.projects);
        }

        private void buttonCreateSusp_Click(object sender, EventArgs e)
        {
            createSuspension();
        }

        void cpDaSusp_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand newSuspID = new OleDbCommand("SELECT @@IDENTITY FROM suspensions", cpOleDbConnection);
                e.Row["id_suspension"] = (int)newSuspID.ExecuteScalar();
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }



        private void buttonDelSusp_Click(object sender, EventArgs e)
        {
            delFromDataSet(cpCmSusp, tables.suspensions);
        }

        private void buttonDelSeries_Click(object sender, EventArgs e)
        {
            delFromDataSet(cpCmSeries, tables.series);
        }

        private void buttonCreateSeries_Click(object sender, EventArgs e)
        {
            createSeries();
        }

        void cpDaSeries_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand newSeriesID = new OleDbCommand("SELECT @@IDENTITY FROM series", cpOleDbConnection);
                e.Row["id_series"] = (int)newSeriesID.ExecuteScalar();
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        private void buttonCreateSimul_Click(object sender, EventArgs e)
        {
            createSimulation();
        }

        void cpDaSimul_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand newSimulID = new OleDbCommand("SELECT @@IDENTITY FROM simulations", cpOleDbConnection);
                e.Row["id_simulation"] = (int)newSimulID.ExecuteScalar();
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        private void fmDataGridSeries_CellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            if (fmDataGridSeries.Rows.Count != 0)
                fmDataGridSeries.CurrentRow.Cells["lastModifDate"].Value = DateTime.Now;
        }

        private void fmDataGridProject_CellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            /* if (fmDataGridSeries.Rows.Count != 0)
             {
                 //String projectName = fmDataGridProject.CurrentRow.Cells["project_name"].Value.ToString();
                 //fmDataGridSeries.CurrentRow.Cells["ColumnProject"].Value = projectName;
                 fmDataGridSeries.Columns["ColumnProject"].DataPropertyName = fmDataGridProject.Columns["project_name"].DataPropertyName;
             }
             */
        }

        private void fmDataGridSusp_CellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            if (fmDataGridSeries.Rows.Count != 0)
            {
                createChangeStringSuspProj();
            }
        }

        private void buttonDelSimul_Click(object sender, EventArgs e)
        {
            delFromDataSet(cpCmSimul, tables.simulations);
            No_simulations();
        }

        private void buttonUndoProject_Click(object sender, EventArgs e)
        {
            Undo_not_saved(projectsTable);
        }

        private void buttonUndoSusp_Click(object sender, EventArgs e)
        {
            Undo_not_saved(suspensionsTable);
        }

        private void buttonUndoSeries_Click(object sender, EventArgs e)
        {
            Undo_not_saved(seriesTable);
        }

        private void buttonUndoSimul_Click(object sender, EventArgs e)
        {
            if ((simulationsTable.GetChanges() != null) || (groupsTable.GetChanges() != null) || (valuesTable.GetChanges() != null))
            {
                if (MessageBox.Show("Undo the changes to " + simulationsTable.TableName + "?", "Undo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        List<List<DataRow[]>> ListMain = new List<List<DataRow[]>>();
                        List<DataTable> tablesList = new List<DataTable>();
                        //added
                        if ((simulationsTable.GetChanges(DataRowState.Added) != null) || (groupsTable.GetChanges(DataRowState.Added) != null) || (valuesTable.GetChanges(DataRowState.Added) != null))
                        {
                            valuesTable.RejectChanges();
                            groupsTable.RejectChanges();
                            simulationsTable.RejectChanges();
                        }
                        //deleted
                        if ((simulationsTable.GetChanges(DataRowState.Deleted) != null) || (groupsTable.GetChanges(DataRowState.Deleted) != null) || (valuesTable.GetChanges(DataRowState.Deleted) != null))
                        {
                            ListMain.Clear();
                            tablesList.Clear();
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            tablesList.Add(valuesTable);
                            tablesList.Add(groupsTable);
                            tablesList.Add(simulationsTable);
                            tablesList.Add(seriesTable);
                            tablesList.Add(suspensionsTable);
                            tablesList.Add(projectsTable);
                            String s = String.Empty, primKey = String.Empty;
                            ListMain[0].Add(tablesList[0].Select(null, null, DataViewRowState.Deleted));
                            for (int c_tables = 0; c_tables < ListMain.Count - 1; c_tables++)
                            {
                                if (ListMain[c_tables].Count == 0)
                                    break;
                                else
                                {
                                    primKey = tablesList[c_tables + 1].PrimaryKey[0].ToString();
                                    for (int c_rows = 0; c_rows < ListMain[c_tables].Count(); c_rows++)
                                    {
                                        for (int c_rows_index = 0; c_rows_index < ListMain[c_tables][c_rows].Count(); c_rows_index++)
                                        {
                                            String IdCur = ListMain[c_tables][c_rows].ElementAt(c_rows_index)[primKey, DataRowVersion.Original].ToString();
                                            ListMain[c_tables + 1].Add(tablesList[c_tables + 1].Select(primKey + " = " + IdCur, null, DataViewRowState.Deleted));
                                        }
                                    }
                                }
                            }
                            ListMain.Reverse();
                            tablesList.Reverse();
                            for (int c_tables = 0; c_tables < ListMain.Count; c_tables++)
                            {
                                if (ListMain[c_tables].Count == 0)
                                    break;
                                else
                                    tablesList[c_tables].RejectChanges();
                            }
                        }
                        //modified
                        if ((simulationsTable.GetChanges(DataRowState.Modified) != null) || (groupsTable.GetChanges(DataRowState.Modified) != null) || (valuesTable.GetChanges(DataRowState.Modified) != null))
                        {
                                    valuesTable.RejectChanges();
                                    groupsTable.RejectChanges();
                                    simulationsTable.RejectChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating " + simulationsTable.TableName + ":\n" + ex.ToString(), "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

        }

        private void Undo_not_saved(DataTable table)
        {
            if (table.GetChanges() != null)
            {
                if (MessageBox.Show("Undo the changes to " + table.TableName + "?", "Undo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        List<List<DataRow[]>> ListMain = new List<List<DataRow[]>>();
                        List<DataTable> tablesList = new List<DataTable>();
                        bool notRemoved = (table.TableName != simulationsTable.TableName) || (table.TableName != groupsTable.TableName) || (table.TableName != valuesTable.TableName);
                        //added
                        if (table.GetChanges(DataRowState.Added) != null)
                        {
                            ListMain.Clear();
                            tablesList.Clear();
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            tablesList.Add(projectsTable);
                            tablesList.Add(suspensionsTable);
                            tablesList.Add(seriesTable);
                            tablesList.Add(simulationsTable);
                            tablesList.Add(groupsTable);
                            tablesList.Add(valuesTable);
                            for (int c_table = 1; c_table < tablesList.Count; c_table++)
                            {
                                if (table.TableName == tablesList[c_table].TableName)
                                {
                                    for (int rem = c_table - 1; rem > -1; rem--)
                                    {
                                        ListMain.RemoveAt(rem);
                                        tablesList.RemoveAt(rem);
                                    }
                                }
                             }
                            Int32 endOfList = ListMain.Count - 1;
                            String s = String.Empty;
                            ListMain[0].Add(tablesList[0].Select(null, null, DataViewRowState.Added));
                            for (int c_tables = 0; c_tables < ListMain.Count; c_tables++)
                            {
                                if (ListMain[c_tables].Count == 0)
                                    break;
                                else
                                {
                                    String primKey = tablesList[c_tables].PrimaryKey[0].ToString();
                                    for (int c_rows = 0; c_rows < ListMain[c_tables].Count(); c_rows++)
                                    {
                                        for (int c_rows_index = 0; c_rows_index < ListMain[c_tables][c_rows].Count(); c_rows_index++)
                                        {
                                            if (c_tables != endOfList)
                                            {
                                                Int32 IdCur = Convert.ToInt32(ListMain[c_tables][c_rows].ElementAt(c_rows_index)[primKey, DataRowVersion.Default]);
                                                ListMain[c_tables + 1].Add(tablesList[c_tables + 1].Select(primKey + " = " + IdCur.ToString(), null, DataViewRowState.Added));
                                            }
                                        }
                                    }
                                }
                            }
                            ListMain.Reverse();
                            tablesList.Reverse();
                            for (int c_tables = 0; c_tables < ListMain.Count; c_tables++)
                            {
                                    tablesList[c_tables].RejectChanges();
                            }
                        }
                        //deleted
                        if (table.GetChanges(DataRowState.Deleted) != null)
                        {
                            ListMain.Clear();
                            tablesList.Clear();
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            ListMain.Add(new List<DataRow[]>());
                            tablesList.Add(valuesTable);
                            tablesList.Add(groupsTable);
                            tablesList.Add(simulationsTable);
                            tablesList.Add(seriesTable);
                            tablesList.Add(suspensionsTable);
                            tablesList.Add(projectsTable);

                            for (int c_table = 1; c_table < tablesList.Count; c_table++)
                            {
                                if (table.TableName == tablesList[c_table].TableName && notRemoved)
                                {
                                    for (int rem = c_table - 1; rem > -1; rem--)
                                    {
                                        ListMain.RemoveAt(rem);
                                        tablesList.RemoveAt(rem);
                                    }
                                }
                            }
                            String s = String.Empty, primKey = String.Empty;
                            ListMain[0].Add(tablesList[0].Select(null, null, DataViewRowState.Deleted));
                            for (int c_tables = 0; c_tables < ListMain.Count - 1; c_tables++)
                            {
                                if (ListMain[c_tables].Count == 0)
                                    break;
                                else
                                {
                                    primKey = tablesList[c_tables + 1].PrimaryKey[0].ToString();
                                    for (int c_rows = 0; c_rows < ListMain[c_tables].Count(); c_rows++)
                                    {
                                        for (int c_rows_index = 0; c_rows_index < ListMain[c_tables][c_rows].Count(); c_rows_index++)
                                        {
                                            String IdCur = ListMain[c_tables][c_rows].ElementAt(c_rows_index)[primKey, DataRowVersion.Original].ToString();
                                            ListMain[c_tables + 1].Add(tablesList[c_tables + 1].Select(primKey + " = " + IdCur, null, DataViewRowState.Deleted));
                                        }
                                    }
                                }
                            }
                            ListMain.Reverse();
                            tablesList.Reverse();
                            for (int c_tables = 0; c_tables < ListMain.Count; c_tables++)
                            {
                                if (ListMain[c_tables].Count == 0)
                                    break;
                                else
                                    tablesList[c_tables].RejectChanges();
                            }
                        }
                        //modified
                        if (table.GetChanges(DataRowState.Modified) != null)
                        {
                            for (int c_table = 0; c_table < tablesList.Count; c_table++)
                            {
                                if (table.TableName == tablesList[c_table].TableName)
                                {
                                    tablesList[c_table].RejectChanges();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating " + table.TableName + ":\n" + ex.ToString(), "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void Save_to_Db(DataTable table)
        {
            if (table.GetChanges() != null)
            {
                if (MessageBox.Show("Save the changes to " + table.TableName + "?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        List<List<DataRow[]>> deleteListMain = new List<List<DataRow[]>>();
                        List<DataTable> tablesList = new List<DataTable>();
                        List<OleDbDataAdapter> dataAdaptersList = new List<OleDbDataAdapter>();
                        bool notRemoved = (table.TableName != simulationsTable.TableName) || (table.TableName != groupsTable.TableName) || (table.TableName != valuesTable.TableName);
                        //deleted
                        if (table.GetChanges(DataRowState.Deleted) != null)
                        {
                            deleteListMain.Clear();
                            tablesList.Clear();
                            dataAdaptersList.Clear();
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            tablesList.Add(projectsTable);
                            tablesList.Add(suspensionsTable);
                            tablesList.Add(seriesTable);
                            tablesList.Add(simulationsTable);
                            tablesList.Add(groupsTable);
                            tablesList.Add(valuesTable);
                            dataAdaptersList.Add(cpDaProj);
                            dataAdaptersList.Add(cpDaSusp);
                            dataAdaptersList.Add(cpDaSeries);
                            dataAdaptersList.Add(cpDaSimul);
                            dataAdaptersList.Add(cpDaGroups);
                            dataAdaptersList.Add(cpDaValues);
                            for (int c_table = 1; c_table < tablesList.Count; c_table++)
                            {
                                if (table.TableName == tablesList[c_table].TableName)
                                {
                                    for (int rem = c_table - 1; rem > -1; rem--)
                                    {
                                        deleteListMain.RemoveAt(rem);
                                        tablesList.RemoveAt(rem);
                                        dataAdaptersList.RemoveAt(rem);
                                    }
                                }
                                //else
                                //throw new Exception("Tables not found!");
                            }
                            Int32 endOfList = deleteListMain.Count - 1;
                            String s = String.Empty;
                            deleteListMain[0].Add(tablesList[0].Select(null, null, DataViewRowState.Deleted));
                            for (int c_tables = 0; c_tables < deleteListMain.Count; c_tables++)
                            {
                                if (deleteListMain[c_tables].Count == 0)
                                    break;
                                else
                                {
                                    String primKey = tablesList[c_tables].PrimaryKey[0].ToString();
                                    for (int c_rows = 0; c_rows < deleteListMain[c_tables].Count(); c_rows++)
                                    {
                                        for (int c_rows_index = 0; c_rows_index < deleteListMain[c_tables][c_rows].Count(); c_rows_index++)
                                        {
                                            if (c_tables != endOfList)
                                            {
                                                Int32 IdCur = Convert.ToInt32(deleteListMain[c_tables][c_rows].ElementAt(c_rows_index)[primKey, DataRowVersion.Original]);
                                                deleteListMain[c_tables + 1].Add(tablesList[c_tables + 1].Select(primKey + " = " + IdCur.ToString(), null, DataViewRowState.Deleted));
                                            }
                                        }
                                    }
                                }
                            }
                            for (int c_tables = deleteListMain.Count - 1; c_tables > -1; c_tables--)
                            {
                                if (deleteListMain[c_tables].Count > 0)
                                    for (int c_row = 0; c_row < deleteListMain[c_tables].Count; c_row++)
                                        dataAdaptersList[c_tables].Update(deleteListMain[c_tables][c_row]);
                            }
                        }
                        //added
                        if (table.GetChanges(DataRowState.Added) != null)
                        {
                            deleteListMain.Clear();
                            tablesList.Clear();
                            dataAdaptersList.Clear();
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            tablesList.Add(valuesTable);
                            tablesList.Add(groupsTable);
                            tablesList.Add(simulationsTable);
                            tablesList.Add(seriesTable);
                            tablesList.Add(suspensionsTable);
                            tablesList.Add(projectsTable);
                            dataAdaptersList.Add(cpDaValues);
                            dataAdaptersList.Add(cpDaGroups);
                            dataAdaptersList.Add(cpDaSimul);
                            dataAdaptersList.Add(cpDaSeries);
                            dataAdaptersList.Add(cpDaSusp);
                            dataAdaptersList.Add(cpDaProj);

                            for (int c_table = 1; c_table < tablesList.Count; c_table++)
                            {
                                if (table.TableName == tablesList[c_table].TableName && notRemoved)
                                {
                                    for (int rem = c_table - 1; rem > -1; rem--)
                                    {
                                        deleteListMain.RemoveAt(rem);
                                        tablesList.RemoveAt(rem);
                                        dataAdaptersList.RemoveAt(rem);
                                    }
                                }
                            }
                            String s = String.Empty, primKey = String.Empty;
                            deleteListMain[0].Add(tablesList[0].Select(null, null, DataViewRowState.Added));
                            for (int c_tables = 0; c_tables < deleteListMain.Count - 1; c_tables++)
                            {
                                if (deleteListMain[c_tables].Count == 0)
                                    break;
                                else
                                {
                                    primKey = tablesList[c_tables + 1].PrimaryKey[0].ToString();
                                    for (int c_rows = 0; c_rows < deleteListMain[c_tables].Count(); c_rows++)
                                    {
                                        for (int c_rows_index = 0; c_rows_index < deleteListMain[c_tables][c_rows].Count(); c_rows_index++)
                                        {
                                            String IdCur = deleteListMain[c_tables][c_rows].ElementAt(c_rows_index)[primKey, DataRowVersion.Default].ToString();
                                            deleteListMain[c_tables + 1].Add(tablesList[c_tables + 1].Select(primKey + " = " + IdCur, null, DataViewRowState.Added));
                                        }
                                    }
                                }
                            }
                            deleteListMain.Reverse();
                            tablesList.Reverse();
                            dataAdaptersList.Reverse();
                            for (int c_tables = 0; c_tables < deleteListMain.Count; c_tables++)
                            {
                                    for (int c_row = 0; c_row < deleteListMain[c_tables].Count; c_row++)
                                        dataAdaptersList[c_tables].Update(deleteListMain[c_tables][c_row]);
                            }
                        }
                        //modified
                        if (table.GetChanges(DataRowState.Modified) != null)
                        {
                            for (int c_table = 0; c_table < tablesList.Count; c_table++)
                            {
                                if (table.TableName == tablesList[c_table].TableName)
                                {
                                    dataAdaptersList[c_table].Update(tablesList[c_table].Select(null, null, DataViewRowState.ModifiedCurrent));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating " + table.TableName + ":\n" + ex.ToString(), "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void buttonSaveProject_Click(object sender, EventArgs e)
        {
            Save_to_Db(projectsTable);
        }

        private void buttonSaveSusp_Click(object sender, EventArgs e)
        {
            Save_to_Db(suspensionsTable);
        }

        private void buttonSaveSeries_Click(object sender, EventArgs e)
        {
            Save_to_Db(seriesTable);
        }

        private void buttonSaveSimul_Click(object sender, EventArgs e)
        {
            if ((simulationsTable.GetChanges() != null) || (groupsTable.GetChanges() != null) || (valuesTable.GetChanges() != null))
            {
                if (MessageBox.Show("Save the changes to " + simulationsTable.TableName + "?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        List<List<DataRow[]>> deleteListMain = new List<List<DataRow[]>>();
                        List<DataTable> tablesList = new List<DataTable>();
                        List<OleDbDataAdapter> dataAdaptersList = new List<OleDbDataAdapter>();
                        //deleted
                        if ((simulationsTable.GetChanges(DataRowState.Deleted) != null) || (groupsTable.GetChanges(DataRowState.Deleted) != null) || (valuesTable.GetChanges(DataRowState.Deleted) != null))
                        {
                            cpDaValues.Update(cpDataSet.Tables[tables.values].Select(null, null, DataViewRowState.Deleted));
                            cpDaGroups.Update(cpDataSet.Tables[tables.groups].Select(null, null, DataViewRowState.Deleted));
                            cpDaSimul.Update(cpDataSet.Tables[tables.simulations].Select(null, null, DataViewRowState.Deleted));
                        }
                        //added
                        if ((simulationsTable.GetChanges(DataRowState.Added) != null) || (groupsTable.GetChanges(DataRowState.Added) != null) || (valuesTable.GetChanges(DataRowState.Added) != null))
                        {
                            deleteListMain.Clear();
                            tablesList.Clear();
                            dataAdaptersList.Clear();
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            deleteListMain.Add(new List<DataRow[]>());
                            tablesList.Add(valuesTable);
                            tablesList.Add(groupsTable);
                            tablesList.Add(simulationsTable);
                            tablesList.Add(seriesTable);
                            tablesList.Add(suspensionsTable);
                            tablesList.Add(projectsTable);
                            dataAdaptersList.Add(cpDaValues);
                            dataAdaptersList.Add(cpDaGroups);
                            dataAdaptersList.Add(cpDaSimul);
                            dataAdaptersList.Add(cpDaSeries);
                            dataAdaptersList.Add(cpDaSusp);
                            dataAdaptersList.Add(cpDaProj);
                            String s = String.Empty, primKey = String.Empty;
                            deleteListMain[0].Add(tablesList[0].Select(null, null, DataViewRowState.Added));
                            for (int c_tables = 0; c_tables < deleteListMain.Count - 1; c_tables++)
                            {
                                if (deleteListMain[c_tables].Count == 0)
                                    break;
                                else
                                {
                                    primKey = tablesList[c_tables + 1].PrimaryKey[0].ToString();
                                    for (int c_rows = 0; c_rows < deleteListMain[c_tables].Count(); c_rows++)
                                    {
                                        for (int c_rows_index = 0; c_rows_index < deleteListMain[c_tables][c_rows].Count(); c_rows_index++)
                                        {
                                            String IdCur = deleteListMain[c_tables][c_rows].ElementAt(c_rows_index)[primKey, DataRowVersion.Default].ToString();
                                            deleteListMain[c_tables + 1].Add(tablesList[c_tables + 1].Select(primKey + " = " + IdCur, null, DataViewRowState.Added));
                                        }
                                    }
                                }
                            }
                            deleteListMain.Reverse();
                            tablesList.Reverse();
                            dataAdaptersList.Reverse();
                            for (int c_tables = 0; c_tables < deleteListMain.Count; c_tables++)
                            {
                                for (int c_row = 0; c_row < deleteListMain[c_tables].Count; c_row++)
                                    dataAdaptersList[c_tables].Update(deleteListMain[c_tables][c_row]);
                            }
                        }
                        //modified
                        if ((simulationsTable.GetChanges(DataRowState.Modified) != null) || (groupsTable.GetChanges(DataRowState.Modified) != null) || (valuesTable.GetChanges(DataRowState.Modified) != null))
                        {
                            dataAdaptersList[tablesList.Count - 3].Update(tablesList[tablesList.Count - 3].Select(null, null, DataViewRowState.ModifiedCurrent));
                            dataAdaptersList[tablesList.Count - 2].Update(tablesList[tablesList.Count - 2].Select(null, null, DataViewRowState.ModifiedCurrent));
                            dataAdaptersList[tablesList.Count - 1].Update(tablesList[tablesList.Count - 1].Select(null, null, DataViewRowState.ModifiedCurrent));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating " + simulationsTable.TableName + ":\n" + ex.ToString(), "CyclonPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Undo_not_saved(suspensionsTable);
        }

        void cpDaProj_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand newProjID = new OleDbCommand("SELECT @@IDENTITY FROM projects", cpOleDbConnection);
                e.Row["id_project"] = (int)newProjID.ExecuteScalar();
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        protected void writeToOleDb(fsParameterIdentifier parameter, DataGridViewCellEventArgs e)
        {
            //int currentRow = e.RowIndex;
            //fsValue val = hydrocycloneControl1.GetValue(parameter);
            //DataRowView drv = (DataRowView)cpCmSimul.Current;
            //DataRow[] idCurrentGroup = cpDataSet.Tables[tables.groups].Select("id_simulation = " + drv.Row["id_simulation"]);
            //DataRow[] tableValue;

            //if (Convert.ToInt32(idCurrentGroup.First()["id_group"]) < Convert.ToInt32(idCurrentGroup.Last()["id_group"]))
            //    tableValue = cpDataSet.Tables[tables.values].Select("id_group >= " + idCurrentGroup.First()["id_group"].ToString() + " AND id_group <= " + idCurrentGroup.Last()["id_group"].ToString());
            //else
            //    tableValue = cpDataSet.Tables[tables.values].Select("id_group >= " + idCurrentGroup.Last()["id_group"].ToString() + " AND id_group <= " + idCurrentGroup.First()["id_group"].ToString());
            //DataRow dataRowEdit = tableValue[currentRow];
            //dataRowEdit.BeginEdit();
            //dataRowEdit["valueOfParam"] = val.Value;
            //dataRowEdit["defined"] = val.Defined;
            //dataRowEdit.EndEdit();
         }


        private void buttonDuplicateSeries_Click(object sender, EventArgs e)
        {
            cloneSimulation();
        }

        private void fmDataGridSimul_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            throw new Exception("Method commented out.");
//             if (cpCmSimul.Count > 0)
//             {
//                 int curRowIndex = e.RowIndex;
//                 DataRow[] tableGroup = groupsTable.Select("id_simulation = " + fmDataGridSimul.Rows[curRowIndex].Cells["id_simulation"].Value.ToString());
//                 for (int c = 0; c < tableGroup.Length; c++)
//                 {
//                     fsParametersGroup fspgNew = hydrocycloneControl1.Groups[c];
//                     fspgNew.Representator = FindParamIdentifNameFromGroups(tableGroup[c]["representator"].ToString());
//                     DataRow[] tableValue = valuesTable.Select("id_group = " + tableGroup[c]["id_group"].ToString());
//                     for (int v = 0; v < hydrocycloneControl1.Groups[c].Parameters.Count; v++)
//                     {
//                         Double value = Convert.ToDouble(tableValue[v]["valueOfParam"]);
//                         bool defined = Convert.ToBoolean(tableValue[v]["defined"]);
//                         hydrocycloneControl1.SetValue(hydrocycloneControl1.Groups[c].Parameters[v], new fsValue(value, defined));
//                     }
//                 }
//                 String option = fmDataGridSimul.Rows[curRowIndex].Cells["options"].Value.ToString();
//                 hydrocycloneControl1.ChangeCalculationOption((CalculatorModules.Hydrocyclone.HydrocycloneControl.fsCalculationOption)findCalculationOption(option));
//                 hydrocycloneControl1.comboBoxCalculationOption.SelectedItem = option;
//             }
        }

        private void No_simulations()
        {
            if (fmDataGridSimul.Rows.Count == 0)
            {
                hydrocycloneControl1.dataGrid.Enabled = false;
                for (int i = 0; i < hydrocycloneControl1.dataGrid.Rows.Count; i++)
                    hydrocycloneControl1.dataGrid.Rows[i].Cells[2].Value = null;
            }
        }

        private void MainCyclonPlusForm_Load(object sender, EventArgs e)
        {
            No_simulations();
        }

        public fsParameterIdentifier FindParamIdentifNameFromGroups(String nameOfParamIdentif)
        {
            throw new Exception("Method commented out.");
//             for (int c = 0; c < hydrocycloneControl1.Groups.Count; c++)
//             {
//                 for (int par = 0; par < hydrocycloneControl1.Groups[c].Parameters.Count; par++)
//                 {
//                     if (hydrocycloneControl1.Groups[c].Parameters[par].Name == nameOfParamIdentif)
//                         return hydrocycloneControl1.Groups[c].Parameters[par];
//                 }
//             }
//             return new fsParameterIdentifier("");
        }

        public Enum findCalculationOption(String option)
        {
            switch (option)
            {
                case "Dp":
                    return fsHydrocycloneControl.fsCalculationOption.Dp;
                case "n":
                    return fsHydrocycloneControl.fsCalculationOption.n;
                case "Q":
                    return fsHydrocycloneControl.fsCalculationOption.Q;
                default:
                    return fsHydrocycloneControl.fsCalculationOption.Dp;
            }
        }
    }


}

