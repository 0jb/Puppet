using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Puppet.Source.Network
{

    public class Player : MonoBehaviourPun, IPunObservable
    {
        public string _playerName = "";

        [SerializeField]
        private string _playerTarget;
        

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_playerTarget);
            }
            else if (stream.IsReading)
            {
                string possibleName = (string)stream.ReceiveNext();

                // Established link between both
                if(possibleName == _playerName)
                {
                    Debug.Log("Found me!");
                }
            }
        }
    }

}