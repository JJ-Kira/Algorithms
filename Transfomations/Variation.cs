namespace Algorithms.Transfomations
{
    internal class Variation<T>
    {
        // Stores a single variation
        private List<T> aVariation = new List<T>();

        // Stores all generated variations
        private List<List<T>> allVariations = new List<List<T>>();

        // Displays all generated variations
        private void PrintVariations()
        {
            foreach (var variation in allVariations)
            {
                foreach (var item in variation)
                    Console.Write(item + " "); // Output each item of the variation
                Console.WriteLine(); // Move to the next line for the next variation
            }
        }

        // Method to set variation values and generate variations based on conditions
        public void VariateAndPrint(T[] data, int k, int[] usageTimes, bool repetition)
        {
            try
            {
                // Generate variations based on the conditions specified
                if (repetition)
                    VariationWithRepetition(data, k);
                else
                    VariationWithoutRepetition(data, k, usageTimes);

                // Display all generated variations
                PrintVariations();
            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect size of arrays!");
            }
        }

        // Method to generate variations with repetition
        private void VariationWithRepetition(T[] data, int k)
        {
            if (k == 0)
            {
                // Add the generated variation to the list of all variations
                allVariations.Add(new List<T>(aVariation));
            }
            else
            {
                // Iterate through each element in the data array
                for (int i = 0; i < data.Length; i++)
                {
                    // Add the element to the current variation
                    aVariation.Add(data[i]);

                    // Recursively generate variations with repetition
                    VariationWithRepetition(data, k - 1);

                    // Restore the state before the current iteration
                    aVariation.RemoveAt(aVariation.Count - 1);
                }
            }
        }

        // Method to generate variations without repetition
        private void VariationWithoutRepetition(T[] data, int k, int[] usageTimes)
        {
            if (k == 0)
            {
                // Add the generated variation to the list of all variations
                allVariations.Add(new List<T>(aVariation));
            }
            else
            {
                // Iterate through each element in the timeUsed array
                for (int i = 0; i < usageTimes.Length; i++)
                {
                    // Check if the current element can still be used
                    if (usageTimes[i] == 0)
                        continue; // Skip if the element's count reaches zero

                    // Decrement the count of the element used
                    usageTimes[i]--;

                    // Add the element to the current variation
                    aVariation.Add(data[i]);

                    // Recursively generate variations without repetition
                    VariationWithoutRepetition(data, k - 1, usageTimes);

                    // Restore the state before the current iteration
                    usageTimes[i]++;
                    aVariation.RemoveAt(aVariation.Count - 1);
                }
            }
        }
    }
}
