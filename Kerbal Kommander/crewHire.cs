using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Kerbal_Kommander
{
    public class crewHire : PartModule
    {
        public bool hireCrewWindow = false;
        public Texture starTrue = GameDatabase.Instance.GetTexture("KerbalKommander/Assets/starTrue", false);
        public Texture starFalse = GameDatabase.Instance.GetTexture("KerbalKommander/Assets/starFalse", false);
        public Texture KerbalePortrait = GameDatabase.Instance.GetTexture("KerbalKommander/Assets/KerbalePortrait", false);
        public Texture KerbalPortrait = GameDatabase.Instance.GetTexture("KerbalKommander/Assets/KerbalPortrait", false);
        public List<ProtoCrewMember> crewToHire;


        [KSPEvent(guiActive = true, guiName = "Hire Crew", guiActiveEditor = false, externalToEVAOnly = false, guiActiveUnfocused = true)]
        public void ActivateEvent()
        {
            Events["ActivateEvent"].active = false;
            Events["DeactivateEvent"].active = true;
            GenerateCrew();
        }
        [KSPEvent(guiActive = true, guiName = "Close Window", active = false, guiActiveEditor = false, externalToEVAOnly = false, guiActiveUnfocused = true)]
        public void DeactivateEvent()
        {
            Events["ActivateEvent"].active = true;
            Events["DeactivateEvent"].active = false;
            hireCrewWindow = false;
        }

        void OnGUI()
        {
            if (hireCrewWindow == true)
            {
                GUI.Window(GetInstanceID(), new Rect(5f, 40f, 700, 400), DrawHireCrewWindow, "Crew Hire", HighLogic.Skin.window);
            }
        }

        void DrawHireCrewWindow(int windowID)
        {
            GUILayout.BeginVertical();


            foreach (ProtoCrewMember Crew in crewToHire)
            {
                GUILayout.BeginHorizontal();
                if (Crew.gender == ProtoCrewMember.Gender.Female) { GUILayout.Label(KerbalePortrait); }
                else { GUILayout.Label(KerbalPortrait); }
                GUILayout.Label(Crew.name + "\n" + Crew.trait, HighLogic.Skin.label);
                if (Crew.experienceLevel >= 1) { GUILayout.Label(starTrue); }
                else { GUILayout.Label(starFalse); }
                if (Crew.experienceLevel >= 2) { GUILayout.Label(starTrue); }
                else { GUILayout.Label(starFalse); }
                if (Crew.experienceLevel >= 3) { GUILayout.Label(starTrue); }
                else { GUILayout.Label(starFalse); }
                if (Crew.experienceLevel >= 4) { GUILayout.Label(starTrue); }
                else { GUILayout.Label(starFalse); }
                if (Crew.experienceLevel >= 5) { GUILayout.Label(starTrue); }
                else { GUILayout.Label(starFalse); }
                GUILayout.Label("Price: " + (Crew.experienceLevel * 10000 + 10000), HighLogic.Skin.label);
                if (GUILayout.Button("Hire", HighLogic.Skin.button))
                {
                    if (Funding.Instance.Funds < Crew.experienceLevel * 10000 + 10000)
                    {
                        ScreenMessages.PostScreenMessage("not enought funds", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                    }
                    else
                    {
                        foreach (Part CrewPart in vessel.Parts)
                        {
                            if (CrewPart.protoModuleCrew.Count < CrewPart.CrewCapacity)
                            {
                                Funding.Instance.AddFunds(Crew.experienceLevel * 10000 + 10000, TransactionReasons.CrewRecruited);
                                CrewPart.AddCrewmember(Crew);
                                crewToHire.Remove(Crew);
                                CrewPart.Actions.part.SpawnIVA();
                                CameraManager.Instance.SetCameraMap();
                                CameraManager.Instance.SetCameraFlight();
                                ScreenMessages.PostScreenMessage("crew added !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                                break;
                            }
                        }
                    }

                }
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Refresh", HighLogic.Skin.button))
            {
                crewToHire.Clear();
                GenerateCrew();
            }
            if (GUILayout.Button("Close", HighLogic.Skin.button))
            {
                Events["ActivateEvent"].active = true;
                Events["DeactivateEvent"].active = false;
                hireCrewWindow = false;
            }
            GUILayout.EndVertical();

        }

        void GenerateCrew()
        {
            int i;
            for (i = crewToHire.Count; i <= 5;)
            {
                ProtoCrewMember crew = HighLogic.CurrentGame.CrewRoster.GetNewKerbal();
                crew.type = ProtoCrewMember.KerbalType.Crew;
                crew.experienceLevel = Math.Abs(UnityEngine.Random.Range(0, 6));
                crewToHire.Add(crew);
                i = crewToHire.Count;
            }
            hireCrewWindow = true;
        }
    }
}
