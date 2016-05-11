using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Code.Environnement.Agents;
using Assets.Code.Environnement.Items;
using Assets.Code.Environnement;
using Assets.Code.Environnement.Chains;

public class Environnement : MonoBehaviour
{

    public GameObject environnement;
    // Use this for initialization
    void Start()
    {
        // Création de l'environnement de travail
        environnement = GameObject.CreatePrimitive(PrimitiveType.Plane);
        environnement.transform.position = new Vector3(0, 0, 0);
        environnement.transform.localScale = new Vector3(2, 2, 2);

        AddElement("AgentReactif", "Toto", new Vector3(1.0f, 0.1f, -1.0f));

        AddWall(new Vector3(-9.5f, 0.5f, -9.5f), new Vector3(-9.5f, 0.5f, 9.5f));
        AddWall(new Vector3(9.5f, 0.5f, -9.5f), new Vector3(9.5f, 0.5f, 9.5f));
        AddWall(new Vector3(-8.5f, 0.5f, 9.5f), new Vector3(8.5f, 0.5f, 9.5f));
        AddWall(new Vector3(-8.5f, 0.5f, -9.5f), new Vector3(8.5f, 0.5f, -9.5f));

        AddWall(new Vector3(3f, 0.5f, 2.5f), new Vector3(3f, 0.5f, -6f));
        AddWall(new Vector3(6.5f, 0.5f, 2.5f), new Vector3(6.5f, 0.5f, -6f));
        AddWall(new Vector3(-8.5f, 0.5f, 4f), new Vector3(-1f, 0.5f, 4f));
        AddWall(new Vector3(-1f, 0.5f, 4f), new Vector3(-1f, 0.5f, -6f));
        AddWall(new Vector3(3f, 0.5f, 5.5f), new Vector3(6.5f, 0.5f, 5.5f));

        AddElement("SupplyZone", "Zone", new Vector3(-6.0f, 0.01f, -6.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Instancie tous les éléments de l'environnement
    public void AddElement(string typeElement, string nom, Vector3 pos)
    {
        // Chargement des agents
        if (typeElement == "AgentReactif")
        {
            GameObject agentModel = Instantiate(Resources.Load("AgentReactif/AgentReactifv2")) as GameObject;
            AgentReactif.CreateComponent(agentModel, nom, pos);
        }
        if (typeElement == "SupplyZone")
        {
            GameObject supplyZone = GameObject.CreatePrimitive(PrimitiveType.Plane);
            SupplyZone.CreateComponent(supplyZone, nom, pos, new Vector3(pos.x + 1.5f, 0.01f, pos.z + 1.5f));
        }
    }

    public void AddWall(Vector3 firstPos, Vector3 secondPos)
    {
        GameObject elementStatique = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ElementStatique.CreateComponent(elementStatique, "Wall", firstPos, secondPos);
    }
}

