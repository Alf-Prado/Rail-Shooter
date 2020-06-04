using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


//dealing with the join game interface

public class JoinGame : MonoBehaviour
{
    List<GameObject> roomList = new List<GameObject>();
    private NetworkManager nm;

    [SerializeField]
    private Text status;

    //prefab button of scroll list
    [SerializeField]
    private GameObject roomListItemPrefab;

    //scrolllist  
    [SerializeField]
    private Transform roomListParent;

    void Start()
    {
        nm = NetworkManager.singleton;
        if(nm.matches == null)
        {
            nm.StartMatchMaker();
        }

        RefreshRoomList();
    }

    //publique car on l'appelle depuis un bouton
    public void RefreshRoomList()
    {
        ClearRoomList();
        //(pages numbers, elements per list, filter, callback method)
        nm.matchMaker.ListMatches(0, 20, "",false,0,0,OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";
        if(matchList == null)
        {
            status.text = "Couldn't get room list.";
            return;
        }


        foreach(MatchInfoSnapshot match in matchList)
        {
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
            //parent the element to the list
            _roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
            if(_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }

            roomList.Add(_roomListItemGO); 
        }

        if(roomList.Count == 0)
        {
            status.text = "No room available.";
        }
    }


    private void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        //(netId,password,callback)
        nm.matchMaker.JoinMatch(_match.networkId, "","","",0,0, nm.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining " + _match.name + "...";
    }
}