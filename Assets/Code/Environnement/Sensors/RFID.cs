using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Sensors
{
    public enum RFID_Tags
    {
        Agent,
        Crate
    }

    class RFID : ASensor
    {

        public static RFID CreateComponent(GameObject gameObj, string nom)
        {
            RFID newComponent = gameObj.AddComponent<RFID>();
            newComponent.name = nom;

            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            throw new NotImplementedException();
        }
    }
}
