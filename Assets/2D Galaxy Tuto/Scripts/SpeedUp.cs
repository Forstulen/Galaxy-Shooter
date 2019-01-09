using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{

    [SerializeField]
    AudioClip _Sound;

    [SerializeField]
    private float _Speed = 4.0f;

    [SerializeField]
    private float _MinSpeed = 1.25f;
    [SerializeField]
    private float _MaxSpeed = 5.0f;

    void Update()
    {
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);

        if (transform.position.y < -7.0f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with : " + other.name);

        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.BoostSpeed(Random.Range(_MinSpeed,_MaxSpeed));
            player.DeactivateBoostSpeed();
            AudioSource.PlayClipAtPoint(_Sound, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}