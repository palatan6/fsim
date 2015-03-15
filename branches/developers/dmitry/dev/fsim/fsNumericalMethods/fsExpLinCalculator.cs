﻿using System;

namespace ExpLinCalculator
{
    public class fsExpLinCalculator
    {
        /*
         * This class was automatically generated from the Maple module using CodeGeneration[CSharp] procedure
         * and then slightly tuned.
         * What's happening here in details is a terrible mystery! :-)
         * See the attached Maple doc (if I'll have a time and a possibility to attach it!).
         * Also see the comments in fsSpecialFunctions.cs
         * ---Tyshkevich Dmitry---.
         */
        private static double eps = 0.1e-23;

        public static double ExpLinPosInfinity(double x)
        {
            return ExpLinPosCommon(x, 0.10e1, (0.1e1 - x + Math.Sqrt(Math.Pow(0.1e1 - x, 0.2e1) - 0.2e1 * x * x)) / x, nNewtSetInfinity(x));
        }

        public static double ExpLinPosInfinitySpline(double x)
        {
            double y;
            y = Math.Log(x);
            if (y < -0.195e2)
                return -0.525859247031295e1 + (-0.216909108083721e1 + (-0.561809844136134e-1 - 0.93634974022689e-3 * y) * y) * y;
            else if (y < -0.190e2)
                return 0.31903494366753e1 + (-0.869253863853268e0 + (0.10477334406076e-1 + 0.203108701135322e-3 * y) * y) * y;
            else if (y < -0.185e2)
                return 0.106904068355781e1 + (-0.120419735183626e1 + (-0.715127022460788e-2 - 0.106165064315273e-3 * y) * y) * y;
            else if (y < -0.180e2)
                return 0.156769806230816e1 + (-0.112333399179884e1 + (-0.278027779015288e-2 - 0.274084438746418e-4 * y) * y) * y;
            else if (y < -0.175e2)
                return 0.141657510747842e1 + (-0.114852115205351e1 + (-0.417956447096746e-2 - 0.53321160186023e-4 * y) * y) * y;
            else if (y < -0.170e2)
                return 0.142565519862753e1 + (-0.114696456463894e1 + (-0.409061661870674e-2 - 0.516269153810569e-4 * y) * y) * y;
            else if (y < -0.165e2)
                return 0.139428931482168e1 + (-0.115249972058063e1 + (-0.441621402704136e-2 - 0.580111782895789e-4 * y) * y) * y;
            else if (y < -0.160e2)
                return 0.13720209816997e1 + (-0.115654850810314e1 + (-0.466159508901146e-2 - 0.629683714606918e-4 * y) * y) * y;
            else if (y < -0.155e2)
                return 0.134667917337835e1 + (-0.116130009676748e1 + (-0.495856938053303e-2 - 0.691553358673913e-4 * y) * y) * y;
            else if (y < -0.150e2)
                return 0.132115220013354e1 + (-0.116624080140521e1 + (-0.527732451845088e-2 - 0.760102850699258e-4 * y) * y) * y;
            else if (y < -0.145e2)
                return 0.129444502067011e1 + (-0.117158223758418e1 + (-0.56334202637156e-2 - 0.839235238535862e-4 * y) * y) * y;
            else if (y < -0.140e2)
                return 0.126721440837342e1 + (-0.117721615692279e1 + (-0.602196642499896e-2 - 0.928556195152726e-4 * y) * y) * y;
            else if (y < -0.135e2)
                return 0.123857149971152e1 + (-0.118335392352169e1 + (-0.646037832492005e-2 - 0.103293998084822e-3 * y) * y) * y;
            else if (y < -0.130e2)
                return 0.120935604537678e1 + (-0.118984624628734e1 + (-0.694129112237606e-2 - 0.115168388145465e-3 * y) * y) * y;
            else if (y < -0.125e2)
                return 0.117863307082275e1 + (-0.119693616531014e1 + (-0.748666950874535e-2 - 0.129152449334421e-3 * y) * y) * y;
            else if (y < -0.120e2)
                return 0.114670071637801e1 + (-0.120459993023914e1 + (-0.809977070306543e-2 - 0.14550181451629e-3 * y) * y) * y;
            else if (y < -0.115e2)
                return 0.111369853987634e1 + (-0.12128504727713e1 + (-0.87873159140783e-2 - 0.16460029259998e-3 * y) * y) * y;
            else if (y < -0.110e2)
                return 0.107881465719622e1 + (-0.122195061741697e1 + (-0.957863283978857e-2 - 0.187537015084336e-3 * y) * y) * y;
            else if (y < -0.105e2)
                return 0.10425653579375e1 + (-0.123183678882503e1 + (-0.104773756950675e-1 - 0.214771647062484e-3 * y) * y) * y;
            else if (y < -0.100e2)
                return 0.100435823666251e1 + (-0.124275310975638e1 + (-0.115170253075764e-1 - 0.247776396665941e-3 * y) * y) * y;
            else if (y < -0.95e1)
                return 0.964091868903173e0 + (-0.125483302063845e1 + (-0.12725016395784e-1 - 0.288042766272862e-3 * y) * y) * y;
            else if (y < -0.90e1)
                return 0.92155768771178e0 + (-0.126826486639949e1 + (-0.141388948969453e-1 - 0.337652538243433e-3 * y) * y) * y;
            else if (y < -0.85e1)
                return 0.876349084764516e0 + (-0.128333440022909e1 + (-0.158132875446795e-1 - 0.399667080752109e-3 * y) * y) * y;
            else if (y < -0.80e1)
                return 0.828194279629015e0 + (-0.130033021380009e1 + (-0.178127950236197e-1 - 0.478079138749763e-3 * y) * y) * y;
            else if (y < -0.75e1)
                return 0.776366966930654e0 + (-0.131976545709582e1 + (-0.202422004355862e-1 - 0.579304364248367e-3 * y) * y) * y;
            else if (y < -0.70e1)
                return 0.720878996435447e0 + (-0.134196064509719e1 + (-0.23201558835769e-1 - 0.710831404256491e-3 * y) * y) * y;
            else if (y < -0.65e1)
                return 0.659173772236072e0 + (-0.136840574142424e1 + (-0.269794297396335e-1 - 0.890730018726232e-3 * y) * y) * y;
            else if (y < -0.60e1)
                return 0.595397470077763e0 + (-0.139784095756693e1 + (-0.315079245308161e-1 - 0.112296052083816e-2 * y) * y) * y;
            else if (y < -0.55e1)
                return 0.512781060912434e0 + (-0.143914916229184e1 + (-0.383926253183015e-1 - 0.150544389792068e-2 * y) * y) * y;
            else if (y < -0.50e1)
                return 0.45379180784806e0 + (-0.147132511834434e1 + (-0.442427991460291e-1 - 0.185999988747993e-2 * y) * y) * y;
            else if (y < -0.45e1)
                return 0.289087225129328e0 + (-0.157014786819529e1 + (-0.640073491162196e-1 - 0.31776365521593e-2 * y) * y) * y;
            else if (y < -0.40e1)
                return 0.363106117103396e0 + (-0.152080194031249e1 + (-0.530415873644848e-1 - 0.236535790388264e-2 * y) * y) * y;
            else if (y < -0.35e1)
                return -0.26523807067338e0 + (-0.199206008087704e1 + (-0.170856122505622e0 - 0.121832358323108e-1 * y) * y) * y;
            else if (y < -0.30e1)
                return 0.783333212169294e0 + (-0.109328469572222e1 + (0.859368446814693e-1 + 0.122732372331265e-1 * y) * y) * y;
            else if (y < -0.25e1)
                return -0.234524271121229e1 + (-0.422186061872193e1 + (-0.956921796318435e0 - 0.103599945100196e0 * y) * y) * y;
            else if (y < -0.20e1)
                return 0.34800471682114e1 + (0.276848723630034e1 + (0.183921734569047e1 + 0.269218607167658e0 * y) * y) * y;
            else if (y < -0.15e1)
                return -0.927296840610412e1 + (-0.163610361245568e2 + (-0.772554433473809e1 - 0.132490833957044e1 * y) * y) * y;
            else
                return -0.833624944718535e0 + (0.517650797563738e0 + (0.352691361334226e1 + 0.117563787111409e1 * y) * y) * y;
        }

