using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Kerbal_Kommander
{
    public class slavesTraffic : PartModule
    {
        Vector2 scrollPos = new Vector2();
        public bool slaveWindow = false;
        double slavePrice;
        [KSPEvent(guiActive = true, guiName = "slaves traffic", guiActiveEditor = false, externalToEVAOnly = false, guiActiveUnfocused = true)]
        public void ActivateEvent()
        {
            CheckPrice();
            slaveWindow = true;
        }



        void OnGUI()
        {
            if (slaveWindow == true)
            {
                GUI.Window(GetInstanceID(), new Rect(5f, 40f, 425, 350), DrawGUISlaves, "Slaves traffic", HighLogic.Skin.window);
            }
        }

        void DrawGUISlaves(int windowID)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Hello ! You want to traffic slaves... Make shure the police don't find out your's traffic...", HighLogic.Skin.label);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Male  " + "      price: " + slavePrice.ToString(), HighLogic.Skin.label);
            if (GUILayout.Button("buy slave", HighLogic.Skin.button))
            {
                if (Funding.Instance.Funds < 1000)
                {
                    ScreenMessages.PostScreenMessage("not enought funds", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
                else
                {
                    Funding.Instance.AddFunds(-slavePrice, TransactionReasons.CrewRecruited);
                    ProtoCrewMember slave = HighLogic.CurrentGame.CrewRoster.GetNewKerbal();
                    slave.type = ProtoCrewMember.KerbalType.Tourist;
                    slave.name = CrewGenerator.GetRandomName(ProtoCrewMember.Gender.Male) + " (slave)";
                    slave.gender = ProtoCrewMember.Gender.Male;
                    slave.courage = 1;
                    slave.stupidity = 1;
                    slave.isBadass = true;
                    foreach (Part CrewPart in vessel.Parts)
                    {
                        if (CrewPart.protoModuleCrew.Count < CrewPart.CrewCapacity)
                        {
                            CrewPart.AddCrewmemberAt(slave, CrewPart.protoModuleCrew.Count);
                            CrewPart.SpawnIVA();
                            if (CameraManager.Instance.currentCameraMode == CameraManager.CameraMode.Flight)
                            {
                                CameraManager.Instance.SetCameraMap();
                                CameraManager.Instance.SetCameraFlight();
                            }

                            break;
                        }
                    }

                    ScreenMessages.PostScreenMessage("new slave added (male)", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }

            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Female" + "      price: " + slavePrice.ToString(), HighLogic.Skin.label);
            if (GUILayout.Button("buy slave", HighLogic.Skin.button))
            {
                if (Funding.Instance.Funds < 900)
                {
                    ScreenMessages.PostScreenMessage("not enought funds", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }
                else
                {
                    Funding.Instance.AddFunds(-slavePrice, TransactionReasons.CrewRecruited);
                    ProtoCrewMember slave = HighLogic.CurrentGame.CrewRoster.GetNewKerbal();
                    slave.type = ProtoCrewMember.KerbalType.Tourist;
                    slave.name = CrewGenerator.GetRandomName(ProtoCrewMember.Gender.Female) + " (slave)";
                    slave.gender = ProtoCrewMember.Gender.Female;
                    slave.courage = 1;
                    slave.stupidity = 1;
                    slave.isBadass = true;
                    foreach (Part CrewPart in vessel.Parts)
                    {
                        if (CrewPart.protoModuleCrew.Count < CrewPart.CrewCapacity)
                        {
                            CrewPart.AddCrewmemberAt(slave, CrewPart.protoModuleCrew.Count + 1);
                            CrewPart.SpawnIVA();
                            if (CameraManager.Instance.currentCameraMode != CameraManager.CameraMode.Flight) { CameraManager.Instance.SetCameraFlight(); }
                            CameraManager.Instance.SetCameraMap();
                            CameraManager.Instance.SetCameraFlight();

                            break;
                        }
                    }
                    ScreenMessages.PostScreenMessage("new slave added (female)", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                }

            }
            GUILayout.EndHorizontal();
            GUILayout.Label("Sell slaves:", HighLogic.Skin.label);

            scrollPos = GUILayout.BeginScrollView(scrollPos, HighLogic.Skin.scrollView);
            foreach (Part vPart in vessel.Parts)
            {
                foreach (ProtoCrewMember slaves in vPart.protoModuleCrew)
                {
                    if (slaves.type == ProtoCrewMember.KerbalType.Tourist)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("name: " + slaves.name, HighLogic.Skin.label);
                        GUILayout.Label("gender: " + slaves.gender.ToString());
                        GUILayout.Label("Price: " + slavePrice.ToString());
                        if (GUILayout.Button("Sell", HighLogic.Skin.button))
                        {
                            ScreenMessages.PostScreenMessage("Slave selled", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                            Funding.Instance.AddFunds(slavePrice, TransactionReasons.CrewRecruited);
                            vPart.protoModuleCrew.Remove(slaves);
                            vPart.DespawnIVA();
                            if (CameraManager.Instance.currentCameraMode != CameraManager.CameraMode.Flight) { CameraManager.Instance.SetCameraFlight(); }
                            CameraManager.Instance.SetCameraMap();
                            CameraManager.Instance.SetCameraFlight();
                            vPart.SpawnIVA();
                            if (CameraManager.Instance.currentCameraMode != CameraManager.CameraMode.Flight) { CameraManager.Instance.SetCameraFlight(); }
                            CameraManager.Instance.SetCameraMap();
                            CameraManager.Instance.SetCameraFlight();
                            break;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }

            GUILayout.EndScrollView();
            if (GUILayout.Button("close", HighLogic.Skin.button))
            {
                foreach (Part parts in vessel.Parts)
                {
                    slaveWindow = false;
                    parts.SpawnIVA();
                    if (CameraManager.Instance.currentCameraMode != CameraManager.CameraMode.Flight) { CameraManager.Instance.SetCameraFlight(); }
                    CameraManager.Instance.SetCameraMap();
                    CameraManager.Instance.SetCameraFlight();
                }

            }
            GUILayout.EndVertical();
        }

        void CheckPrice()
        {
            if (vessel.mainBody.name == "Kerbin") { slavePrice = 1000; }
            if (vessel.mainBody.name == "Minmus") { slavePrice = 1100; }
            if (vessel.mainBody.name == "Moho") { slavePrice = 2000; }
            if (vessel.mainBody.name == "Dres") { slavePrice = 1800; }
            if (vessel.mainBody.name == "Gilly") { slavePrice = 1200; }
            if (vessel.mainBody.name == "Jool") { slavePrice = 2000; }
            if (vessel.mainBody.name == "Bop") { slavePrice = 2200; }
            if (vessel.mainBody.name == "Tylo") { slavePrice = 2400; }
        }
    }
}
