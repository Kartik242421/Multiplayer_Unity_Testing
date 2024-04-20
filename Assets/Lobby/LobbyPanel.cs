using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Unity services initialized");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log($"Player Authentication{AuthenticationService.Instance.PlayerId}");

    }

    public async void CreateLobby()
    {
        Lobby myLobby = await LobbyService.Instance.CreateLobbyAsync("My Lobby", 4);
        Debug.Log($"Lobby created {myLobby.Id},{myLobby.LobbyCode}, {myLobby.Name}");
    }

    public async void JoinLobby()
    {
        try
        {
            Lobby myLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
            Debug.Log($"Joinned Lobby Success {myLobby.Id},{myLobby.LobbyCode}, {myLobby.Name}");
        }
        catch(LobbyServiceException e)
        {
            Debug.Log($"Unable to join the lobby, reason {e.Reason}, {e.Message}");
        }
        
    }

    public async void JoinLobbyByCode(TMPro.TMP_InputField codeField)
    {
        try
        {
            Lobby myLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(codeField.text);
            Debug.Log($"Joinned Lobby Success {myLobby.Id},{myLobby.LobbyCode}, {myLobby.Name}");
        }
        catch (LobbyServiceException e)
        {
            Debug.Log($"Unable to join the lobby, reason {e.Reason}, {e.Message}");
        }

    }
}
