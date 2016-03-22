using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Code.Environnement.Agents
{
    class AgentReactif : AAgent
    {
        
        private static string typeElement = "Agent";
        public string TypeElement { get { return typeElement; } }
        // Constructeurs d'agent réactif
        public static AgentReactif CreateComponent(GameObject gameObj, string nom, Vector3 pos)
        {
            AgentReactif newComponent = gameObj.AddComponent<AgentReactif>();
            newComponent.name = nom;
            newComponent.transform.position = pos;
            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            this.transform.position += newPos;
        }

        void Update()
        {
           Move(new Vector3(0.005f, 0, 0.005f));
        }
    }
}
