using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float bulletSpeed = 50f;
    void Start()
    {
	    GetComponent<Rigidbody2D>().AddForce(bulletSpeed*transform.right, ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }
}
