using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	
	[SerializeField]
	GameObject bullet;
	Enemy enemy;
	float fireRate;
	float nextFire;
	float distance;
	public GameObject player;
	public float MaxDistance;
	// Use this for initialization
	void Start () {
		fireRate = 1f;
		nextFire = Time.time;
		enemy = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if (distance > 5 && distance< MaxDistance)
		{
			CheckIfTimeToFire();
		}
	}

	void CheckIfTimeToFire()
	{
		if (Time.time > nextFire) {
			Instantiate (bullet, transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
		}
	
	}


}
