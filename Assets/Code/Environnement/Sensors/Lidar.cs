using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement.Sensors
{

    public delegate void NearWallDetected(int angle, float distance);
    public delegate void NearWallEscaped();
    public delegate void NoWallInFront();

    public class Lidar : ASensor
    {
        private int pollingRate;
        public static int degreesRange = 270;
        private double?[] data;
        private int currentDegree;
        private float range;
        public bool wallNearFlag = false;
        public bool emergencyEscapeFlag = false;
        public float startTime;
        private int closeWallDegree = -1; // Used to stock the angle of the latest wall that triggered an NearWallDetected event 

        public event NearWallDetected OnNearWallDetected;
        public event NearWallEscaped OnNearWallEscaped;
        public event NoWallInFront OnNoWallInFront;
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

        // Launches the polling routine on creation of the Lidar
        void Start()
        {
            PollingRate = 500;
            Data = new double?[DegreesRange + 2];
            CurrentDegree = 0;
            Range = 6; 

            InvokeRepeating("updatePolling", 0.0005f, 0.0005f);
        }

        // Adds the value found by the Lidar lasor at this angle and increments the angle by 1° 
        public void updatePolling()
        {
            // Do several iterations to get closer to 2000 mesures per second 
            for (int i = 0; i <= 6; i++)
            {
                pollAtAngle(CurrentDegree);
                
                CurrentDegree = (CurrentDegree + 1) % DegreesRange;
            }

            Debug.DrawLine(transform.position, transform.forward * 10, Color.blue);
            CurrentDegree = (CurrentDegree + 1) % DegreesRange;
        }

        public void pollAtAngle(int angle)
        {
            Transform parentTransform = transform.parent;

            float agentAngle = Vector3.Angle(Vector3.forward, parentTransform.forward); // Find the angle of the agent on which the sensor is attached
            Vector3 cross = Vector3.Cross(Vector3.forward, parentTransform.forward); // Cross product to find if the angle should be positive or not
            if (cross.y < 0)
                agentAngle = -agentAngle;

            RaycastHit hit;

            transform.rotation = Quaternion.AngleAxis((angle - DegreesRange / 2) + agentAngle, Vector3.up);

            if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
            {
                Data[angle] = hit.distance;

                if (angle > DegreesRange / 2 - 30 && angle < DegreesRange / 2 + 30)
                {
                    if (emergencyEscapeFlag == true)
                    {
                        if (Data[closeWallDegree] > 0.5f)
                        {
                            OnNearWallEscaped();
                            emergencyEscapeFlag = false;
                        }
                    }
                    if ((Data[DegreesRange / 2] ?? 5.0f) > 2f && emergencyEscapeFlag == false)
                    {
                        OnNoWallInFront();
                        wallNearFlag = false;
                        if (hit.distance < 0.5f)
                        {
                            OnNearWallDetected(angle, hit.distance);
                            emergencyEscapeFlag = true;
                            closeWallDegree = angle;
                        }
                    }
                    else
                    {
                        if (hit.distance < 1.5f) // Triggers a near wall event when agent gets too close to a wall.
                            if (OnNearWallDetected != null && wallNearFlag == false)
                            {
                                OnNearWallDetected(angle, hit.distance);
                                wallNearFlag = true;
                                closeWallDegree = angle;
                            }

                        if (angle == closeWallDegree && wallNearFlag == true) // Escapes the near wall event when agent is far enough  
                            if (hit.distance > 1.5f)
                            {
                                OnNearWallEscaped();
                                wallNearFlag = false;
                                closeWallDegree = -1;
                            }
                    }
                }
            }
            else
                Data[angle] = null;

        }
        public override void Move(Vector3 newPos)
        {
            throw new NotImplementedException();
        }
    }
}
