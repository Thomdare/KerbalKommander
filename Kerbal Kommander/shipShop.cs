using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using UnityEngine;

namespace Kerbal_Kommander
{
    public class shipShop : PartModule
    {
        private class VesselData
        {
            public string name = null;
            public Guid? id = null;
            public string craftURL = null;
            public AvailablePart craftPart = null;
            public string flagURL = null;
            public VesselType vesselType = VesselType.Ship;
            public CelestialBody body = null;
            public Orbit orbit = null;
            public bool orbiting = false;
            public bool owned = false;
            public VesselData() { }
            public VesselData(VesselData vd)
            {
                name = vd.name;
                id = vd.id;
                craftURL = vd.craftURL;
                craftPart = vd.craftPart;
                flagURL = vd.flagURL;
                vesselType = vd.vesselType;
                body = vd.body;
                orbit = vd.orbit;
                orbiting = vd.orbiting;
                owned = vd.owned;
            }
        }


        bool MenuWindow = false;
        CraftBrowser craftBrowser;
        bool sellWindow = false;
        Vessel vesselSell = null;
        double vesselPrice = 0;
        Vector2 scrollPos = new Vector2();
        bool aboutWindow = false;

        [KSPEvent(guiActive = true, guiName = "ship shop", guiActiveEditor = false, externalToEVAOnly = false, guiActiveUnfocused = true)]
        public void ActivateEvent()
        {
            Events["ActivateEvent"].active = false;
            Events["DeactivateEvent"].active = true;
            MenuWindow = true;
        }
        [KSPEvent(guiActive = true, guiName = "close window", active = false, guiActiveEditor = false, externalToEVAOnly = false, guiActiveUnfocused = true)]
        public void DeactivateEvent()
        {
            Events["ActivateEvent"].active = true;
            Events["DeactivateEvent"].active = false;
            MenuWindow = false;
            sellWindow = false;
            aboutWindow = false;
        }

        void OnGUI()
        {
            if (MenuWindow == true)
            {
                GUI.Window(GetInstanceID(), new Rect(5f, 40f, 200, 200), menuWindow, "Ship shop2", HighLogic.Skin.window);
            }
            if (sellWindow == true)
            {
                GUI.Window(GetInstanceID(), new Rect(5f, 40f, 300f, 400f), SellWindow, "Sell ship", HighLogic.Skin.window);
            }
            if (craftBrowser != null)
            {
                craftBrowser.OnGUI();
            }
            if (aboutWindow == true)
            {
                GUI.Window(GetInstanceID(), new Rect(5f, 40f, 300f, 130f), AboutWindow, "About", HighLogic.Skin.window);
            }
        }

        void menuWindow(int windowID)
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("buy a new ship", HighLogic.Skin.button))
            {
                StartCoroutine(StartVesselSpawnRoutine());
            }
            if (GUILayout.Button("Sell a ship", HighLogic.Skin.button))
            {
                sellWindow = true;
            }
            if (GUILayout.Button("Design a new ship", HighLogic.Skin.button))
            {
                GamePersistence.SaveGame("persistent", "kerbal kommander", SaveMode.BACKUP);
                EditorDriver.StartEditor(EditorFacility.VAB);
            }
            if (GUILayout.Button("close", HighLogic.Skin.button))
            {
                MenuWindow = false;
                Events["ActivateEvent"].active = true;
                Events["DeactivateEvent"].active = false;
            }

