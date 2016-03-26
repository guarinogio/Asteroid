using UnityEngine;
using System.Collections;

public class StartGameplay : MonoBehaviour {


	public void PlayNow () {
		ScoreManager.Instance.Score = 0;
		Application.LoadLevel("GamePlayScene");
	}
}
