using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField]
    private float _Speed = 10.0f;

	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
        
        LaserMovement();
	}

    private void LaserMovement()
    {
        transform.Translate(Vector3.up * _Speed * Time.deltaTime);

        if (transform.position.y > 6.0f)
        {
            if (transform.parent != null) {
                Destroy(this.transform.parent.gameObject, 2.0f);
            } else {
                Destroy(this.gameObject, 2.0f);
            }
           
        }
    }
}
