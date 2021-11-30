using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum EBType {
	STR8, SPREAD, STAR
}
enum EType {
	MER, JINN, IFRT
}

public class EnemyManager : MonoBehaviour {
	public List<GameObject> enemies = new List<GameObject>();
	public List<EnemyAgent> spawnedEnemies = new List<EnemyAgent>();
	public float waveMult = 4.0f;
    public MainCharacter mainChar;
	private float lastEnemySpawned;
	private int index = 0;
	// Use this for initialization
	void Start () {
		
	}

	public void SpawnEnemy(float interval) {
		interval /= waveMult;
		if (lastEnemySpawned >= interval) {
			index = (index + 1) % enemies.Count;
			GameObject agent = Instantiate<GameObject>(enemies[index]);
			agent.transform.parent = transform;
			EnemyAgent eAgent = agent.GetComponent<EnemyAgent>();
			spawnedEnemies.Add(eAgent);
			// randomized position
			var randomZ = Random.Range(-13.0f, 9.0f);
			switch (index)
			{
				case (int)EType.MER:
					eAgent.Init(new Vector3(15.0f, 0.0f, randomZ), waveMult, (int)EBType.SPREAD, 4.0f, mainChar);
					break;
				case (int)EType.JINN:
					eAgent.Init(new Vector3(15.0f, 0.0f, randomZ), waveMult, (int)EBType.STAR, 4.0f, mainChar);
					break;
				case (int)EType.IFRT:
					eAgent.Init(new Vector3(15.0f, 0.0f, randomZ), waveMult, (int)EBType.STR8, 4.0f, mainChar);
					break;
				default:
					break;
			}

			lastEnemySpawned = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		lastEnemySpawned += Time.deltaTime;
		SpawnEnemy(10f); 
		foreach (EnemyAgent enemy in spawnedEnemies) {
			enemy.Advance();
			enemy.TakeShot(Time.deltaTime);
		}
		// enemy destroyed near the end
		if (spawnedEnemies.Count > 0 && spawnedEnemies[0].MustDestroy()){
			Destroy(spawnedEnemies[0].gameObject);
			spawnedEnemies.RemoveAt(0);
		}
	}
}
