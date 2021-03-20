using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    class GraphBuilder
    {
        public readonly Graph graph;

        public GraphBuilder(GameObject[,] matrix)
        {
            graph = new Graph(matrix.GetLength(0) * matrix.GetLength(1));

            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matrix.GetLength(1) - 1; j++)
                {
                    int currentNode = (j + i * matrix.GetLength(0));

                    if (matrix[i, j - 1].transform.gameObject.tag != "Obstacle") //sola hareket edebilir
                    {
                        int adjacentNode = (j + i * matrix.GetLength(0))-1;
                        graph.AddEdge(currentNode, adjacentNode);
                    }
                    if (matrix[i + 1, j].transform.gameObject.tag != "Obstacle") //yukarı hareket edebilir
                    {
                        int adjacentNode = (j + i * matrix.GetLength(0))+matrix.GetLength(0);
                        graph.AddEdge(currentNode, adjacentNode);
                    }
                    if (matrix[i, j + 1].transform.gameObject.tag != "Obstacle") //sağa hareket edebilir
                    {
                        int adjacentNode = (j + i * matrix.GetLength(0))+1;
                        graph.AddEdge(currentNode, adjacentNode);
                    }
                    if (matrix[i - 1, j].transform.gameObject.tag != "Obstacle") //aşağı hareket edebilir
                    {
                        int adjacentNode = (j + i * matrix.GetLength(0))-matrix.GetLength(0);
                        graph.AddEdge(currentNode, adjacentNode);
                    }
                }
            }
        }

        internal class Graph //içerde babaya erişsinler dışardan bilmem
        {
            //Adjacency Lists 
            private LinkedList<int>[] _adj;

            public LinkedList<int>[] Adj { get { return _adj; } }

            public Graph(int V)
            {
                _adj = new LinkedList<int>[V];
                for (int i = 0; i < _adj.Length; i++)
                {
                    _adj[i] = (new LinkedList<int>());
                }
            }

            // Function to add an edge into the graph 
            public void AddEdge(int v, int w)
            {
                _adj[v].AddLast(w);
            }
        }
    }
}
