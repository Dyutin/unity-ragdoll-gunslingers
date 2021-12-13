using System;
using UnityEngine;

public class BalancePlayer : MonoBehaviour
{
	[SerializeField] private float targetRotation;
	private Rigidbody2D rigidBody;
	[SerializeField] private float force = 500f;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	private void Update()
    {
        rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force*Time.fixedDeltaTime));
    }
}
