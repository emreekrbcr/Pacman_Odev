using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Classes
{
    class Algorithms
    {
        public Dictionary<int, LinkedList<int>> BreadthFirstSearch(LinkedList<int>[] adjGraph, int numOfVertices, int currentIndex, int finalIndex, out int performanceCounter)
        {
            performanceCounter = 0;

            //bundan yola çıkarak pacmanin ekmeğe yol kat etmesi sağlanacak
            Dictionary<int, LinkedList<int>> parentChildPairs = new Dictionary<int, LinkedList<int>>();

            //totalMovements = 0; //algoritmanın performansı için toplam hareket sayısı

            bool[] visited = new bool[numOfVertices];

            for (int i = 0; i < numOfVertices; i++) //bütün node'ler unvisited
            {
                visited[i] = false;
            }

            LinkedList<int> queue = new LinkedList<int>(); //kuyruk oluşumu

            visited[currentIndex] = true; //başlangıç node'u ziyaret edildi

            queue.AddLast(currentIndex); //kuyruğa eklendi

            while (queue.Any()) //kuyrukta adam kalmayana dek
            {
                performanceCounter++; //hemen zabıt tutsun, cezadan kaçmak yoğ

                currentIndex = queue.First(); //yeni index değeri kuyruğun başındaki adamın index değeri

                queue.RemoveFirst();//kuyruktaki ilk adamı çıkar

                if (currentIndex == finalIndex)
                {
                    return parentChildPairs;
                }

                LinkedList<int> list = adjGraph[currentIndex]; //pacman'in olduğu yerden gidilebilecekler listesini al

                parentChildPairs.Add(currentIndex, list);

                foreach (var val in list)
                {
                    if (!visited[val]) //eğer ziyaret edilmeyen bir node ise
                    {
                        visited[val] = true; //ziyaret et
                        queue.AddLast(val); //kuyruğun sonuna ekle
                    }
                }
            }

            return null;
        }

        public Dictionary<int, LinkedList<int>> DepthFirstSearch(LinkedList<int>[] adjGraph, int numOfVertices,
            int currentIndex, int finalIndex, out int performanceCounter)
        {
            performanceCounter = 0; //burada performanceCounter ile ilgili ref-out kullanımına güzel bir örnek oldu, sanki return ediyormuş gibi dışarı out olarak veriyoruz, ancak alt metoddan buradaki değişkenin değerini değiştirebilmek için ref kullanmak gerekiyor

            Dictionary<int, LinkedList<int>> parentChildPairs = new Dictionary<int, LinkedList<int>>(); //referans tipinde olduğu için diğer metoddan dolum yaparken bir sıkıntı olmayacaktır

            bool[] visited = new bool[numOfVertices];

            DFSHelper(adjGraph, visited, currentIndex, finalIndex, parentChildPairs,ref performanceCounter);
            
            return parentChildPairs;
        }

        void DFSHelper(LinkedList<int>[] adjGraph, bool[] visited,
            int currentIndex, int finalIndex, Dictionary<int, LinkedList<int>> parentChildPairs, ref int performanceCounter)
        {
            performanceCounter++;

            visited[currentIndex] = true;

            if (currentIndex == finalIndex)
            {
                return;
            }

            if (adjGraph[currentIndex] != null)
            {
                parentChildPairs.Add(currentIndex,adjGraph[currentIndex]); //bi yerden gidilebilecek alt node'lar listesi varsa o babadır, altındakilerde child'dır onları dictionarye ekleyip pacman'ın başlangıç pozisyonundan btiş pozisyonuna gitmesini sağlayacak

                foreach (var nodeIndex in adjGraph[currentIndex])
                {
                    if (!visited[nodeIndex] == true)
                    {
                        //Stack kullanılmamasının sebebi özyine fonksiyonun stack özelliği göstermesidir.
                        DFSHelper(adjGraph, visited, nodeIndex, finalIndex, parentChildPairs,ref performanceCounter);
                    }
                }
            }
        }
    }
}
