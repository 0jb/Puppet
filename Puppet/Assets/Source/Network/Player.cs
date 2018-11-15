using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Puppet.Source.Network
{

    public class Player : MonoBehaviourPun, IPunObservable
    {

        public float debug;
        public float receiving_debug;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(debug);
            }
            else if (stream.IsReading)
            {
                transform.position = new Vector3( 0.0f, (float)stream.ReceiveNext(), 0.0f);
            }
        }
    }

}