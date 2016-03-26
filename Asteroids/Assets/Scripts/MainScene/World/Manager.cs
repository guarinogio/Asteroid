using UnityEngine;
using System.Collections;
using SimpleJSON;
using System;

public class ScoreManager : Singleton<ScoreManager> {

	// invocacion al manager por Singleton
	protected ScoreManager () {}

    private string gameDataResource = "game_data.json";

    //Contador de puntos global
    public int Score = 0;
    private JSONNode jsonGameData;
    public QuestionTypeData questions;

    public void Start()
    {
        jsonGameData = JSON.Parse(LoadResourceTextfile(gameDataResource));
        ProcessGameData();
    }

    public static string LoadResourceTextfile(string path)
    {

        string filePath = "SetupData/" + path.Replace(".json", "");

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }


    private void ProcessGameData()
    {
        questions = new QuestionTypeData();
        questions.BalancingFillData(jsonGameData);
    }

}


