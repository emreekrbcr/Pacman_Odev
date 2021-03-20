using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes;
using UnityEngine;

public class PacmanManager : MonoBehaviour
{
    private int currentI, currentJ, pacmanStartIndex, ekmekIndex;

    private GameObject[,] matrix;

    private GameObject ekmek;

    private static int keySteps; //MoveForKeys'in adım sayısını tutabilmek için

    public int KeySteps { get { return keySteps; } } //dışardan erişebilmek için

    private bool isGameOver;

    public bool IsGameOver { get { return isGameOver; } }

    private void Awake()
    {
        //Pacman'ın hareket edeceği node matrixini bir kereliğine oluşturmasını singleton ile temin ettik ve oluşturduğumuz matrisi çektik
        matrix = NodeMatrix.getInstance().Matrix;
    }

    private void Start()
    {
        ekmek = GameObject.Find("Ekmek");

        //Oyunu başta istediğim gibi resetlesin, ayrıca butonlarda kullanılacak
        ResetGame();
    }

    public int MoveForDFS(float waitTime)
    {
        GraphBuilder graphBuilder = new GraphBuilder(matrix);
        LinkedList<int>[] adjGraph = graphBuilder.graph.Adj;
        var numOfVertices = matrix.GetLength(0) * matrix.GetLength(1);
        int numOfSteps;
        Algorithms algorithms = new Algorithms();

        var parentChildPairs = algorithms.DepthFirstSearch(adjGraph, numOfVertices, pacmanStartIndex, ekmekIndex, out numOfSteps);
        var pacmanMovements = GetPacmanMovements(parentChildPairs);
        StartCoroutine(MoveForDFScorona(pacmanMovements, waitTime));

        return numOfSteps;
    }

    public int MoveForBFS(float waitTime)
    {
        GraphBuilder graphBuilder = new GraphBuilder(matrix);
        LinkedList<int>[] adjGraph = graphBuilder.graph.Adj;
        var numOfVertices = matrix.GetLength(0) * matrix.GetLength(1);
        int numOfSteps;
        Algorithms algorithms = new Algorithms();
        var parentChildPairs = algorithms.BreadthFirstSearch(adjGraph, numOfVertices, pacmanStartIndex, ekmekIndex, out numOfSteps);

        var pacmanMovements = GetPacmanMovements(parentChildPairs);
        StartCoroutine(MoveForBFScorona(pacmanMovements, waitTime));

        return numOfSteps;
    }

    public void MoveForKeys(float waitTime)
    {
        StartCoroutine(MoveForKeyscorona(waitTime));
    }

    private IEnumerator MoveForDFScorona(LinkedList<int> pacmanMovements, float waitTime)
    {
        foreach (var movement in pacmanMovements)
        {
            MoveByAlgorithms(movement);
            EkmekYe();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveForBFScorona(LinkedList<int> pacmanMovements, float waitTime)
    {
        foreach (var movement in pacmanMovements)
        {
            MoveByAlgorithms(movement);
            EkmekYe();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveForKeyscorona(float waitTime)
    {
        while (true)
        {
            MoveByKeys();
            EkmekYe();
            yield return new WaitForSeconds(waitTime);
        }
    }

    #region CommonMethods

    private void MoveByAlgorithms(int movement)
    {
        var leftNode = FindLeftNode();
        var upNode = FindUpNode();
        var rightNode = FindRightNode();
        var downNode = FindDownNode();

        if (movement == leftNode)
        {
            MoveLeft();
        }
        else if (movement == upNode)
        {
            MoveUp();
        }
        else if (movement == rightNode)
        {
            MoveRight();
        }
        else if (movement == downNode)
        {
            MoveDown();
        }
    }

    private void MoveByKeys()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (currentJ > 0 && matrix[currentI, currentJ - 1].transform.gameObject.tag != "Obstacle")
            {
                MoveLeft();
                keySteps++;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (currentI < matrix.GetLength(0) - 1 && matrix[currentI + 1, currentJ].transform.gameObject.tag != "Obstacle")
            {
                MoveUp();
                keySteps++;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (currentJ < matrix.GetLength(1) - 1 && matrix[currentI, currentJ + 1].transform.gameObject.tag != "Obstacle")
            {
                MoveRight();
                keySteps++;
            }

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (currentI > 0 && matrix[currentI - 1, currentJ].transform.gameObject.tag != "Obstacle")
            {
                MoveDown();
                keySteps++;
            }
        }
    }

    private LinkedList<int> GetPacmanMovements(Dictionary<int, LinkedList<int>> parentChildPairs)
    {
        LinkedList<int> pacmanMovements = new LinkedList<int>();

        var temp = ekmekIndex;

        foreach (var parentChildPair in parentChildPairs.Reverse()) //Tersinden gelerek arama yapsın
        {
            if (parentChildPair.Value.Contains(temp))
            {
                pacmanMovements.AddFirst(temp);
                temp = parentChildPair.Key;
            }
        }

        return pacmanMovements;
    }

    private void EkmekYe()
    {
        if (transform.position == ekmek.transform.position)
        {
            ekmek.SetActive(false); //ekmeği yesin
            isGameOver = true;
        }
    }

    public void ResetGame()
    {
        //pakmanın mevcut pozisyonu (başlangıç olarak böyle ayarladım)
        currentI = 1;
        currentJ = 31;

        //pakmanın sol baştan sayınca başlangıç lokasyonu ve gerçek dünya başlangıç pozisyon bilgisini ayarlar
        pacmanStartIndex = (currentJ + currentI * matrix.GetLength(0));
        transform.position = matrix[currentI, currentJ].transform.position;

        //ekmeğın pozisyon bilgisi ve başlangıç ayarlarının ayarlanması

        ekmek.SetActive(true);
        isGameOver = false;
        ekmek.transform.position = matrix[1, 1].transform.position;
        ekmekIndex = (1 + 1 * matrix.GetLength(0));
        
        keySteps = 0;
    }

    #endregion

    #region FindNodeMethods

    private int FindDownNode()
    {
        int downNode = (currentJ + currentI * matrix.GetLength(0)) - matrix.GetLength(0);
        return downNode;
    }

    private int FindRightNode()
    {
        int rightNode = (currentJ + currentI * matrix.GetLength(0)) + 1;
        return rightNode;
    }

    private int FindUpNode()
    {
        int upNode = (currentJ + currentI * matrix.GetLength(0)) + matrix.GetLength(0);
        return upNode;
    }

    private int FindLeftNode()
    {
        int leftNode = (currentJ + currentI * matrix.GetLength(0)) - 1;
        return leftNode;
    }

    #endregion

    #region MoveMethods

    private void MoveLeft()
    {
        currentJ -= 1;
        transform.rotation = Quaternion.Euler(0, 0, 90f);
        transform.position = matrix[currentI, currentJ].transform.position;
    }

    private void MoveUp()
    {
        currentI += 1;
        transform.rotation = Quaternion.Euler(0, 0, 0f);
        transform.position = matrix[currentI, currentJ].transform.position;
    }

    private void MoveRight()
    {
        currentJ += 1;
        transform.rotation = Quaternion.Euler(0, 0, -90f);
        transform.position = matrix[currentI, currentJ].transform.position;
    }

    private void MoveDown()
    {
        currentI -= 1;
        transform.rotation = Quaternion.Euler(0, 0, 180f);
        transform.position = matrix[currentI, currentJ].transform.position;
    }
    #endregion
}