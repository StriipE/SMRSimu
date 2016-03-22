using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Environnement.Agents
{
    public class AgentFactory : MonoBehaviour
    {
        private static readonly AgentFactory instance = new AgentFactory();
        private AgentFactory()
        {

        }

        public static AgentFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public AAgent createAgent(GameObject gameObj, string nom, Vector3 pos, string typeAgent)
        {     
            switch(typeAgent)
            {
                case "AgentReactif" :
                    {
                        return AgentReactif.CreateComponent(gameObj,nom, pos);
                    }
                default :
                    throw new NotImplementedException();
            }
        }
    }
}
