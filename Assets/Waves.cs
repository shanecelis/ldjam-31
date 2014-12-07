using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Waves : MonoBehaviour {
  private Spawner spawner;
  private Swarm swarm;
  private int slope = 10;
  private int offset = 0;
  private int wave = 0;
  public Text waveText;

	// Use this for initialization
	void Start () {
	    swarm = Swarm.instance;
      spawner = GetComponent<Spawner>();
      NextWave();
	}
	
	// Update is called once per frame
	void Update () {
    if (swarm.members.Count == 0) {
      // Start next wave.
      //Invoke("NextWave", 0.5f);
      NextWave();
      // Show text for wave.
    }
	}

  IEnumerator ShowWave() {
    waveText.text = "Wave: " + wave;
    waveText.gameObject.SetActive(true);
    yield return new WaitForSeconds(1f);
    //waveText.text = "";
    waveText.gameObject.SetActive(false);
  }

  public void NextWave() {
    wave++;
    StartCoroutine(ShowWave());
    spawner.spawnCount = slope * wave + offset;
    spawner.StartSpawn();
  }
  
  
}
