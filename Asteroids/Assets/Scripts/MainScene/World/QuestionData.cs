using UnityEngine;
using System.Collections;
using SimpleJSON;

public class QuestionData : IBalancingDeserializable
{

    public string question;
    public float solution;


    /// <summary>
    /// Question Constructor
    /// </summary>
    public QuestionData()
    {
        question = "";
        solution = 0;
    }

    /// <summary>
    /// Base Serializer
    /// </summary>
    public void BalancingFillData(JSONNode jsonNode)
    {
        this.question = jsonNode[this.BalancingKey]["question"];
        this.solution = jsonNode[this.BalancingKey]["solution"].AsFloat;
    }


    /// <summary>
    /// Base Class Name
    /// </summary>
    public string BalancingKey
    {
        get
        {
            return "QuestionData";
        }
    }
}

