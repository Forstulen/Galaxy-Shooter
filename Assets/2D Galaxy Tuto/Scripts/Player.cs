using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _Speed = 5.0f;

    private float _OriginalSpeed;

    [SerializeField]
    private GameObject _Laser;

    [SerializeField]
    private GameObject _TripleShot;

    [SerializeField]
    private float _FireRate = 0.25f;

    private float _CanFire = 0.0f;
    [SerializeField]
    private bool _CanTripleShot = false;
    [SerializeField]
    private int _Life = 3;

    [SerializeField]
    private GameObject _Explosion;

    [SerializeField]
    private GameObject _Shield;

    [SerializeField]
    private GameObject _Hurt;

    private bool shield = false;

    private GameObject _ShieldDisap;

    private UIManager _uiManager;

    /*[SerializeField]
    private Image LivesImage;*/

    [SerializeField]
    private Sprite _ThreeLives;

    [SerializeField]
    private Sprite _TwoLives;

    [SerializeField]
    private Sprite _OneLives;

    [SerializeField]
    private Sprite _NoLives;

    private AudioSource _LaserSound;

    // Use this for initialization
    void Start()
    {
        _LaserSound = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_Life);
        }
        _OriginalSpeed = _Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }


    private void Movement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * _Speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * _Speed * verticalInput);

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }



        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
    }

    private void Shoot()
    {
        bool SpaceInput = Input.GetKeyDown(KeyCode.Space);
        bool Mouse0Input = Input.GetMouseButton(0);
        bool MobileInput = CrossPlatformInputManager.GetButtonDown("Fire");

#if UNITY_ANDROID
            if (MobileInput)
            {
#else 
        if (SpaceInput || Mouse0Input)
        {
#endif
                if (Time.time > _CanFire && _CanTripleShot == false)
            {

                // Instantiate (_Laser, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                _LaserSound.Play();
                Instantiate(_Laser, new Vector3(_Laser.transform.position.x + transform.position.x,
                                         _Laser.transform.position.y + transform.position.y,
                                        _Laser.transform.position.z + transform.position.z), Quaternion.identity);

                _CanFire = Time.time + _FireRate;
            }

            if (Time.time > _CanFire && _CanTripleShot == true)
            {
                _LaserSound.Play();
                Instantiate(_TripleShot, new Vector3(_TripleShot.transform.position.x + transform.position.x,
                                        _TripleShot.transform.position.y + transform.position.y,
                                        _TripleShot.transform.position.z + transform.position.z), Quaternion.identity);
                /*Instantiate(_Laser, new Vector3((_Laser.transform.position.x-0.55f) + transform.position.x,
                                               (_Laser.transform.position.y-0.90f) + transform.position.y,
                                        _Laser.transform.position.z + transform.position.z), Quaternion.identity);
                Instantiate(_Laser, new Vector3(_Laser.transform.position.x + transform.position.x,
                                         _Laser.transform.position.y + transform.position.y,
                                        _Laser.transform.position.z + transform.position.z), Quaternion.identity);
                Instantiate(_Laser, new Vector3((_Laser.transform.position.x+0.55f) + transform.position.x,
                                                (_Laser.transform.position.y-0.90f) + transform.position.y,
                                        _Laser.transform.position.z + transform.position.z), Quaternion.identity);*/

                _CanFire = Time.time + _FireRate;
            }

        }
    }

    public void ActivateTripleShot()
    {
        _CanTripleShot = true;
    }

    public void DeactivateTripleShot()
    {
        StartCoroutine(TripleShotPowerRoutine());
    }

    IEnumerator TripleShotPowerRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        _CanTripleShot = false;
    }

    public void BoostSpeed(float boost)
    {
        _Speed = _Speed * boost;

    }

    public void DeactivateBoostSpeed()
    {
        StartCoroutine(BoostSpeedRoutine());
    }

    IEnumerator BoostSpeedRoutine()
    {
        yield return new WaitForSeconds(10.0f);

        _Speed = _OriginalSpeed;
    }

    public void ShieldUp()
    {
        _ShieldDisap = Instantiate(_Shield, transform.position, Quaternion.identity, this.transform);
        _Shield.transform.localScale = new Vector3(2, 2, 2);
        shield = true;

    }

    public void LoseLife()  
    {

        if (shield != true)
        {
            _Life -= 1;
            if (_Life < 0)
            {
                _Life = 0;
            }
            _uiManager.UpdateLives(_Life);
            GetHurt();

        }
        else
        {
            shield = false;
            Destroy(_ShieldDisap);
        }

        //UpdateLifeUI();


        if (_Life == 0)
        {
            //UpdateLifeUI();
            Instantiate(_Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }

    /*private void UpdateLifeUI() {
        if (_Life == 3)
        {
            LivesImage.sprite = _ThreeLives;
        }
        else if (_Life == 2)
        {
            LivesImage.sprite = _TwoLives;
        }
        else if (_Life == 1)
        {
            LivesImage.sprite = _OneLives;
        }
        else
        {
            LivesImage.sprite = _NoLives;
        }
    }*/

    public void GetHurt()
    {
        if (_Life < 2)
        {
            Instantiate(_Hurt, new Vector3(this.transform.position.x,
                                           this.transform.position.y - 1.1f,
                                           this.transform.position.z), 
                        Quaternion.identity, 
                        this.transform);
        }
    }

    public int GetLife()
    {
        return _Life;
    }
}
