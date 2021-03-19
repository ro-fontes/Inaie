using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Animator anim;
    bool isisprinting = false;
    bool Escalando = false;
    bool Pulo = false;

    //enemy
    public float combate_enemy;
    Enemy enemy;

    //camera
    public Camera Camera;
    public int PowerUpCamera = 0;

    //roupa
    bool roupaAgua = false;
    int Life_Roupa_Agua = 10;


    //movement
    Rigidbody rb;
    bool jumpB = false;
    int jump;
    public float jumpForce;
    public float speed;
    public float forceScale = 1;
    public float _NEWforceScale = 1;
    //timers 
    public float timer_Roupa = 3, Timer_Speed = 5, Timer_Combat = 3, Timer_Camera = 5;

    //arrastavel
    public Transform Arrastavel;

    //hp
    public float Life_Player = 100f;
    public Image life_player_Bar;
    public static float life;
    public HealthBar healthbar;

    bool isGrounded = false;
    bool escalavel = false;

    //Roupa Invisivel
    public static bool Roupa = false;
    public static bool invisivel = false;
    
    //Grappling hook
    public static bool hook = false;
    private Vector3 grapplePoint;
    public LayerMask WhatIsGrappleable;
    public Transform GrappleOrigin, player;
    private Vector3 HookShotPosition;

    private float MaxDistance = 100f;

    //bar_itens
    /*public GameObject BG_PW_Speed;
    public GameObject BG_PW_CAMERA;
    public GameObject BG_PW_Roupa;*/
    public GameObject Cvictory;
    public GameObject Arco;

    /*public Image PW_Barra_Speed;
    public Image PW_Barra_Camera;
    public Image PW_Barra_Roupa;*/



    float Mat = 1000f;
    bool speedB = false;
    bool cameraA = false;

    //loadvictory
    MainMenu MainMenuScr;

    private State state;

    private enum State
    {
        Normal,
        HookShotFlyingPlayer
    }
    void Start()
    {
        healthbar.SetMaxHealth(Life_Player);
        state = State.Normal;
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        /*BG_PW_Speed.SetActive(false);
        BG_PW_CAMERA.SetActive(false);
        BG_PW_Roupa.SetActive(false);*/
        Cvictory.SetActive(false);
        //life_player_Bar = GetComponent<Image>();         
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)&& isGrounded == true)
        {
            isisprinting = true;
            anim.SetBool("isSprinting", isisprinting);
        }
        else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isisprinting = false;
            anim.SetBool("isSprinting", isisprinting);
        }

        life_player_Bar.fillAmount = Life_Player / life;
        switch (state)
        {
            default:
            case State.Normal:
                Movem();
                Jump();
                Powerspeed();
                PowerCamera();
                HandleHookShotStart();
                break;
            case State.HookShotFlyingPlayer:
                HandleHookShotMovement();
                break;

        }
    }
    void Movem()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true || Input.GetKeyDown(KeyCode.Space) && jumpB == true && jump>0 )
        {
            Pulo = true;
            anim.SetBool("Pulo", Pulo);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            jump = jump - 1;
        }
        else
        {
            Pulo = false;
            anim.SetBool("Pulo", Pulo);
        }
    }
    void roupa()
    {
        if(Roupa == true)
        {
            timer_Roupa -= Time.deltaTime;
            invisivel = true;
            //BG_PW_Roupa.SetActive(true);
            if (timer_Roupa <= 0)
            {
                timer_Roupa = 3;
                Roupa = false;
                invisivel = false;
               // BG_PW_Roupa.SetActive(false);
            }
        }
    }
    void Powerspeed()
    {
        if(speedB == true && Input.GetKey(KeyCode.Alpha1))
        {
            speed = 10;
            jumpForce = 7;
            Timer_Speed -= Time.deltaTime;
            //PW_Barra_Speed.fillAmount -= Timer_Speed / Mat;
            //BG_PW_Speed.SetActive(true);
            if (Timer_Speed<=0)
            {
                speedB = false;
                //BG_PW_Speed.SetActive(false);
            }
        }
    }
    void PowerCamera()
    {
        
        if (Input.GetKey(KeyCode.Alpha2) && cameraA == true)
        {
            //BG_PW_CAMERA.SetActive(true);
            //PW_Barra_Camera.fillAmount -= Timer_Camera / Mat;
            Timer_Camera -= Time.deltaTime;
            Camera.fieldOfView = PowerUpCamera;

        }
        if (Timer_Camera <= 0)
        {
            cameraA = false;
            Camera.fieldOfView = 60;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) || cameraA == false)
        {
            Camera.fieldOfView = 60;
        }
    }
    void GrapplingHook()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3) && hook == true)
        {
            HandleHookShotStart();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3) && hook == true)
        {
           
        }
    }
    private void HandleHookShotStart()
    {
        if (Input.GetKeyDown(KeyCode.E) && hook == true)
        {
            Debug.Log("Apertou E");
            if(Physics.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition), Vector3.zero, out RaycastHit raycastHit, MaxDistance, WhatIsGrappleable))
            {
                Debug.Log("Acertou o raycast");
                //Hit Something
                HookShotPosition = raycastHit.point;
                state = State.HookShotFlyingPlayer;
            }
        }
            
        
    }
    private void HandleHookShotMovement()
    {
        Debug.Log("Foi em direção ao lugar");
        rb.useGravity = false;
        Vector3 HookshotDir = (HookShotPosition - transform.position).normalized;

        Vector3.MoveTowards(player.position, HookShotPosition, 0f);

        rb.useGravity = true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp_Jump"))
        {
            jumpB = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PowerUp_Speed"))
        {          
            speedB = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Roupa_Azul"))
        {
            roupaAgua = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PowerUp_Camera"))
        {
            cameraA = true;
            Destroy(other.gameObject);
            Timer_Camera = 5;
        }
        if(other.gameObject.CompareTag("Bullet"))
        {
            Life_Player = Life_Player - 10;
            healthbar.SetHealth(Life_Player);
            if (Life_Player<=0)
            {
                Destroy(gameObject);
                print("morreu");
            }
           
        }
        if (other.gameObject.CompareTag("GrapplingHook"))
        {
            hook = true;
        }
        if(other.gameObject.CompareTag("Portal"))
        {
            print("colidiu");          
            Cvictory.SetActive(true);
            
        }
        if (other.gameObject.CompareTag("Arco"))
        {
            print("colidiu");
            Arco.SetActive(true);
            Destroy(other.gameObject);
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Agua"))
        {
            print("entrou na agua");
            if (roupaAgua == true)
            {
                print("roupa ativada");
                timer_Roupa -= Time.deltaTime;
                print(timer_Roupa);
                //PW_Barra_Roupa.fillAmount -= timer_Roupa / Mat;
                if (timer_Roupa < 0)
                {
                    
                    Life_Roupa_Agua = Life_Roupa_Agua - 1;
                    timer_Roupa = 3;
                    if (Life_Roupa_Agua <= 0)
                    {
                        roupaAgua = false;
                    }
                    
                   
                }
            }

            if (roupaAgua == false)
            {
                timer_Roupa -= Time.deltaTime;
                if (timer_Roupa < 0)
                {
                    Life_Player = Life_Player - 1;
                    healthbar.SetHealth(Life_Player);
                    timer_Roupa = 3;
                }
                if (Life_Player == 0)
                {
                    Destroy(gameObject);
                    print("MORREU");
                }
            }
        }

        if (other.gameObject.CompareTag("Escalavel"))
        {
            escalavel = true;
            
            if (Input.GetKey(KeyCode.W) && escalavel == true)
            {
                Escalando = true;
                anim.SetBool("Escalando", Escalando);
                rb.drag = 2f;
                rb.useGravity = false;
                float y = Input.GetAxis("Vertical");
                float moveBy3 = y * forceScale;
                rb.velocity = new Vector2(rb.velocity.x, forceScale);
            }
            if (Input.GetKey(KeyCode.S) && escalavel == true)
            {
                Escalando = true;
                anim.SetBool("Escalando", Escalando);
                rb.drag = 2f;
                rb.useGravity = false;
                float y = Input.GetAxis("Vertical");
                float moveBy3 = y * forceScale;
                rb.velocity = new Vector2(rb.velocity.x, -forceScale);
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Timer_Combat -= Time.deltaTime;
            if (Timer_Combat <= 0)
            {
                Life_Player = Life_Player - combate_enemy;
                Timer_Combat = 3;
                healthbar.SetHealth(Life_Player);
                if (Life_Player <= 0)
                {
                    print("morreu");
                    Destroy(gameObject);
                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Escalavel"))
        {
            Escalando = false;
            anim.SetBool("Escalando", Escalando);
            escalavel = false;
            rb.useGravity = true;
            rb.drag = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            isGrounded = true;
            jump = 2;
        }

        if (collision.gameObject.CompareTag("PowerUp_Roupa"))
        {
            Roupa = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PowerUp_Escalada"))
        {           
            Destroy(collision.gameObject);
            forceScale = _NEWforceScale;
        }
    }
  
}


