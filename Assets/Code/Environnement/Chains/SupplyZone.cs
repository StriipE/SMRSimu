using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Chains
{
    public class SupplyZone : AChain
    {
        public static SupplyZone CreateComponent(GameObject gameObj, string nom, Vector3 firstPos, Vector3 secondPos)
        {
            gameObj.GetComponent<Renderer>().material.color = Color.cyan;
            gameObj.AddComponent<BoxCollider>();

            SupplyZone newComponent = gameObj.AddComponent<SupplyZone>();
            newComponent.name = nom;
            Vector3 distance = secondPos - firstPos;
            newComponent.transform.localScale = new Vector3(distance.x * .1f, 1f, distance.z * .1f);
            newComponent.transform.position = firstPos + (distance / 2.0f);
            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            throw new NotImplementedException();
        }
    }
}
