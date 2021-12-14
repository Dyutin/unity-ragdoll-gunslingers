using UnityEngine;

public class Arms : MonoBehaviour
{
	private int speed = 300;
	private Camera cameraMain;
	private Rigidbody2D rigidBody2D;
	[SerializeField] private KeyCode mouseButton;
	private DeadEyeSystem deadEyeSystem;
	void Start()
    {
        cameraMain = Camera.main;
        rigidBody2D = GetComponent<Rigidbody2D>();
        deadEyeSystem = GetComponent<DeadEyeSystem>();
        if(deadEyeSystem==null) Debug.Log("whatttt");
    }

    private void Update()
    {
	  HandleAimShooting();
    }
    
    private void HandleAimShooting()
    {
	    var lookMousePointer =
		    Quaternion.LookRotation(cameraMain.ScreenToWorldPoint(Input.mousePosition) - transform.position);
	    var lookMousePointer2 = Quaternion.Slerp(transform.rotation, lookMousePointer, speed * Time.deltaTime);
	    rigidBody2D.MoveRotation(lookMousePointer2);
	    //Debug.Log(transform.eulerAngles.magnitude -  lookMousePointer2.eulerAngles.magnitude);
    }
}
