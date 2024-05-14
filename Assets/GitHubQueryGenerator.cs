using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GraphQL;
using UnityEngine;
using UnityEngine.UI;
using GraphQL.Client;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Button _queryButton;
    [SerializeField] private Text _queryInputField;
    private string _apiKey = "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";
    void Start()
    {
        _queryButton.onClick.AddListener(() =>
        {
            _sendQuery();
        });
    }

    private async void _sendQuery()
    {
        GraphQLHttpClient graphQLClient = new GraphQLHttpClient($"https://api.github.com/graphql", new NewtonsoftJsonSerializer());
        graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);
        //graphQLClient.Options.MediaType = "application/json";
        
        

        GraphQLRequest query = new GraphQLRequest
        {
            Query = _queryInputField.text,
        };

        var response = await graphQLClient.SendQueryAsync<GitHubQueryResponse>(query, CancellationToken.None);

        Debug.Log($"[Query] {JsonConvert.SerializeObject(response.Data)}");
    }
}


public class GitHubQueryResponse
{
    public Viewer Viewer { get; set; }
}

public class Viewer
{
    public string Login { get; set; }
}