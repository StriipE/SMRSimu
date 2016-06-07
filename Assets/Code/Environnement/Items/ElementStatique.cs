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
            // Material wallTexture = Resources.Load("Materials/Bricks red smooth/Bricks red smooth Mat") as Material;
            Vector3 distance = secondPos - firstPos;

            // gameObj.GetComponent<Renderer>().material = wallTexture;
            // gameObj.GetComponent<Renderer>().material.mainTextureScale = new Vector2(Math.Abs(distance.x) + 1,Math.Abs(distance.z) + 1);
            gameObj.GetComponent<Renderer>().material.color = Color.red;
            gameObj.AddComponent<BoxCollider>();
            gameObj.AddComponent<Rigidbody>();
            gameObj.GetComponent<Rigidbody>().mass = 10000f;

            ElementStatique newComponent = gameObj.AddComponent<ElementStatique>();
            newComponent.transform.localScale += distance;
            newComponent.transform.position = firstPos + (distance / 2.0f);
            newComponent.name = nom;

            // Debug.Log(newComponent.GetComponent<MeshFilter>().mesh.subMeshCount);
            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            
        }
    }
}
