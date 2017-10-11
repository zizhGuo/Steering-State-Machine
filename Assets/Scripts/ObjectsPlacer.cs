using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPlacer : MonoBehaviour
{
    public GameObject obstaclePooler;
    public GameObject chickenPooler;
    public GameObject wolfiePooler;

    private NewObjectPoolerScript rock;
    private NewObjectPoolerScript chicken;
    private NewObjectPoolerScript wolfie;

    Camera mainCamera;
    Vector3 mousePos;
    bool _hit;
    RaycastHit hit;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        rock = obstaclePooler.GetComponent<NewObjectPoolerScript>();
        chicken = chickenPooler.GetComponent<NewObjectPoolerScript>();
        wolfie = wolfiePooler.GetComponent<NewObjectPoolerScript>();
    }
    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        _hit = Physics.Raycast(ray, out hit);
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < rock.pooledAmount; i++)
            {
                if (!rock.pooledObjects[i].activeInHierarchy)
                {
                    rock.pooledObjects[i].transform.position = new Vector3(hit.point.x, 0f, hit.point.z); // hit.transform.position;
                    rock.pooledObjects[i].SetActive(true);
                    break;
                }
                if (rock.pooledObjects[i] == null) return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            for (int i = 0; i < chicken.pooledAmount; i++)
            {
                if (!chicken.pooledObjects[i].activeInHierarchy)
                {
                    chicken.pooledObjects[i].transform.position = new Vector3(hit.point.x, 0f, hit.point.z); // hit.transform.position;
                    chicken.pooledObjects[i].SetActive(true);
                    break;
                }
                if (chicken.pooledObjects[i] == null) return;
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            for (int i = 0; i < wolfie.pooledAmount; i++)
            {
                if (!wolfie.pooledObjects[i].activeInHierarchy)
                {
                    wolfie.pooledObjects[i].transform.position = new Vector3(hit.point.x, 0f, hit.point.z); // hit.transform.position;
                    wolfie.pooledObjects[i].SetActive(true);
                    break;
                }
                if (rock.pooledObjects[i] == null) return;
            }
        }
    }
}
