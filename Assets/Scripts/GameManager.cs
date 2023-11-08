using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance { 
        get {
            if (instance == null)
            {
                GameManager tempInstance = FindObjectOfType<GameManager>();
                if (tempInstance != null && !tempInstance.canBeInstance)
                    tempInstance = null;
                if (tempInstance != null)
                    instance = tempInstance;
            }
            return instance; 
        } 
    }
    protected bool canBeInstance = true;

    protected bool paused = false;
    public bool Paused 
    { 
        get 
        { 
            return paused; 
        }
        set
        {
            if (value != paused)
            {
                paused = value;
                onPause?.Invoke(paused);
            }
        }
    }
    public event System.Action<bool> onPause;

    protected int enemiesRemaining = 0;

    protected TimeController timeController = new TimeController();
    public TimeController TimeController { get { return timeController; } }
    public static float Time { get { return Instance.TimeController.Time; } }
    public static float deltaTime { get { return Instance.TimeController.deltaTime; } }
    public static float TimeScale { get { return Instance.TimeController.TimeScale; } set { Instance.TimeController.TimeScale = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Instance is already set, deleting new GameManager...");
            Destroy(gameObject);
        }

        SceneManager.sceneUnloaded += GameManagerUnload;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        instance = null;
        canBeInstance = false;
    }

    public void GameManagerUnload(Scene scene)
    {
        if (onPause != null)
        {
            foreach (System.Delegate deleg in onPause.GetInvocationList())
            {
                onPause -= (deleg as System.Action<bool>);
            }
        }
    }
    public int AddEnemy()
    {
        enemiesRemaining++;
        return enemiesRemaining;
    }

    public int RemoveEnemy()
    {
        enemiesRemaining--;
        if (enemiesRemaining == 0)
        {
            SceneManager.LoadScene(1);
        }
        return enemiesRemaining;
    }

    private void FixedUpdate()
    {
        timeController.Tick();
        // Debug.Log("Current time: " + timeController.Time);
    }
}
