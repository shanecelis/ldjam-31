using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {
  private bool open = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown("space")) {
        JointSpring spring = hingeJoint.spring;
        spring.targetPosition = open ? -90f : 0f;
        hingeJoint.spring = spring;
        open = !open;
    }
	}
}