        public static double ExpLinPosZero(double x)
        {
            return ExpLinPosCommon(x, 0.0e0, 0.10e1, nNewtSetZero(x));
        }

        public static double ExpLinPosZeroSpline(double x)
        {
            double y = Math.Log(x);
            if (y < -0.195e2)
                return 0.103663015610262e-4 + (0.154993242954967e-5 + (0.77379030688477e-7 + 0.128965051147462e-8 * y) * y) * y;
            else if (y < -0.190e2)
                return 0.444487435385051e-5 + (0.638943628445656e-6 + (0.306616562728865e-7 + 0.491062914626916e-9 * y) * y) * y;
            else if (y < -0.185e2)
                return 0.963519326214795e-5 + (0.14584676665979e-5 + (0.737945003861626e-7 + 0.124777947801772e-8 * y) * y) * y;
            else if (y < -0.180e2)
                return 0.140170463594988e-4 + (0.216903843914122e-5 + (0.11220373133445e-6 + 0.193983769330219e-8 * y) * y) * y;
            else if (y < -0.175e2)
                return 0.21539581213436e-4 + (0.342279424813136e-5 + (0.181856831833902e-6 + 0.322970992477352e-8 * y) * y) * y;
            else if (y < -0.170e2)
                return 0.327232723591053e-4 + (0.533999844453159e-5 + (0.291411357342487e-6 + 0.531646279160371e-8 * y) * y) * y;
            else if (y < -0.165e2)
                return 0.496788341991919e-4 + (0.83321564163109e-5 + (0.467420649800093e-6 + 0.876762538881168e-8 * y) * y) * y;
            else if (y < -0.160e2)
                return 0.752261935339314e-4 + (0.129771308408089e-4 + (0.748934251284821e-6 + 0.144547688531496e-7 * y) * y) * y;
            else if (y < -0.155e2)
                return 0.113635570201316e-3 + (0.201788889659471e-4 + (0.119904413410596e-5 + 0.238320580785899e-7 * y) * y) * y;
            else if (y < -0.150e2)
                return 0.171207969863231e-3 + (0.31321934061801e-4 + (0.191795026932234e-5 + 0.392924050724906e-7 * y) * y) * y;
            else if (y < -0.145e2)
                return 0.257236419349703e-3 + (0.48527623959097e-4 + (0.30649962624754e-5 + 0.647823160314476e-7 * y) * y) * y;
            else if (y < -0.140e2)
                return 0.385357524993377e-3 + (0.750354389198455e-4 + (0.48931214321822e-5 + 0.106808182001719e-6 * y) * y) * y;
            else if (y < -0.135e2)
                return 0.575487481526713e-3 + (0.115777572462701e-3 + (0.780327382810046e-5 + 0.176097524761677e-6 * y) * y) * y;
            else if (y < -0.130e2)
                return 0.856559872672346e-3 + (0.178238103828426e-3 + (0.124299798551912e-4 + 0.290337179751571e-6 * y) * y) * y;
            else if (y < -0.125e2)
                return 0.12703704744121e-2 + (0.273732858076021e-3 + (0.197757301819293e-4 + 0.478689752232036e-6 * y) * y) * y;
            else if (y < -0.120e2)
                return 0.187690585544385e-2 + (0.41930134952364e-3 + (0.314212094977388e-4 + 0.789235867320289e-6 * y) * y) * y;
            else if (y < -0.115e2)
                return 0.27616962237955e-2 + (0.640498941611572e-3 + (0.498543421717331e-4 + 0.13012673304868e-5 * y) * y) * y;
            else if (y < -0.110e2)
                return 0.404565260175818e-2 + (0.975444083689063e-3 + (0.789800067002106e-4 + 0.214548949073252e-5 * y) * y) * y;
            else if (y < -0.105e2)
                return 0.589859229386038e-2 + (0.148079127244283e-2 + (0.12492066022328e-3 + 0.353763050658311e-5 * y) * y) * y;
            else if (y < -0.100e2)
                return 0.855562771376782e-2 + (0.223994424956122e-2 + (0.197220943758365e-3 + 0.583287760293502e-5 * y) * y) * y;
            else if (y < -0.95e1)
                return 0.12342435672509e-1 + (0.337598663718376e-2 + (0.310825182520619e-3 + 0.961968556167682e-5 * y) * y) * y;
            else if (y < -0.90e1)
                return 0.176930684115761e-1 + (0.506566013372913e-2 + (0.488685550578026e-3 + 0.158604002303578e-4 * y) * y) * y;
            else if (y < -0.85e1)
                return 0.252149973168826e-1 + (0.757296976883695e-2 + (0.767275510034451e-3 + 0.26178546876892e-4 * y) * y) * y;
            else if (y < -0.80e1)
                return 0.356276697657689e-1 + (0.112480306331352e-1 + (0.119963561171659e-2 + 0.431338449820741e-4 * y) * y) * y;
            else if (y < -0.75e1)
                return 0.501201452667009e-1 + (0.166827089459808e-1 + (0.18789704008223e-2 + 0.714394611948116e-4 * y) * y) * y;
            else if (y < -0.70e1)
                return 0.694168367945472e-1 + (0.244013855571334e-1 + (0.290812728230931e-2 + 0.117179767038679e-3 * y) * y) * y;
            else if (y < -0.65e1)
                return 0.968288624045948e-1 + (0.361493965328672e-1 + (0.4586414564557e-2 + 0.197098209050474e-3 * y) * y) * y;
            else if (y < -0.60e1)
                return 0.129179596149612e0 + (0.510805044149769e-1 + (0.688350808488157e-2 + 0.314897876759426e-3 * y) * y) * y;
            else if (y < -0.55e1)
                return 0.184076573982228e0 + (0.785289933314355e-1 + (0.114582562376247e-1 + 0.56905055191182e-3 * y) * y) * y;
            else if (y < -0.50e1)
                return 0.220169401419735e0 + (0.982159901155292e-1 + (0.15037710198369e-1 + 0.785987155593294e-3 * y) * y) * y;
            else if (y < -0.45e1)
                return 0.366675838184894e0 + (0.186119852174657e0 + (0.326184826101946e-1 + 0.1958038649715e-2 * y) * y) * y;
            else if (y < -0.40e1)
                return 0.281347425168195e0 + (0.129234243499434e0 + (0.199772362379227e-1 + 0.102165002954671e-2 * y) * y) * y;
            else if (y < -0.35e1)
                return 0.899564486709937e0 + (0.592897039653903e0 + (0.13589293527654e0 + 0.106812916160982e-1 * y) * y) * y;
            else if (y < -0.30e1)
                return -0.145149764601295e0 + (-0.302572318609975e0 + (-0.119955452798854e0 - 0.136852215339393e-1 * y) * y) * y;
            else if (y < -0.25e1)
                return 0.296655310780827e1 + (0.280913055379719e1 + (0.917278838003533e0 + 0.101563032999659e0 * y) * y) * y;
            else if (y < -0.20e1)
                return -0.28220404713162e1 + (-0.41371817411595e1 + (-0.186124607997914e1 - 0.268906956064698e0 * y) * y) * y;
            else if (y < -0.15e1)
                return 0.986404756163365e1 + (0.148919503083264e2 + (0.765331994476383e1 + 0.131685404805913e1 * y) * y) * y;
            else
                return 0.153284251033547e1 + (-0.177045979431252e1 + (-0.345495345699548e1 - 0.115165115233183e1 * y) * y) * y;
        }

