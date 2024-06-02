using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ログイン・テスト
public class GitHubQueryResponse
{
    public Viewer Viewer { get; set; }
}

public class Viewer
{
    public string Login { get; set; }
}



//草取得
public class ContributionInfo
{
    public User User { get; set; }
}
public class User
{
    public ContributionsCollection ContributionsCollection { get; set; }
}
public class ContributionsCollection
{
    public ContributionCalendar ContributionCalendar { get; set; }
}
public class ContributionCalendar
{
    public int TotalContributions { get; set; }
    public Week[] Weeks { get; set; }
}
public class Week
{
    public ContributionDay[] ContributionDays { get; set; }
}
public class ContributionDay
{
    public int ContributionCount { get; set; }
    public string Date { get; set; }
}