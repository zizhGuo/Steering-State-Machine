using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject obstaclePooler;
    public GameObject mainCharater;
    private NewObjectPoolerScript temp;

    void Start()
    {
        temp = obstaclePooler.GetComponent<NewObjectPoolerScript>();
    }
    void Fire()
    {
        for (int i = 0; i < temp.pooledAmount; i++)
        {
            if (!temp.pooledObjects[i].activeInHierarchy)
            {
                temp.pooledObjects[i].transform.position = mainCharater.transform.position;
                temp.pooledObjects[i].transform.rotation = mainCharater.transform.rotation;
                temp.pooledObjects[i].SetActive(true);
                break;
            }
            if (temp.pooledObjects[i] == null) return;
        }
       
        //pooledObstacles.Add(obj);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Fire();
        }
        for (int i = 0; i < temp.pooledAmount; i++)
        {
            if (temp.pooledObjects[i].activeInHierarchy)
            {
                Debug.DrawLine(temp.pooledObjects[i].transform.position, mainCharater.transform.position, Color.red);
            }
        }
    }
}
