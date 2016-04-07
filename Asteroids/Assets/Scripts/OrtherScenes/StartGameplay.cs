using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGameplay : MonoBehaviour {


	public void PlayNow () {
		ScoreManager.Instance.Score = 0;
        SceneManager.LoadScene("GamePlayScene");
	}
}
