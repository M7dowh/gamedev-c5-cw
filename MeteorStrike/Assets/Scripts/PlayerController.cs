using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
	Rigidbody rb;
	[SerializeField] private float speed;
	[SerializeField] private float xMin, xMax, zMin, zMax;
	[SerializeField] private float tiltValue;
	[SerializeField] private GameObject shot; 
	[SerializeField] private Transform shotSpawn; 
	[SerializeField] private float fireRate;
	
	float nextFire;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

	}

	void move()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
	}

	void fire()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	private void FixedUpdate()
    {
		move();
		clampPosition();
		tilt();
	}

	// Update is called once per frame
	void Update()
    {
		fire();
    }

	void clampPosition()
	{
		float clampX = Mathf.Clamp(rb.position.x, xMin, xMax);
		float clampZ = Mathf.Clamp(rb.position.z, zMin, zMax);
		rb.position = new Vector3(clampX, 0.0f, clampZ);
	}

	void tilt()
	{
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tiltValue);
	}
}
