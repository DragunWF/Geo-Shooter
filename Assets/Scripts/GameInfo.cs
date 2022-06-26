using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int Score { get; private set; }
    private string factionChosen;

    private void Awake()
    {
        Score = 0;
    }

    private void Update()
    {

    }
}
