using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{

    public bool _GameOver = true;
    public GameObject _Player;
    private Player _playerScript;

    private UIManager _uiManager;

    private SpawnManager _Spawn;
    [SerializeField]
    private float _Difficulty;

    private float _ElapseTime;

    [SerializeField]
    private float _Time = 20.0f;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _Spawn = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _playerScript = null;

        _uiManager.ShowTitle();
    }

    void Update()
    {
        TimeDifficulty();

        if (_GameOver == true)
        {
#if UNITY_ANDROID
            if (CrossPlatformInputManager.GetButtonDown("Fire"))
            {
                Initialize();
            }
#else
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Initialize();
            }
#endif 

        }
        if (_playerScript == null)
        {
            _uiManager.ShowTitle();
            _GameOver = true;
            _Spawn.DeactivateSpawn();

        }
	}

    public void TimeDifficulty()
    {
        _ElapseTime += Time.deltaTime;
        if (_ElapseTime > _Time )
        {
            // Call method in Spawn Manager
            _Spawn.IncreaseDifficulty();
            _ElapseTime = 0;
        }
    }

    private void Initialize()
    {
        GameObject go = (GameObject)Instantiate(_Player, new Vector3(0f, 0f, 0f), Quaternion.identity);
        _playerScript = go.GetComponent<Player>();
        _GameOver = false;
        _uiManager.HideTitle();
        _uiManager.ResetScore();
        _Spawn.ActivateSpawn();
        _Spawn.StartSpawning();
        _ElapseTime = 0;
        _Spawn.ResetDifficulty();
    }
}
