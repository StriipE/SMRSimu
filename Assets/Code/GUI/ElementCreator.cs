using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Assets.Code.Environnement.Agents;

namespace Assets.Code.GUI
{
    class ElementCreator : MonoBehaviour
    {
        void Start()
        {

        }
        public void createAgent()
        {
            Debug.Log("Attente d'un agent");
            StartCoroutine(WaitForMouseDown());
        }

        public void createWall()
        {
            Debug.Log("Waiting for wall");
            StartCoroutine(WaitForMouseDown());       
        }

        IEnumerator WaitForMouseDown()
        {
            while (!Input.GetMouseButtonDown(0))
              yield return null;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject agentModel = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                agentModel.GetComponent<Renderer>().material.color = Color.green;
                agentModel.AddComponent<SphereCollider>();
                agentModel.AddComponent<Rigidbody>();
                AgentReactif.CreateComponent(agentModel, "Toto2", new Vector3(hit.point.x, 0.5f, hit.point.z));
                Debug.Log(hit.point);
            }            
        }
    }
}
