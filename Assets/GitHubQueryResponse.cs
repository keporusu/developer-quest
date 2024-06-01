using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHubQueryResponse
{
    public Viewer Viewer { get; set; }
}

public class Viewer
{
    public string Login { get; set; }
}
