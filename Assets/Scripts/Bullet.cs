using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float moveSpeed = 7f;

	Rigidbody rb;

	Player target;
	Player player;
	Vector2 moveDirection;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		target = GameObject.FindObjectOfType<Player>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector3 (moveDirection.x, moveDirection.y,transform.position.z);
		Destroy (gameObject, 3f);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name.Equals ("Player")) 
		{
			Debug.Log ("Hit!");
			Destroy (gameObject);
		}

	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Chao"))
		{
			Destroy(gameObject);
		}
		if (other.gameObject.CompareTag("Caixa"))
		{
			Destroy(gameObject);
		}
		if (other.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
