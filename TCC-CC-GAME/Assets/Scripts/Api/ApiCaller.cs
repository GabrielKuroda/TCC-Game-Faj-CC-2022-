using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;

public class ApiCaller
{
    private static string urlApi = "http://localhost:8080/questions/";

    private ApiCaller()
    {
    }

    public static QuestionList getByOperation(string operation)
    {
        var requisicaoWeb = WebRequest.CreateHttp(urlApi + "filter/operation/" + operation);
        requisicaoWeb.Method = "GET";

        object objResponse = webCaller(requisicaoWeb);

        return QuestionMapper.convertJsonToQuestion(objResponse.ToString());
    }

    public static QuestionList getByDifficulty(string difficulty)
    {
        var requisicaoWeb = WebRequest.CreateHttp(urlApi + "filter/difficulty/" + difficulty);
        requisicaoWeb.Method = "GET";

        object objResponse = webCaller(requisicaoWeb);

        return QuestionMapper.convertJsonToQuestion(objResponse.ToString());
    }

    public static QuestionList getByDifficultyAndOperation(string difficulty, string operation)
    {
        var requisicaoWeb = WebRequest.CreateHttp(urlApi + "filter/" + difficulty + "/" + operation);
        requisicaoWeb.Method = "GET";

        object objResponse = webCaller(requisicaoWeb);

        return QuestionMapper.convertJsonToQuestion(objResponse.ToString());
    }

    public static QuestionList getAll()
    {
        var requisicaoWeb = WebRequest.CreateHttp(urlApi);
        requisicaoWeb.Method = "GET";

        object objResponse = webCaller(requisicaoWeb);

        return QuestionMapper.convertJsonToQuestion(objResponse.ToString());
    }

    private static object webCaller(WebRequest requisicaoWeb)
    {
        using (var resposta = requisicaoWeb.GetResponse())
        {
            var streamDados = resposta.GetResponseStream();
            StreamReader reader = new StreamReader(streamDados);

            streamDados.Close();
            resposta.Close();

            return reader.ReadToEnd();
        }
    }
}
