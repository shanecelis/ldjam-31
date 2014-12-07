using UnityEngine;
using System.Collections;

public class SwarmMember : MonoBehaviour {
  public Vector3 velocity;
  public string swarmName = "Swarm";
  private Swarm swarm;

	// Use this for initialization
	void Start () {
    swarm = Swarm.GetSwarm(swarmName);
    swarm.members.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
    Vector3 v1 = Rule1();
    Vector3 v2 = Rule2();
    Vector3 v3 = Rule3();
    Vector3 v4 = Rule4();
    Vector3 v5 = Rule5();
    this.velocity += (v1 + v2 + v3 + v4 + v5) * Time.deltaTime;
    // Debug.Log("v1 " + v1);
    // Debug.Log("v2 " + v2);
    // Debug.Log("v3 " + v3);
    if (this.velocity.magnitude > swarm.maxVelocity) {
      this.velocity = this.velocity.normalized * swarm.maxVelocity;
    }
    this.transform.position += this.velocity * Time.deltaTime;
	}

  Vector3 FixAverage(Vector3 average, int count, Vector3 removeThis) {
    if (count > 1)
      return (average - removeThis/count) * count / (count - 1);
    else
      return Vector3.zero;
  }

  Vector3 AverageVelocityMinusSelf() {
    return FixAverage(swarm.averageVelocity, swarm.members.Count, velocity);
  }

  Vector3 CenterOfMassMinusSelf() {
    return FixAverage(swarm.centerOfMass, swarm.members.Count, transform.position);
  }

  // Towards center.
  Vector3 Rule1() {
    return (CenterOfMassMinusSelf() - transform.position)
      * swarm.matchPositionP;
  }

  // Avoid collisions.
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

  // Match average velocity.
  Vector3 Rule3() {
    return (AverageVelocityMinusSelf() - velocity) * swarm.matchVelocityP;
    //return swarm.averageVelocity * swarm.matchVelocityP;
  }

  // Move towards attractants.
  Vector3 Rule4() {
    if (swarm.averagePositionAttractors != null) 
      return (swarm.averagePositionAttractors.Value - transform.position).normalized * swarm.attractorSpeed;
    else
      return Vector3.zero;
  }

  Vector3 Rule5() {
    if (swarm.averagePositionDetractors != null) 
      return -(swarm.averagePositionDetractors.Value - transform.position).normalized * swarm.detractorSpeed;
    else
      return Vector3.zero;
  }

  void OnDisable() {
    // Must use static incase Swarm has been destroyed before we have been.
    Swarm.Deregister(this);
  }

  void OnDestroy() {
    // Must use static incase Swarm has been destroyed before we have been.
    Swarm.Deregister(this);
  }


  void OnDrawGizmos() {
    Gizmos.DrawLine(transform.position, transform.position + this.velocity);
  }
}
