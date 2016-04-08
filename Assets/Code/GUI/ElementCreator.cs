using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Assets.Code.Environnement.Agents;
using Assets.Code.Environnement.Items;
using Assets.Code.Environnement.Sensors;

namespace Assets.Code.GUI
{
    class ElementCreator : MonoBehaviour
    {
        void Start()
        {

        }

        // This method must be called from a GameObject in Unity
        // Starts the Coroutine to create an agent on click
        public void createAgent()
        {
            StartCoroutine(WaitForMouseDown("AgentReactif"));
        }

        // This method must be called from a GameObject in Unity
        // Starts the Coroutine to create a wall on click
        public void createWall()
        {
            StartCoroutine(WaitForMouseDown("ElementStatique"));
        }

        public void createSupply()
        {
            StartCoroutine(WaitForMouseDown("Supply"));
        }

        public void addSensor()
        {
            StartCoroutine(WaitForMouseDown("Sensor"));
        }
        // This Coroutine waits for a left mouse click to create the selected item
        IEnumerator WaitForMouseDown(string typeObject)
        {
            while (!Input.GetMouseButtonDown(0))
                yield return null;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (typeObject == "AgentReactif")
                    AddElement("AgentReactif", "Toto", new Vector3(hit.point.x, 0.5f, hit.point.z), new Vector3());
                if (typeObject == "ElementStatique") // To create a wall, run a new coroutine waiting for a second input
                    StartCoroutine(WaitForSecondMouseDown(new Vector3(hit.point.x, 0.5f, hit.point.z)));
                if (typeObject == "Supply")
                    AddElement("Supply", "Caisse", new Vector3(hit.point.x, 0.5f, hit.point.z), new Vector3());
                if (typeObject == "Sensor")
                {
                    try
                    {
                        AgentReactif agent = hit.rigidbody.GetComponent<AgentReactif>();
                        AddSensorToAgent("Lidar", "Lidar", agent);
                        Debug.Log("agent hit");
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.Log(e);
                    }
                }
            }
        }
        // Waits for the second mouse input
        // If both clicks are on a valid surface, creates a wall between the two clicks
        IEnumerator WaitForSecondMouseDown(Vector3 firstPos)
        {
            while (!Input.GetMouseButtonDown(1))
                yield return null;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 secondPos = new Vector3(hit.point.x, 0.5f, hit.point.z);
                AddElement("ElementStatique", "Wall", firstPos, secondPos);
            }
        }
        // Instancie tous les éléments de l'environnement
        public void AddElement(string typeElement, string nom, Vector3 firstPos, Vector3 secondPos)
        {
            // Creating agents
            if (typeElement == "AgentReactif")
            {
                GameObject agentModel = Instantiate(Resources.Load("AgentReactif/AgentReactifv2")) as GameObject;
                AgentReactif.CreateComponent(agentModel, nom, firstPos);

            }
            // Creating walls
            if (typeElement == "ElementStatique")
            {
                GameObject elementStatique = GameObject.CreatePrimitive(PrimitiveType.Cube);
                ElementStatique.CreateComponent(elementStatique, nom, firstPos, secondPos);
            }

            if (typeElement == "Supply")
            {
                GameObject supply = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Supply.CreateComponent(supply, nom, firstPos);
            }
            
        }

        public void AddSensorToAgent(string typeSensor, string nom, AAgent agent)
        {
            if (typeSensor == "Lidar")
            {
                GameObject sensor = new GameObject();
                Lidar lidar = Lidar.CreateComponent(sensor, nom);
                agent.Sensors.Add(lidar);
            }
        }
    }
}
