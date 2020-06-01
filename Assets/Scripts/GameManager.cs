using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    void Awake()
    {
        instance = this;
    }

    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    [SerializeField] private GameUI ui;

    private int guysCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnableCursor();
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public Transform GetCamera()
    {
        return cam;
    }

    public GameUI GetUI()
    {
        return ui;
    }

    void Start()
    {
        HideCursor();
    }

    void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AddGuy()
    {
        guysCount++;
    }

    public void RemoveGuy()
    {
        guysCount--;
    }

    public int GetGuysCount()
    {
        return guysCount;
    }

}
