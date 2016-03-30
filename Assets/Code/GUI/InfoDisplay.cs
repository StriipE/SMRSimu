using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Assets.Code.Environnement.Agents;

namespace Assets.Code.GUI
{
    public class InfoDisplay : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(WaitForGameObjectClick());
        }

        IEnumerator WaitForGameObjectClick()
        {
            while (!Input.GetMouseButtonDown(0))
                yield return null;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Display(hit.collider.gameObject);
            }
        }

        public void Display(GameObject gameObject)
        {
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

            Debug.Log(gameObject.GetComponentInParent<AgentReactif>().steerAngle);
            if (gameObject.GetComponentInParent<AgentReactif>() != null)
            {
                AgentReactif agent = gameObject.GetComponentInParent<AgentReactif>();
                canvas.AddComponent<Text>().text = "BrakeTorque : " + agent.brakeTorque.ToString();
                canvas.GetComponent<Text>().transform.position = new Vector3(50, 50, 0);
                canvas.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            }
        }
    }
}
