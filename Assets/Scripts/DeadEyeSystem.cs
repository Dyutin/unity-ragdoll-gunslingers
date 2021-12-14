using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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
	private int speed = 300;
	private Camera cameraMain;
	private Rigidbody2D rigidBody2D;
	private float coolDownTimer = 0.0f;
	private Gun gun;
	private float lerpPercent = 0.0f;
	private AudioSource audioSource;
	[SerializeField] PostProcessVolume postProcessVolume;
	[SerializeField] GameObject target;
	[SerializeField] private float slowMoTime = 0.2f;
	[SerializeField] private float slowMoBulletPitch = 0.3f;
	[SerializeField] private float fadeTime = 2f;


	void Start()
    {
	    cameraMain = Camera.main;
	    rigidBody2D = GetComponent<Rigidbody2D>();
	    gun = FindObjectOfType<Gun>();
	    audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
	    if (coolDownTimer > 0.0f)
	    {
		    coolDownTimer -= Time.deltaTime;
	    }
	    else
	    {
		    coolDownTimer = 0.0f;
	    }
	    if (Input.GetButton("Fire2"))
	    {
		    if (deadEyeState == DeadEyeState.Off)
			    deadEyeState = DeadEyeState.Aiming;
	    }

	    if (Input.GetButtonDown("Fire1"))
	    {
		    if (deadEyeState == DeadEyeState.Off)
			    Fire(1f);
		    if (deadEyeState == DeadEyeState.Aiming)
		    {
			    var mousePosition = cameraMain.ScreenToWorldPoint(Input.mousePosition);
			    mousePosition.z = 0;
			    var tmpTarget = Instantiate(target, mousePosition,
				    quaternion.identity);
			    targets.Add(tmpTarget.transform);
			    //GameObject tmpTarget = new GameObject();
			    //tmpTarget.transform.position = cameraMain.ScreenToWorldPoint(Input.mousePosition);
			    //targets.Add(tmpTarget.transform);
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
	    switch (deadEyeState)
	    {
		    case DeadEyeState.Shooting:
			    UpdateTargets();
			    Time.timeScale = 1;
			    Time.fixedDeltaTime = Time.timeScale * 0.02f;
			    break;
		    case DeadEyeState.Off:
			    Time.timeScale = 1;
			    Time.fixedDeltaTime = Time.timeScale * 0.02f;
			    if (postProcessVolume.weight > 0.0f)
				    postProcessVolume.weight -= Time.deltaTime * fadeTime;
			    if(audioSource.pitch >= 0.5f)
				    audioSource.pitch = Mathf.Clamp(audioSource.pitch += Time.deltaTime * fadeTime, 0.5f, 1f);

			    break;
		    case DeadEyeState.Aiming:
			    coolDownTimer = 0.5f;
			    Time.timeScale = slowMoTime;
			    Time.fixedDeltaTime = Time.timeScale * 0.02f;
			    if (postProcessVolume.weight < 1.0f)
				    postProcessVolume.weight += Time.deltaTime * fadeTime * 2.0f;
			    if(audioSource.pitch <= 1.0f)
				    audioSource.pitch =  Mathf.Clamp(audioSource.pitch -= Time.deltaTime * fadeTime, 0.5f, 1f);
			    break;
		    default:
			    throw new ArgumentOutOfRangeException();
	    }
    }

    private void UpdateTargets()
    {
	    if (deadEyeState == DeadEyeState.Shooting && targets.Count > 0)
	    {
		    Transform currentTarget = targets[0];
		    var lookMousePointer =
			    Quaternion.LookRotation(currentTarget.position - transform.position);
		    
		    lerpPercent = Mathf.MoveTowards(lerpPercent, 1f, Time.deltaTime * speed);
		    rigidBody2D.MoveRotation(Quaternion.Slerp(transform.rotation, lookMousePointer, lerpPercent));
		    //Debug.Log(transform.eulerAngles.magnitude -  lookMousePointer.eulerAngles.magnitude);
		    //var diff = transform.eulerAngles.magnitude - lookMousePointer.eulerAngles.magnitude;
		   // Debug.Log(diff);
		   // Debug.Log(lerpPercent);
		    if (lerpPercent >= 1f && coolDownTimer <= 0.0f)
		    {
			    Fire(slowMoBulletPitch);
			   targets.Remove(currentTarget);
			   Destroy(currentTarget.gameObject,0.2f);
		    }
	    }
	    else
	    {
		    deadEyeState = DeadEyeState.Off;
	    }
    }

    private void Fire(float pitch)
    {
	    Debug.Log("Fire");
	    coolDownTimer = 0.5f;
	    gun.Fire(pitch);
    }
}
