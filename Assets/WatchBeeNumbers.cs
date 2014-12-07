using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WatchBeeNumbers : MonoBehaviour {

  public string swarmName = "Swarm";
  Swarm swarm;
  Text text;
	// Use this for initialization
	void Start () {
    swarm = Swarm.GetSwarm(swarmName);
    text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
    text.text = swarm.members.Count.ToString();
	}
}
