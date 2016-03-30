using Assets.Code.Environnement.Items;
using Assets.Code.Environnement.Chains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Assets.Code.Environnement.Agents
{
    class AgentReactif : AAgent
    {
        private ACarryable carriedCrate;
        public ACarryable CarriedCrate
        {
            get
            {
                return carriedCrate;
            }

            set
            {
                carriedCrate = value;
            }
        }

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
            if (carriedCrate == null)
                findClosestSupply();
            else
                findClosestDropZone();
        }

        // Looks for the closest supply for this agent
        public void findClosestSupply()
        {
            Supply[] supplies = FindObjectsOfType<Supply>();
            Supply closestSupply = null;
            // Searching for closest supply if there is 
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

                // Moving and turning the agent towards the closest supply
                var wheels = GetComponentsInChildren<WheelCollider>();
                Debug.DrawLine(transform.position, transform.forward * 10, Color.red);
                wheels[0].motorTorque = 10f;
                wheels[1].motorTorque = 10f;
                wheels[0].brakeTorque = 0f;
                wheels[1].brakeTorque = 0f;
                wheels[2].steerAngle = 30f;
                wheels[3].steerAngle = 30f;

                // Stop the agent rotation when he's aligned with the supply
                RaycastHit hit;
                if (closestSupply.GetComponent<Collider>().Raycast(new Ray(transform.position, transform.forward), out hit, 10f))
                {
                    wheels[2].steerAngle = 0;
                    wheels[3].steerAngle = 0;
                }

                if (Vector3.Distance(transform.position, closestSupply.transform.position) < 0.4f) // If the agent is close enough, he picks up the crate
                    Carry(closestSupply);

            }
        }
        public void findClosestDropZone()
        {
            SupplyZone[] zones = FindObjectsOfType<SupplyZone>();
            SupplyZone closestZone = null;


            if (zones.Length > 0)
            {
                foreach (SupplyZone zone in zones)
                {
                    if (!closestZone)
                    {
                        closestZone = zone;
                    }

                    if (Vector3.Distance(transform.position, zone.transform.position) <= Vector3.Distance(transform.position, closestZone.transform.position))
                    {
                        closestZone = zone;
                    }
                }
            }

            var wheels = GetComponentsInChildren<WheelCollider>();
            Debug.DrawLine(transform.position, closestZone.transform.position , Color.red);
            wheels[0].motorTorque = 10f;
            wheels[1].motorTorque = 10f;
            wheels[0].brakeTorque = 0f;
            wheels[1].brakeTorque = 0f;
            wheels[2].steerAngle = 30f;
            wheels[3].steerAngle = 30f;

            RaycastHit hit;
            if (closestZone.GetComponent<Collider>().Raycast(new Ray(transform.position, closestZone.transform.position), out hit, 20f))
            {
                wheels[2].steerAngle = 0;
                wheels[3].steerAngle = 0;
            }

            if (Vector3.Distance(transform.position, closestZone.transform.position) < 0.4f) // If the agent is close enough, he picks up the crate
                Drop(closestZone);
        }

        public void Carry(ACarryable item)
        {
            item.transform.position = transform.position + new Vector3(0, 0.2f, 0);
            CarriedCrate = item;
            var wheels = GetComponentsInChildren<WheelCollider>();
            wheels[0].brakeTorque = 10f;
            wheels[1].brakeTorque = 10f;
        }

        public void Drop(AChain chain)
        {
            CarriedCrate.transform.position = transform.position + new Vector3(0, 0, 0.5f);
            CarriedCrate = null;
            var wheels = GetComponentsInChildren<WheelCollider>();
            wheels[0].brakeTorque = 10f;
            wheels[1].brakeTorque = 10f;
        }
    }
}
