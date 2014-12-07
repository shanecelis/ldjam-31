using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Damagable : MonoBehaviour {
  public int health;
  public AudioClip[] hurtSounds;
  public GameObject hurtUI;
  public GameObject restartButton;
  public Text healthText;
	// Use this for initialization
	void Start () {
    healthText.text = health.ToString();
	}

  void Cancel () {
    hurtUI.SetActive(false);
  }
	
	// Update is called once per frame
	void Update () {
	
	}

  public void Flash(float duration) {
    hurtUI.SetActive(true);
    Invoke("Cancel", duration);
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "Bee") {
      // Bee dies.
      Destroy(collision.transform.parent.gameObject);
      // Flash screen red.
      Flash(0.5f);
      // Play a sound.
      if (! audio.isPlaying) {
        audio.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
        audio.Play();
      }
      // Decrement health.
      health--;
      healthText.text = health.ToString();
      if (health <= 0) {
        // We died.
        hurtUI.SetActive(true);
        restartButton.SetActive(true);
        Time.timeScale = 0f;
      }
    }
  }

  public void Restart() {
    Time.timeScale = 1f;
    Application.LoadLevel(0);
  }
}
