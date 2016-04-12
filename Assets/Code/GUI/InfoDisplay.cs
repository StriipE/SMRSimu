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
        private GameObject selectedObject;
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
           // selectedObject = hit.collider.gameObject;
        }

        public void Display(GameObject gameObject)
        {
            GameObject infoDisplayPanel = GameObject.FindGameObjectWithTag("InfoDisplayPanel");

            if (gameObject.GetComponentInParent<AgentReactif>() != null)
            {
                AgentReactif agent = gameObject.GetComponentInParent<AgentReactif>();

                GameObject motorTorqueText = new GameObject("motorTorqueText");
                motorTorqueText.transform.parent = infoDisplayPanel.transform;
                motorTorqueText.AddComponent<Slider>();
               
                motorTorqueText.AddComponent<Text>();
                motorTorqueText.GetComponent<Text>().text = "MotorTorque : " + agent.motorTorque.ToString();
                motorTorqueText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

                RectTransform motorTorqueTextRect = motorTorqueText.GetComponent<RectTransform>();
                motorTorqueTextRect.anchorMin = new Vector2(0, 1);
                motorTorqueTextRect.anchorMax = new Vector2(0, 1);
                motorTorqueTextRect.sizeDelta = new Vector2(128, 25);
                motorTorqueTextRect.anchoredPosition = new Vector2(80, -25);
            }
        }

        void Update()
        {
            if (GameObject.Find("motorTorqueText"))
                GameObject.Find("motorTorqueText").GetComponent<Text>().text = "MotorTorque : " + 
                selectedObject.GetComponentInParent<AgentReactif>().motorTorque.ToString();
        }
    }
}
