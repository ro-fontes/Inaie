using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float Timer = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer <=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Chao")|| other.gameObject.CompareTag("Caixa")|| other.gameObject.CompareTag("Agua"))
        {
            Destroy(gameObject);
        }//Sou deus
    }
}
