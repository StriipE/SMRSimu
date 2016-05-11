using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Code.Environnement.Sensors;

namespace Assets.Code.Environnement.Agents
{
    public abstract class AAgent : AElement
    {
        private float[,] map;
        private List<ASensor> sensors;

        public float[,] Map
        {
            get
            {
                return map;
            }

            set
            {
                map = value;
            }
        }
        public List<ASensor> Sensors
        {
            get
            {
                return sensors;
            }

            set
            {
                sensors = value;
            }
        }

        public void AddLidarListner(Lidar lidar)
        {
            lidar.OnNearWallDetected += HandleOnNearWallDetected;
            lidar.OnNearWallEscaped += HandleOnNearWallEscaped;
            lidar.OnNoWallInFront += HandleOnNoWallInFront;
        }

        public abstract void HandleOnNearWallDetected(int angle, float distance);
        public abstract void HandleOnNearWallEscaped();
        public abstract void HandleOnNoWallInFront();
    }

}