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
        public WheelCollider[] wheels;
        public static AgentReactif CreateComponent(GameObject gameObj, string nom, Vector3 pos)
        {
          //  gameObj.AddComponent<Lidar>();
            AgentReactif newComponent = gameObj.AddComponent<AgentReactif>();
            newComponent.name = nom;
            newComponent.transform.position = pos;
            newComponent.motorTorque = 0;
            newComponent.brakeTorque = 0;
            newComponent.steerAngle = 0;
            newComponent.Sensors = new List<ASensor>();
            newComponent.wheels = gameObj.GetComponentsInChildren<WheelCollider>();
            return newComponent;
        }

        public override void Move(Vector3 newPos)
        {
            this.transform.position += newPos;
        }

        void Update()
        {
            motorTorque = 5f;
            //var wheels = GetComponentsInChildren<WheelCollider>();
            wheels[0].motorTorque = motorTorque;
            wheels[1].motorTorque = motorTorque;
            wheels[2].steerAngle = steerAngle;
            wheels[3].steerAngle = steerAngle;
        
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

        public override void HandleOnNearWallDetected(int angle, float distance)
        {
            Debug.Log("Wall close " + angle.ToString() + " " + distance.ToString());
            if (angle < Lidar.degreesRange / 2)
                steerAngle = -30f;
            else
                steerAngle = 30f;
        }

        public override void HandleOnNearWallEscaped()
        {
            steerAngle = 0;
        }

        public override void HandleOnNoWallInFront()
        {
            steerAngle = 0;
        }

        public override void HandleOnNearCreateDetected(Transform RfidTransform)
        {
            // While the agent isn't going toward the crate (not checking y axis because crate position is lower than 
            while ( (transform.forward.x != RfidTransform.position.x - transform.position.x) && 
                    (transform.forward.z != RfidTransform.position.z - transform.position.z) )
            {
                steerAngle = 30f;
            }
        }
    }
}
