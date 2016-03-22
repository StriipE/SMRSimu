using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Environnement
{
    public abstract class AElement : MonoBehaviour, IMovable
    {
        public abstract void Move(Vector3 newPos);
    }
}
