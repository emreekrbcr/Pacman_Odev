# Pacman_Odev
This is a simple project on the implementation of BFS and DFS Algorithms to the pacman game that I have done in the Artificial Intelligence lesson.

Bu Yapay Zeka dersi için yapmış olduğum PAC-MAN karakterinin yiyeceğe ulaşması ile alakalı BFS ve DFS algoritmalarının kullanıdığı ve performanslarının karşılaştırıldığı ödevdir.

Algoritmaların içerisinde ufak tefek bazı ekstra kodlar bulunmaktadır ancak çalışma mantığı olarak orjinaliyle neredeyse aynılar. Bunu oyunda pacman’in hareketini sağlayabilmek ve algoritmaların ne kadar iterasyon gerçekleştirdiğini tespit edebilmek için yaptım. 

Oyunun çalışma mantığı şu şekildedir: 

-Bir adet yaygın olarak kullanılan labirent haritası alındı. 

-Bu harita her biri matrix’in bir koordinatı olacak şekilde Tilemap kullanılarak parçalara bölündü. Tilemap Unity’de bir bütün olarak kabul edildiği için bunların üzerlerine node adı verilen yeni nesneler eklendi. Matrix’in boyutu 37x37 oldu. Sol alttaki parça 0.node olarak kabul edildi ve bundan nodelar birer birer artırıldı (Algoritmaları implement ederken bu özellikten yararlandım).

-Bu parçalardan Pacman’in hareket edebileceği alanlara dokunulmadı, diğer alanlara yani labirentin engel olacak parçalarına “Obstacle” adında bir tag verildi. Bu sayede tag’i “Obstacle” olan parçaların üzerine Pacman’in hareket edememesi sağlandı (kodlama esnasında bu özelliği kullandım)

-Node’ların hepsi bir matrix’e atandı.

-Pacman’in ve yiyeceğin başlangıç için gerçek dünya pozisyonları, matrix üzerindeki ilgili node’lara atandı. Bu sayede konumdan kaynaklı bug oluşmasının önüne geçildi.

-Algoritmalarda kullanmak üzere her node’dan gidilebilecek node’ları belirten Adjacency List yani Komşuluk Listesi oluşturuldu.

-Oyunda kullanılacak Algoritmalar yandan seçilebilir, eğer tuşla hareket edilmek isteniyorsa Keys seçilmelidir.

Ek Özelliklerle İlgili:
Bu projeyi geçen dönem yapmış olduğum için ve şu anki ödev ve derslerimin yoğunluğundan dolayı aklımda olan bazı ek özellikleri ekleyememekteyim. İlerleyen zamanlarda projeye A* algoritması eklenecek ve haritada çıkan yemin "Obstacle" olarak etiketlenmeyen bir konumda rastgele çıkması sağlanacaktır. Buna bağlı olarak algoritmaların sonuçlarının yemin değişken konumlarına göre nasıl değiştiğinin gözlemlenmesi sağlanacaktır.
