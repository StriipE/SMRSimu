using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Items
{
    public abstract class AItem : AElement
    {
        private List<ASensor> sensors;
        public List<ASensor> Sensors
        {
            get
            {
                return sensors;
            }

            set
            {
                sensors = value;
            }
        }
    }
}
