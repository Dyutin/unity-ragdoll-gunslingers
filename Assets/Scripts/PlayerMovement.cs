using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Animator animator;
	private Collider2D[] colliders;
	
	[SerializeField] private bool isGround;
	[SerializeField] private  Transform playerTransform;
	[SerializeField] private Rigidbody2D rigidBody2D;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private float positionRadius = 0.3f;
	[SerializeField] private float playerSpeed = 50f;
	[SerializeField] private float jumpForce;
	
    private void Start()
    {
	    animator = GetComponent<Animator>();
	    colliders = GetComponentsInChildren<Collider2D>();
	    for (var i = 0; i < colliders.Length; i++)
	    {
		    for (var k = i + 1; k < colliders.Length; k++)
		    {
			    Physics2D.IgnoreCollision(colliders[i], colliders[k]);
		    }
	    }
	    
    }

    private void Update()
    {
	    HandleMovement();
	    HandleJump();
    }
    

    private void HandleJump()
    {
	    isGround = Physics2D.OverlapCircle(playerTransform.position, positionRadius, groundLayer);
	    if (isGround == true && Input.GetKeyDown(KeyCode.Space))
	    {
		    Debug.Log("jumping");
		    rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	    }
    }

    private void HandleMovement()
    {
	    if (Input.GetAxis("Horizontal") != 0)
	    {
		    if (Input.GetAxisRaw("Horizontal") > 0)
		    {
			    animator.Play("Walking");
			    rigidBody2D.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
		    }
		    else
		    {
			    animator.Play("WalkingBackward");
			    rigidBody2D.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
		    }
	    }
	    else
	    {
		    animator.Play("Idle");
	    }
    }
}
