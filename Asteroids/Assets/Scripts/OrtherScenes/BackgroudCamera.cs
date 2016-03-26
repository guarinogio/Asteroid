using UnityEngine;
using System.Collections;

public class BackgroudCamera : MonoBehaviour {
	
	SpriteRenderer sr;
	float worldScreenHeight;
	float worldScreenWidth;

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer>();
		
		worldScreenHeight = Camera.main.orthographicSize * 2;
		worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		transform.localScale = new Vector3(
		worldScreenWidth / sr.sprite.bounds.size.x,
		worldScreenHeight / sr.sprite.bounds.size.y, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
