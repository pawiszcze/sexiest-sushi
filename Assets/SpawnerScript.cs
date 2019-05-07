using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject spawnedEnemy;
    GameObject myLilBaby;
    public bool isBabyDead;
    bool lostABaby;

    // Start is called before the first frame update
    void Start()
    {
       myLilBaby = Instantiate(spawnedEnemy, gameObject.transform);
        myLilBaby.GetComponent<FoeScript>().parent = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBabyDead)
        {
            myLilBaby = Instantiate(spawnedEnemy, gameObject.transform);
            myLilBaby.GetComponent<FoeScript>().parent = gameObject;
            myLilBaby.GetComponent<FoeScript>().expValue = 0;
            myLilBaby.GetComponent<SpriteRenderer>().sprite = myLilBaby.GetComponent<FoeScript>().spent;
            isBabyDead = false;
        }
    }
}
