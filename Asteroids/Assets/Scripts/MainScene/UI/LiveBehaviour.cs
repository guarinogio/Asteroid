using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LiveBehaviour : MonoBehaviour {

	// Cantidad de vidas de la nave
	private int lives = 3;

	// Referencia a la vida 1
	public GameObject live1;

	// Referencia a la vida 2
	public GameObject live2;

	// Referencia a la vida 3
	public GameObject live3;

	// Referencia a la nave
	public GameObject ship;

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this,"RemoveLive");
	}

	void RemoveLive(Notification notification){

		switch (lives)
		{
		case 3:
			Destroy(live3);
			lives--;
			Instantiate(ship);
		break;
		case 2:
			Destroy(live2);
			lives--;
			Instantiate(ship);
			break;
		case 1:
			Destroy(live1);
			lives--;
			Instantiate(ship);
			break;
		case 0:
                SceneManager.LoadScene("GameOverScene");
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