        public static double ExpLinNeg(double x)
        {
            int i;
            int nTris;
            double xAbs;
            double xSqrt;
            double step;
            double zLeft;
            double zRight;
            double zMidLeft;
            double zMidRight;
            double z;
            double funcLeft;
            double funcRight;
            double funcMidLeft;
            double funcMidRight;
            if (-eps < x)
                return 0.0e0;
            xAbs = -x;
            xSqrt = 0.5e0 * (0.1e1 + Math.Sqrt(0.1e1 + 0.4e1 * xAbs));
            zLeft = Math.Log(xSqrt);
            zRight = xSqrt - 0.1e1;
            funcLeft = zLeft * Math.Exp(zLeft) - xAbs;
            funcRight = zRight * Math.Exp(zRight) - xAbs;
            nTris = (int)Math.Ceiling(0.910239226626837393614236e0 * (0.230258509299404568401799e1 + Math.Log(zRight - zLeft)));
            step = (zRight - zLeft) / 3;
            for (i = 1; i <= nTris; i++)
            {
                zMidLeft = step + zLeft;
                zMidRight = zMidLeft + step;
                funcMidLeft = zMidLeft * Math.Exp(zMidLeft) - xAbs;
                funcMidRight = zMidRight * Math.Exp(zMidRight) - xAbs;
                if (funcLeft < 0.0e0 && 0.0e0 < funcMidLeft || 0.0e0 < funcLeft && funcMidLeft < 0.0e0)
                {
                    zRight = zMidLeft;
                    funcRight = funcMidLeft;
                }
                else if (funcMidLeft < 0.0e0 && 0.0e0 < funcMidRight || 0.0e0 < funcMidLeft && funcMidRight < 0.0e0)
                {
                    zLeft = zMidLeft;
                    funcLeft = funcMidLeft;
                    zRight = zMidRight;
                    funcRight = funcMidRight;
                }
                else
                {
                    zLeft = zMidRight;
                    funcLeft = funcMidRight;
                }
                step /= 3;
            }
            z = 0.5e0 * (zLeft + zRight);
            for (i = 1; i <= 4; i++)
                z = z - (z - xAbs * Math.Exp(-z)) / (0.1e1 + z);
            return -z;
        }

