using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puppet.Source.Network
{
    public class Launcher : MonoBehaviourPunCallbacks
    {

        #region Private Serializable Variables

        [SerializeField]
        private GameManager _gameManager;

        #endregion
        

        /// This client's version number. Users are separated from each other by gameversion. (which allows you to make breaking changes).
        private string _gameVersion = "1";
        
        /// The maximum number of players per room. If maximum is reached, will create a new room.
        [SerializeField]
        private byte _maxPlayersPerRoom = 4;

        RoomOptions roomOptions;

        TypedLobby typedLobby;
        
        
        public enum GameConnectState
        {
            Disconnected,
            Connected,
            Joined
        }

        public GameConnectState CurrentState;

        public string roomName;
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected!");
            CurrentState = GameConnectState.Connected;
            Debug.Log("room name:" + roomName);
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Joined room failed! Error: " + returnCode + " " + message);
            Debug.Log("Will create room.");
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = _maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            CurrentState = GameConnectState.Joined;
            _gameManager.CreatePlayer();
            _gameManager.Connected();
            Debug.Log("Joined room!");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected because: " + cause);
        }

        /// <sumary>
        /// Monobehaviour method called on early initialization
        /// </sumary>
        private void Awake()
        {
            // # Critical
            // We don't join the lobby. There's no need to join the loby to get the list of rooms.
            PhotonNetwork.AutomaticallySyncScene = true;
            
        }            


        public void SetRoomName(string name)
        {
            roomName = name;
        }
        
        // Start the connection process.
        // - If already connected, we attempt joining a random room.
        // - If not yet connected, Connect this application instance to Photon Cloud Network
        public void Connect()
        {
            CurrentState = GameConnectState.Disconnected;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;

            TypedLobby typedLobby = new TypedLobby();
            typedLobby.Type = LobbyType.Default;

            if(PhotonNetwork.IsConnected)
            {
                //PhotonNetwork.JoinRandomRoom();
                Debug.Log("room name:" +roomName);
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby);
            }
            else
            {
                PhotonNetwork.GameVersion = _gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        
    }

}
