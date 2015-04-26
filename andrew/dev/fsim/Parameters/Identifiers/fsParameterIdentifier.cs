using System;
using Units;

namespace Parameters
{
    public class fsParameterIdentifier
    {
        #region fsParameterIdentifier

        #region Constructors

        public fsParameterIdentifier(string name)
        {
            Name = name;
            FullName = name;
            MeasurementCharacteristic = fsCharacteristic.NoUnits;
        }

        public fsParameterIdentifier(string name, fsCharacteristic units)
        {
            Name = name;
            FullName = name;
            MeasurementCharacteristic = units;
        }

        public fsParameterIdentifier(string name, string fullName, fsCharacteristic units)
        {
            Name = name;
            FullName = fullName;
            MeasurementCharacteristic = units;
        }

        public fsParameterIdentifier(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
            MeasurementCharacteristic = fsCharacteristic.NoUnits;
        }

        #endregion

        public string Name { get; private set; }

        public string FullName { get; private set; }

        public fsCharacteristic MeasurementCharacteristic { get; private set; }

        public override string ToString()
        {
            return FullName == Name ? Name : FullName + " (" + Name + ")";
        }

        #endregion

        #region Parameters Collection

        public static fsParameterIdentifier CakePorosity =
            new fsParameterIdentifier("eps", "Cake Porosity", fsCharacteristic.Concentration);

        public static fsParameterIdentifier Kappa = new fsParameterIdentifier("kappa");

        public static fsParameterIdentifier CakePermeability =
            new fsParameterIdentifier("Pc", "Cake Permeability", fsCharacteristic.CakePermeability);

        public static fsParameterIdentifier PracticalCakePermeability =
            new fsParameterIdentifier("K", "Practical Cake Permeability", fsCharacteristic.PracticalCakePermeability);

