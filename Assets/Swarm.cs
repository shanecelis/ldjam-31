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
  public List<Transform> attractors = new List<Transform>();
  public List<Transform> detractors = new List<Transform>();
  private Vector3? _averagePositionAttractor;
  private Vector3? _averagePositionDetractor;
  public float attractorSpeed = 1f;
  public float detractorSpeed = 1f;
  
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
      return _averageVelocity.Value;
    }
  }

  public Vector3 SumVectors(List<Vector3> vectors) {
    Vector3 sum = Vector3.zero;
    foreach(Vector3 vector in vectors) {
      sum += vector;
    }
    return sum;
  }

  public Vector3? averagePositionAttractors {
    get {
      if (_averagePositionAttractor == null) {
        if (attractors.Count > 0) {
          Vector3 sum
            = SumVectors(attractors.Select(t => t.position).Cast<Vector3>().ToList());
          _averagePositionAttractor = sum/members.Count;
        } else {
          _averagePositionAttractor = null;
        }
      }
      return _averagePositionAttractor;
    }
  }

  public Vector3? averagePositionDetractors {
    get {
      if (_averagePositionDetractor == null) {
        if (attractors.Count > 0) {
          Vector3 sum
            = SumVectors(detractors.Select(t => t.position).Cast<Vector3>().ToList());
          _averagePositionDetractor = sum/members.Count;
        } else {
          _averagePositionDetractor = null;
        }
      }
      return _averagePositionDetractor;
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
