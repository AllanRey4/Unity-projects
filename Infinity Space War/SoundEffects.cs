
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static AudioClip playerFairB, playerFairD, playerFairL, EnemyDie;
    static AudioSource audioSrc;
    void Start()
    {
        playerFairB = Resources.Load<AudioClip>("blaster");
        playerFairD = Resources.Load<AudioClip>("DoubleBlaster1");
        playerFairL = Resources.Load<AudioClip>("laser3");
        EnemyDie = Resources.Load<AudioClip>("explosion");

        audioSrc  = GetComponent<AudioSource>();
    }
    
    public static void PlaySound(string clip)
    {
            switch(clip)
            {
                case "blaster":
                audioSrc.PlayOneShot(playerFairB);
                break;  
                case "DoubleBlaster1":
                audioSrc.PlayOneShot(playerFairD);
                break; 
                case "laser3":
                audioSrc.PlayOneShot(playerFairL);
                break; 
                case "explosion":
                audioSrc.PlayOneShot(EnemyDie);
                break; 

            }
    }
}
