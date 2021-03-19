using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public Transform[] waypoints;

	public int Life_enemy = 100;

	public float moveSpeed = 2f;

	int waypointIndex = 0;
	public int CombatPlAYER;
	public int DanoFlecha;

	public float jumpForce;

	float distance;
	public GameObject player, enemy;

	public Transform target;

	public float raycast_D;

	bool Patrol = true;
	bool combate = false;
	bool distancia = false;


	Player Player_script;

	void Start()
	{
		
	}
	void Update()
	{

		if(combate == true && Input.GetKeyDown(KeyCode.Mouse0))
		{
			Life_enemy -= CombatPlAYER;
			print("Combat");
			print(Life_enemy);
			if (Life_enemy <= 0)
			{
				Destroy(gameObject);
			}
		}
		if (Patrol == true && distancia==false || Player.invisivel == true && distancia == false)
		{
			Move();
		}
		if (Patrol == false && distancia == false)
		{
			follow();
		}
		if(distance < 3)
		{
			distancia = true;

		}
		if (distance > 3)
		{
			distancia = false;
		}
		distance = Vector3.Distance(player.transform.position, enemy.transform.position);
	}
	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

		if (transform.position == waypoints[waypointIndex].transform.position)
		{
			waypointIndex += 1;
		}

		if (waypointIndex == waypoints.Length)
			waypointIndex = 0;
	}
	void follow()
	{
		if (distance >= 1)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && Player.Roupa == false)
		{
			Patrol = false;
		}
		if (other.gameObject.CompareTag("Flecha"))
		{
			Life_enemy = Life_enemy - DanoFlecha;
			if (Life_enemy < 0)
			{
				Destroy(gameObject);
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		{
			Patrol = true;
		}
		if (other.gameObject.CompareTag("Player"))
		{
			combate = false;
			print("saiu");
		}
	}
private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			combate = true;
			print("Player");
		}
		
	}
}


