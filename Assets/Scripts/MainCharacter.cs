using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Edges {
	RIGHT, LEFT, TOP, BOTTOM
}
public class MainCharacter : MonoBehaviour {
	public Camera cam;
	public GameObject mainObj;
	public BulletAgent[] bullet = new BulletAgent[2];
	public List<AudioSource> waveMusic = new List<AudioSource>(4); 
	private float[] sceneBounds = new float[4];
	private float switchDelay;
	private float buttonFlag;
	public float horizontalSpeed = 5.0f;
	public float verticalSpeed = 10.5f;
	private string movementPlayer = "P1";
	private string shootyPlayer = "P2";
	public bool facingRight = true;
	private int waveCount;
    public int scoreCount;
    private NeonScript neon;
    private NoirScript noir;
	private bool firstWave = true;

	// Use this for initialization
	void Start () {
		sceneBounds[(int)Edges.LEFT] = -95.0f;
		sceneBounds[(int)Edges.RIGHT] = 95.0f;
		sceneBounds[(int)Edges.TOP] = -4.0f;
		sceneBounds[(int)Edges.BOTTOM] = 19.0f;
        scoreCount = 0;
        waveCount = 0;
        neon = this.GetComponentInChildren<NeonScript>();
        noir = this.GetComponentInChildren<NoirScript>();
    }

	void CheckBounds() {
		Vector3 pos = mainObj.transform.localPosition;

		if (pos.x < sceneBounds[(int)Edges.LEFT]) {
			mainObj.transform.localPosition = new Vector3(
				sceneBounds[(int)Edges.LEFT], 
				mainObj.transform.localPosition.y, 
				mainObj.transform.localPosition.z
			);
		}
		else if (pos.x > sceneBounds[(int)Edges.RIGHT]) {
			mainObj.transform.localPosition = new Vector3(
				sceneBounds[(int)Edges.RIGHT], 
				mainObj.transform.localPosition.y, 
				mainObj.transform.localPosition.z
			);
		}
		if (pos.y < sceneBounds[(int)Edges.TOP]) {
			mainObj.transform.localPosition = new Vector3(
				mainObj.transform.localPosition.x,
				sceneBounds[(int)Edges.TOP], 
				mainObj.transform.localPosition.z
			);
		}
		else if (pos.y > sceneBounds[(int)Edges.BOTTOM]) {
			mainObj.transform.localPosition = new Vector3(
				mainObj.transform.localPosition.x, 
				sceneBounds[(int)Edges.BOTTOM], 
				mainObj.transform.localPosition.z
			);
		}
		Debug.Log(mainObj.transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
		// update switch delay
		switchDelay += Time.deltaTime;
		// keyboard input
		if (Input.GetAxisRaw("Horizontal (" + movementPlayer + ")") > 0) {
			mainObj.transform.Translate(
				Vector3.right * Time.deltaTime * horizontalSpeed);
		}
		if (Input.GetAxisRaw("Horizontal (" + movementPlayer + ")") < 0) {
			mainObj.transform.Translate(
				Vector3.left * Time.deltaTime * horizontalSpeed);
		}
		if (Input.GetAxisRaw("Vertical (" + movementPlayer + ")") > 0) {
			mainObj.transform.Translate(
				Vector3.down * Time.deltaTime * verticalSpeed);
		}
		if (Input.GetAxisRaw("Vertical (" + movementPlayer + ")") < 0) {
			mainObj.transform.Translate(
				Vector3.up * Time.deltaTime * verticalSpeed);
		}
		if (Input.GetButtonDown("Fire1 (" + shootyPlayer + ")")) { 
			Vector3 temp = new Vector3(
				transform.position.x + 3.0f, 1.2f, transform.position.z);
            if(shootyPlayer == "P2")
            {
                neon.neonShoot();
            }
            else
            {
                noir.noirShoot();
            }
			var bulletSelect = (shootyPlayer == "P2") ? 1 : 0;
			BulletAgent _bullet = Instantiate<BulletAgent>(bullet[bulletSelect]);
			_bullet.Create(temp, Vector3.right);
		}

		// keep within screen
		CheckBounds();
	}

	void OnTriggerEnter(Collider other) {
		// enemy recognition
		if (other.name.Contains("Enemy_") || other.name.Contains("Bullet_")) {
			GameObject ui = GameObject.Find("UI");
			if (ui) {
				//Set time.timescale to 0, this will cause animations and physics to stop updating
				Time.timeScale = 0;
				ShowPanels panel = ui.GetComponent<ShowPanels>();
				panel.ShowGameOver();
			} 
		}
		else {
			SwapPlayers();
            
		}
		// play wave music
		if (firstWave) {
			firstWave = false;
		}
		else {
			if (other.name.Contains("WaveType_1")) {
				waveMusic[0].volume = 1.0f;
				waveMusic[1].volume = 0.0f;
				waveMusic[2].volume = 0.0f;
				waveMusic[3].volume = 0.0f;
			}
			if (other.name.Contains("WaveType_2")) {
				waveMusic[0].volume = 0.0f;
				waveMusic[1].volume = 1.0f;
				waveMusic[2].volume = 0.0f;
				waveMusic[3].volume = 0.0f;
			}
			if (other.name.Contains("WaveType_3")) {
				waveMusic[0].volume = 0.0f;
				waveMusic[1].volume = 0.0f;
				waveMusic[2].volume = 1.0f;
				waveMusic[3].volume = 0.0f;
			}
			if (other.name.Contains("WaveType_4")) {
				waveMusic[0].volume = 0.0f;
				waveMusic[1].volume = 0.0f;
				waveMusic[2].volume = 0.0f;
				waveMusic[3].volume = 1.0f;
			}
		}
	}

	void OnTriggerExit(Collider other) {
        if(other.GetType() == typeof(WaveAgent))
        {
            wavePassed();
			
        }
		// play wave music
		if (other.name.Contains("WaveType_1")) {
			waveMusic[0].volume = 1.0f;
			waveMusic[1].volume = 0.0f;
			waveMusic[2].volume = 0.0f;
			waveMusic[3].volume = 0.0f;
		}
		if (other.name.Contains("WaveType_2")) {
			waveMusic[0].volume = 0.0f;
			waveMusic[1].volume = 1.0f;
			waveMusic[2].volume = 0.0f;
			waveMusic[3].volume = 0.0f;
		}
		if (other.name.Contains("WaveType_3")) {
			waveMusic[0].volume = 0.0f;
			waveMusic[1].volume = 0.0f;
			waveMusic[2].volume = 1.0f;
			waveMusic[3].volume = 0.0f;
		}
		if (other.name.Contains("WaveType_4")) {
			waveMusic[0].volume = 0.0f;
			waveMusic[1].volume = 0.0f;
			waveMusic[2].volume = 0.0f;
			waveMusic[3].volume = 1.0f;
		}
	}

	public void SwapPlayers()
	{
		if (switchDelay > 1.0f) {
			if(movementPlayer == "P1")
			{
				movementPlayer = "P2";
				shootyPlayer = "P1";
			}
			else
			{
				movementPlayer = "P1";
				shootyPlayer = "P2";
			}
			this.transform.Rotate(0, 0, 180);
			switchDelay = 0.0f;
		}
		
	}

    public void threatNeutralized()
    {
        scoreCount = scoreCount + (10 + ( 10 * waveCount));
    }

    public void wavePassed()
    {
        //wave.passed = true;
        waveCount++;
        scoreCount = scoreCount + (100 * waveCount);
        SwapPlayers();
    }
}
