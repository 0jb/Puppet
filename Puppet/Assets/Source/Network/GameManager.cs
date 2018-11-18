using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puppet.Source.Network
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        static public GameManager Instance;

        [SerializeField]
        private Player _player;

        [SerializeField]
        private Button _joinButton;

        private string _playerName;

        private string _targetName;

        private GameObject _playerRef;


        public void InputName (string name)
        {
            _playerName = name;
        }

        public void TargetName (string name)
        {
            _targetName = name;
        }

        public void CreatePlayer()
        {
            Instance = this;
            _playerRef = PhotonNetwork.Instantiate(_player.name, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            _playerRef.GetComponent<Player>().ChangePlayerName(_playerName);
            _playerRef.GetComponent<Player>().ChangeTargetName(_targetName);
        }

        public void UpdateLocalPlayerVisual( Color targetColor)
        {
            if(_playerRef != null)
            {
                _playerRef.GetComponent<Player>().ChangePlayerColor(targetColor);
            }
        }

        public void Connected()
        {
            _joinButton.interactable = true;
        }

    }

}
