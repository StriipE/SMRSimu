using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Items
{
    class ElementStatique : AItem
    {
        public static ElementStatique CreateComponent(GameObject gameObj, string nom, Vector3 position)
        {
            ElementStatique newComponent = gameObj.AddComponent<ElementStatique>();
            newComponent.name = nom;
            newComponent.transform.position = position;
            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            
        }
    }
}
