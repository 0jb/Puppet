using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puppet.Source.Network
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        
        static public GameManager Instance;

        [SerializeField]
        private Player _player;

        public override void OnEnable()
        {
            Instance = this;
            PhotonNetwork.Instantiate(_player.name, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        }
        
    }

}
