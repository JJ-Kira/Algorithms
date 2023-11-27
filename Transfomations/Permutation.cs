namespace Algorithms.Transfomations
{
    internal class Permutation<T>
    {
        // Stores a single permutation
        private List<T> aPermutation = new List<T>();

        // Stores all generated permutations
        public List<List<T>> allPermutations = new List<List<T>>();

        // Generates permutations and prints them
        public void PermuteAndPrint(T[] data, int[] usageTimes)
        {
            try
            {
                // Generate permutations
                Permute(data, usageTimes);

                // Print all generated permutations
                PrintPermutations();
            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect input data!");
            }
        }

        // Prints all generated permutations
        private void PrintPermutations()
        {
            foreach (var permutation in allPermutations)
            {
                foreach (var item in permutation)
                    Console.Write(item + " ");
                Console.WriteLine();
            }
        }

        // Method to generate permutations recursively
        public void Permute(T[] data, int[] usageTimes)
        {
            // If the current permutation is complete (when all elements are used)
            if (aPermutation.Count == usageTimes.Length)
            {
                // Add the generated permutation to the list of all permutations
                allPermutations.Add(new List<T>(aPermutation));
            }
            else
            {
                // Iterate through each element in the data array
                for (int i = 0; i < usageTimes.Length; i++)
                {
                    // Check if the current element can still be used
                    if (usageTimes[i] == 0)
                        continue; // Skip if the element's count reaches zero

                    // Decrement the count of the element used
                    usageTimes[i]--;
                    // Add the element to the current permutation
                    aPermutation.Add(data[i]);

                    // Recursively generate permutations with the updated counts
                    Permute(data, usageTimes);

                    // Restore the state before the current iteration
                    usageTimes[i]++;
                    aPermutation.RemoveAt(aPermutation.Count - 1);
                }
            }
        }
    }
}