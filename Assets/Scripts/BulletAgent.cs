using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct BulletType {
	
}
public class BulletAgent : MonoBehaviour {
	private int shooterID;
	private float speed;
	private Rigidbody rigidB;
	private EnemyManager enemies;
	private Vector3 dir;
	// Use this for initialization
	void Start () {}
	
	public void Create(Vector3 pos, Vector3 _dir, int ID = 0, float _speed = 10.0f) {
		transform.localPosition = pos;
		speed = _speed;
		shooterID = ID;
		dir = _dir;
		enemies = GetComponent<EnemyManager>();
		rigidB = gameObject.GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other) {
		if (shooterID == 0) {
			// main player
			if (other.name == "Bound_R" || other.name.Contains("Enemy")) {
				Destroy(gameObject);
			}
			if (other.name.Contains("Bullet_")) {
				Destroy(gameObject);
			}
		} 
		else {
			// enemy
			if (other.name == "Bound_L" || other.name.Contains("MainChar")) {
				Destroy(gameObject);
			}
			if (other.name.Contains("Player_Bullet")) {
				Destroy(gameObject);
			}
		}
    }

	// Update is called once per frame
	void Update () {
		rigidB.MovePosition(transform.position + dir * Time.deltaTime * speed);
	}
}
