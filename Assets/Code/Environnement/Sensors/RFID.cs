using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Sensors
{
    public enum RFID_Tags
    {
        Agent,
        Crate,
        DropZone
    }

    class RFID : ASensor
    {
        private int sensorRange;
        public int SensorRange
        {
            get
            {
                return sensorRange;
            }

            set
            {
                sensorRange = value;
            }
        }

        private RFID_Tags rfidTag;
        public RFID_Tags RfidTag
        {
            get
            {
                return rfidTag;
            }

            set
            {
                rfidTag = value;
            }
        }

        public static RFID CreateComponent(GameObject gameObj, string nom)
        {
            RFID newComponent = gameObj.AddComponent<RFID>();
            newComponent.sensorRange = 4;
            gameObj.AddComponent<SphereCollider>();
            gameObj.GetComponent<SphereCollider>().radius = newComponent.sensorRange;
            gameObj.GetComponent<SphereCollider>().isTrigger = true;
            gameObj.AddComponent<Rigidbody>();
            gameObj.GetComponent<Rigidbody>().isKinematic = true;
            newComponent.name = nom;

            return newComponent;
        }

        void OnTriggerEnter(Collider collider)
        {
            try
            {
                if(RfidTag == RFID_Tags.Agent && collider.gameObject.GetComponent<RFID>().RfidTag == RFID_Tags.Crate)
                {
                    Debug.Log("It works");
                }
            }
            catch (NullReferenceException)
            {

            }
        }
        public override void Move(Vector3 newPos)
        {
            throw new NotImplementedException();
        }
    }
}
