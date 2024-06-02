using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GraphQL;
using UnityEngine;
using UnityEngine.UI;
using GraphQL.Client;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using JetBrains.Annotations;
using Newtonsoft.Json;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Button _loginQueryButton;
    [SerializeField] private Button _contributionQueryButton;
    [SerializeField] private Text _loginQueryInputField;
    [SerializeField] private Text _contributionQueryInputField;
    [CanBeNull] private string _userName;
    [CanBeNull] private string _from = "";
    
    private string _apiKey = "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";
    void Start()
    {
        _loginQueryButton.onClick.AddListener(() =>
        {
            _sendLoginQuery();
        });
        _contributionQueryButton.onClick.AddListener(() =>
        {
            _sendContributionsQuery();
        });
    }

    private async void _sendLoginQuery()
    {
        GraphQLHttpClient graphQLClient = new GraphQLHttpClient($"https://api.github.com/graphql", new NewtonsoftJsonSerializer());
        graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);
        //graphQLClient.Options.MediaType = "application/json";

        GraphQLRequest query = new GraphQLRequest
        {
            Query = _loginQueryInputField.text,
        };

        var response = await graphQLClient.SendQueryAsync<GitHubQueryResponse>(query, CancellationToken.None);
        
        Debug.Log($"[Query] {JsonConvert.SerializeObject(response.Data)}");

        _userName = response.Data.Viewer.Login;
    }
    
    private async void _sendContributionsQuery()
    {
        GraphQLHttpClient graphQLClient = new GraphQLHttpClient($"https://api.github.com/graphql", new NewtonsoftJsonSerializer());
        graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);
        
        //graphQLClient.Options.MediaType = "application/json";
        
        var variables = new {
            userName = _userName,
        };

        GraphQLRequest query = new GraphQLRequest
        {
            Query = _contributionQueryInputField.text,
            Variables = variables
        };

        var response = await graphQLClient.SendQueryAsync<ContributionInfo>(query, CancellationToken.None);

        Debug.Log($"[Query] {JsonConvert.SerializeObject(response.Data)}");
    }
}