        public static double ExpLinNegSpline(double x)
        {
            double y = Math.Log(-x);
            if (y < -0.20e2)
                return -0.323509197739048e-5 + (-0.460136029298672e-6 + (-0.218657241289927e-7 - 0.347074986174487e-9 * y) * y) * y;
            else if (y < -0.19e2)
                return -0.448545433701508e-5 + (-0.647690383242366e-6 + (-0.312434418261774e-7 - 0.503370281127566e-9 * y) * y) * y;
            else if (y < -0.18e2)
                return -0.112269508339104e-4 + (-0.171213719854163e-5 + (-0.872669584208753e-7 - 0.148623899331525e-8 * y) * y) * y;
            else if (y < -0.17e2)
                return -0.259358805625844e-4 + (-0.416362548665352e-5 + (-0.22346075220487e-6 - 0.400834628561144e-8 * y) * y) * y;
            else if (y < -0.16e2)
                return -0.598169474825647e-4 + (-0.101426372960616e-4 + (-0.575167329228875e-6 - 0.10904553678239e-7 * y) * y) * y;
            else if (y < -0.15e2)
                return -0.136550480966317e-3 + (-0.245301748242743e-4 + (-0.147438842474217e-5 - 0.296383265014325e-7 * y) * y) * y;
            else if (y < -0.14e2)
                return -0.308443522766907e-3 + (-0.589087831843781e-4 + (-0.376629564874909e-5 - 0.805695981460309e-7 * y) * y) * y;
            else if (y < -0.13e2)
                return -0.68828286418417e-3 + (-0.140302927773805e-3 + (-0.958016311942243e-5 - 0.218995014114444e-6 * y) * y) * y;
            else if (y < -0.12e2)
                return -0.151509941887602e-2 + (-0.331106748087253e-3 + (-0.242573800666107e-4 - 0.595333910196195e-6 * y) * y) * y;
            else if (y < -0.11e2)
                return -0.328228760232756e-2 + (-0.772903793950032e-3 + (-0.610738005551756e-4 - 0.161801225710078e-5 * y) * y) * y;
            else if (y < -0.10e2)
                return -0.698300999587285e-2 + (-0.17821917194629e-2 + (-0.152827248329073e-3 - 0.43984197654007e-5 * y) * y) * y;
            else if (y < -0.9e1)
                return -0.145339605117674e-1 + (-0.404747687423162e-2 + (-0.379355763805945e-3 - 0.119493702812964e-4 * y) * y) * y;
            else if (y < -0.8e1)
                return -0.294881794144912e-1 + (-0.90322165084641e-2 + (-0.93321572316511e-3 - 0.324627021094136e-4 * y) * y) * y;
            else if (y < -0.7e1)
                return -0.57916521492764e-1 + (-0.196928447878181e-1 + (-0.226579425808436e-2 - 0.87986807731049e-4 * y) * y) * y;
            else if (y < -0.6e1)
                return -0.109275668488044e0 + (-0.417039077858133e-1 + (-0.541023182922653e-2 - 0.237721930166391e-3 * y) * y) * y;
            else if (y < -0.5e1)
                return -0.194880028067754e0 + (-0.845060875758091e-1 + (-0.125439284608925e-1 - 0.634038409703389e-3 * y) * y) * y;
            else if (y < -0.4e1)
                return -0.321008516856971e0 + (-0.160183180849559e0 + (-0.276793471156425e-1 - 0.164306632002005e-2 * y) * y) * y;
            else if (y < -0.3e1)
                return -0.466172248646337e0 + (-0.269055979690984e0 + (-0.548975468259987e-1 - 0.39112496292164e-2 * y) * y) * y;
            else if (y < -0.2e1)
                return -0.565201754177915e0 + (-0.368085485216228e0 + (-0.879073820010799e-1 - 0.757900909311432e-2 * y) * y) * y;
            else if (y < -0.1e1)
                return -0.573360961642387e0 + (-0.380324296438772e0 + (-0.940267876123519e-1 - 0.859891002832631e-2 * y) * y) * y;
            else if (y < 0.0e0)
                return -0.567143290361074e0 + (-0.361671282674534e0 + (-0.753737738481142e-1 - 0.238123877358042e-2 * y) * y) * y;
            else if (y < 0.1e1)
                return -0.567143290409784e0 + (-0.361671282674534e0 + (-0.753737738481142e-1 + 0.418834692264802e-2 * y) * y) * y;
            else if (y < 0.2e1)
                return -0.56847186716034e0 + (-0.357685552393513e0 + (-0.793595041291351e-1 + 0.551692368298831e-2 * y) * y) * y;
            else if (y < 0.3e1)
                return -0.559408587657236e0 + (-0.371280471644586e0 + (-0.725620445035985e-1 + 0.438401374539887e-2 * y) * y) * y;
            else if (y < 0.4e1)
                return -0.523640191159411e0 + (-0.407048867714123e0 + (-0.606392458137528e-1 + 0.305925833541603e-2 * y) * y) * y;
            else if (y < 0.5e1)
                return -0.46072976499494e0 + (-0.45423168799311e0 + (-0.488435407440061e-1 + 0.207628291293713e-2 * y) * y) * y;
            else if (y < 0.6e1)
                return -0.378989401999368e0 + (-0.503275905500742e0 + (-0.390346972424797e-1 + 0.142236001283537e-2 * y) * y) * y;
            else if (y < 0.7e1)
                return -0.286783326988266e0 + (-0.549378943029049e0 + (-0.313508576544285e-1 + 0.995480035721412e-3 * y) * y) * y;
            else if (y < 0.8e1)
                return -0.190275752688488e0 + (-0.590739332171067e0 + (-0.254422306341402e-1 + 0.714116844279114e-3 * y) * y) * y;
            else if (y < 0.9e1)
                return -0.933524849815836e-1 + (-0.627085557537545e0 + (-0.208989524633305e-1 + 0.524813587162045e-3 * y) * y) * y;
            else if (y < 0.10e2)
                return 0.169145941412951e-2 + (-0.658766872099274e0 + (-0.173788064009161e-1 + 0.394437807072622e-3 * y) * y) * y;
            else if (y < 0.11e2)
                return 0.936030815170741e-1 + (-0.686340358856786e0 + (-0.14621457725165e-1 + 0.302526184547585e-3 * y) * y) * y;
            else if (y < 0.12e2)
                return 0.181766830528762e0 + (-0.710385017778048e0 + (-0.124355796414138e-1 + 0.236287454736943e-3 * y) * y) * y;
            else if (y < 0.13e2)
                return 0.265952327220261e0 + (-0.731431391734406e0 + (-0.106817151450507e-1 + 0.187568996504633e-3 * y) * y) * y;
            else if (y < 0.14e2)
                return 0.34612182049145e0 + (-0.749932043425172e0 + (-0.925858809191488e-2 + 0.151078559244741e-3 * y) * y) * y;
            else if (y < 0.15e2)
                return 0.422522444130582e0 + (-0.766303605549565e0 + (-0.808919079731534e-2 + 0.123235766516181e-3 * y) * y) * y;
            else if (y < 0.16e2)
                return 0.494806133400273e0 + (-0.780760345031776e0 + (-0.712540816516791e-2 + 0.101818374690682e-3 * y) * y) * y;
            else if (y < 0.17e2)
                return 0.565165752054785e0 + (-0.793952772528586e0 + (-0.630088144661729e-2 + 0.846407347208777e-4 * y) * y) * y;
            else if (y < 0.18e2)
                return 0.625753099885705e0 + (-0.804644658400268e0 + (-0.56719469835772e-2 + 0.72308686425974e-4 * y) * y) * y;
            else if (y < 0.19e2)
                return 0.711449489854098e0 + (-0.818927388579216e0 + (-0.487846197363566e-2 + 0.576145195752046e-4 * y) * y) * y;
            else if (y < 0.20e2)
                return 0.671195968954397e0 + (-0.812571569478385e0 + (-0.521297876841621e-2 + 0.634832352731091e-4 * y) * y) * y;
            else if (y < 0.25e2)
                return 0.904410603534238e0 + (-0.847553764728456e0 + (-0.346386900591269e-2 + 0.343314058980504e-4 * y) * y) * y;
            else if (y < 0.30e2)
                return 0.116021458107802e1 + (-0.878250242059279e0 + (-0.223600991267978e-2 + 0.179599513216115e-4 * y) * y) * y;
            else if (y < 0.35e2)
                return 0.13441678047904e1 + (-0.89664556482576e0 + (-0.16228324871304e-2 + 0.111468688155074e-4 * y) * y) * y;
            else if (y < 0.40e2)
                return 0.151527573451668e1 + (-0.911311958417621e0 + (-0.120379267022008e-2 + 0.715601341636147e-5 * y) * y) * y;
            else if (y < 0.45e2)
                return 0.165862907214883e1 + (-0.922063458724757e0 + (-0.935005162541677e-3 + 0.491611751904144e-5 * y) * y) * y;
            else if (y < 0.50e2)
                return 0.178772412288919e1 + (-0.930669795870086e0 + (-0.743753225978811e-3 + 0.349943650746466e-5 * y) * y) * y;
            else if (y < 0.55e2)
                return 0.190247663656985e1 + (-0.937554946292707e0 + (-0.606050217526394e-3 + 0.258141645111521e-5 * y) * y) * y;
            else if (y < 0.60e2)
                return 0.200655396733466e1 + (-0.943231892067359e0 + (-0.502833021623625e-3 + 0.195585768806812e-5 * y) * y) * y;
            else if (y < 0.65e2)
                return 0.21014006248457e1 + (-0.947974224894971e0 + (-0.423794141163435e-3 + 0.151675279662263e-5 * y) * y) * y;
            else if (y < 0.70e2)
                return 0.218858354028123e1 + (-0.95199805157743e0 + (-0.361889115279437e-3 + 0.119929112542263e-5 * y) * y) * y;
            else if (y < 0.75e2)
                return 0.226917770899933e1 + (-0.955452087406063e0 + (-0.312545746298974e-3 + 0.964322701706144e-6 * y) * y) * y;
            else if (y < 0.80e2)
                return 0.234407934807667e1 + (-0.958448153104344e0 + (-0.272598203655222e-3 + 0.786778067733912e-6 * y) * y) * y;
            else if (y < 0.85e2)
                return 0.241398426944828e1 + (-0.961069587479348e0 + (-0.239830273967671e-3 + 0.650245027369116e-6 * y) * y) * y;
            else if (y < 0.90e2)
                return 0.247986957009364e1 + (-0.963394950938566e0 + (-0.212473056800405e-3 + 0.542961822791603e-6 * y) * y) * y;
            else if (y < 0.95e2)
                return 0.254053267211081e1 + (-0.965417054572939e0 + (-0.190005238640706e-3 + 0.459747681459384e-6 * y) * y) * y;
            else if (y < 0.100e3)
                return 0.260337846024129e1 + (-0.9674016583025e0 + (-0.169114673066375e-3 + 0.386447451374011e-6 * y) * y) * y;
            else if (y < 0.105e3)
                return 0.263936340230632e1 + (-0.968481206452772e0 + (-0.158319191563665e-3 + 0.350462513031644e-6 * y) * y) * y;
            else if (y < 0.110e3)
                return 0.277341643814815e1 + (-0.972311291999174e0 + (-0.121842186359829e-3 + 0.234662496511532e-6 * y) * y) * y;
            else if (y < 0.115e3)
                return 0.24765701192884e1 + (-0.964215484338831e0 + (-0.1954404378175e-3 + 0.457687500928716e-6 * y) * y) * y;
            else if (y < 0.120e3)
                return 0.399850976485639e1 + (-0.100391825688527e1 + (0.149801062586288e-3 - 0.543012500241685e-6 * y) * y) * y;
            else
                return -0.22025478789598e1 + (-0.848891816872757e0 + (-0.114208593751796e-2 + 0.304556250004788e-5 * y) * y) * y;
        }

