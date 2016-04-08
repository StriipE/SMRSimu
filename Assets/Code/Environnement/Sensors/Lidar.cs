using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Sensors
{
    public class Lidar : ASensor
    {
        private int pollingRate;
        private int degreesRange;
        private double?[] data;
        private int currentDegree;
        private float range;

        public int PollingRate
        {
            get
            {
                return pollingRate;
            }

            set
            {
                pollingRate = value;
            }
        }
        public int DegreesRange
        {
            get
            {
                return degreesRange;
            }

            set
            {
                degreesRange = value;
            }
        }
        public double?[] Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }
        public int CurrentDegree
        {
            get
            {
                return currentDegree;
            }

            set
            {
                currentDegree = value;
            }
        }
        public float Range
        {
            get
            {
                return range;
            }

            set
            {
                range = value;
            }
        }

        public static Lidar CreateComponent(GameObject gameObj, string nom, Vector3 pos)
        {
            Lidar newComponent = gameObj.AddComponent<Lidar>();
            newComponent.name = nom;
            newComponent.transform.position = pos;

            return newComponent;
        }

        void Start()
        {
            PollingRate = 500;
            DegreesRange = 270;
            Data = new double?[DegreesRange + 2];
            CurrentDegree = 0;
            Range = 6; 

            InvokeRepeating("updatePolling", 0.0005f, 0.0005f);
        }

        public void updatePolling()
        {
            RaycastHit hit;
            transform.rotation = Quaternion.AngleAxis((currentDegree - DegreesRange / 2), Vector3.up);
            if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
                Data[CurrentDegree] = hit.distance;
            else
                Data[CurrentDegree] = null;

            Debug.DrawLine(transform.position, transform.forward * 10, Color.red);
            CurrentDegree = (CurrentDegree++ <= DegreesRange) ? CurrentDegree++ : 0;
        }
        public override void Move(Vector3 newPos)
        {
            throw new NotImplementedException();
        }
    }
}
