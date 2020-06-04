using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    [SerializeField]
    private uint roomSize = 2;
    [SerializeField]
    private string roomName = "default";
    private NetworkManager nm;
    GameObject waitingText;
    GameObject buttonStart;

    // Comment to merge

    public void SetRoomName(string _name){
        roomName = _name;
    }

    public void CreateRoom(){
        if(roomName != "" && roomName != null){
            nm.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0,  nm.OnMatchCreate);
        }
        GameObject.FindGameObjectWithTag("JoinComponent").SetActive(false);
        waitingText.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        waitingText = GameObject.FindGameObjectWithTag("WaitingText");
        buttonStart = GameObject.FindGameObjectWithTag("ButtonStart");
        waitingText.SetActive(false);
        buttonStart.SetActive(false);
        nm = NetworkManager.singleton;
        if(nm.matchMaker == null){
            nm.StartMatchMaker();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(nm.numPlayers >= 2){
            if (buttonStart != null)
            {
                buttonStart.SetActive(true);
            }
            
        }
    }
}
