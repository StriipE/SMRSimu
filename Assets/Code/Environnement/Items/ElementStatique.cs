using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Items
{
    class ElementStatique : AItem
    {
        public static ElementStatique CreateComponent(GameObject gameObj, string nom, Vector3 firstPos, Vector3 secondPos)
        {
            ElementStatique newComponent = gameObj.AddComponent<ElementStatique>();
            Vector3 distance = secondPos - firstPos;
            newComponent.transform.localScale += distance;
            newComponent.transform.position = firstPos + (distance / 2.0f);
            newComponent.name = nom;

            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            
        }
    }
}