            GUILayout.EndVertical();
        }
        void SellWindow(int windowID)
        {
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, HighLogic.Skin.scrollView);
            foreach (Vessel vessel in FlightGlobals.Vessels)
            {
                if (vessel.vesselType != VesselType.Debris && vessel.vesselType != VesselType.EVA && vessel.vesselType != VesselType.Flag && vessel.vesselType != VesselType.SpaceObject && vessel.vesselType != VesselType.Unknown && vessel.vesselType != VesselType.Station && vessel != FlightGlobals.ActiveVessel && vessel.loaded == true)
                {
                    double price = 0;
                    foreach (Part part in vessel.Parts)
                    {
                        if (part.partInfo.name == "LargeTank") { price = price + 3000; }
                        else
                        {
                            if (part.partInfo.name == "SmallTank") { price = price + 1000; Debug.Log(part.partName + "+1000"); }
                            else
                            {
                                if (part.partInfo.name == "RadialOreTank") { price = price + 300; Debug.Log(part.partName + "+300"); }
                                else
                                {
                                    price = price + part.partInfo.cost;
                                    foreach (PartResource resource in part.Resources.list)
                                    {
                                        price = price - resource.maxAmount * resource.info.unitCost /*+ resource.amount * resource.info.unitCost*/;
                                    }
                                }
                            }
                        }

                    }
                    price = Math.Round(price, MidpointRounding.AwayFromZero);


                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button(vessel.vesselName + "       price: " + price + "\n" + vessel.situation + " " + vessel.mainBody.bodyName, HighLogic.Skin.button))
                    {
                        if (vessel.GetCrewCount() != 0)
                        {
                            ScreenMessages.PostScreenMessage("there are still some crew in the vessel", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                        }
                        else
                        {
                            vesselPrice = price;
                            vesselSell = vessel;
                            ScreenMessages.PostScreenMessage("Vessel selected", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                        }
                    }
                    GUILayout.EndHorizontal();


                }
            }
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Sell", HighLogic.Skin.button))
            {
                if (vesselSell != null)
                {
                    Funding.Instance.AddFunds(vesselPrice, TransactionReasons.VesselRecovery);
                    foreach (Part part in vesselSell.parts)
                    {
                        foreach (PartResource resource in part.Resources.list)
                        {
                            resource.amount = 0;
                        }
                    }
                    vesselSell.Die();
                    ScreenMessages.PostScreenMessage("vessel sell", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
                else
                {
                    ScreenMessages.PostScreenMessage("please select a vessel", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
            }
            if (GUILayout.Button("close", HighLogic.Skin.button)) { sellWindow = false; }
            if (GUILayout.Button("about", HighLogic.Skin.button)) { aboutWindow = true; }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        void AboutWindow(int windowID)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("To Sell a vessel , it: \n - Have to be near the station you are in \n - must not be a space station or a debris \n - must not contain crew \n WARNING: when you sell a vessel, the resources are not selled !");
            if (GUILayout.Button("close", HighLogic.Skin.button)) { aboutWindow = false; }
            GUILayout.EndVertical();
        }


        IEnumerator StartVesselSpawnRoutine()
        {
            float width = 450;
            float height = Screen.height * 0.7f;
            yield return null;

            craftBrowser = new CraftBrowser(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), EditorFacility.VAB, HighLogic.CurrentGame.Title.Split(new string[] { " (" }, StringSplitOptions.None)[0], "Buy vessel", OnSelected, OnCancelled, HighLogic.Skin, Texture2D.whiteTexture, false, false);
        }

        void OnSelected(string fullPath, string flagUrl, CraftBrowser.LoadType loadType)
        {
            Vessel youpi = new Vessel();
            Vector3 gpsPos = vessel.GetWorldPos3D();
            SpawnVesselFromCraftFile(fullPath);
            craftBrowser = null;
        }

        void OnCancelled()
        {
            craftBrowser = null;
        }


        void SpawnVesselFromCraftFile(string craftURL)
        {
            VesselData newData = new VesselData();

            newData.craftURL = craftURL;
            newData.body = FlightGlobals.currentMainBody;
            newData.orbiting = true;
            newData.flagURL = HighLogic.CurrentGame.flagURL;
            newData.owned = true;
            newData.vesselType = VesselType.Ship;
            SpawnVessel(newData);
        }


        void SpawnVessel(VesselData vesselData)
        {
            string gameDataDir = KSPUtil.ApplicationRootPath;
            vesselData.orbit = vessel.GetOrbit();
            vesselData.orbit.vel = vessel.orbit.vel;
            vesselData.orbit.meanAnomalyAtEpoch = vesselData.orbit.meanAnomalyAtEpoch + Math.Atan(UnityEngine.Random.Range(200, 300) / vesselData.orbit.semiMajorAxis);
            ConfigNode[] partNodes;
            ShipConstruct shipConstruct = null;
            float lcHeight = 0;
            ConfigNode craftNode;
            ConfigNode currentShip = ShipConstruction.ShipConfig;
            shipConstruct = ShipConstruction.LoadShip(vesselData.craftURL);
            float dryCost = 0;
            float cost = 0;
            shipConstruct.GetShipCosts(out dryCost, out cost);
            cost = cost + dryCost;
            if (cost > Funding.Instance.Funds)
            {
                foreach (var p in FindObjectsOfType<Part>())
                {
                    if (!p.vessel)
                    {
                        Destroy(p.gameObject);
                    }
                }
                ScreenMessages.PostScreenMessage("not enought funds", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                return;
            }
            craftNode = ConfigNode.Load(vesselData.craftURL);
            lcHeight = ConfigNode.ParseVector3(craftNode.GetNode("PART").GetValue("pos")).y;
            ShipConstruction.ShipConfig = currentShip;
            vesselData.name = shipConstruct.shipName;
            ConfigNode empty = new ConfigNode();
            ProtoVessel dummyProto = new ProtoVessel(empty, null);
            Vessel dummyVessel = new Vessel();
            dummyVessel.parts = shipConstruct.parts;
            dummyProto.vesselRef = dummyVessel;
            foreach (Part p in shipConstruct.parts)
            {
                dummyProto.protoPartSnapshots.Add(new ProtoPartSnapshot(p, dummyProto));
            }
            foreach (ProtoPartSnapshot p in dummyProto.protoPartSnapshots)
            {
                p.storePartRefs();
            }
            List<ConfigNode> partNodesL = new List<ConfigNode>();
            foreach (var snapShot in dummyProto.protoPartSnapshots)
            {
                ConfigNode node = new ConfigNode("PART");
                snapShot.Save(node);
                partNodesL.Add(node);
            }
            partNodes = partNodesL.ToArray();
            ConfigNode[] additionalNodes = new ConfigNode[0];
            ConfigNode protoVesselNode = ProtoVessel.CreateVesselNode(vesselData.name, vesselData.vesselType, vesselData.orbit, 0, partNodes, additionalNodes);
            ProtoVessel protoVessel = HighLogic.CurrentGame.AddVessel(protoVesselNode);
            vesselData.id = protoVessel.vesselRef.id;
            protoVessel.vesselRef.Load();
            Funding.Instance.AddFunds(-cost, TransactionReasons.Vessels);
            ScreenMessages.PostScreenMessage("vessel buy !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
            foreach (var p in FindObjectsOfType<Part>())
            {
                if (!p.vessel)
                {
                    Destroy(p.gameObject);
                }
            }
        }

    }
}
