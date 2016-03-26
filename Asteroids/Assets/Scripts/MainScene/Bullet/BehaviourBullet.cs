using UnityEngine;
using System.Collections;

public class BehaviourBullet : MonoBehaviour {

	// Tiempo de vida del ataque
	public int timeoutDestructor = 2;

	// Velocidad del ataque
	public float acceleration = 150f;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration);
	}

	// UFixedUpdate
	void FixedUpdate()
	{
		Destroy(this.gameObject,timeoutDestructor);
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Collider
	void OnTriggerEnter2D(Collider2D otherCollider){
		if(otherCollider.gameObject.tag =="Asteroid"){
			Destroy(this.gameObject);
		}
	}

}
