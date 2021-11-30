using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAgent : MonoBehaviour {
	private Vector3 waveStart = new Vector3(30.0f, -3.9f, -9.0f);
	private Vector3 myForward = new Vector3(-10.0f, -0.0001f, 0.0f);
	private bool advancing = false;
	private float speed;
	private Rigidbody rigidB;
    public bool passed = false;
    private MainCharacter mainChar;

	// Call method to have wave begin entering frame
	public void Enter(MainCharacter getChar, float _speed = 1.0f) {
		rigidB = gameObject.GetComponent<Rigidbody>();
		advancing = true;
		speed = _speed;
		transform.localPosition = waveStart;
        mainChar = getChar;
		// get wave component
	}

	public void Advance() {
		if (advancing) {
			rigidB.MovePosition(transform.position + myForward * Time.deltaTime * speed);
			//transform.position = Vector3.Lerp(transform.position, waveEnd, Time.deltaTime * speed);
		}
		if (transform.localPosition.x < 0.5f) {
			advancing = false;
			transform.localPosition = new Vector3(0.0f, -4f, -9.0f); 
		}
	}

	public float currentPos() {
		return transform.position.x;
	}

	public bool IsDone() {
		return !advancing;
	}

	public void Disappear() {
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
