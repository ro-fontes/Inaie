using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField]
    private Rigidbody flecha;
    [SerializeField]
    private Transform ponta;

    private float velo_flecha = 500f;
    private float firerate = 1f;
    private float proxTiro = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Mouse1) && Time.time >proxTiro)
        {
            proxTiro = Time.time + firerate;
            var spawnBullet = Instantiate(flecha, ponta.position, ponta.rotation);
            spawnBullet.AddForce(ponta.right * velo_flecha);
        }


        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
