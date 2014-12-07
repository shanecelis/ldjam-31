using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
  public GameObject prefab;
  public int spawnCount = 10;
  public float waitBetweenSpawns = 0.2f;

	// Use this for initialization
	void Start () {
    //StartSpawn();
	}
	
	// Update is called once per frame
	void Update () {
    
	}

  public void StartSpawn() {
    StartCoroutine(SpawnOnInterval());
  }

  IEnumerator SpawnOnInterval() {
    while (spawnCount > 0) {
      SpawnThing();
      spawnCount--;
      yield return new WaitForSeconds(waitBetweenSpawns);
    }
  }

  void SpawnThing() {
    Instantiate(prefab, 
                transform.position,
                Quaternion.identity);
  }
}
