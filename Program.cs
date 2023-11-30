using Algorithms.Transfomations;
using Algorithms.Chess;

namespace Algorithms
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


            // Permutation
            if (false)
            {
                Console.WriteLine("Permutations");
                int[] data = { 1, 2, 3 };
                float[] data2 = { 3.1F, 1.4F, 4.1F };
                string[] data3 = { "raz", "dwa", "trzy" };

                int[] timeUsed = { 2, 1, 1 };
                Permutation<int> perm = new Permutation<int>();
                perm.PermuteAndPrint(data, timeUsed);
                Console.WriteLine();
            }

            // Combination
            if (false)
            {
                Console.WriteLine("Combinations");
                Combination<int> comb = new Combination<int>();
                int[] data = { 1, 2, 3 };
                comb.CombinateAndPrint(data, 2);
                Console.WriteLine();
            }

            // Variation
            if (false)
            {
                Console.WriteLine("Variations");
                Variation<int> variation = new Variation<int>();
                int[] data = { 1, 2, 3 };
                int[] timeUsed = { 1, 1, 1 };
                variation.VariateAndPrint(data, 2, timeUsed, false);
                Console.WriteLine();
                Console.WriteLine("===========================================");
                Console.WriteLine();
            }

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

            if (false)
            {
                Console.WriteLine("Knight Tour Problem");
                Knight knight = new Knight();
                knight.SolveKnightTour(6, 1, 1);
                knight.FindAllSolutions(5, 1, 1);
            }

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

            if (true)
            {
                Queen queen = new Queen();
                queen.SingleSolveQueens(8);
                queen.SolveAllQueens(14);
                // granica "rozsądnego" czasu z uwzględnieniem wypisania wyników to 10
                // dla samych obliczeń to 14 (zajmuję trochę ponad 6 s)
            }
        }
    }
}
