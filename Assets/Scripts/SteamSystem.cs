using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;


public class SteamSystem : MonoBehaviour
{
    private void Start()
    {
        SteamUserStats.ClearAchievement("ACH_WIN_ONE_GAME");
    }

    void Update()
    {

    }
    public void win()
    {
        if (!SteamManager.Initialized) { return; }
        Debug.Log(SteamUser.GetSteamID());
        SteamUserStats.SetAchievement("ACH_WIN_ONE_GAME");
        SteamUserStats.StoreStats();
    }
    public void snowAward()
    {
        if (!SteamManager.Initialized) { return; }
        Debug.Log(SteamUser.GetSteamID());
        SteamUserStats.SetAchievement("ACH_TRAVEL_FAR_SINGLE");
        SteamUserStats.StoreStats();
    }
}