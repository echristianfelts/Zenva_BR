using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public enum PickupType
{
    Health,
    Ammo
}

public class Pickup : MonoBehaviour
{

    public PickupType type;
    public int value;
    public PhotonView pickup;

    void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
        { return; }

        if (other.CompareTag("Player"))
        {
            // get the player
            PlayerController player = GameManager.instance.GetPlayer(other.gameObject);  // Makes an instanced copy of a specific Player that can be changed localy.  Change this one, and you change all of them.
            if (type == PickupType.Health)// If this is health, then tell everyone's machine that I have healed.
            {
                player.photonView.RPC("Heal", player.photonPlayer, value);
                pickup.RPC("DestroyThis", RpcTarget.All);
            }
        else if (type == PickupType.Ammo)// If this is Ammo, then tell everyone's machine that I have more bullets.
            {
                player.photonView.RPC("GiveAmmo", player.photonPlayer, value);
                pickup.RPC("DestroyThis", RpcTarget.All);
            }

            // destroy the object

        }
        //  Destroy(gameObject);
    }

    [PunRPC]
    void DestroyThis()
    {
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
