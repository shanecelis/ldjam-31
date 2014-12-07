using UnityEngine;
using System.Collections;

public class KillOnContact : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "Kill Surface") {
      // Play a sound.
      // Stop the following behavior.
      Destroy(GetComponent<SpringJoint>());
      rigidbody.useGravity = true;
      // Multiple sounds would be good.
      if (! audio.isPlaying)
        audio.Play();
      Invoke("KillYourselfAndYourParent", 2f);
    }
  }

  void KillYourselfAndYourParent() {
    Destroy(transform.parent.gameObject);
  }
}
