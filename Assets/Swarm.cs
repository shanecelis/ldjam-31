using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Swarm : MonoBehaviour {
  public List<SwarmMember> members = new List<SwarmMember>();
  private Vector3? _centerOfMass;
  private Vector3? _averageVelocity;
  public float matchPositionP = 1f/8f;
  public float matchVelocityP = 1f/8f;
  public float distanceThreshold = 2f;
  public float maxVelocity = 1f;
  public Vector3 cm;
  public Vector3 av;
  private static Swarm _instance;
  public static Swarm instance {
    get {
      if (_instance == null) {
        GameObject o = GameObject.Find("Swarm");
        if (o == null) {
          o = new GameObject("Swarm");
          o.AddComponent<Swarm>();
        }
        _instance = o.GetComponent<Swarm>() as Swarm;
      }
      return _instance;
    }
  }

  public Vector3 centerOfMass {
    get {
      if (_centerOfMass == null) {
        if (members.Count > 0) {
          Vector3 sum = Vector3.zero;
          foreach(SwarmMember member in members) {
            //Debug.Log("sum center of mass " + member.transform.position);
            sum += member.transform.position;
          }
          // _centerOfMass.Value =
          //   members.Select(m => m.transform.position).Sum()/members.Count;
          _centerOfMass = 1f/(float) members.Count * sum;
        } else {
          _centerOfMass = transform.position;
        }
      }
      cm = _centerOfMass.Value;
      return _centerOfMass.Value;
    }
  }

  public Vector3 averageVelocity {
    get {
      if (_averageVelocity == null) {
        if (members.Count > 0) {
          Vector3 sum = Vector3.zero;
          foreach(SwarmMember member in members) {
            sum += member.velocity;
          }
          _averageVelocity = 1f/(float) members.Count * sum;
        } else {
          _averageVelocity = Vector3.zero;
        }
      }
      av = _averageVelocity.Value;
      return _averageVelocity.Value;
    }
  }

	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
    _averageVelocity = null;
    _centerOfMass = null;
	}

  void OnDestroy() {
    Swarm._instance = null;
  }

  public static void Deregister(SwarmMember member) {
    if (_instance == null) {
      // Don't worry about it.
    } else {
      Swarm.instance.members.Remove(member);
    }
  }

  void OnDrawGizmos() {
    Gizmos.DrawSphere(centerOfMass, 0.1f);
    Gizmos.DrawLine(centerOfMass, centerOfMass + averageVelocity);
  }
}
