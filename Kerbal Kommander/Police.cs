using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Kerbal_Kommander
{
    public class Police : PartModule
    {
        public bool policeGUI = false;
        private float lastFixedUpdate = 0.0f;
        private float logInterval = 20f;

        void FixedUpdate()
        {
            if ((Time.time - lastFixedUpdate) > logInterval)
            {
                lastFixedUpdate = Time.time;
                foreach (Part part in FlightGlobals.ActiveVessel.parts)
                {
                    foreach (PartResource resource in part.Resources)
                    {
                        if (resource.resourceName == "Weapons" && resource.amount > 0)
                        {
                            resource.amount = 0;
                            policeGUI = true;
                        }
                    }
                    foreach (ProtoCrewMember crew in part.protoModuleCrew)
                    {
                        if (crew.type == ProtoCrewMember.KerbalType.Tourist)
                        {
                            removeSlaves();
                            policeGUI = true;
                            break;
                        }
                    }
                }
            }
        }

        void removeSlaves()
        {
            foreach (Part part in FlightGlobals.ActiveVessel.Parts)
            {
            restartForeach:
                foreach (ProtoCrewMember crewMember in part.protoModuleCrew)
                {
                    if (crewMember.type == ProtoCrewMember.KerbalType.Tourist)
                    {
                        part.protoModuleCrew.Remove(crewMember);
                        goto restartForeach;
                    }
                }
            }
        }

        void OnGUI()
        {
            if (policeGUI == true)
            {
                GUI.Window(GetInstanceID(), new Rect(new Vector2(500, 500), new Vector2(300, 120)), PoliceGUI, "Message from the police", HighLogic.Skin.window);
            }
        }

        void PoliceGUI(int windowID)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("We have found illequal stuff in your ship ! All the illequal stuff have been taken and you have to pay a penalty of 10 000$.");
            if (GUILayout.Button("pay", HighLogic.Skin.button))
            {
                Funding.Instance.AddFunds(-10000, TransactionReasons.Any);
                if (Funding.Instance.Funds < 0) { Funding.Instance.AddFunds(-Funding.Instance.Funds, TransactionReasons.Any); }
                policeGUI = false;
            }
            GUILayout.EndVertical();
        }

    }
}
