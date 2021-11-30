using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAgent : MonoBehaviour {
	public BulletAgent bullet;
	private Rigidbody rigidB;
	private Animator anim;
	private int shotType;
	private float speed; 
	private float lastShotTaken, shootInterval; 
	private bool dead = false, bulletDeath = false;
    private MainCharacter mainChar;
	
	public void Init(Vector3 pos, float _speed, int _shotType, float interval, MainCharacter getPlayer) {
		speed = _speed;
		shotType = _shotType;
		shootInterval = interval;
		transform.localPosition = pos;
		rigidB = gameObject.GetComponent<Rigidbody>();
		anim = gameObject.GetComponentInChildren<Animator>();
        mainChar = getPlayer;
	}

	public void TakeShot(float deltaTime) {
		lastShotTaken += deltaTime;
		if (!dead && lastShotTaken >= shootInterval) {
			Shoot();

			lastShotTaken = 0.0f;
		}
	}

	void Shoot() {
		Vector3 temp = new Vector3(
				transform.position.x - 3.0f, 1.2f, transform.position.z);
		//BulletAgent _bullet = Instantiate<BulletAgent>(bullet);
		//_bullet.Create(temp);

		switch (shotType)
		{
			case 0:
			{
				// fire 3 straight line
				var _speed = Random.Range(8.0f, 15.0f);
				BulletAgent _bullet = Instantiate<BulletAgent>(bullet);
				_bullet.Create(temp, Vector3.left ,1, _speed);
			}
				break;
			case 1:
			{
				// fire 3 diagonal
				Vector3 top = Quaternion.AngleAxis(10.0f, Vector3.up) * Vector3.left;
				Vector3 bot = Quaternion.AngleAxis(-10.0f, Vector3.up) * Vector3.left;
				BulletAgent _bullet = Instantiate<BulletAgent>(bullet);
				BulletAgent _bullet2 = Instantiate<BulletAgent>(bullet);
				BulletAgent _bullet3 = Instantiate<BulletAgent>(bullet);
				
				_bullet.Create(temp, top, 1, 10.0f);
				_bullet2.Create(temp, Vector3.left ,1, 10.0f);
				_bullet3.Create(temp, bot, 1, 10.0f);
			}
				break;
			case 2:
			{
				// fire 5 star
				var _speed = 5f;
				var _speed2 = 5.0f;
				Vector3 bot1 = Quaternion.AngleAxis(-60.0f, Vector3.up) * Vector3.left;
				Vector3 bot2 = Quaternion.AngleAxis(-120.0f, Vector3.up) * Vector3.left;
				BulletAgent _bullet = Instantiate<BulletAgent>(bullet);
				BulletAgent _bullet2 = Instantiate<BulletAgent>(bullet);
				BulletAgent _bullet3 = Instantiate<BulletAgent>(bullet);
				BulletAgent _bullet4 = Instantiate<BulletAgent>(bullet);
				BulletAgent _bullet5 = Instantiate<BulletAgent>(bullet);
				
				_bullet.Create(temp, Vector3.forward, 1, _speed2);
				_bullet2.Create(temp, Vector3.left ,1, _speed);
				_bullet3.Create(temp, Vector3.right, 1, _speed);
				_bullet4.Create(temp, bot1, 1, _speed2);
				_bullet5.Create(temp, bot2, 1, _speed2);
			}
				break;
			default:
				break;
		}
	} 

	public void Advance() {
		rigidB.MovePosition(transform.position + Vector3.left * Time.deltaTime * speed);
	}

	void OnTriggerEnter(Collider other) {
        // enemy recognition
        if (other.name == "Bound_L") {
            dead = true;
        }
		else if (other.name.Contains("Player_Bullet")) {
			dead = true;
			bulletDeath = true;
			gameObject.SetActive(false);
            mainChar.threatNeutralized();
		}
    }

	public bool MustDestroy() {
		return dead;
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
