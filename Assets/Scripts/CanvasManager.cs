using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private int numOfSteps = 0;

    private PacmanManager pacmanManager;

    private GameObject stepsIndicator, selectState;

    private bool isKeySystem = false;

    // Start is called before the first frame update
    void Start()
    {
        pacmanManager = GameObject.Find("Pacman").GetComponent<PacmanManager>();
        FindStepsIndicatorNSelectState();
    }

    private void FindStepsIndicatorNSelectState()
    {
        var components = transform.GetChild(0).GetComponentsInChildren<RectTransform>();

        foreach (var component in components)
        {
            if (component.name == "Steps")
            {
                stepsIndicator = component.gameObject;
            }

            if (component.name == "SelectState")
            {
                selectState = component.gameObject;
            }
        }
    }

    public void ClickEventsForDFS()
    {
        pacmanManager.ResetGame();
        isKeySystem = false;
        Time.timeScale = 1;
        numOfSteps = pacmanManager.MoveForDFS(0.1f);
        stepsIndicator.GetComponent<Text>().text = numOfSteps.ToString();
    }

    public void ClickEventsForBFS()
    {
        pacmanManager.ResetGame();
        isKeySystem = false;
        Time.timeScale = 1;
        numOfSteps = pacmanManager.MoveForBFS(0.1f);
        stepsIndicator.GetComponent<Text>().text = numOfSteps.ToString();
    }

    public void ClickEventsForKeys()
    {
        pacmanManager.ResetGame();
        isKeySystem = true;
        Time.timeScale = 1;
        pacmanManager.MoveForKeys(0.15f);
        stepsIndicator.GetComponent<Text>().text = numOfSteps.ToString();
    }

    public void ClickEventsForExit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeySystem)
        {
            stepsIndicator.GetComponent<Text>().text = pacmanManager.KeySteps.ToString();
        }

        if (pacmanManager.IsGameOver)
        {
            selectState.GetComponent<Text>().text = "Game Over";
            Time.timeScale = 0;
        }
    }
}
