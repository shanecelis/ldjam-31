using UnityEngine;
using System.Collections;

public class ApproachTarget : MonoBehaviour {

  public Transform target;
  public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
	}
}