        private static double ExpLinPosCommon(
          double x,
          double left,
          double right,
          int nNewt)
        {
            int i;
            int nTris;
            double xExpz;
            double step;
            double zLeft;
            double zRight;
            double zMidLeft;
            double zMidRight;
            double z;
            double funcLeft;
            double funcRight;
            double funcMidLeft;
            double funcMidRight;
            if (Math.Abs(x - 0.367879441171442321595524e0) < eps)
                return 0.10e1;
            zLeft = left;
            zRight = right;
            funcLeft = x * Math.Exp(zLeft) - zLeft;
            funcRight = x * Math.Exp(zRight) - zRight;
            nTris = (int)Math.Ceiling(0.910239226626837393614236e0 * (0.230258509299404568401799e1 + Math.Log(zRight - zLeft)));
            step = (zRight - zLeft) / 3;
            for (i = 1; i <= nTris; i++)
            {
                zMidLeft = zLeft + step;
                zMidRight = zMidLeft + step;
                funcMidLeft = x * Math.Exp(zMidLeft) - zMidLeft;
                funcMidRight = x * Math.Exp(zMidRight) - zMidRight;
                if (funcLeft < 0.0e0 && 0.0e0 < funcMidLeft || 0.0e0 < funcLeft && funcMidLeft < 0.0e0)
                {
                    zRight = zMidLeft;
                    funcRight = funcMidLeft;
                }
                else if (funcMidLeft < 0.0e0 && 0.0e0 < funcMidRight || 0.0e0 < funcMidLeft && funcMidRight < 0.0e0)
                {
                    zLeft = zMidLeft;
                    funcLeft = funcMidLeft;
                    zRight = zMidRight;
                    funcRight = funcMidRight;
                }
                else
                {
                    zLeft = zMidRight;
                    funcLeft = funcMidRight;
                }
                step /= 3;
            }
            z = 0.5e0 * (zLeft + zRight);
            for (i = 1; i <= nNewt; i++)
            {
                xExpz = x * Math.Exp(z);
                z = z - (xExpz - z) / (xExpz - 0.1e1);
            }
            return z;
        }

