using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Kerbal_Kommander
{
    public class trading : PartModule
    {
        public double priceLiqFuelS;
        public double priceLiqFuelB;
        public double priceOxidizerS;
        public double priceOxidizerB;
        public double priceMonoS;
        public double priceMonoB;
        public double priceXenonS;
        public double priceXenonB;
        public double priceOreS;
        public double priceOreB;
        public double priceSteelB;
        public double priceSteelS;
        public double priceDiamondB;
        public double priceDiamondS;
        public double priceWoodB;
        public double priceWoodS;
        public double priceWeaponB;
        public double priceWeaponS;
        short BuyResources = 0;
        short SellResource = 0;
        bool SellWindow = false;
        bool SellWindow2 = false;
        string resourceName;
        double priceBuy;
        double priceSell;
        int ResourceID;
        Vector2 scrollPos = new Vector2();
        double mass;
        public List<PartResource> ModResources = null;

        void setPriceKer()
        {
            priceLiqFuelB = 0.8;
            priceLiqFuelS = 0.7;
            priceOxidizerB = 0.18;
            priceOxidizerS = 0.08;
            priceOreB = 0.02;
            priceOreS = 0.01;
            priceXenonB = 4;
            priceXenonS = 3.5;
            priceMonoB = 1.2;
            priceMonoS = 1;
            priceSteelB = 2;
            priceSteelS = 1.6;
            priceDiamondB = 20;
            priceDiamondS = 19;
            priceWoodB = 5;
            priceWoodS = 4.5;
            priceWeaponB = 50;
            priceWeaponS = 40;
        }
        void setPriceMun()
        {
            priceLiqFuelB = 0.5;
            priceLiqFuelS = 0.4;
            priceOxidizerB = 0.1;
            priceOxidizerS = 0.05;
            priceOreB = 0.01;
            priceOreS = 0.001;
            priceXenonB = 5;
            priceXenonS = 4;
            priceMonoB = 1;
            priceMonoS = 0.8;
            priceSteelB = 1.2;
            priceSteelS = 1;
            priceDiamondB = 15;
            priceDiamondS = 14;
            priceWoodB = 7;
            priceWoodS = 6.5;
            priceWeaponB = 45;
            priceWeaponS = 38;
        }
        void setPriceMinmus()
        {
            priceLiqFuelB = 0.5;
            priceLiqFuelS = 0.4;
            priceOxidizerB = 0.1;
            priceOxidizerS = 0.05;
            priceOreB = 0.01;
            priceOreS = 0.001;
            priceXenonB = 5;
            priceXenonS = 4;
            priceMonoB = 1;
            priceMonoS = 0.8;
            priceSteelB = 1.2;
            priceSteelS = 1;
            priceDiamondB = 13;
            priceDiamondS = 12;
            priceWoodB = 7;
            priceWoodS = 6.5;
            priceWeaponB = 45;
            priceWeaponS = 38;
        }
        void setPriceDuna()
        {
            priceLiqFuelB = 1;
            priceLiqFuelS = 0.9;
            priceOxidizerB = 0.2;
            priceOxidizerS = 0.18;
            priceOreB = 0.01;
            priceOreS = 0;
            priceXenonB = 5;
            priceXenonS = 4.5;
            priceMonoB = 1.5;
            priceMonoS = 1.4;
            priceSteelB = 1;
            priceSteelS = 0.8;
            priceDiamondB = 17;
            priceDiamondS = 15;
            priceWoodB = 8;
            priceWoodS = 7.9;
            priceWeaponB = 60;
            priceWeaponS = 55;
        }
        void setPriceIke()
        {
            priceLiqFuelB = 0.8;
            priceLiqFuelS = 0.7;
            priceOxidizerB = 0.18;
            priceOxidizerS = 0.16;
            priceOreB = 0.005;
            priceOreS = 0;
            priceXenonB = 4.5;
            priceXenonS = 3;
            priceMonoB = 1.3;
            priceMonoS = 1.1;
            priceSteelB = 0.8;
            priceSteelS = 0.7;
            priceDiamondB = 14;
            priceDiamondS = 10;
            priceWoodB = 12;
            priceWoodS = 11;
            priceWeaponB = 55;
            priceWeaponS = 53;
        }
        void setPriceEve()
        {
            priceLiqFuelB = 2;
            priceLiqFuelS = 1.8;
            priceOxidizerB = 0.5;
            priceOxidizerS = 0.4;
            priceOreB = 0.11;
            priceOreS = 0.1;
            priceXenonB = 6;
            priceXenonS = 4;
            priceMonoB = 2;
            priceMonoS = 1.2;
            priceSteelB = 4;
            priceSteelS = 3.8;
            priceDiamondB = 22;
            priceDiamondS = 15;
            priceWoodB = 12;
            priceWoodS = 11;
            priceWeaponB = 30;
            priceWeaponS = 28;
        }
        void setPriceGilly()
        {
            priceLiqFuelB = 2;
            priceLiqFuelS = 1.8;
            priceOxidizerB = 0.5;
            priceOxidizerS = 0.4;
            priceOreB = 0.09;
            priceOreS = 0.08;
            priceXenonB = 6;
            priceXenonS = 4;
            priceMonoB = 2;
            priceMonoS = 1.2;
            priceSteelB = 3.8;
            priceSteelS = 3.7;
            priceDiamondB = 21;
            priceDiamondS = 11;
            priceWoodB = 14;
            priceWoodS = 12;
            priceWeaponB = 27.5;
            priceWeaponS = 25;
        }
        void setPriceMoho()
        {
            priceLiqFuelB = 3;
            priceLiqFuelS = 2.5;
            priceOxidizerB = 2;
            priceOxidizerS = 1.5;
            priceOreB = 0.2;
            priceOreS = 0.17;
            priceXenonB = 8;
            priceXenonS = 7;
            priceMonoB = 3;
            priceMonoS = 2.5;
            priceSteelB = 5;
            priceSteelS = 4.3;
            priceDiamondB = 25;
            priceDiamondS = 16;
            priceWoodB = 30;
            priceWoodS = 25;
            priceWeaponB = 23;
            priceWeaponS = 15;
        }
        void setPriceDres()
        {
            priceLiqFuelB = 0.8;
            priceLiqFuelS = 0.7;
            priceOxidizerB = 0.2;
            priceOxidizerS = 0.18;
            priceOreB = 0.01;
            priceOreS = 0;
            priceXenonB = 4.5;
            priceXenonS = 4;
            priceMonoB = 1;
            priceMonoS = 0.9;
            priceSteelB = 0.7;
            priceSteelS = 0.6;
            priceDiamondB = 15;
            priceDiamondS = 12;
            priceWoodB = 20;
            priceWoodS = 18;
            priceWeaponB = 40;
            priceWeaponS = 38;
        }
        void setPriceEeloo()
        {
            priceLiqFuelB = 3.5;
            priceLiqFuelS = 3.2;
            priceOxidizerB = 2.2;
            priceOxidizerS = 1.8;
            priceOreB = 0.3;
            priceOreS = 0.2;
            priceXenonB = 8.5;
            priceXenonS = 7.8;
            priceMonoB = 3.5;
            priceMonoS = 3.2;
            priceSteelB = 6;
            priceSteelS = 5.5;
            priceDiamondB = 30;
            priceDiamondS = 25;
            priceWoodB = 18;
            priceWoodS = 15;
            priceWeaponB = 50;
            priceWeaponS = 48;
        }
        void setPriceJool()
        {
            priceLiqFuelB = 1.1;
            priceLiqFuelS = 0.9;
            priceOxidizerB = 0.25;
            priceOxidizerS = 0.21;
            priceOreB = 0.04;
            priceOreS = 0.03;
            priceXenonB = 4.6;
            priceXenonS = 4;
            priceMonoB = 1.8;
            priceMonoS = 1.5;
            priceSteelB = 2.3;
            priceSteelS = 1.8;
            priceDiamondB = 23;
            priceDiamondS = 21;
            priceWoodB = 6;
            priceWoodS = 5;
            priceWeaponB = 50;
            priceWeaponS = 40;
        }
        void setPriceLaythe()
        {
            priceLiqFuelB = 1;
            priceLiqFuelS = 0.9;
            priceOxidizerB = 0.2;
            priceOxidizerS = 0.18;
            priceOreB = 0.03;
            priceOreS = 0.02;
            priceXenonB = 4.1;
            priceXenonS = 3.8;
            priceMonoB = 1.6;
            priceMonoS = 1.4;
            priceSteelB = 2;
            priceSteelS = 1.8;
            priceDiamondB = 20;
            priceDiamondS = 19.5;
            priceWoodB = 5;
            priceWoodS = 4.5;
            priceWeaponB = 45;
            priceWeaponS = 40;
        }
        void setPriceVall()
        {
            priceLiqFuelB = 0.9;
            priceLiqFuelS = 0.8;
            priceOxidizerB = 0.18;
            priceOxidizerS = 0.12;
            priceOreB = 0.025;
            priceOreS = 0.02;
            priceXenonB = 4;
            priceXenonS = 3.8;
            priceMonoB = 1.4;
            priceMonoS = 1.3;
            priceSteelB = 1.9;
            priceSteelS = 1.75;
            priceDiamondB = 19.7;
            priceDiamondS = 19.5;
            priceWoodB = 7;
            priceWoodS = 6;
            priceWeaponB = 45;
            priceWeaponS = 40;
        }
        void setPriceBop()
        {
            priceLiqFuelB = 0.5;
            priceLiqFuelS = 0.4;
            priceOxidizerB = 0.1;
            priceOxidizerS = 0.05;
            priceOreB = 0.01;
            priceOreS = 0.001;
            priceXenonB = 3.2;
            priceXenonS = 3.1;
            priceMonoB = 1;
            priceMonoS = 0.8;
            priceSteelB = 1.2;
            priceSteelS = 1;
            priceDiamondB = 13;
            priceDiamondS = 12;
            priceWoodB = 9;
            priceWoodS = 6;
            priceWeaponB = 45;
            priceWeaponS = 38;
        }
        void setPriceTylo()
        {
            priceLiqFuelB = 1.5;
            priceLiqFuelS = 1.4;
            priceOxidizerB = 0.4;
            priceOxidizerS = 0.3;
            priceOreB = 0.06;
            priceOreS = 0.05;
            priceXenonB = 5;
            priceXenonS = 4.8;
            priceMonoB = 2.2;
            priceMonoS = 2.1;
            priceSteelB = 3;
            priceSteelS = 2.7;
            priceDiamondB = 25;
            priceDiamondS = 24;
            priceWoodB = 6;
            priceWoodS = 5;
            priceWeaponB = 50;
            priceWeaponS = 40;
        }
        void setPricePol()
        {
            priceLiqFuelB = 0.5;
            priceLiqFuelS = 0.4;
            priceOxidizerB = 0.1;
            priceOxidizerS = 0.05;
            priceOreB = 0.01;
            priceOreS = 0.001;
            priceXenonB = 3.2;
            priceXenonS = 3.1;
            priceMonoB = 1;
            priceMonoS = 0.8;
            priceSteelB = 1.2;
            priceSteelS = 1;
            priceDiamondB = 13;
            priceDiamondS = 12;
            priceWoodB = 9;
            priceWoodS = 6;
            priceWeaponB = 45;
            priceWeaponS = 38;
        }
        void checkPrice()
        {
            if (vessel.orbit.referenceBody.name == "Kerbin") { setPriceKer(); }
            if (vessel.orbit.referenceBody.name == "Mun") { setPriceMun(); }
            if (vessel.orbit.referenceBody.name == "Minmus") { setPriceMinmus(); }
            if (vessel.orbit.referenceBody.name == "Duna") { setPriceDuna(); }
            if (vessel.orbit.referenceBody.name == "Ike") { setPriceIke(); }
            if (vessel.orbit.referenceBody.name == "Eve") { setPriceEve(); }
            if (vessel.orbit.referenceBody.name == "Gilly") { setPriceGilly(); }
            if (vessel.orbit.referenceBody.name == "Moho") { setPriceMoho(); }
            if (vessel.orbit.referenceBody.name == "Dres") { setPriceDres(); }
            if (vessel.orbit.referenceBody.name == "Eeloo") { setPriceEeloo(); }
            if (vessel.orbit.referenceBody.name == "Jool") { setPriceJool(); }
            if (vessel.orbit.referenceBody.name == "Laythe") { setPriceLaythe(); }
            if (vessel.orbit.referenceBody.name == "Vall") { setPriceVall(); }
            if (vessel.orbit.referenceBody.name == "Bop") { setPriceBop(); }
            if (vessel.orbit.referenceBody.name == "Tylo") { setPriceTylo(); }
            if (vessel.orbit.referenceBody.name == "Pol") { setPricePol(); }
        }



        [KSPEvent(guiActive = true, guiName = "trading", guiActiveEditor = false, externalToEVAOnly = false, guiActiveUnfocused = true)]
        public void ActivateEvent()
        {
            Events["ActivateEvent"].active = false;
            Events["DeactivateEvent"].active = true;
            checkPrice();
            SellWindow = true;
        }
        [KSPEvent(guiActive = true, guiName = "close window", active = false, guiActiveEditor = false, externalToEVAOnly = false, guiActiveUnfocused = true)]
        public void DeactivateEvent()
        {
            Events["ActivateEvent"].active = true;
            Events["DeactivateEvent"].active = false;
            SellWindow = false;
            SellWindow2 = false;
        }
        void OnGUI()
        {
            if (SellWindow == true)
            {
                GUI.Window(GetInstanceID(), new Rect(5f, 40f, 200f, 320f), DrawGUISell, "trading", HighLogic.Skin.window);
            }
            if (SellWindow2 == true)
            {
                GUI.Window(GetInstanceID(), new Rect(5f, 40f, 600f, 160), sellWindow, resourceName, HighLogic.Skin.window);
            }
        }


        void DrawGUISell(int windowID)
        {
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, HighLogic.Skin.scrollView);
            if (GUILayout.Button("Liquide Fuel", HighLogic.Skin.button))
            {
                resourceName = "Liquide Fuel";
                priceBuy = priceLiqFuelB;
                priceSell = priceLiqFuelS;
                ResourceID = 374119730;
                SellWindow2 = true;
                BuyResources = 0;
            }
            if (GUILayout.Button("Oxidizer", HighLogic.Skin.button))
            {
                resourceName = "Oxydizer";
                priceBuy = priceOxidizerB;
                priceSell = priceOxidizerS;
                ResourceID = -1823983486;
                SellWindow2 = true;
                BuyResources = 0;
            }
            if (GUILayout.Button("Mono Propellant", HighLogic.Skin.button))
            {
                resourceName = "Mono Propellant";
                priceBuy = priceMonoB;
                priceSell = priceMonoS;
                ResourceID = 2001413032;
                SellWindow2 = true;
                BuyResources = 0;
            }
            if (GUILayout.Button("Xenon Gas", HighLogic.Skin.button))
            {
                resourceName = "Xenon Gas";
                priceBuy = priceXenonB;
                priceSell = priceXenonS;
                ResourceID = 1447111193;
                SellWindow2 = true;
                BuyResources = 0;
            }
            if (GUILayout.Button("Ore", HighLogic.Skin.button))
            {
                resourceName = "Ore";
                priceBuy = priceOreB;
                priceSell = priceOreS;
                ResourceID = 79554;
                SellWindow2 = true;
                BuyResources = 0;
            }
            if (GUILayout.Button("Steel", HighLogic.Skin.button))
            {
                resourceName = "Steel";
                priceBuy = priceSteelB;
                priceSell = priceSteelS;
                ResourceID = 80208299;
                SellWindow2 = true;
                BuyResources = 0;
            }
            if (GUILayout.Button("Diamond", HighLogic.Skin.button))
            {
                resourceName = "Diamond";
                priceBuy = priceDiamondB;
                priceSell = priceDiamondS;
                ResourceID = -975259340;
                SellWindow2 = true;
                BuyResources = 0;

            }

            if (GUILayout.Button("Wood", HighLogic.Skin.button))
            {
                resourceName = "Wood";
                priceBuy = priceWoodB;
                priceSell = priceWoodS;
                ResourceID = 2702029;
                SellWindow2 = true;
                BuyResources = 0;
            }
            if (part.partInfo.name == "spasta-Pirate")
            {
                GUILayout.Label("Illegal products:", HighLogic.Skin.label);
                if (GUILayout.Button("Weapons", HighLogic.Skin.button))
                {
                    resourceName = "Weapons";
                    priceBuy = priceWeaponB;
                    priceSell = priceWeaponS;
                    ResourceID = -1406985801;
                    SellWindow2 = true;
                    BuyResources = 0;
                }
            }
            vesselResources();
            if (ModResources.Count > 1)
            {
                GUILayout.Label("Mods:", HighLogic.Skin.label);
                foreach (PartResource resource in ModResources)
                {
                    if (resource.resourceName != "ElectricCharge" && resource.resourceName != "LiquidFuel" && resource.resourceName != "Oxidizer" && resource.resourceName != "MonoPropellant" && resource.resourceName != "XenonGas" && resource.resourceName != "Ore" && resource.resourceName != "Steel" && resource.resourceName != "Diamond" && resource.resourceName != "Wood" && resource.resourceName != "Weapons" && resource.resourceName != "SolidFuel" && resource.resourceName != "IntakeAir")
                    {
                        if (GUILayout.Button(resource.resourceName, HighLogic.Skin.button))
                        {

                            resourceName = resource.resourceName;
                            priceSell = resource.info.unitCost;
                            priceBuy = resource.info.unitCost;
                            ResourceID = resource.info.id;
                            ConfigNode c = new ConfigNode("RESSOURCE");
                            c.AddValue("name", resourceName);
                            c.AddValue("amount", 0);
                            c.AddValue("maxAmount", 100000);
                            part.AddResource(c);

                            SellWindow2 = true;
                            BuyResources = 0;
                        }
                    }
                }
            }

            GUILayout.EndScrollView();
            if (GUILayout.Button("close", HighLogic.Skin.button))
            {
                SellWindow = false;
                Events["ActivateEvent"].active = true;
                Events["DeactivateEvent"].active = false;
            }



            GUILayout.EndVertical();
        }
        void vesselResources()
        {
            bool ex = false;
            ModResources.Clear();
            foreach (Part part in vessel.parts)
            {
                foreach (PartResource resource in part.Resources)
                {
                    ex = false;
                    if (resource.resourceName != "LiquidFuel" && resource.resourceName != "Oxidizer" && resource.resourceName != "MonoPropellant" && resource.resourceName != "XenonGas" && resource.resourceName != "Ore" && resource.resourceName != "Steel" && resource.resourceName != "Diamond" && resource.resourceName != "Wood" && resource.resourceName != "ElectricCharge" && resource.resourceName != "SolidFuel")
                    {
                        foreach (PartResource res in ModResources)
                        {
                            if (res.resourceName == resource.resourceName)
                            {
                                ex = true;
                            }
                        }
                        if (ex == false) { ModResources.Add(resource); }
                    }
                }
            }
        }

        void sellWindow(int windowID)
        {
            double funds = Funding.Instance.Funds;
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            mass = part.Resources.Get(ResourceID).amount;
            float maxS = Convert.ToSingle(mass);
            GUILayout.Label("Buy " + resourceName, HighLogic.Skin.label);
            GUILayout.Label("price: " + priceBuy, HighLogic.Skin.label);
            float max = Convert.ToSingle(funds / priceBuy);

            GUILayout.Label("quantité: " + BuyResources, HighLogic.Skin.label);

            GUILayout.Label("prix total: " + BuyResources * priceBuy, HighLogic.Skin.label);
            if (GUILayout.Button("buy", HighLogic.Skin.button))
            {
                if (BuyResources * priceBuy <= funds)
                {
                    Funding.Instance.AddFunds(-(BuyResources * priceBuy), TransactionReasons.Cheating);
                    part.TransferResource(ResourceID, BuyResources);
                    ScreenMessages.PostScreenMessage("done !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
                else
                {
                    ScreenMessages.PostScreenMessage("not enought money !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
                BuyResources = 0;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (max < 10000)
            {

                BuyResources = (short)GUILayout.HorizontalSlider(BuyResources, 0, max, HighLogic.Skin.horizontalSlider, HighLogic.Skin.horizontalSliderThumb);
            }
            else
            {
                BuyResources = (short)GUILayout.HorizontalSlider(BuyResources, 0, 10000, HighLogic.Skin.horizontalSlider, HighLogic.Skin.horizontalSliderThumb);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Sell " + resourceName, HighLogic.Skin.label);
            GUILayout.Label("price: " + priceSell, HighLogic.Skin.label);


            GUILayout.Label("quantité: " + SellResource, HighLogic.Skin.label);
            GUILayout.Label("prix total: " + SellResource * priceSell, HighLogic.Skin.label);
            if (GUILayout.Button("Sell", HighLogic.Skin.button))
            {
                if (mass >= SellResource)
                {
                    Funding.Instance.AddFunds((SellResource * priceSell), TransactionReasons.Cheating);
                    part.TransferResource(ResourceID, -SellResource);
                    ScreenMessages.PostScreenMessage("done !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
                else
                {
                    ScreenMessages.PostScreenMessage("not enought resources !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
                SellResource = 0;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (maxS > 10000)
            {
                SellResource = (short)GUILayout.HorizontalSlider(SellResource, 0, 10000, HighLogic.Skin.horizontalSlider, HighLogic.Skin.horizontalSliderThumb);
            }
            else
            {

                SellResource = (short)GUILayout.HorizontalSlider(SellResource, 0, maxS, HighLogic.Skin.horizontalSlider, HighLogic.Skin.horizontalSliderThumb);
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("close", HighLogic.Skin.button))
            {
                SellWindow2 = false;
            }
            GUILayout.EndVertical();
        }
    }
}
