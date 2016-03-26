using UnityEngine;
using System.Collections;

public class Worldbehaviour : MonoBehaviour {

	// Referencia a la camara principal
	private Camera camara;

	// Vector de posicion del objeto que utiliza esta funcionalidad
	private Vector3 screenPos;

	// Use this for initialization
	void Start () {
		camara = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		camara = Camera.main;
		screenPos = camara.WorldToScreenPoint(this.transform.position);
		
		if (screenPos.x >= camara.pixelWidth) {
			this.transform.position = new Vector3 (-1 * (this.transform.position.x - 0.5f), this.transform.position.y, this.transform.position.z);
		} else {
			if(screenPos.x <= 0f){
				this.transform.position = new Vector3 (-1 * (this.transform.position.x + 0.5f), this.transform.position.y, this.transform.position.z);
			}
		}
		
		if(screenPos.y >= camara.pixelHeight) {
			this.transform.position = new Vector3(this.transform.position.x, -1* (this.transform.position.y  - 0.5f) ,this.transform.position.z);
		}else {
			if(screenPos.y <= 0f){
				this.transform.position = new Vector3(this.transform.position.x, -1* (this.transform.position.y  + 0.5f) ,this.transform.position.z);
			}
		}
	}
}
