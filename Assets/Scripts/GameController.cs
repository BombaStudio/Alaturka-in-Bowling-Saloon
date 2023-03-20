using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<ObjectData> objectdata;

    [SerializeField] List<GameObject> SelectableObjects;
    GameObject myCam;
    public string GameMode;
    void Start()
    {
        foreach (ObjectData data in objectdata)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(data.tag))
            {
                if (!g.GetComponent<UsableObject>()) g.AddComponent<UsableObject>();
                SelectableObjects.Add(g);
            }
        }
        myCam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        foreach (Transform t in transform.GetComponentInChildren<Transform>())
        {
            if (GameMode == t.name) t.gameObject.SetActive(true);
            else t.gameObject.SetActive(false);
        }
        switch (GameMode)
        {
            case "Game":
                Time.timeScale = 1;
                RaycastHit hit;
                if (Physics.Raycast(myCam.transform.position, myCam.transform.TransformDirection(Vector3.forward), out hit, 10))
                {
                    foreach (GameObject g in SelectableObjects)
                    {
                        if (hit.transform.gameObject == g)
                        {
                            if (g.GetComponent<MeshRenderer>() && g.GetComponent<MeshRenderer>().materials.Length > 0)
                            {
                                foreach (Material mat in g.GetComponent<MeshRenderer>().materials)
                                {
                                    if (mat.shader.name == "Universal Render Pipeline/Lit")
                                    {
                                        mat.EnableKeyword("_EMISSION");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (g.GetComponent<MeshRenderer>() && g.GetComponent<MeshRenderer>().materials.Length > 0)
                            {
                                foreach (Material mat in g.GetComponent<MeshRenderer>().materials)
                                {
                                    if (mat.shader.name == "Universal Render Pipeline/Lit")
                                    {
                                        mat.DisableKeyword("_EMISSION");
                                    }
                                }
                            }
                        }
                    }
                }
                break;
            case "Pause":
                Time.timeScale = 0;
                break;
            default: break;
        }
    }
    #region ui
    public void GameStateChange(string state)
    {
        GameMode = state;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
[System.Serializable]
public class ObjectData
{
    public string tag;
    public GameObject prefab;
}