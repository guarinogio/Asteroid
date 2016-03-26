using UnityEngine;
using System.Collections;

public class ScoreBehaviour : MonoBehaviour {

	// Contador de puntos del juego
	private int score = 0;


	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this,"AddPoints");
	}

	void AddPoints(Notification notification){
		score+=10;
		ScoreManager.Instance.Score +=10;
		this.GetComponent<UnityEngine.UI.Text> ().text = score.ToString ();
	}
}
