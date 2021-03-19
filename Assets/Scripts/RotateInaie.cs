using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInaie : MonoBehaviour
{
    GameObject inaie;
    bool colidindo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotor = transform.rotation;
        if(Input.GetKeyDown(KeyCode.A) && colidindo == false)
        {
            rotor.y = -90;
            transform.localRotation = Quaternion.Euler(0, -90, 0);

        }
        if (Input.GetKeyDown(KeyCode.D) && colidindo == false)
        {
            rotor.y = 90;
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        if(colidindo == true)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }  

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Escalavel"))
        {
            print("colidiu parede");
            colidindo = true;           
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Escalavel"))
        {
            print("colidiu saiu parede");
            colidindo = false;
        }
    }
}
