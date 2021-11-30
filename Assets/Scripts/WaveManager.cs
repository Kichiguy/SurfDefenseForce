using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WaveManager : MonoBehaviour {
	public MainCharacter mainChar;
	public List<GameObject> waveTypes = new List<GameObject>(4);
	private List<WaveAgent> waveAgents = new List<WaveAgent>();
	public List<AudioSource> drums = new List<AudioSource>(7); 
	private bool changeFloor; 
	private AudioSource musicComponent;	
	private int index = 0; 
	private int musicIndex = 0;
	private float lastWave = 0.0f;
	public float waveInterval;
	public float waveSpeed;
	// Use this for initialization
	void Start () {
		// Play drums normal first
		drums[0].volume = 1.0f;

		// Start wave
		WaveAgent wave = CreateWave(0);
		waveAgents.Add(wave);

		// create main character
		//Instantiate<GameObject>
	}

	// instantiate wave object
	WaveAgent CreateWave(int type) {
		GameObject obj = Instantiate<GameObject>(waveTypes[type]);
		obj.transform.parent = transform;
		return obj.GetComponent<WaveAgent>();
	}

	// wave generator
	void GenerateWave(float timeBetweenWaves) {
		if (lastWave >= timeBetweenWaves) {
			index = (index + 1) % waveTypes.Count;

			WaveAgent newWave = CreateWave(index);
			newWave.Enter(mainChar, waveSpeed);
			waveAgents.Add(newWave);

			lastWave = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// update wave time
		lastWave += Time.deltaTime;
		// floor wave generation
		GenerateWave(waveInterval);
		// wave advance and change
		foreach (WaveAgent wave in waveAgents) {
			wave.Advance();
		}
		if (waveAgents.Count > 1) {
			// delete old wave
			if(waveAgents[1].IsDone()) {
				waveAgents[0].Disappear();
				waveAgents.RemoveAt(0);
			}
		}
		
	}
}
