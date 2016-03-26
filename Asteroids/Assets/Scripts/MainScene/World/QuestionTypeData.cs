using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class QuestionTypeData : IBalancingDeserializable
{

    public List<QuestionData> question { get; protected set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public QuestionTypeData()
    {
        question = new List<QuestionData>();
    }

    public QuestionData GetQuestion()
    {
        return question[Random.Range(0, question.Count)];
    }

    public string BalancingKey
    {
        get
        {
            return "QuestionTypes";
        }
    }
    /// <summary>
    /// Serializer
    /// </summary>
    public void BalancingFillData(JSONNode jsonNode)
    {
        if (jsonNode[this.BalancingKey] != null)
        {
            foreach (JSONNode jsonNodeChild in jsonNode[this.BalancingKey].AsArray)
            {
                QuestionData questionData = new QuestionData();
                questionData.BalancingFillData(jsonNodeChild);
                this.question.Add(questionData);
            }
        }
    }
}

