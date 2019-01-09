using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAI : MonoBehaviour {
    [SerializeField]
    private float _Speed = 5.0f;
    [SerializeField]
    private int _RangeMin = -8;
    [SerializeField]
    private int _RangeMax = 8;

    private float _RealSpeed;

    [SerializeField]
    private GameObject _Explosion;

    private UIManager _uiManager;
                                

	// Use this for initialization
	void Start () {
        InitializePosition();
        _RealSpeed = Random.Range(_Speed / 2, _Speed);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * _RealSpeed * Time.deltaTime);

        if (transform.position.y < -7.0f)
        {
            InitializePosition();
        }
	}

    private void InitializePosition()
    {
        transform.position = new Vector3(Random.Range(_RangeMin, _RangeMax), 6.0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ennemy Collided with : " + other.name);

        if (other.tag == "Laser" )
        {
            if (other.transform.parent != null)
            {   
                _uiManager.UpdateScore();
                Destroy(other.transform.parent.gameObject);
            } else {
                _uiManager.UpdateScore();
                Destroy(other.gameObject);
            }
            Explosion();

        }
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.LoseLife();
            }

            Explosion();
                
        }

    }

    public void Explosion()
    {
        Instantiate(_Explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
