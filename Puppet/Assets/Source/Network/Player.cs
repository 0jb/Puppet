using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Puppet.Source.Network
{

    public class Player : MonoBehaviourPun, IPunObservable
    {

        [SerializeField]
        private GameObject _playerRender;
        [SerializeField]
        private MeshRenderer _playerMeshRenderer;

        private string _playerName = "";
        
        private string _playerTarget = "";
        
        private void Start()
        {
            PhotonNetwork.NickName = _playerName;
            Debug.Log(photonView.Owner.NickName);
            if(photonView.Owner.IsLocal)
            {

            }
        }

        public void ChangePlayerName(string newName)
        {
            _playerName = newName;
        }

        public void ChangePlayerColor (Color playerColor)
        {
            _playerMeshRenderer.material.color = playerColor;
        }

        public void ChangeTargetName(string targetName)
        {
            _playerTarget = targetName;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_playerTarget);
            }
            else if (stream.IsReading)
            {
                string possibleName = (string)stream.ReceiveNext();
                Debug.Log(possibleName);
                Debug.Log(_playerTarget);

                Debug.Log("comparing between my name: " + _playerName + " |target name: " + possibleName);

                // Established link between both
                if(possibleName == _playerName)
                {
                    Debug.Log("Found me!");
                }
            }
        }
    }

}