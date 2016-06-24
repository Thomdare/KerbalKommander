using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Kerbal_Kommander
{
    public class MultiTank : PartModule
    {
        [KSPField]
        public double TankAmount;



        void Update()
        {
            double TotalAmount = 0;
            foreach (PartResource resource in part.Resources)
            {
                TotalAmount = TotalAmount + resource.amount;
            }
            foreach (PartResource resource in part.Resources)
            {
                resource.maxAmount = TankAmount - TotalAmount + resource.amount;
            }
        }
    }
}
