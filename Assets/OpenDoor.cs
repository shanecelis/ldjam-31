using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OpenDoor : MonoBehaviour {

  public string[] swarmsToAttractWhenOpen;
  public Transform attractor;
  private List<Swarm> swarms;
  public enum State { Open, Opening, Closed, Closing };
  private State _state = State.Opening;
  // states: open, opening, closed, closing
  //private bool open = false;

  public bool open {
    get {
      return (state == State.Open || state == State.Opening);
    }
    set {
      if (value) {
        // open it.
        if (state == State.Closing || state == State.Closed)
          state = State.Opening;
      } else {
        // close it.
        if (state == State.Opening || state == State.Open)
          state = State.Closing;
      }
    }
  }

  public State state {
    get { return _state; }
    set {
      if (value == _state)
        return;
      switch (_state) {
        case State.Open:
        case State.Opening:
        case State.Closing:
          // From Open -> Closed. Remove yourself from swarm attractors.
          if (value == State.Closed) {
            RemoveAttractor();
            AddDetractor();
          }
          break;
        case State.Closed: 
          switch (value) {
            case State.Open:
            case State.Opening:
            case State.Closing:
              // From Closed -> Openish.  Add yourself to the swarms
              // attractors.
              //AddAttractor();
              break;
            case State.Closed:
              break;
          }
          break;
      }
      if (value == State.Open) {
        AddAttractor();
        RemoveDetractor();
      }
      //Debug.Log("State transition " + _state + " -> " + value);
      _state = value;
    }
  }

  private void RemoveAttractor() {
    foreach(var swarm in swarms) {
      if (swarm.attractors.Contains(attractor))
        swarm.attractors.Remove(attractor);
    }
  }

  private void AddAttractor() {
    foreach(var swarm in swarms) {
      if (! swarm.attractors.Contains(attractor))
        swarm.attractors.Add(attractor);
    }
  }

  private void RemoveDetractor() {
    foreach(var swarm in swarms) {
      if (swarm.detractors.Contains(attractor))
        swarm.detractors.Remove(attractor);
    }
  }

  private void AddDetractor() {
    foreach(var swarm in swarms) {
      if (! swarm.detractors.Contains(attractor))
        swarm.detractors.Add(attractor);
    }
  }


	// Use this for initialization
	void Start () {
    swarms = swarmsToAttractWhenOpen.Select(s => Swarm.GetSwarm(s)).Cast<Swarm>().ToList();
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown("space")) {
        JointSpring spring = hingeJoint.spring;
        open = !open;
        spring.targetPosition = open ? -90f : 0f;
        hingeJoint.spring = spring;
    }
	}

  void FixedUpdate() {
    if (hingeJoint.angle < -85f && state == State.Opening) {
      state = State.Open;
    }

    if (hingeJoint.angle > -5f && state == State.Closing) {
      state = State.Closed;
    }
  }
}
