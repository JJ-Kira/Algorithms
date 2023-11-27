namespace Algorithms.Transfomations
{
    internal class Combination<T>
    {
        // Stores a single combination
        private List<T> aCombination = new List<T>();

        // Stores all generated combinations
        private List<List<T>> allCombinations = new List<List<T>>();

        // Displays all generated combinations
        private void PrintCombinations()
        {
            foreach (var combination in allCombinations)
            {
                foreach (var item in combination)
                    Console.Write(item + " ");
                Console.WriteLine();
            }
        }

        // Method to generate combinations and print them
        public void CombinateAndPrint(T[] data, int k)
        {
            try
            {
                // Generate combinations based on input data and length 'k'
                Combinate(data, k, 0);
                // Display all generated combinations
                PrintCombinations();
            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect size of arrays!");
            }
        }

        // Method to generate combinations using recursion
        private void Combinate(T[] data, int k, int step)
        {
            if (k == 0)
            {
                // Add the generated combination to the list of all combinations
                allCombinations.Add(new List<T>(aCombination));
            }
            else
            {
                // Iterate through the data array starting from 'step'
                for (int i = step; i < data.Length; i++)
                {
                    // Add the element to the current combination
                    aCombination.Add(data[i]);
                    // Recursively generate combinations with reduced length 'k'
                    Combinate(data, k - 1, i + 1);
                    // Remove the last added element to backtrack and explore other combinations
                    aCombination.RemoveAt(aCombination.Count - 1);
                }
            }
        }
    }
}
