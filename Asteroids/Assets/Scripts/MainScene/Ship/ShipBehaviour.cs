using UnityEngine;
using System.Collections;

public class ShipBehaviour : MonoBehaviour {

	// Fuerza inicial de la nave
	public float initialForce = 100f;

	// Velocidad maxima de la nave
	public float maxVelocity = 10f;

	// Velocidad de giro de la nave
	public float angleSpeed = 150f;

	// Friccion de la nave
	public float friction = 1f;

	// Velocidad de disparo
	public float countdownTimeBullet = 0.2f;

	// Tiempo de carga del superdisparo
	public float chargeBulletCountdown = 1f;

	// Tiempo de carga para realizar la teletransportacion
	public float countdownTimeWarp = 0.8f;

	// Tiempo para poder empezar a jugar luego de que muera la naves
	public float countdownSpam = 2f;

	// Referencia al objeto a disparar
	public GameObject bullet;

	// Referencia al objeto de superdisparo
	public GameObject superBullet;

	// Referencia a la camara principal 
	private Camera camara;

	// Cuenta atras de carga del superdisparo
	private float chargeBulletTimeLeft;

	// Cuenta atras para volver a poder disparar
	private float timeLeftCountdownBullet = 0f;

	// Cuenta atras para volver a usar teletransportacion
	private float timeLeftCountdownWarp = 0f;

	// Vector referencia a una nueva posicion
	private Vector3 newPos;

	// Bandera que determina si ya se puede jugar
	private bool canPlay = false;

	// Bandera que determina si la nave se esta moviendo
	private bool isMoving = false;

	// Bandera que determina si esta cargado el super disparo
	private bool isCharged = false;

	// // Bandera que determina si se esta cargando el super disparo
	private bool chargeBullet = false;

	// Bandera que determina si se puede volver a disparar
	private bool canShot = false;

	// Arreglo de audios de la nave
	private AudioSource[] aSource;

	// Sonido de teletransportacion
	private AudioSource warpSound;

	// Sonido de super disparos
	private AudioSource superShotSound;

	// Sonido de disparo
	private AudioSource shotSound;

	// Sonido de destruccion
	private AudioSource destroySound;

	// Referencia a las animaciones de la nave
	private Animator animator;

	void Awake(){
		animator = this.GetComponent<Animator>();
		aSource = this.GetComponents<AudioSource>();
	}

	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter().AddObserver(this, "DestroyShip");
        chargeBulletTimeLeft = chargeBulletCountdown;
		camara = Camera.main;

		warpSound = aSource [0];
		superShotSound = aSource [1];
		shotSound = aSource [2];
		destroySound = aSource [3];
	}

	void FixedUpdate() {
		if (countdownSpam <= 0) {
			canPlay = true;
		}

		Vector3 v = GetComponent<Rigidbody2D> ().velocity;
		v = Vector3.ClampMagnitude(v,maxVelocity);
		GetComponent<Rigidbody2D> ().velocity = v;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeftCountdownBullet -= Time.deltaTime;
		timeLeftCountdownWarp -= Time.deltaTime;

		if(canPlay == false){
			countdownSpam -= Time.deltaTime;
		}

		animator.SetBool ("InMovement",isMoving);
		animator.SetBool ("IsReady",canPlay);
		animator.SetBool ("IsCharging",chargeBullet);
		animator.SetBool ("IsCharged",isCharged);


		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				isMoving = true;
				GetComponent<Rigidbody2D> ().AddForce (transform.up * initialForce * Time.deltaTime);
			} else {
				isMoving = false;
				GetComponent<Rigidbody2D> ().velocity *= friction;
			}
		
		
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate(0,0,Time.deltaTime * angleSpeed);
		} 

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate(0,0,-1*(Time.deltaTime * angleSpeed));
		}

		if ( (Input.GetKeyDown (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && timeLeftCountdownWarp < 0) {
			AudioSource.PlayClipAtPoint(warpSound.clip, this.transform.position);
			newPos = camara.ScreenToWorldPoint (new Vector3 (camara.pixelWidth, camara.pixelHeight, 0f)); 
			newPos = new Vector3 (Random.Range (-1 * (newPos.x - 1f), newPos.x - 1f), Random.Range (-1 * (newPos.y - 1f), newPos.y - 1f), 0f);
			this.transform.position = new Vector3 (newPos.x, newPos.y, this.transform.position.z);
			timeLeftCountdownWarp = countdownTimeWarp;
		}


		if (Input.GetKey (KeyCode.Space)) {
			if(isCharged == false && chargeBulletTimeLeft < chargeBulletCountdown/1.2){
				chargeBullet = true;
			}

			if(chargeBulletTimeLeft < 0){
				isCharged = true;
				chargeBullet = false;
			}

			canShot = true;
			chargeBulletTimeLeft -= Time.deltaTime;
		}

		if(!Input.GetKey (KeyCode.Space) && (chargeBullet == true || isCharged == true || canShot == true) ){
			if(chargeBulletTimeLeft < 0){
				Instantiate (superBullet, transform.position, transform.rotation);
				AudioSource.PlayClipAtPoint(superShotSound.clip, this.transform.position);
				chargeBulletTimeLeft = chargeBulletCountdown;
				chargeBullet = false;
				isCharged = false;
				canShot = false;
			}else{
				if(timeLeftCountdownBullet < 0){
					Instantiate (bullet, transform.position, transform.rotation);
					AudioSource.PlayClipAtPoint(shotSound.clip, this.transform.position);
					timeLeftCountdownBullet = countdownTimeBullet;
					chargeBulletTimeLeft = chargeBulletCountdown;
					chargeBullet = false;
					isCharged = false;
					canShot = false;
				}
			}
		}

	}

	// Collider
	void OnTriggerEnter2D(Collider2D otherCollider){
		if(otherCollider.gameObject.tag =="Asteroid" && canPlay == true){
			AudioSource.PlayClipAtPoint(destroySound.clip, this.transform.position);
			Destroy(this.gameObject);
			NotificationCenter.DefaultCenter().PostNotification(this,"RemoveLive");
		}
	}

    public void DestroyShip(Notification notification)
    {
        Destroy(this.gameObject);
    }
}
