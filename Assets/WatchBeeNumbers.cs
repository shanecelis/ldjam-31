using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WatchBeeNumbers : MonoBehaviour {

  Swarm swarm;
  Text text;
	// Use this for initialization
	void Start () {
    swarm = Swarm.instance;
    text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
    text.text = swarm.members.Count.ToString();
	}
}
