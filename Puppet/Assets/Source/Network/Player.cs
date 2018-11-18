using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Puppet.Source.Network
{

    public class Player : MonoBehaviourPun, IPunObservable
    {
        public string playerName = "";
        
        public string playerTarget = "";
        

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(playerTarget);
            }
            else if (stream.IsReading)
            {
                string possibleName = (string)stream.ReceiveNext();

                // Established link between both
                if(possibleName == playerName)
                {
                    Debug.Log("Found me!");
                }
            }
        }
    }

}