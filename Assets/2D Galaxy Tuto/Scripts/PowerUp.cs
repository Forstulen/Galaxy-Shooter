using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour 
{
    [SerializeField]
    AudioClip _Sound;

    [SerializeField]
    private float _Speed = 3.0f;

	// Update is called once per frame
	void Update () {
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

        if ( player != null)
        {
            player.ActivateTripleShot();
            player.DeactivateTripleShot();
            AudioSource.PlayClipAtPoint(_Sound,Camera.main.transform.position);
            Destroy(this.gameObject);
        }

    }

}
