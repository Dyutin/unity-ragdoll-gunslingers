using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] private KeyCode mouseButton;
	[SerializeField] private GameObject bullet;
	[SerializeField] private Transform firePoint;
	[SerializeField] private Transform lowerArm;
	[SerializeField] private float recoilForce;
    void Start()
    {
        
    }

    void Update()
    {
	    if (Input.GetKeyDown(mouseButton))
	    {
		    Instantiate(bullet, firePoint.position, firePoint.rotation);
		    //playerTrans.gameObject.GetComponent<Rigidbody2D>().AddForce(recoilForce*transform.right, ForceMode2D.Impulse);
	    }
    }
}
