using Assets.Code.Environnement.Items;
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
        private Vector3 speed;
        public Vector3 Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public static AgentReactif CreateComponent(GameObject gameObj, string nom, Vector3 pos)
        {
            gameObj.GetComponent<Renderer>().material.color = Color.green;
            gameObj.AddComponent<SphereCollider>();
            gameObj.AddComponent<Rigidbody>();

            AgentReactif newComponent = gameObj.AddComponent<AgentReactif>();
            newComponent.name = nom;
            newComponent.transform.position = pos;
            newComponent.Speed = new Vector3();
            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            this.transform.position += newPos;
        }

        void Update()
        {
            //Move(Speed);
            findClosestSupply();
        }

        void OnCollisionEnter()
        {
            Speed = new Vector3(-Speed.x, -Speed.y, -Speed.z);
        }

        public void findClosestSupply()
        {
            Supply[] supplies = FindObjectsOfType<Supply>();
            Supply closestSupply = null;

            if (supplies.Length > 0)
            {
                foreach (Supply supply in supplies)
                {
                    if (!closestSupply)
                    {
                        closestSupply = supply;
                    }

                    if (Vector3.Distance(transform.position, supply.transform.position) <= Vector3.Distance(transform.position, closestSupply.transform.position))
                    {
                        closestSupply = supply;
                    }
                }

                Vector3 agentDirection = closestSupply.transform.position - transform.position;
                agentDirection = agentDirection.normalized;
                GetComponent<Rigidbody>().AddForce(agentDirection * 0.2f);
            }
        }
    }
}
