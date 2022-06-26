using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int Score { get; private set; }
    public string FactionChosen { get; private set; }

    private void Awake()
    {
        Score = 0;
        FactionChosen = "";
    }

    private void Update()
    {

    }
}
