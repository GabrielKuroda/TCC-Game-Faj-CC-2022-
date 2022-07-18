using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;

public class ApiCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            string soma = "soma";
            var requisicaoWeb = WebRequest.CreateHttp("http://localhost:8080/questions/filter/operation/" + soma);
            requisicaoWeb.Method = "GET";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                Debug.Log(objResponse.ToString());
                streamDados.Close();
                resposta.Close();
            }
        }
    }
}
