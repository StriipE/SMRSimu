using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Items
{
    class Supply : ACarryable
    {
        public bool available; 
        public static Supply CreateComponent(GameObject gameObj, string nom, Vector3 pos)
        {
            gameObj.GetComponent<Renderer>().material.color = Color.blue;
            gameObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            gameObj.AddComponent<BoxCollider>();
            gameObj.AddComponent<Rigidbody>();
            gameObj.GetComponent<Rigidbody>().mass = 20f;

            Supply newComponent = gameObj.AddComponent<Supply>();
            newComponent.transform.position = pos;
            newComponent.name = nom;
            newComponent.available = true;

            return newComponent;
        }
        public override void Move(Vector3 newPos)
        {
          
        }
    }
}
