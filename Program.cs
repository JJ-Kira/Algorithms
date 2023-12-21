//#define PERMUTATION
//#define VARIATION
//#define COMBINATION
//#define KNIGHT
//#define QUEEN
//#define H_KNIGHT
//#define H_QUEEN
//#define HASH
//#define KRUSKAL
//#define IICNF
#define BAP

using Algorithms.Transfomations;
using Algorithms.Chess;
using Algorithms.Other;
using Algorithms.Graphs;
using System.Reflection;

namespace Algorithm
{
    class Test
    {
        static void Main(string[] args)
        {
            // Zadanie 1

            // Permutacja:
            // - Definicja: Układ obiektów, gdzie kolejność ma znaczenie.
            // - Wzór: Dla n elementów wybranych k elementów na raz (kolejność ma znaczenie), P(n, k) = n! / (n - k)!
            // - Przykład: Dla {A, B, C} i wybierając 2 elementy (k = 2), permutacje to AB, AC, BA, BC, CA, CB.

            // Wariacja:
            // - Definicja: Układ obiektów, gdzie kolejność ma znaczenie, ale nie wszystkie elementy są koniecznie używane.
            // - Wzór: Dla n elementów wybranych k elementów na raz (kolejność ma znaczenie, nie wszystkie elementy używane), V(n, k) = n^k.
            // - Przykład: Dla {A, B, C} i wybierając 2 elementy (k = 2), wariacje obejmują AB, AC, BA, BC, CA, CB.

            // Kombinacja:
            // - Definicja: Wybór obiektów, gdzie kolejność nie ma znaczenia.
            // - Wzór: Dla n elementów wybranych k elementów na raz (kolejność nie ma znaczenia), C(n, k) = n! / (k! * (n - k)!).
            // - Przykład: Dla {A, B, C} i wybierając 2 elementy (k = 2), kombinacje to AB, AC, BC.

            // Różnice:
            // - Permutacja vs. Wariacja: W permutacji używane są wszystkie elementy, a kolejność ma znaczenie. W wariacji kolejność ma znaczenie, ale nie wszystkie elementy są używane.
            // - Wariacja vs. Kombinacja: W wariacji kolejność ma znaczenie, a w kombinacji kolejność nie ma znaczenia.
            // - Permutacja vs. Kombinacja: Permutacja uwzględnia kolejność i używa wszystkich elementów, podczas gdy kombinacja ignoruje kolejność i uwzględnia jedynie wybór elementów.

            int[] data = { 1, 2, 3 };

            // Permutation
#if PERMUTATION
            Console.WriteLine("Permutations");
            float[] data2 = { 3.1F, 1.4F, 4.1F };
            string[] data3 = { "raz", "dwa", "trzy" };

            int[] timeUsed = { 2, 1, 1 };
            Permutation<int> perm = new Permutation<int>();
            perm.PermuteAndPrint(data, timeUsed);
            Console.WriteLine();
#endif

            // Combination
#if COMBINATION
            Console.WriteLine("Combinations");
            Combination<int> comb = new Combination<int>();
            comb.CombinateAndPrint(data, 2);
            Console.WriteLine();
#endif

            // Variation
#if VARIATION
            Console.WriteLine("Variations");
            Variation<int> variation = new Variation<int>();
            int[] timesUsed = { 1, 1, 1 };
            variation.VariateAndPrint(data, 2, timesUsed, false);
            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Zadanie 2 - problem skoczka

            // Metoda backtrackingu do rozwiązywania problemu trasy skoczka na szachownicy
            // Opiera się na próbie wszystkich możliwych ruchów skoczka, cofaniu się i ponownym próbowaniu nowych ścieżek

            // 1. Rozpoczynamy od ustalenia pozycji startowej na szachownicy.

            // 2. Dla danej pozycji skoczka, sprawdzamy wszystkie możliwe ruchy (8 ruchów zgodnie z regułami poruszania się skoczka na szachownicy).

            // 3. Dla każdego możliwego ruchu:
            //    - Sprawdzamy, czy nowa pozycja mieści się w granicach szachownicy i czy pole nie zostało wcześniej odwiedzone.
            //    - Jeśli warunki są spełnione, oznaczamy to pole jako odwiedzone i przechodzimy do następnego ruchu skoczka.

            // 4. Rekurencyjnie powtarzamy kroki 2 i 3 dla kolejnych ruchów skoczka, aż znajdziemy poprawną trasę lub wykorzystamy wszystkie możliwości.

            // 5. Jeśli znaleziono poprawną trasę (skoczek odwiedził każde pole na szachownicy dokładnie raz), zwracamy sukces.
            //    W przeciwnym razie, jeśli wszystkie możliwe ruchy zostały sprawdzone, ale nie znaleziono rozwiązania, zwracamy brak rozwiązania.

#if KNIGHT
            Console.WriteLine("Knight Tour Problem");
            Knight knight = new Knight();
            knight.SolveKnightTour(6, 1, 1);
            knight.FindAllSolutions(5, 1, 1);

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Zadanie 3 - problem N hetmanów

            // Problem N Hetmanów polega na umieszczeniu N hetmanów na szachownicy N x N tak,
            // aby żaden z hetmanów nie był zagrożony przez innego. Oznacza to, że żadne dwa
            // hetmani nie mogą znajdować się w tej samej linii, kolumnie ani na tej samej przekątnej.

            // Metoda backtrackingu (cofnij i spróbuj ponownie) jest techniką algorytmiczną używaną
            // do znajdowania wszystkich (lub niektórych) możliwych konfiguracji w problemach kombinatorycznych.
            // W kontekście problemu N Hetmanów, algorytm backtrackingu umieszcza hetmana w pierwszym możliwym
            // miejscu w rzędzie, a następnie przechodzi do następnego rzędu. Jeśli w następnym rzędzie nie
            // można bezpiecznie umieścić hetmana (tj. jest zagrożony przez innego hetmana), algorytm cofa się
            // (usuwa hetmana) i próbuje umieścić hetmana w kolejnym miejscu w poprzednim rzędzie.

            // Ten proces jest kontynuowany, aż hetman zostanie umieszczony w każdym rzędzie (co oznacza znalezienie
            // rozwiązania) lub wszystkie możliwości zostaną wyczerpane bez znalezienia rozwiązania.

            // Do śledzenia pozycji, gdzie można umieścić hetmana, używamy kilku zmiennych:
            // 1. columnPositions: Tablica przechowująca pozycje kolumn hetmanów w każdym rzędzie.
            //    Indeks tablicy reprezentuje rząd, a wartość w tym indeksie to kolumna, w której znajduje się hetman.
            // 2. rowAvailable: Tablica boolowska określająca, czy dana kolumna jest wolna od innych hetmanów.
            // 3. mainDiagonalAvailable i antiDiagonalAvailable: Tablice boolowskie do śledzenia dostępności przekątnych.
            //    Przekątne są śledzone za pomocą sumy i różnicy indeksów rzędów i kolumn, co pozwala na efektywne
            //    sprawdzenie, czy dana przekątna jest wolna.

            // Podczas próby umieszczenia hetmana w danym rzędzie i kolumnie, algorytm najpierw sprawdza,
            // czy kolumna oraz przekątne przechodzące przez tę pozycję są wolne. Jeśli tak, hetman jest umieszczany,
            // a następnie algorytm przechodzi do następnego rzędu. Jeśli nie można umieścić hetmana, algorytm
            // kontynuuje próby w kolejnych kolumnach tego samego rzędu.

            // Gdy hetman jest umieszczany, odpowiadające mu wartości w tablicach rowAvailable,
            // mainDiagonalAvailable i antiDiagonalAvailable są ustawiane na false, co oznacza, że te linie
            // są już zajęte. Gdy algorytm cofa się (backtracking), wartości te są resetowane na true,
            // co oznacza ponowne uwolnienie tych linii.

#if QUEEN
            Queen queen = new Queen();
            queen.SingleSolveQueens(8);
            queen.SolveAllQueens(14);
            // granica "rozsądnego" czasu z uwzględnieniem wypisania wyników to 10
            // dla samych obliczeń to 14 (zajmuję trochę ponad 6 s)  

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Zadanie 4 - wersje heurystyczne

            // implementacja skoczka szachowego z regułą Warnsdorfa

            // Podsumowanie reguły Warnsdorfa:
            // Reguła Warnsdorfa to heurystyka używana do rozwiązywania problemu trasy skoczka (Knight's Tour) w szachach.
            // Problem trasy skoczka polega na znalezieniu ścieżki skoczka, który odwiedza każde pole szachownicy dokładnie raz.

            // Jak działa reguła Warnsdorfa:
            // 1. Skoczek zaczyna od dowolnego pola na szachownicy.
            // 2. W każdym ruchu skoczek wybiera pole, z którego ma najmniej możliwości dalszego ruchu.
            //    - To znaczy, że wybiera pole, z którego może się przenieść na najmniejszą liczbę nieodwiedzonych jeszcze pól.
            // 3. Proces jest powtarzany aż do odwiedzenia wszystkich pól lub do momentu, gdy nie można już wykonać żadnego ruchu.

            // Zastosowanie w tym kodzie:
            // - `CheckDegree(int x, int y)`: Metoda ta oblicza liczbę możliwych ruchów z każdego potencjalnego następnego pola.
            //   Wybiera ruch, który prowadzi do pola z najmniejszą liczbą możliwych dalszych ruchów.
            // - `Warnsdorff(int i, int x, int y, ref bool q)`: Metoda rekurencyjnie próbuje rozwiązać problem trasy skoczka,
            //   stosując regułę Warnsdorfa. Jeśli napotka ślepy zaułek (brak dalszych ruchów), cofa się (backtracking) i próbuje innego ruchu.

            // Kluczowe kroki w regule Warnsdorfa:
            // 1. Wybór początkowego pola.
            // 2. W każdym kroku, wybór następnego pola z najmniejszą liczbą możliwych dalszych ruchów.
            // 3. Kontynuacja procesu aż do odwiedzenia wszystkich pól lub stwierdzenia, że dalszy ruch nie jest możliwy.

#if H_KNIGHT
            WarnsdorffKnight warnsdorfKnight = new WarnsdorffKnight();
            warnsdorfKnight.SolveKnightTour(30, 25, 7);

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Metoda gradientowa jako heurystyka w problemie N Hetmanów

            /*
             * 1. Co to jest metoda gradientowa (heurystyczna)?
             *    - Metoda gradientowa to heurystyczna technika optymalizacji, która polega na iteracyjnym
             *      poprawianiu rozwiązania w kierunku zmniejszania "kosztu" lub "konfliktu".
             *    - W kontekście problemu N Hetmanów, "konflikty" to sytuacje, gdzie hetmany atakują się nawzajem.
             *
             * 2. Jak stosuje się ją do problemu N Hetmanów?
             *    - Rozpoczynamy od losowego rozmieszczenia hetmanów na szachownicy.
             *    - Następnie iteracyjnie przesuwamy hetmany w taki sposób, aby zmniejszyć liczbę konfliktów (hetmanów atakujących się nawzajem).
             *
             * 3. Kluczowe kroki w tej implementacji:
             *    a. Inicjalizacja: Hetmany są umieszczane losowo, każdy w innej kolumnie (metoda Permutation).
             *    b. Ocena konfliktów: Sprawdzamy, czy dany hetman jest atakowany przez innego (metoda IsUnderAttack).
             *    c. Redukcja konfliktów: Próbujemy zamienić miejscami pary hetmanów, aby zmniejszyć liczbę konfliktów (metoda ReductionOfCollisionsCheck).
             *    d. Iteracyjne ulepszanie: Powtarzamy proces zamiany hetmanów, dopóki nie znajdziemy rozwiązania bez konfliktów (metoda QueenSearch).
             *    e. Wizualizacja rozwiązania: Po znalezieniu rozwiązania, wyświetlamy położenie hetmanów na szachownicy (metoda PrintQueensPositions).
             *
             * 4. Uwagi:
             *    - Metoda ta nie gwarantuje znalezienia optymalnego rozwiązania.
             *    - Może istnieć ryzyko utknięcia w lokalnym minimum (konfiguracji, która nie jest rozwiązaniem optymalnym, ale żadna pojedyncza zmiana nie prowadzi do poprawy).
             *    - Skuteczność metody zależy od początkowej konfiguracji i strategii wyboru par hetmanów do zamiany.
             */

#if H_QUEEN
            GradientQueen heuristicQueen = new GradientQueen();
            heuristicQueen.SolveNQueens(24); 

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Zadanie 5 - hashowanie

            // Hashowanie to proces przekształcania danych wejściowych (dowolnego typu) 
            // w ciąg znaków o stałej długości, zwykle znacznie krótszy niż dane wejściowe. 
            // Jest to realizowane przez funkcję haszującą.

            // Funkcja haszująca to funkcja, która bierze "klucz" jako dane wejściowe 
            // i zwraca indeks tablicy, w której powinna być przechowywana wartość odpowiadająca temu kluczowi.

            // Hashowanie jest często używane w strukturach danych, takich jak tablice haszujące, 
            // aby szybko lokalizować element danych (na przykład wartości słownikowe) bez konieczności przeszukiwania każdego elementu.

            // Przykład działania algorytmu haszowania:
            // 1. Dane wejściowe (klucz) są przekazywane do funkcji haszującej.
            // 2. Funkcja haszująca przetwarza klucz i zwraca indeks tablicy.
            // 3. Wartość jest przechowywana w tablicy haszującej pod tym indeksem.

            // W przypadku kolizji, czyli sytuacji, gdy dwa różne klucze mają ten sam indeks haszujący, 
            // stosuje się różne metody rozwiązania, np. łańcuchowanie (przechowywanie wszystkich elementów o tym samym indeksie w liście).

#if HASH
            double[] doubles0 = { 2.46, 1.54, 19.5868, 13.59, 29.535, 8.123, 92847.124, 1901.39, 33.21, 356.153, 860.321 };
            double[] doubles1 = { 1.43, 54.76, 345.543, 8768.32, 9.324, 56.896, 0.234, 930.32, 104.234, 40.32, 12.304 };
            Hash<double> hashTable0 = new Hash<double>();

            int[] ints0 = { 45, 73, 5, 2309, 456, 6508, 3, 64, 237, 345, 8904, 2, 546, 85, 258, 214, 2132, 3097, 2346 };
            int[] ints1 = { 54, 134, 876, 540, 79990, 8764, 24085, 2222, 456, 123, 570, 4535, 1794, 137, 25, 7 };
            int[] ints2 = { 174, 75667, 432, 896, 4699, 345, 2675, 6893, 87534, 876, 468, 435467, 149, 2356 };
            Hash<int> hashTable1 = new Hash<int>();


            string[] strings0 = { "eryk", "filemon", "lolek", "egon", "lucifent", "czeslaw", "kaja", "husky", "luna", "parys", "horacy", "homer", "midori", "kise", "cipciak", "yoki", "moon", "waszka", "charon" };
            string[] strings1 = { "piotr", "pawel", "dariusz", "magda", "kasia", "ola" };
            string[] strings2 = { "blus", "wena", "paco", "mamba", "tola", "kredka", "bidon", "guzik", "calka", "kawa", "zbychu", "gucio", "stefan"};
            Hash<string> hashTable2 = new Hash<string>();

            hashTable2.HashInsert(strings0);
            hashTable1.HashInsert(ints0);
            hashTable0.HashInsert(doubles0);
            hashTable2.PrintHashTable();

            Console.WriteLine("==========================");
            string element = "lolek";
            Console.WriteLine($"{element} ind: {hashTable2.HashSearch(element)}");


            Console.WriteLine("==========================");
            hashTable2.HashInsert(strings1);
            hashTable1.HashInsert(ints1);
            hashTable0.HashInsert(doubles1);
            hashTable2.PrintHashTable();

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Zadanie 6 - Kruskal union-find

            //https://wazniak.mimuw.edu.pl/index.php?title=Algorytmy_i_struktury_danych/Find-Union#Kompresja_%C5%9Bcie%C5%BCki

            // Struktura danych Union-Find służy do reprezentowania zbiorów rozłącznych.
            // Pozwala na operacje łączenia (union) dwóch zbiorów oraz znajdowania (find) reprezentanta zbioru.
            // Kompresja ścieżki (path compression) jest techniką optymalizacji działania algorytmu find,
            // która polega na zmianie każdego odwiedzonego węzła na węzeł reprezentanta zbioru, redukując długość ścieżki.

            // Algorytm Kruskala to algorytm wyboru minimalnego drzewa rozpinającego w grafie.
            // Polega na iteracyjnym dodawaniu najlżejszych krawędzi, które nie tworzą cyklu.
            // Etapy działania algorytmu obejmują sortowanie krawędzi według wagi,
            // następnie iteracyjne dodawanie krawędzi do drzewa rozpinającego, jeśli nie tworzą cyklu.
            // W algorytmie wykorzystywana jest struktura Union-Find do śledzenia połączeń między wierzchołkami.

            // Algorytm Kruskala z union-find opartym o drzewa z kompresją ścieżki (log∗) łączy w sobie
            // wydajność algorytmu Kruskala z optymalizacją związaną z kompresją ścieżki (z logarytmicznym czasem operacji).

            // Główne kroki algorytmu Kruskala z użyciem Union-Find z kompresją ścieżki:
            // 1. Inicjalizacja: Tworzenie zbioru dla każdego wierzchołka grafu.
            // 2. Sortowanie krawędzi: Sortowanie wszystkich krawędzi grafu według wag (rosnąco lub malejąco).
            // 3. Iteracja przez posortowane krawędzie: Wybieranie krawędzi zgodnie z ich kolejnością.
            // 4. Sprawdzenie cyklu: Dla każdej krawędzi sprawdzenie, czy wierzchołki, które łączy, należą do różnych zbiorów.
            //    Jeśli tak, łączenie zbiorów za pomocą operacji union w strukturze Union-Find.
            // 5. Zakończenie: Powtarzanie kroków 3-4 aż do momentu, gdy wszystkie wierzchołki zostaną połączone w jedno drzewo.
            //    W wyniku otrzymujemy minimalne drzewo rozpinające (MST) dla danego grafu.

            // Kompresja ścieżki (log∗): Jest to ulepszona forma kompresji ścieżki, która gwarantuje bardzo krótki czas wykonania operacji find.
            // W optymalnych warunkach, gdzie n jest bardzo duże, czas operacji find jest bliski logarytmicznemu (∗) przy użyciu tej techniki.

            // Minimum Spanning Tree (MST) to podgraf spójny w grafie ważonym,
            // który zawiera wszystkie wierzchołki oryginalnego grafu oraz jest drzewem (acyklicznym grafem spójnym).
            // Jest to drzewo rozpinające o minimalnej sumie wag krawędzi, łączące wszystkie wierzchołki grafu.
            // MST w grafach ważonych jest użyteczny do znalezienia najtańszej sieci połączeń
            // lub minimalnego zbioru krawędzi, które połączą wszystkie wierzchołki w grafie.

            // Algorytmy takie jak algorytm Kruskala lub algorytm Prima są używane do znalezienia MST w grafach ważonych.
            // Algorytm Kruskala wybiera krawędzie o najmniejszej wadze, tworząc MST etapami,
            // podczas gdy algorytm Prima zaczyna od jednego wierzchołka i rośnie, dodając najtańsze krawędzie,
            // aż do utworzenia całego drzewa rozpinającego (MST).

            // MST ma wiele zastosowań, takich jak sieci komunikacyjne, trasowanie w sieciach komputerowych,
            // rozmieszczanie sieci telekomunikacyjnych, zarządzanie projektem, itp.

#if KRUSKAL
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\Users\julia\OneDrive\Dokumenty\GitHub\Algorithms-II\Graphs\graph.txt");
            Graph graph = new Graph(path, true);
            graph.PrintGraph();
            Console.WriteLine("----------------");

            KruskalUnionFind kruskalUF = new KruskalUnionFind(graph);
            kruskalUF.KruskalAlgorithm();
            kruskalUF.PrintMST();

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Zadanie 7 - 2-CNF

            // Spełnialność wyrażenia logicznego odnosi się do możliwości znalezienia wartości logicznych zmiennych,
            // tak aby wyrażenie to przyjęło wartość prawdziwą (true).

            // 2-CNF (2 Conjunctive Normal Form) to forma normalna wyrażeń logicznych,
            // w której każda klauzula składa się z co najwyżej dwóch literałów (zmiennych lub ich negacji),
            // połączonych operatorem alternatywy (OR), a całe wyrażenie jest iloczynem klauzul (AND).

            // Silnie spójne składowe (SCC - Strongly Connected Components) to części grafu, 
            // w których każdy wierzchołek jest osiągalny z każdego innego w tej samej składowej.

            // Badanie spełnialności wyrażenia logicznego 2-CNF poprzez silnie spójne składowe 
            // polega na reprezentacji wyrażenia 2-CNF jako grafu, a następnie badaniu jego struktury 
            // poprzez identyfikację silnie spójnych składowych w tym grafie.

            // Kluczowe kroki tego badania:
            // 1. Konwersja wyrażenia logicznego 2-CNF na odpowiedni graf, gdzie wierzchołki reprezentują zmienne
            //    oraz ich negacje, a krawędzie odzwierciedlają klauzule 2-CNF.
            // 2. Wykorzystanie algorytmu przeszukiwania w głąb (DFS) do zidentyfikowania silnie spójnych składowych
            //    w tym grafie.
            // 3. Sprawdzenie, czy nie istnieją w składowej takie wierzchołki (zmienne i ich negacje),
            //    które należą do tej samej silnie spójnej składowej, co oznaczałoby niemożność spełnienia wyrażenia 2-CNF.
            // 4. Jeśli nie ma takich wierzchołków, wyrażenie jest spełnialne, w przeciwnym razie nie jest spełnialne.

            // Badanie spełnialności wyrażeń 2-CNF jest używane w teorii języków formalnych, 
            // algorytmice, teorii grafów oraz w zagadnieniach związanych z logiką matematyczną,
            // takich jak automatyczne dowodzenie twierdzeń, analiza złożoności obliczeniowej i inne.

#if IICNF
            Graph graph = new Graph(@"C:\Users\julia\OneDrive\Dokumenty\GitHub\Algorithms-II\Graphs\2CNF_graph.txt");
            graph.PrintLogicalFormula();
            graph.PrintGraph();

            Console.WriteLine("Vertex neighborhood: ");
            foreach (var node in graph.vertices)
                node.PrintNeighbors();

            Console.WriteLine("2CNF Vertieces neighborhood: ");
            foreach (var node in graph.twoCNFVertieces)
                node.PrintNeighbors();

            TwoCNF alg = new(graph);

            if (alg.Algorithm2CNF())
            {
                Console.WriteLine("The expression is satisfiable.");
                alg.Print2CNF();
                alg.CheckLogicFormula();
            }
            else
                Console.WriteLine("The expression is unsatisfiable.");

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif

            // Zadanie 8 - Mosty i punkty artykulacji

            // Mosty w grafach są to krawędzie, których usunięcie powoduje rozspójnienie grafu lub rozdzielenie go na dwie lub więcej spójnych składowych. 
            // Punkty artykulacji to wierzchołki, których usunięcie powoduje podział grafu na więcej spójnych składowych.

            // Znalezienie mostów można osiągnąć algorytmem, np. DFS (przeszukiwanie w głąb), identyfikując krawędzie w grafie, których usunięcie
            // powoduje zmniejszenie liczby spójnych składowych. Znalezienie punktów artykulacji również jest możliwe dzięki algorytmom,
            // takim jak algorytm Tarjana lub algorytm Hopcrofta-Tarjana, które analizują strukturę grafu w celu identyfikacji tych wierzchołków.

            // Mosty i punkty artykulacji mają znaczenie w analizie sieci komunikacyjnych, planowaniu tras w sieciach telekomunikacyjnych,
            // a także w optymalizacji sieci drogowych. Identyfikacja tych elementów pozwala na wykrycie kluczowych połączeń w sieciach,
            // co jest istotne dla zapewnienia spójności i wydajności komunikacji w różnego rodzaju systemach.

#if BAP
            Graph graphB = new Graph(@"C:\Users\julia\OneDrive\Dokumenty\GitHub\Algorithms-II\Graphs\bridge_graph.txt", false);
            graphB.PrintGraph();
            Bridges bridges = new Bridges(graphB);
            bridges.FindBridges();
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine();


            Graph graphAP = new Graph(@"C:\Users\julia\OneDrive\Dokumenty\GitHub\Algorithms-II\Graphs\ap_graph.txt", false);
            //graphAP.PrintGraph();
            ArticulationPoints aps = new ArticulationPoints(graphAP);
            aps.FindArticulationPoints();
            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();
#endif
        }
    }
}
