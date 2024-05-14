using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using GraphQL;
using UnityEngine;
using UnityEngine.UI;
using GraphQL.Client;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;


public class QueryGenerator : MonoBehaviour
{
    [SerializeField] private Button _queryButton;
    [SerializeField] private Text _queryInputField;
    void Start()
    {
        _queryButton.onClick.AddListener(() =>
        {
            _sendQuery();
        });
    }

    private async void _sendQuery()
    {
        GraphQLHttpClient graphQLClient = new GraphQLHttpClient($"https://countries.trevorblades.com/", new NewtonsoftJsonSerializer());
        //graphQLClient.Options.MediaType = "application/json";
        

        GraphQLRequest query = new GraphQLRequest
        {
            Query = _queryInputField.text,
        };

        var response = await graphQLClient.SendQueryAsync<QueryResponse>(query, CancellationToken.None);

        Debug.Log($"[Query] {JsonConvert.SerializeObject(response.Data)}");
    }
}

public class QueryResponse
{
    public Country Country { get; set; }
}

public class Country
{
    public string Name { get; set; }
    public string Native { get; set; }
    public string Capital { get; set; }
    public string Emoji { get; set; }
    public string Currency { get; set; }
    public Language[] Languages { get; set; }
}

public class Language
{
    public string Code { get; set; }
    public string Name { get; set; }
}
