using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float bulletSpeed = 50f;
	[SerializeField] private float bulletLife = 6f;
	private AudioSource audioSource;
	void Start()
    {
	    GetComponent<Rigidbody2D>().AddForce(bulletSpeed*transform.right, ForceMode2D.Impulse);
	    Destroy(gameObject, bulletLife);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
	    if (other.gameObject.layer == LayerMask.NameToLayer("Bottle"))
	    {
		    audioSource = other.gameObject.GetComponent<AudioSource>();
		    //audioSource.PlayOneShot(audioSource.clip,audioSource.volume);
		    AudioSource.PlayClipAtPoint(audioSource.clip,other.transform.position);
		    other.gameObject.SetActive(false);
	    }
    }
}
