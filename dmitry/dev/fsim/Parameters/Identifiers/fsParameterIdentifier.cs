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

        #region Material Parameters

        public static fsParameterIdentifier CakeHeight =
            new fsParameterIdentifier("hc", "Cake Height", fsCharacteristic.CakeHeight);

        #region Cake Porosities (epsilon, epsilon0)

        public static fsParameterIdentifier CakePorosity =
            new fsParameterIdentifier("\u03B5", "Cake Porosity", fsCharacteristic.Concentration); // (unicode epsilon) previously eps

        public static fsParameterIdentifier CakePorosity0 =
            new fsParameterIdentifier("\u03B5" + "0", "Cake Porosity for " + "\u0394" + "p0 = 1 bar", fsCharacteristic.Concentration); // previously eps0 , ""Cake Porosity for Dp0 = 1 bar""

        #endregion

        #region Cake Permeabilities (Pc, Pc0, K)

        public static fsParameterIdentifier CakePermeability =
            new fsParameterIdentifier("Pc", "Cake Permeability", fsCharacteristic.CakePermeability);

        public static fsParameterIdentifier CakePermeability0 =
            new fsParameterIdentifier("Pc0", "Cake Permeability for " + "\u0394" + "p0 = 1 bar", fsCharacteristic.CakePermeability); // previously "Cake Permeability for Dp0 = 1 bar"

        public static fsParameterIdentifier PracticalCakePermeability =
            new fsParameterIdentifier("K", "Practical Cake Permeability", fsCharacteristic.PracticalCakePermeability);

        #endregion            
        
        #region Masses (Cake, Suspension, Filtrate, Solids)

        #region Cake (Mc, Mcw, Mcd)

        public static fsParameterIdentifier CakeMass =
            new fsParameterIdentifier("Mc", "Cake Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier WetCakeMass =
            new fsParameterIdentifier("Mcw", "Wet Cake Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier DryCakeMass =
            new fsParameterIdentifier("Mcd", "Dry Cake Mass", fsCharacteristic.Mass);
 
        #endregion

        #region Suspension (Msus, msus, Ml, Ms)

        public static fsParameterIdentifier SuspensionMass =
            new fsParameterIdentifier("Msus", "Suspension Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SpecificSuspensionMass =
            new fsParameterIdentifier("msus", "Specific Suspension Mass", fsCharacteristic.SpecificMass);

        public static fsParameterIdentifier LiquidMassInSuspension =
            new fsParameterIdentifier("Ml", "Liquid Mass in Suspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier SolidsMassInSuspension =
            new fsParameterIdentifier("Ms", "Solids Mass in Suspension", fsCharacteristic.Mass);
 
        #endregion

        public static fsParameterIdentifier FiltrateMass =
            new fsParameterIdentifier("Mf", "Fitrate Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SolidsMass =
            new fsParameterIdentifier("Ms", "Solids Mass", fsCharacteristic.Mass);

        #endregion

        #region Volumes

        public static fsParameterIdentifier SolidsVolume =
            new fsParameterIdentifier("Vs", "Solids Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SuspensionVolume =
            new fsParameterIdentifier("Vsus", "Suspension Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SpecificSuspensionVolume =
            new fsParameterIdentifier("vsus", "Specific Suspension volume", fsCharacteristic.SpecificVolume);

        public static fsParameterIdentifier FiltrateVolume =
            new fsParameterIdentifier("Vf", "Fitrate Volume", fsCharacteristic.Volume);

        #endregion



        #region Liquid Viscosities (eta, etaw)

        public static fsParameterIdentifier MotherLiquidViscosity =
            new fsParameterIdentifier("\u03B7", "Mother Liquid Viscosity", fsCharacteristic.Viscosity); // previously eta

        public static fsParameterIdentifier WashLiquidViscosity =
            new fsParameterIdentifier("\u03B7" + "w", "Wash Liquid Viscosity", fsCharacteristic.Viscosity); // previously eta_w

        #endregion

        #region Densities

        public static fsParameterIdentifier DryCakeDensity0 =
            new fsParameterIdentifier("\u03C1" + "cd0", "Dry Cake Density for  " + "\u0394" + "p0 = 1 bar", fsCharacteristic.Density); // previously rho_cd0, "Dry cake density for Dp0 = 1 bar"

        public static fsParameterIdentifier DryCakeDensity =
            new fsParameterIdentifier("\u03C1" + "cd", "Dry Cake Density", fsCharacteristic.Density); // previously rho_cd

        public static fsParameterIdentifier CakeWetDensity0 =
            new fsParameterIdentifier("\u03C1" + "cw0", "Cake Wet Density 0", fsCharacteristic.Density); // previously rho_cw0

        public static fsParameterIdentifier CakeWetDensity =
            new fsParameterIdentifier("\u03C1" + "cw", "Cake Wet Density", fsCharacteristic.Density); // previously rho_cw

        public static fsParameterIdentifier MotherLiquidDensity =
            new fsParameterIdentifier("\u03C1", "Density of Mother Liquid", fsCharacteristic.Density); // previously rho

        public static fsParameterIdentifier LiquidDensity =
            new fsParameterIdentifier("\u03C1" + "l", "Liquid Density", fsCharacteristic.Density); // previously rho_l

        public static fsParameterIdentifier SolidsDensity =
            new fsParameterIdentifier("\u03C1" + "s", "Solids Density", fsCharacteristic.Density); // previously rho_s 

        public static fsParameterIdentifier SuspensionDensity =
            new fsParameterIdentifier("\u03C1" + "sus", "Suspension Density", fsCharacteristic.Density); // previously rho_sus

        public static fsParameterIdentifier WashLiquidDensity =
            new fsParameterIdentifier("rho_w", "Wash Liquid Density", fsCharacteristic.Density);

        #endregion;

        #region Resistances

        public static fsParameterIdentifier CakeResistanceAlpha0 =
            new fsParameterIdentifier("\u03B1" + "0", "Cake Resistance for " + "\u0394" + "p0 = 1 bar", fsCharacteristic.CakeResistanceAlpha); //  previously alpha0, "Cake Resistance for Dp0 = 1 bar" 

        public static fsParameterIdentifier CakeResistanceAlpha =
            new fsParameterIdentifier("\u03B1", "Cake Resistance", fsCharacteristic.CakeResistanceAlpha); //  previously alpha

        public static fsParameterIdentifier FilterMediumResistanceHce0 =
            new fsParameterIdentifier("hce0", "Filter Medium Resistance hce0", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier FilterMediumResistanceHce =
            new fsParameterIdentifier("hce", "Specific Filter Medium Resistance hce", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier FilterMediumResistanceRm0 =
            new fsParameterIdentifier("Rm0", fsCharacteristic.FilterMediumResistance);

        public static fsParameterIdentifier FilterMediumResistanceRm =
           new fsParameterIdentifier("Rm", "Filter Medium Resistance Rm", fsCharacteristic.FilterMediumResistance);

        public static fsParameterIdentifier CakeResistance0 =
            new fsParameterIdentifier("rc0", "Cake Resistance for  " + "\u0394" + "p0 = 1 bar", fsCharacteristic.CakeResistance); // previously "Cake Resistance for Dp0 = 1 bar" 

        public static fsParameterIdentifier CakePlusMediumPermeability =
           new fsParameterIdentifier("Pc*", "'Cake + Medium' Permeability", fsCharacteristic.CakePermeability);

        public static fsParameterIdentifier CakePlusMediumResistance =
            new fsParameterIdentifier("rc*", "'Cake + Medium' Resistance", fsCharacteristic.CakeResistance);

        public static fsParameterIdentifier CakePlusMediumResistanceAlpha =
            new fsParameterIdentifier("alpha*", "'Cake + Medium' Resistance", fsCharacteristic.CakeResistanceAlpha);

        public static fsParameterIdentifier CakeResistance =
            new fsParameterIdentifier("rc", "Cake Resistance", fsCharacteristic.CakeResistance);

        #endregion

        #region Solutes (C_sol, Cm_sol)

        public static fsParameterIdentifier SolutesConcentrationInMotherLiquid =
            new fsParameterIdentifier("C_sol", "Solutes Concentration in Mother Liquid", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier SolutesMassFractionInMotherLiquid =
            new fsParameterIdentifier("Cm_sol", "Solutes Mass Fraction in Mother Liquid", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SolutesConcentrationInCakeLiquid =
            new fsParameterIdentifier("C_sol", "Solutes Concentration in Cake Liquid", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier SolutesMassFractionInLiquid =
            new fsParameterIdentifier("Cm_sol", "Solutes Mass Fraction in Liquid", fsCharacteristic.Concentration);

        #endregion
        
        #endregion

        #region Machine Geometry Parameters (Numbers, Areas, Lengths, Ratios)

        #region Numbers (ns, nsf, nsr; n)

        public static fsParameterIdentifier ns =
            new fsParameterIdentifier("ns", "Number of Segments", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier nsf =
            new fsParameterIdentifier("nsf", "Segment Number for Cake Formation", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier nsr =
            new fsParameterIdentifier("nsr", "Segment Number for Residual Steps", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier NumberOfCyclones =
            new fsParameterIdentifier("n", "Number of Cyclones", fsCharacteristic.NoUnits); 
        
        #endregion

        #region Areas (A, As)

        public static fsParameterIdentifier FilterArea =
            new fsParameterIdentifier("A", "Filter Area", fsCharacteristic.Area);

        public static fsParameterIdentifier As = new fsParameterIdentifier("As", fsCharacteristic.Area); 

        #endregion

        #region Lengths (l, ls, b, d; D)

        public static fsParameterIdentifier FilterLength =
            new fsParameterIdentifier("l", "Filter Length", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier ls =
            new fsParameterIdentifier("ls", "Length of Segments", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier MachineWidth =
            new fsParameterIdentifier("b", "Machine Width", fsCharacteristic.MachineGeometryLength); 

        public static fsParameterIdentifier FilterElementDiameter =
            new fsParameterIdentifier("d", "Filter Element Diameter", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier MachineDiameter =
            new fsParameterIdentifier("D", "Machine Diameter", fsCharacteristic.MachineGeometryLength);

        #endregion

        #region Ratios (ls/b, l/b, b/D; L/D, l/D, Di/D, Do/D)

        public static fsParameterIdentifier ls_over_b =
            new fsParameterIdentifier("ls/b", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier l_over_b =
            new fsParameterIdentifier("l/b", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier WidthOverDiameterRatio =
            new fsParameterIdentifier("b/D", "Width to Diameter Ratio", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier bigLOverD =
            new fsParameterIdentifier("L/D", "Cyclon Length to Diameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier smallLOverD =
            new fsParameterIdentifier("l/D", "Cylindrical Part Length to Diameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier DiOverD =
            new fsParameterIdentifier("Di/D", "Inlet Diameter to Diameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier DoOverD =
            new fsParameterIdentifier("Do/D", "Outlet Diameter to Diameter", fsCharacteristic.NoUnits);
 
        #endregion
        
        #endregion

        #region Adaptation Parameters

        #region Hydrocyclone (alphas, betas, gammas)

        public static fsParameterIdentifier Alpha1 =
            new fsParameterIdentifier("alpha1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Alpha2 =
            new fsParameterIdentifier("alpha2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Alpha3 =
            new fsParameterIdentifier("alpha3", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta1 =
            new fsParameterIdentifier("beta1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta2 =
            new fsParameterIdentifier("beta2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta3 =
            new fsParameterIdentifier("beta3", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma1 =
            new fsParameterIdentifier("gamma1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma2 =
            new fsParameterIdentifier("gamma2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma3 =
            new fsParameterIdentifier("gamma3", fsCharacteristic.NoUnits); 

        #endregion

        #region Cake Washing (aw1, aw2)

        public static fsParameterIdentifier AdaptationPar1 =
            new fsParameterIdentifier("aw1", "Adaptation Parameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier AdaptationPar2 =
            new fsParameterIdentifier("aw2", "Adaptation Parameter", fsCharacteristic.NoUnits);
 
        #endregion

        #endregion

        #region Machine Functionality Parameters

        public static fsParameterIdentifier PressureDifference =
            new fsParameterIdentifier("\u0394" + "p", "Pressure Difference", fsCharacteristic.Pressure); // (Delta p) previously Dp

        public static fsParameterIdentifier RotationalSpeed =
            new fsParameterIdentifier("n", "Cycle Frequency", fsCharacteristic.Frequency);

        #region Times

        public static fsParameterIdentifier StandardTechnicalTime =
            new fsParameterIdentifier("ttech0", "Standard Technical Time", fsCharacteristic.Time);

        public static fsParameterIdentifier TechnicalTime =
            new fsParameterIdentifier("ttech", "Technical Time", fsCharacteristic.Time);

        public static fsParameterIdentifier CycleTime =
            new fsParameterIdentifier("tc", "Cycle Time", fsCharacteristic.Time);

        public static fsParameterIdentifier SpecificFiltrationTime =
            new fsParameterIdentifier("sf", "Specific Filtration Time", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SpecificResidualTime =
            new fsParameterIdentifier("sr", "Specific Residual Time", fsCharacteristic.Concentration);

        public static fsParameterIdentifier FiltrationTime =
            new fsParameterIdentifier("tf", "Filtration Time", fsCharacteristic.Time);

        public static fsParameterIdentifier ResidualTime =
            new fsParameterIdentifier("tr", "Residual Time", fsCharacteristic.Time);

        #endregion

        #endregion

        public static fsParameterIdentifier Kappa = new fsParameterIdentifier("\u03F0"); // previously kappa

        public static fsParameterIdentifier Kappa0 = new fsParameterIdentifier("\u03F0" + "0"); // previously kappa0

        public static fsParameterIdentifier SuspensionMassFlowrate =
            new fsParameterIdentifier("Qmsus", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qms =
            new fsParameterIdentifier("Qms", "Solids Mass Throughput", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qsus =
            new fsParameterIdentifier("Qsus", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qs =
            new fsParameterIdentifier("Qs", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qmsust =
            new fsParameterIdentifier("Qmsus,t", "Suspension Mass Rate relatively to t", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qmsusd =
            new fsParameterIdentifier("Qmsus,d", "Differential Suspension Mass Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qsust =
            new fsParameterIdentifier("Qsus,t", "Suspension Volume Rate relatively to t", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qsusd =
            new fsParameterIdentifier("Qsus,d", "Differential Suspension Volume Rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier u =
            new fsParameterIdentifier("u", "Belt Speed", fsCharacteristic.Speed);

        public static fsParameterIdentifier lambda =
            new fsParameterIdentifier("\u03BB", fsCharacteristic.NoUnits); // previously lambda

        public static fsParameterIdentifier qsust =
            new fsParameterIdentifier("qsus,t", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qsusd =
            new fsParameterIdentifier("qsus,d", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmsust =
            new fsParameterIdentifier("qmsus,t", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qmsusd =
            new fsParameterIdentifier("qmsus,d", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qft =
            new fsParameterIdentifier("qft", "Specific Filtrate Volume relatively to Time", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmft =
            new fsParameterIdentifier("qmft", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qf =
            new fsParameterIdentifier("qf", "Specific Filtrate Volume relatively to Time", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmf =
            new fsParameterIdentifier("qmf", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier SurfaceTensionLiquidInCake = 
            new fsParameterIdentifier("\u03C3", "Surface Tension Liquid in Cake", fsCharacteristic.SurfaceTension); // previously sigma

        public static fsParameterIdentifier StandardCapillaryPressure =
            new fsParameterIdentifier("pke_st", "Standard Capillary Pressure", fsCharacteristic.Pressure);

        public static fsParameterIdentifier CapillaryPressure =
            new fsParameterIdentifier("pke", "Capillary Pressure", fsCharacteristic.Pressure);

        public static fsParameterIdentifier SuspensionSolidsMassFraction =
            new fsParameterIdentifier("cm", "Suspension Solids Mass Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SuspensionSolidsVolumeFraction =
            new fsParameterIdentifier("cv", "Suspension Solids Volume Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier SuspensionSolidsConcentration =
            new fsParameterIdentifier("C", "Suspension Solids Concentration", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier Ne =
            new fsParameterIdentifier("ne", "Cake Volume Reduction Factor", fsCharacteristic.NoUnits);        

        public static fsParameterIdentifier CakeCompressibility =
            new fsParameterIdentifier("nc", "Cake Compressibility");

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
            new fsParameterIdentifier("Vlc", "Liquid Volume in the Cake", fsCharacteristic.Volume);

        public static fsParameterIdentifier LiquidMassInCake =
            new fsParameterIdentifier("Mlc", "Liquid Mass in the Cake", fsCharacteristic.Mass);

        #endregion

        public static fsParameterIdentifier CakeSaturation =
            new fsParameterIdentifier("S", "Cake Saturation", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeSaturationSw0 =
            new fsParameterIdentifier("Sw0", "Cake Saturation When Starting Cake Washing", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContent =
            new fsParameterIdentifier("RF", "Cake Moisture Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRf0 =
            new fsParameterIdentifier("Rf0", "Cake Moisture Content for " + "\u0394" + "p0 = 1 bar", fsCharacteristic.Concentration); // previously "Cake Moisture Content for Dp0 = 1 bar"

        public static fsParameterIdentifier CakeMoistureContentRfw0 =
            new fsParameterIdentifier("Rfw0", "Cake Moisture Content When Starting Cake Washing", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRfw =
            new fsParameterIdentifier("Rfw", "Cake Moisture Content After Cake Washing", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRf =
            new fsParameterIdentifier("Rf", "Cake Moisture Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWetMassSolidsFractionRs0 =
            new fsParameterIdentifier("Rs0", "Cake Wet Mass Solids Fraction 0", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWetMassSolidsFractionRs =
            new fsParameterIdentifier("Rs", "Cake Wet Mass Solids Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier LiquidMassForResuspension =
            new fsParameterIdentifier("Ml", "Liquid Mass for Resuspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier Ph =
            new fsParameterIdentifier("pH", "pH of Liquid after Resuspension", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier PHcake =
            new fsParameterIdentifier("pH_c", "pH of Liquid in the Cake", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier MeanHeightRate =
            new fsParameterIdentifier("hc/tf", "Mean Height Rate", fsCharacteristic.Speed);

        public static fsParameterIdentifier HcOverTc =
            new fsParameterIdentifier("hc/tc", "Height Rate relatively to tc", fsCharacteristic.Speed);

        public static fsParameterIdentifier DiffHeightRate =
            new fsParameterIdentifier("dhc/dt", "Differrential Height Rate", fsCharacteristic.Speed);

        public static fsParameterIdentifier xg =
            new fsParameterIdentifier("xg", "Median Particle Size of the Feed", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier sigma_g =
            new fsParameterIdentifier("\u03C3" + "g", "Standard Deviation of the Feed Particle Size Distribution", fsCharacteristic.NoUnits); // previously sigma_g 

        public static fsParameterIdentifier sigma_s =
            new fsParameterIdentifier("\u03C3" + "s", "Standard Deviation of the Grade Efficiency Curve", fsCharacteristic.NoUnits); // previously sigma_s

        public static fsParameterIdentifier rf =
            new fsParameterIdentifier("rf", "Underflow Volume Rate to Feed Volume Flow Rate", fsCharacteristic.Concentration);

        public static fsParameterIdentifier UnderflowSolidsMassFraction =
            new fsParameterIdentifier("cmu", "Underflow Solids Mass Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier DuOverD =
            new fsParameterIdentifier("Du/D", "Underflow Diameter to Cyclone Diameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ReducedCutSize =
            new fsParameterIdentifier("x’50", "Reduced Cut Size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowParticleSize =
            new fsParameterIdentifier("xio", "Overflow Percentage Particle Size", fsCharacteristic.ParticleSize); // previously xoi

        public static fsParameterIdentifier UnderflowParticleSize =
            new fsParameterIdentifier("xiu", "Underflow Percentage Particle Size", fsCharacteristic.ParticleSize); // previously xui

        public static fsParameterIdentifier OverflowSolidsMassFraction =
            new fsParameterIdentifier("cmo", "Overflow Solids Mass Fraction", fsCharacteristic.Concentration);

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
       
        public static fsParameterIdentifier StokesNumber =
            new fsParameterIdentifier("Stk’", "Stokes Number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier EulerNumber =
            new fsParameterIdentifier("Eu", "Euler Number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ReynoldsNumber =
            new fsParameterIdentifier("Re", "Reynolds Number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier AverageVelocity =
            new fsParameterIdentifier("v", "Average Velocity", fsCharacteristic.Speed);

        public static fsParameterIdentifier TotalEfficiency =
            new fsParameterIdentifier("ET", "Total Efficiency", fsCharacteristic.Concentration);

        public static fsParameterIdentifier ReducedTotalEfficiency =
            new fsParameterIdentifier("E’T", "Reduced Total Efficiency", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CycloneLength =
            new fsParameterIdentifier("L", "Cyclone Length", fsCharacteristic.Length);

        public static fsParameterIdentifier LengthOfCylindricalPart =
            new fsParameterIdentifier("l", "Length of Cylindrical Part", fsCharacteristic.Length);

        public static fsParameterIdentifier InletDiameter =
            new fsParameterIdentifier("Di", "Inlet Diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier OutletDiameter =
            new fsParameterIdentifier("Do", "Outlet Diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier UnderflowDiameter =
            new fsParameterIdentifier("Du", "Underflow Diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier OverflowVolumeFlowRate =
            new fsParameterIdentifier("Qo", "Overflow Volume Flow Rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier OverflowMassFlowRate =
            new fsParameterIdentifier("Qmo", "Overflow Mass Flow Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier OverflowSolidsMassFlowRate =
            new fsParameterIdentifier("Qmso", "Overflow Solids Mass Flow Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier UnderflowSolidsMassFlowRate =
            new fsParameterIdentifier("Qmsu", "Underflow Solids Mass Flow Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier UnderflowVolumeFlowRate =
            new fsParameterIdentifier("Qu", "Underflow Volume Flow Rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier UnderflowMassFlowRate =
            new fsParameterIdentifier("Qmu", "Underflow Mass Flow Rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier PredeliquorFlowRate =
            new fsParameterIdentifier("fq", "Predeliquoring Flow Rate", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier OverflowMeanParticleSize =
            new fsParameterIdentifier("xo50", "Overflow Mean Particle Size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier UnderflowMeanParticleSize =
            new fsParameterIdentifier("xu50", "Underflow Mean Particle Size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowSolidsVolumeFraction =
            new fsParameterIdentifier("cvo", "Overflow Solids Volume Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier UnderflowSolidsVolumeFraction =
            new fsParameterIdentifier("cvu", "Underflow Solids Volume Fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier OverflowSolidsConcentration =
            new fsParameterIdentifier("Co", "Overflow Solids Concentration", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier UnderflowSolidsConcentration =
            new fsParameterIdentifier("Cu", "Underflow Solids Concentration", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier PercentageOfParticles =
            new fsParameterIdentifier("i", "Percentage of Particles", fsCharacteristic.Concentration);

        #endregion
    }
}

