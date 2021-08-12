using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerWeapon : MonoBehaviour
{

    [Header("Stats")]
    public int damage;
    public int curAmmo;
    public int maxAmmo;
    public float bulletSpeed;
    public float shootRate;
    private float lastShootTime;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;
    private PlayerController player;


    void Awake()
    {
        // get required components
        player = GetComponent<PlayerController>();
    }

    public void TryShoot()
    {
        // can we shoot?
        if (curAmmo <= 0 || Time.time - lastShootTime < shootRate)
            return;     //      do we have ammo?  How long has it been since we have fired...?
        curAmmo--;      //      Minus one ammo.      
        lastShootTime = Time.time;

        // update the ammo UI


        // spawn the bullet
        player.photonView.RPC("SpawnBullet", RpcTarget.All, bulletSpawnPos.transform.position, Camera.main.transform.forward);  //  Hey everybody..!!  Look..!!  It's a bullet.
        //  Note that the above might need to be tweaked to fire in the direction of the gun rather than the direction of the Camera.
    }

    [PunRPC]
    void SpawnBullet(Vector3 pos, Vector3 dir)
    {
        // spawn and orientate it
        GameObject bulletObj = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bulletObj.transform.forward = dir;

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
