using UnityEngine;
using System.Collections;

public class AsteroidBehaviour : MonoBehaviour {
	
	// Velocidad de los asteroides hijos
	public float childVelocityMult = 3f;

	// Velocidad del asteroide actual
	public float velocity = 3f;

	// Fuerza inicial con la que inicia el asteroide
	public float initialForce = 30f;

	// Referencia al objeto asteroide
	private GameObject asteroid;

	// Instancia de la referencia del objeto asteroide
	private GameObject instance;

	// Vector direccion del asteroide
	private Vector3 direction;

	// Cantidad de asteroides hijos
	private int maxAsteroids = 3;

	// Sonido de destruccion del asteroide
	private AudioSource destroySound;


	// Use this for initialization
	void Start () {
		direction = this.transform.up;
		GetComponent<Rigidbody2D>().AddForce(direction * initialForce);
		asteroid = this.gameObject;

		destroySound = this.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

		Vector3 v = GetComponent<Rigidbody2D> ().velocity;
		v = Vector3.ClampMagnitude(v,velocity);
		GetComponent<Rigidbody2D> ().velocity = v;
	}

	// Collider
	void OnTriggerEnter2D(Collider2D otherCollider){
		if(otherCollider.gameObject.tag =="Bullet"){

			AudioSource.PlayClipAtPoint(destroySound.clip, this.transform.position);

			if(transform.localScale.x > 0.3f){

				direction.x = Random.Range(-1f, 1f);
				direction.y = Random.Range(-1f, 1f);
				direction.z = 0f;

				for (int i = 0; i < maxAsteroids; i++) {
					instance = Instantiate(asteroid);
					instance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1f);
					instance.GetComponent<Rigidbody2D>().AddForce(direction.normalized * initialForce * childVelocityMult);
					instance.transform.localScale = this.transform.localScale - new Vector3(0.25f,0.25f,0.25f);

					if (i==0) {
						direction = new Vector3( -1*direction.x,direction.y,0f);	
					}else{
						if (i==1) {
							direction = new Vector3(direction.x,-1*direction.y,0f);	
						}
					}

					direction = direction.normalized;
				}
			}

			NotificationCenter.DefaultCenter().PostNotification(this,"AddPoints");
			Destroy(this.gameObject);
			
		}
	}
}
