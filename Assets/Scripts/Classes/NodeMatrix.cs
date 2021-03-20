using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    class NodeMatrix
    {
        private static NodeMatrix instance;

        private static readonly object padlock = new object();

        //burası static olmadığı için hafızan uçuyor ve missing diyordu
        private static GameObject obj;

        private static GameObject[,] matrix;

        public GameObject[,] Matrix { get { return matrix; } }

        public static NodeMatrix getInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NodeMatrix();
                    }
                }
            }

            return instance;
        }

        private NodeMatrix()
        {
            //Nodes objesine erişiyoruz
            obj = GameObject.Find("Nodes");

            //Tilemap 37'ye 37'lik olduğu için toplam 1369 node
            matrix = new GameObject[37, 37];

            //Matrix'i initialize et. Bu sayede haritada pakman matrix üzerinde hareket ediyormuş gibi olacak ve buna göre algoritma yazacağız
            SetMatrix(matrix, obj);
        }

        private void SetMatrix(GameObject[,] matrix, GameObject obj)
        {
            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = obj.transform.GetChild(counter).gameObject;
                    counter++;
                }
            }
        }
    }
}
