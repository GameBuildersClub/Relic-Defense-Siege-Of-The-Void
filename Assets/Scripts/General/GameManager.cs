using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance { get { if (instance == null) instance = FindObjectOfType<GameManager>(); return instance; } set { instance = value; } }

    protected PlayerController player;
    public PlayerController Player { get { if (player == null) player = FindObjectOfType<PlayerController>(); return player; } set {  player = value; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("GameManager already instantiated... deleting new GameManager.");
            Destroy(gameObject);
        }
    }
}
