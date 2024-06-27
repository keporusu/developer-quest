using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using UnityEngine;
using UnityEngine.UI;
using GraphQL.Client;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using JetBrains.Annotations;
using Newtonsoft.Json;

public class GitHubService
{

    private string _apiKey = System.Environment.GetEnvironmentVariable("GITHUB_APIKEY_FOR_UNITY");
    
    
    //ログイン用クエリ送信
    public async Task<string> SendLoginQuery()
    {
        GraphQLHttpClient graphQLClient = new GraphQLHttpClient($"https://api.github.com/graphql", new NewtonsoftJsonSerializer());
        graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);
        //graphQLClient.Options.MediaType = "application/json";

        GraphQLRequest query = new GraphQLRequest
        {
            Query = "query {    viewer {      login    }  }"//_loginQueryInputField.text
        };

        var response = await graphQLClient.SendQueryAsync<GitHubQueryResponse>(query, CancellationToken.None);
        
        Debug.Log($"[Query] {JsonConvert.SerializeObject(response.Data)}");

        return response.Data.Viewer.Login;
    }
    
    
    
    //草取得用クエリ送信
    public async Task<ContributionsData> SendContributionsQuery(string userName, int need)
    {
        GraphQLHttpClient graphQLClient = new GraphQLHttpClient($"https://api.github.com/graphql", new NewtonsoftJsonSerializer());
        graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);

        var variables = new {
            userName = userName,
        };

        GraphQLRequest query = new GraphQLRequest
        {
            Query = "query($userName: String!) {   user(login: $userName) {     contributionsCollection {       contributionCalendar {         totalContributions         weeks {           contributionDays {             contributionCount             date           }         }       }     }   } }",//_contributionQueryInputField.text,
            Variables = variables
        };

        var response = await graphQLClient.SendQueryAsync<ContributionInfo>(query, CancellationToken.None);

        Debug.Log($"[Query] {JsonConvert.SerializeObject(response.Data)}");


        var contributionCalendar = response.Data.User.ContributionsCollection.ContributionCalendar;
        var counts = contributionCalendar.TotalContributions;
        var calender = contributionCalendar.Weeks;
        
        //need分だけとる
        List<DayContribution> dayContributions = new List<DayContribution>();
        foreach (var week in calender.Reverse())
        {
            foreach (var day in week.ContributionDays.Reverse())
            {
                dayContributions.Add(new DayContribution(day.Date,day.ContributionCount));
                need--;
                if (need == 0) break;
            }
            if (need == 0) break;
        }
        return new ContributionsData(counts, dayContributions);
    }
}


