using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private int damage;
    private int attackerId;
    private bool isMine;

    public Rigidbody rig;


    public void Initialize(int damage, int attackerId, bool isMine)
    {
        this.damage = damage;
        this.attackerId = attackerId;
        this.isMine = isMine;


        Destroy(gameObject, 5.0f);
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        // did we hit a player?
        // if this is the local player's bullet, damage the hit player
        // we're using client side hit detection
        if (other.CompareTag("Player") && isMine)
        {
            PlayerController player = GameManager.instance.GetPlayer(other.gameObject);
            if (player.id != attackerId)
                player.photonView.RPC("TakeDamage", player.photonPlayer, attackerId, damage);
        }

        Destroy(gameObject);
    }
    */
    void OnCollisionEnter(Collision other)
    {
        // did we hit a player?
        // if this is the local player's bullet, damage the hit player
        // we're using client side hit detection
        if (other.collider.tag =="Player" && isMine)
        {
            PlayerController player = GameManager.instance.GetPlayer(other.gameObject);
            if (player.id != attackerId)
                player.photonView.RPC("TakeDamage", player.photonPlayer, attackerId, damage);
        }

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
