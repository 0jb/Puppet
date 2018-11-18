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

        #region Private Variables
        /// <sumary>
        /// This client's version number. Users are separated from each other by gameversion. (which allows you to make breaking changes).
        /// </sumary>
        private string _gameVersion = "1";

        /// <summary>
        /// The maximum number of players per room. If maximum is reached, will create a new room.
        /// </summary>
        [SerializeField]
        private byte _maxPlayersPerRoom = 4;

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        #region Public Variables
        
        public enum GameConnectState
        {
            Disconnected,
            Connected,
            Joined
        }

        public GameConnectState CurrentState;

        #endregion

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected!");
            CurrentState = GameConnectState.Connected;
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Joined room failed! Error: " + returnCode + " " + message);
            Debug.Log("Will create room.");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = _maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            CurrentState = GameConnectState.Joined;
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

        ///<summary>
        /// Monobehaviour called during initialization
        ///</summary>
        
        private void Start()
        {
            Connect();
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room.
        /// - If not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
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
                PhotonNetwork.JoinOrCreateRoom("pum", roomOptions, typedLobby);
            }
            else
            {
                PhotonNetwork.GameVersion = _gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        #endregion
    }

}
