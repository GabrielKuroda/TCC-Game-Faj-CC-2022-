using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMapper : MonoBehaviour
{
    public static QuestionList convertJsonToQuestion(string response)
    {
        response = response.Replace("[", "{\"questions\":[");
        response = response.Replace("]", "]}");

        return JsonUtility.FromJson<QuestionList>(response.ToString());         
    }
}
