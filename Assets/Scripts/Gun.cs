using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
	private AudioSource audioSource;
	[SerializeField] private KeyCode mouseButton;
	[SerializeField] private GameObject bullet;
	[SerializeField] private Transform firePoint;
	[SerializeField] private Transform lowerArm;
	[SerializeField] private float recoilForce;
    void Start()
    {
	   audioSource  = GetComponent<AudioSource>();
    }
    

    public void Fire(float pitch)
    {
	    Instantiate(bullet, firePoint.position, firePoint.rotation);
	    audioSource.pitch = pitch;
	    audioSource.PlayOneShot(audioSource.clip,audioSource.volume);
    }
}
