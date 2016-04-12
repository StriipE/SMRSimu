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

        public float startTime;
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

        // Adds Lidar composent to a game object
        public static Lidar CreateComponent(GameObject gameObj, string nom)
        {
            Lidar newComponent = gameObj.AddComponent<Lidar>();
            newComponent.name = nom;

            return newComponent;
        }

        // Lauches the polling routine on creation of the Lidar
        void Start()
        {
            PollingRate = 500;
            DegreesRange = 270;
            Data = new double?[DegreesRange + 2];
            CurrentDegree = 0;
            Range = 6; 

            InvokeRepeating("updatePolling", 0.0005f, 0.0005f);
        }

        // Adds the value fond by the Lidar lasor at this angle and increments the angle by 1° 
        public void updatePolling()
        {
            RaycastHit hit;
            Transform parentTransform = transform.parent;
            // Benchmark
            //float endTime = 0;
            //if (CurrentDegree == 0)
            //    startTime = Time.time;



            // Do several iterations to get closer to 2000 mesures per second 
            for (int i = 0; i <= 6; i++)
            {
                float angle = Vector3.Angle(Vector3.forward, parentTransform.forward); // Find the angle of the agent on which the sensor is attached
                Vector3 cross = Vector3.Cross(Vector3.forward, parentTransform.forward); // Cross product to find if the angle should be positive or not
                if (cross.y < 0)
                    angle = -angle;

                transform.rotation = Quaternion.AngleAxis((currentDegree - DegreesRange / 2) + angle, Vector3.up);

                if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
                    Data[CurrentDegree] = hit.distance;
                else
                    Data[CurrentDegree] = null;

               
                // Benchmark
                //if (CurrentDegree == 269)
                //{
                //    endTime = Time.time;
                //    Debug.Log(270 * 1 / (endTime - startTime));
                //}

                CurrentDegree = (CurrentDegree + 1) % DegreesRange;
            }

            Debug.DrawLine(transform.position, transform.forward * 10, Color.blue);
            CurrentDegree = (CurrentDegree + 1) % DegreesRange;
        }
        public override void Move(Vector3 newPos)
        {
            throw new NotImplementedException();
        }
    }
}
