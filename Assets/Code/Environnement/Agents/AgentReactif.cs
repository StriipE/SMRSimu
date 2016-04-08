using Assets.Code.Environnement.Items;
using Assets.Code.Environnement.Chains;
using Assets.Code.Environnement.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Assets.Code.Environnement.Agents
{
    public class AgentReactif : AAgent
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

        public float motorTorque;
        public float brakeTorque;
        public float steerAngle;

        public static AgentReactif CreateComponent(GameObject gameObj, string nom, Vector3 pos)
        {
          //  gameObj.AddComponent<Lidar>();
            AgentReactif newComponent = gameObj.AddComponent<AgentReactif>();
            newComponent.name = nom;
            newComponent.transform.position = pos;
            newComponent.motorTorque = 0;
            newComponent.brakeTorque = 0;
            newComponent.steerAngle = 0;
            
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

            // Find the agent wheels and sets his speed / angle
            var wheels = GetComponentsInChildren<WheelCollider>();
            Debug.DrawLine(transform.position, transform.forward * 10, Color.red);
            motorTorque = 10f;
            steerAngle = 30f;
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
          

                // Stop the agent rotation when he's aligned with the supply
                RaycastHit hit;
                if (closestSupply.GetComponent<Collider>().Raycast(new Ray(transform.position, transform.forward), out hit, 10f))
                    steerAngle = 0f;

                // Moving and turning the agent towards the closest supply
                wheels[0].motorTorque = motorTorque;
                wheels[1].motorTorque = motorTorque;
                wheels[2].steerAngle = steerAngle;
                wheels[3].steerAngle = steerAngle;

                if (Vector3.Distance(transform.position, closestSupply.transform.position) < 0.5f) // If the agent is close enough, he picks up the crate
                    Carry(closestSupply);
            }
            else
                motorTorque = 0;

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
           // Debug.DrawLine(transform.position, transform.forward * 10 , Color.red);
            motorTorque = 10f;
            steerAngle = 30f;

            RaycastHit hit;
            closestZone.GetComponent<BoxCollider>().size = new Vector3(5f, 2f, 5f);
            closestZone.GetComponent<BoxCollider>().isTrigger = true;
            if (closestZone.GetComponent<BoxCollider>().Raycast(new Ray(transform.position, transform.forward), out hit, 10f))
                steerAngle = 0f;

            wheels[0].motorTorque = motorTorque;
            wheels[1].motorTorque = motorTorque;
            wheels[2].steerAngle = steerAngle;
            wheels[3].steerAngle = steerAngle;


            if (Vector3.Distance(transform.position, closestZone.transform.position) < 0.7f) // If the agent is close enough, he picks up the crate
                Drop(closestZone);
        }

        public void Carry(ACarryable item)
        {

            item.transform.position = transform.position + new Vector3(0, 0.2f, 0);
            CarriedCrate = item;
        }

        public void Drop(AChain chain)
        {
            CarriedCrate.transform.position = transform.position + new Vector3(0, 0, 0.5f);
            CarriedCrate = null;
        }
    }
}
