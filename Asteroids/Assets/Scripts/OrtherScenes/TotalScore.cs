using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour {

	// Contador de puntos totales
	private int score = 0;

	// Use this for initialization
	void Start () {
		score = ScoreManager.Instance.Score;
		this.GetComponent<Text> ().text = score.ToString ();
	}
}