        public static fsParameterIdentifier DeliquoringStepKParameter = 
            new fsParameterIdentifier("K", "No information", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier FilterArea =
            new fsParameterIdentifier("A", "Filter Area", fsCharacteristic.Area);

        public static fsParameterIdentifier FilterLength =
            new fsParameterIdentifier("l", "Filter Length", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier As = new fsParameterIdentifier("As", fsCharacteristic.Area);

        public static fsParameterIdentifier MachineDiameter =
            new fsParameterIdentifier("D", "Machine Diameter", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier FilterElementDiameter =
            new fsParameterIdentifier("d", "Filter Element Diameter", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier MachineWidth =
            new fsParameterIdentifier("b", "Machine Width", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier WidthOverDiameterRatio =
            new fsParameterIdentifier("b/D", "Width/Diameter Ratio", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier PressureDifference =
            new fsParameterIdentifier("Dp", "Pressure Difference", fsCharacteristic.Pressure);

        public static fsParameterIdentifier Dp_f =
            new fsParameterIdentifier("Dp_f", "Pressure Difference", fsCharacteristic.Pressure);

        public static fsParameterIdentifier Dp_d =
            new fsParameterIdentifier("Dp_d", "Pressure Difference", fsCharacteristic.Pressure);

        public static fsParameterIdentifier CycleTime =
            new fsParameterIdentifier("tc", "Cycle Time", fsCharacteristic.Time);

        public static fsParameterIdentifier RotationalSpeed =
            new fsParameterIdentifier("n", "Cycle Frequency", fsCharacteristic.Frequency);

        public static fsParameterIdentifier SpecificFiltrationTime =
            new fsParameterIdentifier("sf", "Specific Filtration Time", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SpecificResidualTime =
            new fsParameterIdentifier("sr", "Specific Residual Time", fsCharacteristic.Concentration);

        public static fsParameterIdentifier FiltrationTime =
            new fsParameterIdentifier("tf", "Filtration Time", fsCharacteristic.Time);

        public static fsParameterIdentifier ResidualTime =
            new fsParameterIdentifier("tr", "Residual Time", fsCharacteristic.Time);

        public static fsParameterIdentifier DeliquoringTime =
            new fsParameterIdentifier("td", "Deliquoring Time", fsCharacteristic.Time);

        public static fsParameterIdentifier IndexingTime =
            new fsParameterIdentifier("t", "Indexing Time", fsCharacteristic.Time);

        public static fsParameterIdentifier CakeHeight =
            new fsParameterIdentifier("hc", "Cake Height", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier CakeHeight0 =
            new fsParameterIdentifier("hc0", "Cake Height 0", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier WetCakeMass =
            new fsParameterIdentifier("Mcw", "Wet Cake Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier DryCakeMass =
            new fsParameterIdentifier("Mcd", "Dry Cake Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SolidsMass =
            new fsParameterIdentifier("Ms", "Solids Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SuspensionMass =
            new fsParameterIdentifier("Msus", "Suspension Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SpecificSuspensionMass =
            new fsParameterIdentifier("msus", "Specific susp mass", fsCharacteristic.SpecificMass);

        public static fsParameterIdentifier FiltrateMass =
            new fsParameterIdentifier("Mf", "Fitrate Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier FiltrateMassD =
            new fsParameterIdentifier("Mfd", "Fitrate Mass D", fsCharacteristic.Mass);

        public static fsParameterIdentifier LiquidMassInSuspension =
            new fsParameterIdentifier("Ml", "Liquid Mass in Suspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier SolidsMassInSuspension =
            new fsParameterIdentifier("Ms", "Solids Mass in Suspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier CakeMass =
            new fsParameterIdentifier("Mc", "Cake Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SpecificSolidsMass =
            new fsParameterIdentifier("ms", "Specific solids mass", fsCharacteristic.SpecificMass);

        public static fsParameterIdentifier SolidsVolume =
            new fsParameterIdentifier("Vs", "Solids Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SuspensionVolume =
            new fsParameterIdentifier("Vsus", "Suspension Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SpecificSuspensionVolume =
            new fsParameterIdentifier("vsus", "Specific susp volume", fsCharacteristic.SpecificVolume);

        public static fsParameterIdentifier FiltrateVolume =
            new fsParameterIdentifier("Vf", "Fitrate Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier FiltrateVolumeD =
            new fsParameterIdentifier("Vfd", "Fitrate Volume D", fsCharacteristic.Volume);

        public static fsParameterIdentifier DeliquoringVolume =
            new fsParameterIdentifier("Vld", "Deliquoring Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SuspensionMassFlowrate =
            new fsParameterIdentifier("Qmsus", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qms =
            new fsParameterIdentifier("Qms", "Solids Mass Throughput", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qsus =
            new fsParameterIdentifier("Qsus", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qs =
            new fsParameterIdentifier("Qs", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qmsust =
            new fsParameterIdentifier("Qmsus,t", "Susp mass rate rel to t", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qmsusd =
            new fsParameterIdentifier("Qmsus,d", "Diff susp mass rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qsust =
            new fsParameterIdentifier("Qsus,t", "Susp volume rate rel to t", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qsusd =
            new fsParameterIdentifier("Qsus,d", "Diff susp volume rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier ns =
            new fsParameterIdentifier("ns", "Number of segments", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ls_over_b =
            new fsParameterIdentifier("ls/b", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier l_over_b =
            new fsParameterIdentifier("l/b", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ls =
            new fsParameterIdentifier("ls", "Length of segments", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier StandardTechnicalTime =
            new fsParameterIdentifier("ttech0", "Standard technical time", fsCharacteristic.Time);

        public static fsParameterIdentifier lambda =
            new fsParameterIdentifier("lambda", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier TechnicalTime =
            new fsParameterIdentifier("ttech", "Technical Time", fsCharacteristic.Time);

        public static fsParameterIdentifier u =
            new fsParameterIdentifier("u", "Belt speed", fsCharacteristic.Speed);

        public static fsParameterIdentifier nsf =
            new fsParameterIdentifier("nsf", "Segment number for cake formation", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier nsr =
            new fsParameterIdentifier("nsr", "Segment number for residual steps", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier nsd =
            new fsParameterIdentifier("nsd", "Segment number for deliquoring steps", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier sd =
            new fsParameterIdentifier("sd", "???", fsCharacteristic.Concentration);

        public static fsParameterIdentifier qsust =
            new fsParameterIdentifier("qsus,t", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qsusd =
            new fsParameterIdentifier("qsus,d", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmsust =
            new fsParameterIdentifier("qmsus,t", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qmsusd =
            new fsParameterIdentifier("qmsus,d", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qft =
            new fsParameterIdentifier("qft", "Spec filtrate volume rel to time", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmft =
            new fsParameterIdentifier("qmft", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qf =
            new fsParameterIdentifier("qf", "Spec filtrate volume rel to time", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmf =
            new fsParameterIdentifier("qmf", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier Qmfd =
            new fsParameterIdentifier("Qmfd", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qmftd =
            new fsParameterIdentifier("Qmftd", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qfd =
            new fsParameterIdentifier("Qfd", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qftd =
            new fsParameterIdentifier("Qftd", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qmcd =
            new fsParameterIdentifier("Qmcd", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qld =
            new fsParameterIdentifier("Qld", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qmld =
            new fsParameterIdentifier("Qmld", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier qmfd =
            new fsParameterIdentifier("qmfd", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qmftd =
            new fsParameterIdentifier("qmftd", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qfd =
            new fsParameterIdentifier("qfd", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qftd =
            new fsParameterIdentifier("qftd", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmcd =
            new fsParameterIdentifier("qmcd", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qld =
            new fsParameterIdentifier("qld", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmld =
            new fsParameterIdentifier("qmld", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier SurfaceTensionLiquidInCake =
            new fsParameterIdentifier(Convert.ToChar(0x3c3).ToString(), "Surface Tension Liquid in Cake", fsCharacteristic.SurfaceTension);

        public static fsParameterIdentifier StandardCapillaryPressure =
            new fsParameterIdentifier("pke_st", "Standard Capillary Pressure", fsCharacteristic.Pressure);

        public static fsParameterIdentifier CapillaryPressure =
            new fsParameterIdentifier("pke", "Capillary Pressure", fsCharacteristic.Pressure);

        public static fsParameterIdentifier SuspensionSolidsMassFraction =
            new fsParameterIdentifier("Cm", "Suspension Solids Mass Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SuspensionSolidsVolumeFraction =
            new fsParameterIdentifier("Cv", "Suspension Solids Volume Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SuspensionSolidsConcentration =
            new fsParameterIdentifier("C", "Suspension Solids Concentration", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier CakePorosity0 =
            new fsParameterIdentifier("eps0", "Cake Porosity for Dp0 = 1 bar", fsCharacteristic.Concentration);

        public static fsParameterIdentifier Kappa0 = new fsParameterIdentifier("kappa0");

        public static fsParameterIdentifier Ne =
            new fsParameterIdentifier("ne", "Cake Volume Reduction Factor", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier CakePermeability0 =
            new fsParameterIdentifier("Pc0", "Cake Permeability for Dp0 = 1 bar", fsCharacteristic.CakePermeability);

        public static fsParameterIdentifier CakeCompressibility =
            new fsParameterIdentifier("nc", "Cake Compressibility");

        #region Viscosities

        public static fsParameterIdentifier MotherLiquidViscosity =
            new fsParameterIdentifier("eta", "Mother Liquid Viscosity", fsCharacteristic.Viscosity);

        public static fsParameterIdentifier WashLiquidViscosity =
            new fsParameterIdentifier("eta_w", "Wash Liquid Viscosity", fsCharacteristic.Viscosity);

        public static fsParameterIdentifier GasLiquidViscosity =
            new fsParameterIdentifier(Convert.ToChar(0x3B7) + "_g", "Gas Liquid Viscosity", fsCharacteristic.Viscosity);

        #endregion

        #region Densities

        public static fsParameterIdentifier DryCakeDensity0 =
            new fsParameterIdentifier("rho_cd0", "Dry cake density for Dp0 = 1 bar", fsCharacteristic.Density);

        public static fsParameterIdentifier DryCakeDensity =
            new fsParameterIdentifier("rho_cd", "Dry cake density", fsCharacteristic.Density);

        public static fsParameterIdentifier CakeWetDensity0 =
            new fsParameterIdentifier("rho_cw0", "Cake wet density 0", fsCharacteristic.Density);

        public static fsParameterIdentifier CakeWetDensity =
            new fsParameterIdentifier("rho_cw", "Cake wet density", fsCharacteristic.Density);

        public static fsParameterIdentifier MotherLiquidDensity =
            new fsParameterIdentifier("rho", "Density of Mother Liquid", fsCharacteristic.Density);

        public static fsParameterIdentifier LiquidDensity =
            new fsParameterIdentifier("rho_l", "Liquid Density", fsCharacteristic.Density);

        public static fsParameterIdentifier SolidsDensity =
            new fsParameterIdentifier("rho_s", "Solids Density", fsCharacteristic.Density);

        public static fsParameterIdentifier SuspensionDensity =
            new fsParameterIdentifier("rho_sus", "Suspension Density", fsCharacteristic.Density);

        public static fsParameterIdentifier WashLiquidDensity =
            new fsParameterIdentifier("rho_w", "Wash Liquid Density", fsCharacteristic.Density);

        public static fsParameterIdentifier Rho_Bulk =
            new fsParameterIdentifier("rho_bulk", fsCharacteristic.Density);

        #endregion;

        #region Resistances

        public static fsParameterIdentifier CakeResistanceAlpha0 =
            new fsParameterIdentifier("alpha0", "Cake Resistance for Dp0 = 1 bar", fsCharacteristic.CakeResistanceAlpha);

        public static fsParameterIdentifier CakeResistanceAlpha =
            new fsParameterIdentifier("alpha", "Cake Resistance", fsCharacteristic.CakeResistanceAlpha);

        public static fsParameterIdentifier FilterMediumResistanceHce0 =
            new fsParameterIdentifier("hce0", "Filter Medium Resistance hce0", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier FilterMediumResistanceHce =
            new fsParameterIdentifier("hce", "Specific Filter Medium Resistance hce", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier FilterMediumResistanceRm0 =
            new fsParameterIdentifier("Rm0", fsCharacteristic.FilterMediumResistance);

        public static fsParameterIdentifier FilterMediumResistanceRm =
           new fsParameterIdentifier("Rm", "Filter Medium Resistance Rm", fsCharacteristic.FilterMediumResistance);

        public static fsParameterIdentifier CakeResistance0 =
            new fsParameterIdentifier("rc0", "Cake Resistance for Dp0 = 1 bar", fsCharacteristic.CakeResistance);
        public static fsParameterIdentifier CakePlusMediumPermeability =
           new fsParameterIdentifier("Pc*", "'Cake + Medium' Permeability", fsCharacteristic.CakePermeability);

        public static fsParameterIdentifier CakePlusMediumResistance =
            new fsParameterIdentifier("rc*", "'Cake + Medium' Resistance", fsCharacteristic.CakeResistance);

        public static fsParameterIdentifier CakePlusMediumResistanceAlpha =
            new fsParameterIdentifier("alpha*", "'Cake + Medium' Resistance", fsCharacteristic.CakeResistanceAlpha);

        public static fsParameterIdentifier CakeResistance =
            new fsParameterIdentifier("rc", "Cake Resistance", fsCharacteristic.CakeResistance);

        #endregion

        #region Solutes characteristics

        public static fsParameterIdentifier SolutesConcentrationInMotherLiquid =
            new fsParameterIdentifier("C_sol", "Solutes Concentration in Mother Liquid", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier SolutesMassFractionInMotherLiquid =
            new fsParameterIdentifier("Cm_sol", "Solutes Mass Fraction in Mother Liquid", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SolutesConcentrationInCakeLiquid =
            new fsParameterIdentifier("C_sol", "Solutes Concentration in Cake Liquid", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier SolutesMassFractionInLiquid =
            new fsParameterIdentifier("Cm_sol", "Solutes Mass Fraction in Liquid", fsCharacteristic.Concentration);

        #endregion

        #region Washing

        public static fsParameterIdentifier LiquidWashOutMassFraction =
            new fsParameterIdentifier("Cwm", "Liquid Wash Out Mass Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier LiquidWashOutConcentration =
            new fsParameterIdentifier("Cw", "Liquid Wash Out Concentration", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier CakeWashOutContentX0p =
            new fsParameterIdentifier("X0p", "Cake Wash Out Content Before Washing", fsCharacteristic.CakeWashOutContent);

        public static fsParameterIdentifier CakeWashOutConcentration =
            new fsParameterIdentifier("C0p", "Cake Wash Out Concentration Before Washing", fsCharacteristic.CakeWashOutConcentration);

        public static fsParameterIdentifier RemanentWashOutContent =
            new fsParameterIdentifier("xr", "Specific Wash Out Remanent Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier WashIndexFor0 =
            new fsParameterIdentifier("Dn0", "Wash Index for u0, hc0", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier WashIndexFor =
            new fsParameterIdentifier("Dn", "Wash Index for u, hc", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier AdaptationPar1 =
            new fsParameterIdentifier("aw1", "Adaptation Parameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier AdaptationPar2 =
            new fsParameterIdentifier("aw2", "Adaptation Parameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier WashingRatioW =
            new fsParameterIdentifier("w", "Washing Ratio", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier WashingRatioWv =
            new fsParameterIdentifier("wv", "Washing Ratio", fsCharacteristic.WashRatioWv);

        public static fsParameterIdentifier WashingRatioWm =
            new fsParameterIdentifier("wm", "Washing Ratio", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier WashLiquidVolume =
            new fsParameterIdentifier("Vw", "Wash Liquid Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier WashLiquidMass =
            new fsParameterIdentifier("Mw", "Wash Liquid Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier NumberOfWashingSegments =
            new fsParameterIdentifier("nsw", "Number of segments for washing", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier SpecificWashArea =
            new fsParameterIdentifier("sw", "Specific Wash Area", fsCharacteristic.Concentration);

        public static fsParameterIdentifier WashTime =
            new fsParameterIdentifier("tw", "Wash Time", fsCharacteristic.Time);

        public static fsParameterIdentifier WashLiquidVolFlowRate =
            new fsParameterIdentifier("Qw", "Wash Liquid Volume Flow Rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier WashLiquidMassFlowRate =
            new fsParameterIdentifier("Qmw", "Wash Liquid Mass Flow Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier SpecificWashOutConcentration =
            new fsParameterIdentifier("c*", "Specific Wash Out Concentration of Wash Filtrate", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SpecificAverageWashOut =
            new fsParameterIdentifier("ca*", "Specific Average Wash Out Concentration Of Washfiltrate", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SpecificWashOutConcentrationInCake =
            new fsParameterIdentifier("cc*", "Specific Wash Out Concentration In Cake", fsCharacteristic.Concentration);

        public static fsParameterIdentifier WashOutConcentrationInWashfiltrate =
            new fsParameterIdentifier("c", "Wash Out Concentration In Wash Filtrate", fsCharacteristic.CakeWashOutConcentration);

        public static fsParameterIdentifier AverageWashOutConcentration =
            new fsParameterIdentifier("ca", "Average Wash Out Concentration In Wash Filtrate", fsCharacteristic.CakeWashOutConcentration);

        public static fsParameterIdentifier WashOutConcentrationInCake =
            new fsParameterIdentifier("cc", "Wash Out Concentration In The Cake", fsCharacteristic.CakeWashOutConcentration);

        public static fsParameterIdentifier SpecificWashOutXStar =
            new fsParameterIdentifier("x*", "Specific Wash Out Content In The Cake", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SpecificWashOutX =
            new fsParameterIdentifier("x", "Specific Wash Out Content In The Cake", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWashOutContent =
            new fsParameterIdentifier("X", "Cake Wash Out Content", fsCharacteristic.CakeWashOutContent);

        public static fsParameterIdentifier SpecificWashfiltrate =
            new fsParameterIdentifier("wf", "Specific Wash Filtrate", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier VolumeOfWashfiltrate =
            new fsParameterIdentifier("Vwf", "VolumeOfWashfiltrate", fsCharacteristic.Volume);

        public static fsParameterIdentifier MassOfWashfiltrate =
            new fsParameterIdentifier("Mwf", "MassOfWashfiltrate", fsCharacteristic.Mass);

        public static fsParameterIdentifier CakeVolume =
            new fsParameterIdentifier("Vc", "Cake Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier LiquidVolumeInCake =
            new fsParameterIdentifier("Vlc", "Liquid Volume In The Cake", fsCharacteristic.Volume);

        public static fsParameterIdentifier LiquidMassInCake =
            new fsParameterIdentifier("Mlc", "Liquid Mass In The Cake", fsCharacteristic.Mass);

        #endregion

        public static fsParameterIdentifier CakeSaturation =
            new fsParameterIdentifier("S", "Cake Saturation", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeSaturationSw0 =
            new fsParameterIdentifier("Sw0", "Cake Saturation When Starting Cake Washing", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeResidualSaturationSr =
            new fsParameterIdentifier("Sr", "Residual Cake Saturation", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContent =
            new fsParameterIdentifier("RF", "Cake Moisture Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRf0 =
            new fsParameterIdentifier("Rf0", "Cake Moisture Content for Dp0 = 1 bar", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRfw0 =
            new fsParameterIdentifier("Rfw0", "Cake Moisture Content When Starting Cake Washing", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRfw =
            new fsParameterIdentifier("Rfw", "Cake Moisture Content After Cake Washing", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRf =
            new fsParameterIdentifier("Rf", "Cake Moisture Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeSolidsContentCmc =
            new fsParameterIdentifier("Cmc", "Cake Solids Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWetMassSolidsFractionRs0 =
            new fsParameterIdentifier("Rs0", "Cake wet mass solids fraction 0", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWetMassSolidsFractionRs =
            new fsParameterIdentifier("Rs", "Cake wet mass solids fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier LiquidMassForResuspension =
            new fsParameterIdentifier("Ml", "Liquid Mass for Resuspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier Ph =
            new fsParameterIdentifier("pH", "pH of liquid after resuspension", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier PHcake =
            new fsParameterIdentifier("pH_c", "pH of liquid in the cake", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier MeanHeightRate =
            new fsParameterIdentifier("hc/tf", "Mean height rate", fsCharacteristic.Speed);

        public static fsParameterIdentifier HcOverTc =
            new fsParameterIdentifier("hc/tc", "Height rate rel to tc", fsCharacteristic.Speed);

        public static fsParameterIdentifier DiffHeightRate =
            new fsParameterIdentifier("dhc/dt", "Diff. height rate", fsCharacteristic.Speed);

        public static fsParameterIdentifier xg =
            new fsParameterIdentifier("xg", "Median particle size of the feed", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier sigma_g =
            new fsParameterIdentifier("sigma_g", "Standard deviation of the feed particle size distribution", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier sigma_s =
            new fsParameterIdentifier("sigma_s", "Sdandard deviation of the Grade efficiency curve", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier rf =
            new fsParameterIdentifier("rf", "Underflow volume rate to feed volume flow rate", fsCharacteristic.Concentration);

        public static fsParameterIdentifier UnderflowSolidsMassFraction =
            new fsParameterIdentifier("cmu", "Underflow solids mass fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier DuOverD =
            new fsParameterIdentifier("Du/D", "Underflow diameter to cyclone diameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ReducedCutSize =
            new fsParameterIdentifier("x’50", "Reduced cut size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowParticleSize =
            new fsParameterIdentifier("x0i", "Overflow particle size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowSolidsMassFraction =
            new fsParameterIdentifier("cmo", "Overflow solids mass fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier NumberOfCyclones =
            new fsParameterIdentifier("n", "Number of Cyclones", fsCharacteristic.NoUnits);

        #region Flow Rates

        public static fsParameterIdentifier FeedVolumeFlowRate  =
            new fsParameterIdentifier("Q", "Volume Flow Rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier CycleFrequency
            = new fsParameterIdentifier("n", "Cycle Frequency", fsCharacteristic.Frequency);

        public static fsParameterIdentifier FeedSolidsMassFlowRate  =
            new fsParameterIdentifier("Qm", "Mass Flow Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier AverageVolumeFlowRate =
           new fsParameterIdentifier("Qa", "Average Volume Flow Rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier AverageMassFlowRate =
            new fsParameterIdentifier("Qma", "Average Mass Flow Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier SpecificVolumeFlowRate =
            new fsParameterIdentifier("q", "Specific Volume Flow Rate", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier SpecificMassFlowRate =
            new fsParameterIdentifier("qm", "Specific Mass Flow Rate", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier SpecificAverageVolumeFlowRate =
           new fsParameterIdentifier("qa", "Specific Average Volume Flow Rate", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier SpecificAverageMassFlowRate =
            new fsParameterIdentifier("qma", "Specific Average Mass Flow Rate", fsCharacteristic.SpecificMassFlowrate);

        #endregion

        #region Alphas, Betas, Gammas

        public static fsParameterIdentifier Alpha1 =
            new fsParameterIdentifier("alpha1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Alpha2 =
            new fsParameterIdentifier("alpha2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Alpha3 =
            new fsParameterIdentifier("alpha3", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta1 =
            new fsParameterIdentifier("Beta1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta2 =
            new fsParameterIdentifier("Beta2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta3 =
            new fsParameterIdentifier("Beta3", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma1 =
            new fsParameterIdentifier("Gamma1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma2 =
            new fsParameterIdentifier("Gamma2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma3 =
            new fsParameterIdentifier("Gamma3", fsCharacteristic.NoUnits);

        #endregion

        public static fsParameterIdentifier bigLOverD =
            new fsParameterIdentifier("L/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier smallLOverD =
            new fsParameterIdentifier("l/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier DiOverD =
            new fsParameterIdentifier("Di/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier DoOverD =
            new fsParameterIdentifier("Do/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier StokesNumber =
            new fsParameterIdentifier("Stk’", "Stokes number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier EulerNumber =
            new fsParameterIdentifier("Eu", "Euler number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ReynoldsNumber =
            new fsParameterIdentifier("Re", "Reynolds number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier AverageVelocity =
            new fsParameterIdentifier("v", "Average velocity", fsCharacteristic.Speed);

        public static fsParameterIdentifier TotalEfficiency =
            new fsParameterIdentifier("ET", "Total efficiency", fsCharacteristic.Concentration);

        public static fsParameterIdentifier ReducedTotalEfficiency =
            new fsParameterIdentifier("E’T", "Reduced total efficiency", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CycloneLength =
            new fsParameterIdentifier("L", "Cyclone length", fsCharacteristic.Length);

        public static fsParameterIdentifier LengthOfCylindricalPart =
            new fsParameterIdentifier("l", "Length of cylindrical part", fsCharacteristic.Length);

        public static fsParameterIdentifier InletDiameter =
            new fsParameterIdentifier("Di", "Inlet diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier OutletDiameter =
            new fsParameterIdentifier("Do", "Outlet diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier UnderflowDiameter =
            new fsParameterIdentifier("Du", "Underflow diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier OverflowVolumeFlowRate =
            new fsParameterIdentifier("Qo", "Overflow volume flow rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier OverflowMassFlowRate =
            new fsParameterIdentifier("Qmo", "Overflow mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier OverflowSolidsMassFlowRate =
            new fsParameterIdentifier("Qmso", "Overflow solids mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier UnderflowSolidsMassFlowRate =
            new fsParameterIdentifier("Qmsu", "Underflow solids mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier UnderflowVolumeFlowRate =
            new fsParameterIdentifier("Qu", "Underflow volume flow rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier UnderflowMassFlowRate =
            new fsParameterIdentifier("Qmu", "Underflow mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier PredeliquorFlowRate =
            new fsParameterIdentifier("fq", "Predeliquoring flow rate", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier OverflowMeanParticleSize =
            new fsParameterIdentifier("x50,o", "Overflow mean particle size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier UnderflowMeanParticleSize =
            new fsParameterIdentifier("x50,u", "Underflow mean particle size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowSolidsVolumeFraction =
            new fsParameterIdentifier("cvo", fsCharacteristic.Concentration);

        public static fsParameterIdentifier UnderflowSolidsVolumeFraction =
            new fsParameterIdentifier("cvu", fsCharacteristic.Concentration);

        public static fsParameterIdentifier OverflowSolidsConcentration =
            new fsParameterIdentifier("Co", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier UnderflowSolidsConcentration =
            new fsParameterIdentifier("Cu", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier Theta =
            new fsParameterIdentifier(Convert.ToChar(0x3B8).ToString(), fsCharacteristic.Temperature);

        public static fsParameterIdentifier Ad1 =
           new fsParameterIdentifier("ad1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Ad2 = 
            new fsParameterIdentifier("ad2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Ag1 =
            new fsParameterIdentifier("ag1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Ag2 =
            new fsParameterIdentifier("ag2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Ag3 =
            new fsParameterIdentifier("ag3", fsCharacteristic.NoUnits);

        #endregion

        #region Gas

        public static fsParameterIdentifier Qgi =
            new fsParameterIdentifier("Qgi", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qgt =
            new fsParameterIdentifier("Qgt", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qg =
            new fsParameterIdentifier("Qg", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier GasVolume =
            new fsParameterIdentifier("Vg", fsCharacteristic.Volume);

        public static fsParameterIdentifier SpecificGasVolume =
            new fsParameterIdentifier("vg", fsCharacteristic.SpecificVolume);

        public static fsParameterIdentifier SpecificGasConsumption =
            new fsParameterIdentifier("vg", fsCharacteristic.WashRatioWv);

        public static fsParameterIdentifier qgi =
            new fsParameterIdentifier("qgi", fsCharacteristic.Speed);

        public static fsParameterIdentifier qgt =
            new fsParameterIdentifier("qgt", fsCharacteristic.Speed);

        public static fsParameterIdentifier qg =
            new fsParameterIdentifier("qg", fsCharacteristic.Speed);

        #endregion
    }
}

