using System;
using System.Collections.Generic;
using UnityEngine;

public class DeadEyeSystem : MonoBehaviour
{
	public enum DeadEyeState
	{
		Off,
		Aiming,
		Shooting
	};

	public DeadEyeState deadEyeState = DeadEyeState.Off;
	public List<Transform> targets;
	private int speed = 30;
	
    void Start()
    {
        
    }

    void Update()
    {
	    if (Input.GetButton("Fire2"))
	    {
		    if (deadEyeState == DeadEyeState.Off)
			    deadEyeState = DeadEyeState.Aiming;
	    }

	    if (Input.GetButtonDown("Fire1"))
	    {
		    if (deadEyeState == DeadEyeState.Off)
			    Fire();
		    if (deadEyeState == DeadEyeState.Aiming)
		    {
			    //Assign Target
			    RaycastHit hit;
			    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100));
			    {
				    GameObject tmpTarget = new GameObject();
				    tmpTarget.transform.position = hit.point;
				    tmpTarget.transform.parent = hit.transform;
				    targets.Add(tmpTarget.transform);
			    }
		    }
	    }

	    if (Input.GetButtonUp("Fire2"))
	    {
		    if (deadEyeState == DeadEyeState.Aiming)
			    deadEyeState = DeadEyeState.Shooting;
	    }
	    
    }

    private void FixedUpdate()
    {
	    UpdateState();
    }

    private void UpdateState()
    {
	    if (deadEyeState == DeadEyeState.Shooting)
		    UpdateTargets();
    }

    private void UpdateTargets()
    {
	    if (deadEyeState == DeadEyeState.Shooting && targets.Count > 0)
	    {
		    Transform currentTarget = targets[0];
		    var lookTarget =
			    Quaternion.LookRotation(currentTarget.position - transform.position);
		    transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, speed * Time.deltaTime);
	    }
    }

    private void Fire()
    {
	    Debug.Log("Fire");
    }
}
