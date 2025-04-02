using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioMechanic : MonoBehaviour
{
    public GunAsset activeGun;
    private AudioSource audioSource; // Audio source component
    private int currentAudioIndex = 0;
    public GameObject bulletSpawnpoint;
    public float bulletSpeed;
    private GameObject bulletPrefab;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetGun();
    }

    public void OnBeat() 
    {
        /* Beat Timing explained
         *whole note = 4 beats = 35 whole notes a minute at 140bpm
         *half note = 2 beats = 70 half notes a minute at 140bpm
         *quarter note = 1 beats = 140 quarter notes a minute at 140bpm
         *eighth note = .5 beats = 280 eighth notes a minute at 140bpm
         *
         *Since eighth notes are giving trouble this code uses 280bpm
         *whole note = 8 beats = 35 whole notes a minute at 280bpm
         *half note = 4 beats = 70 half notes a minute at 280bpm
         *quarter note = 2 beats = 140 quarter notes a minute at 280bpm
         *eighth note = 1 beats = 280 eighth notes a minute at 280bpm
         */

        if (activeGun.fireRate == GunAsset.FireRate.whole &&  BeatManager.Instance.beatCounter % 8 == 0) // Whole note
        {
            Debug.Log("Whole Note Triggered");
            PlayGunSound();

        }

        if (activeGun.fireRate == GunAsset.FireRate.half && BeatManager.Instance.beatCounter % 4 == 0) // Half note
        {
            Debug.Log("Half Note Triggered");
            PlayGunSound();

        }

        if (activeGun.fireRate == GunAsset.FireRate.quarter && BeatManager.Instance.beatCounter % 2 == 0) // Quarter note
        {
            Debug.Log("Quarter Note Triggered");
            PlayGunSound();

        }
        if (activeGun.fireRate == GunAsset.FireRate.eighth && BeatManager.Instance.beatCounter % 1 == 0) //8th note
        {
            Debug.Log("Eighth Note Triggered");
            PlayGunSound();
        }
    }

    void SetGun() 
    {
        //Set players gun as the activeGun variable, this will need to be done every time we change the players gun
    }

    private void PlayGunSound()
    {
        Shoot();
        if (audioSource != null && activeGun.gunAudios.Count > 0)
        {
            // Play the current clip and loop to the next
            audioSource.PlayOneShot(activeGun.gunAudios[currentAudioIndex]);

            // Increment index and loop back when reaching the end
            currentAudioIndex = (currentAudioIndex + 1) % activeGun.gunAudios.Count;
        }
    }

    void Shoot()
    {
        bulletPrefab = activeGun.bulletType.gameObject;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, bulletSpawnpoint.transform.position);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPos = ray.GetPoint(enter);
            Vector3 direction = (targetPos - bulletSpawnpoint.transform.position).normalized;

            GameObject bullet = ObjectPool.Instance.Spawn(bulletPrefab, bulletSpawnpoint.transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.LookRotation(direction);

            // Pass damage info to the bullet
            BulletManager bulletScript = bullet.GetComponent<BulletManager>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(activeGun); // Pass the player's current GunAsset
            }

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Bullet is missing a Rigidbody component.");
            }
        }
    }
}