        private static int nNewtSetInfinity(double x)
        {
            if (x - 0.3677609e0 < -eps)
                return 4;
            else if (x - 0.367832735949894046886888e0 < -eps)
                return 5;
            else if (x - 0.367866164560240781576825e0 < -eps)
                return 6;
            else if (x - 0.367876359100984821234040e0 < -eps)
                return 7;
            else if (x - 0.367878729924413667665951e0 < -eps)
                return 8;
            else if (x - 0.367879249608909270803826e0 < -eps)
                return 9;
            else if (x - 0.367879395651632487744031e0 < -eps)
                return 10;
            else if (x - 0.367879430265654548901937e0 < -eps)
                return 11;
            else if (x - 0.367879438800618892749092e0 < -eps)
                return 12;
            else if (x - 0.367879440697277635826238e0 < -eps)
                return 13;
            else if (x - 0.367879441023502939635507e0 < -eps)
                return 14;
            else if (x - 0.367879441136354134848597e0 < -eps)
                return 15;
            else if (x - 0.367879441153048349536952e0 < -eps)
                return 16;
            else if (x - 0.367879441170493992223985e0 < -eps)
                return 23;
            else if (x - 0.367879441171186272665208e0 < -eps)
                return 24;
            else if (x - 0.367879441171382576845117e0 < -eps)
                return 25;
            else if (x - 0.367879441171427982855426e0 < -eps)
                return 26;
            else if (x - 0.367879441171438880297901e0 < -eps)
                return 27;
            else if (x - 0.367879441171441702161952e0 < -eps)
                return 28;
            else if (x - 0.367879441171441764105309e0 < -eps)
                return 29;
            else if (x - 0.367879441171442204522579e0 < -eps)
                return 30;
            else
                return 40;
        }

