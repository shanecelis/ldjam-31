using UnityEngine;
using System.Collections;

public class SwarmMember : MonoBehaviour {
  public Vector3 velocity;
  private Swarm swarm;

	// Use this for initialization
	void Start () {
    swarm = Swarm.instance;
    swarm.members.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
    Vector3 v1 = Rule1();
    Vector3 v2 = Rule2();
    Vector3 v3 = Rule3();
    this.velocity += (v1 + v2 + v3) * Time.deltaTime;
    // Debug.Log("v1 " + v1);
    // Debug.Log("v2 " + v2);
    // Debug.Log("v3 " + v3);
    if (this.velocity.magnitude > swarm.maxVelocity) {
      this.velocity = this.velocity.normalized * swarm.maxVelocity;
    }
    this.transform.position += this.velocity * Time.deltaTime;
	}

  Vector3 AverageVelocityMinusSelf() {
    return (swarm.averageVelocity - velocity/swarm.members.Count)
      * swarm.members.Count/(swarm.members.Count - 1);
  }

  Vector3 CenterOfMassMinusSelf() {
    return (swarm.centerOfMass - transform.position/swarm.members.Count)
      * swarm.members.Count/(swarm.members.Count - 1);
  }

  Vector3 Rule1() {
    return (CenterOfMassMinusSelf() - transform.position)
      * swarm.matchPositionP;
  }

  Vector3 Rule2() {
    Vector3 c = Vector3.zero;
    foreach(SwarmMember member in swarm.members) {
      if (member == this)
        continue;
      if (Vector3.Distance(this.transform.position,
                           member.transform.position)
          < swarm.distanceThreshold) {
        c -= (member.transform.position - this.transform.position);
      }
    }
    return c;
  }

  Vector3 Rule3() {
    return (AverageVelocityMinusSelf() - velocity) * swarm.matchVelocityP;
    //return swarm.averageVelocity * swarm.matchVelocityP;
  }

  void OnDisable() {
    // Must use static incase Swarm has been destroyed before we have been.
    Swarm.Deregister(this);
  }

  void OnDrawGizmos() {
    Gizmos.DrawLine(transform.position, transform.position + this.velocity);
  }
}
