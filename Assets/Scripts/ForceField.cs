using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float shrinkWaitTime;
    public float shrinkAmount;
    public float shrinkDuration;
    public float minShrinkAmount;

    public int playerDamage;

    private float lastShrinkEndTime;
    private bool shrinking;  // Are we shrinking...?
    private float targetDiameter;
    private float lastPlayerCheckTime;

    // Start is called before the first frame update
    void Start()
    {
        //Setting initial values for ForceField age and Size.
        lastShrinkEndTime = Time.time;
        targetDiameter = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (shrinking)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * targetDiameter, (shrinkAmount / shrinkDuration) * Time.deltaTime);
            if (transform.localScale.x == targetDiameter)
                shrinking = false;
        }
        else
        {
            // can we shrink again?
            if (Time.time - lastShrinkEndTime >= shrinkWaitTime && transform.localScale.x > minShrinkAmount)
                Shrink();
        }
        CheckPlayers();
    }

    void Shrink()
    {
        shrinking = true;
        // make sure we don't shrink below the min amount
        if (transform.localScale.x - shrinkAmount > minShrinkAmount)
            targetDiameter -= shrinkAmount;
        else
            targetDiameter = minShrinkAmount;

        lastShrinkEndTime = Time.time + shrinkDuration;
    }

    void CheckPlayers()  //Damages all players based on distance from point ( if vector 3 distance...)  Local Scale is a clever way of doing the distance from the centere.
    {
        if (Time.time - lastPlayerCheckTime > 1.0f)
        {
            lastPlayerCheckTime = Time.time;  // Time Marker here...!!!
            // loop through all players
            foreach (PlayerController player in GameManager.instance.players)
            {
                if (player.dead)// || !player)
                    continue;  // Jumps out of loop if player dead is true or if the player does not exist..?  (In case of weird stuff..)
                if (Vector3.Distance(Vector3.zero, player.transform.position) >= transform.localScale.x)
                {
                    // do this thing to ALL PLAYERS who meet the above criterion.  In this case...  a distance check.
                    player.photonView.RPC("TakeDamage", player.photonPlayer, 0, playerDamage); // Note Player ID set to 0.  So no playter gets credit for the kill.
                }
            }
        }
    }

}