        private static int nNewtSetZero(double x)
        {
            if (x - 0.933209385016952208300e-3 < -eps)
                return 2;
            else if (x - 0.296368905147457235639084e0 < -eps)
                return 3;
            else if (x - 0.367514737437719997657146e0 < -eps)
                return 4;
            else if (x - 0.367863767708640430249795e0 < -eps)
                return 5;
            else if (x - 0.367875115316129954268688e0 < -eps)
                return 6;
            else if (x - 0.367878366217998789402643e0 < -eps)
                return 7;
            else if (x - 0.367879181422053792283804e0 < -eps)
                return 8;
            else if (x - 0.367879378879123266193668e0 < -eps)
                return 9;
            else if (x - 0.367879426271120245158031e0 < -eps)
                return 10;
            else if (x - 0.367879437612188248430584e0 < -eps)
                return 11;
            else if (x - 0.367879440322235925689378e0 < -eps)
                return 12;
            else if (x - 0.367879440969161358260524e0 < -eps)
                return 13;
            else if (x - 0.367879441123279831109213e0 < -eps)
                return 14;
            else if (x - 0.367879441159991691895346e0 < -eps)
                return 15;
            else if (x - 0.367879441168717438421848e0 < -eps)
                return 16;
            else if (x - 0.367879441170793799596865e0 < -eps)
                return 17;
            else if (x - 0.367879441171286808105246e0 < -eps)
                return 18;
            else if (x - 0.367879441171403612020213e0 < -eps)
                return 19;
            else if (x - 0.367879441171426124965292e0 < -eps)
                return 20;
            else
                return 40;
        }
    }
}