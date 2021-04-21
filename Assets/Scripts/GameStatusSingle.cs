using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusSingle : MonoBehaviour
{
    private void Awake()
    {
        int objectCount = FindObjectsOfType<GameStatusSingle>().Length;

        if(objectCount > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
