using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform arrastavel;
    bool pegou = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pegou == true && Input.GetKeyDown(KeyCode.E))
        {
            arrastavel.transform.parent = GameObject.Find("Mao").transform;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            arrastavel.transform.parent = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Arrastavel"))
        {
            pegou = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Arrastavel"))
        {
            pegou = false;
        }
    }
}
