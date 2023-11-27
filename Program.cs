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


            //Permutation
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

            //Combination
            if (false)
            {
                Console.WriteLine("Combinations");
                Combination<int> comb = new Combination<int>();
                int[] data = { 1, 2, 3 };
                comb.CombinateAndPrint(data, 2);
                Console.WriteLine();
            }

            //Variation
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

            //Knight

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

            if (true)
            {
                Console.WriteLine("Knight Tour Problem");
                Knight knight = new Knight();
                knight.SolveKnightTour(6, 1, 1);
                knight.FindAllSolutions(5, 1, 1);
            }
        }
    }
}
