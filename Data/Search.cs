public class Search
{
    private string text; // The text in which we are searching for patterns.

    // Constructor: Initializes a new instance of the Search class with the specified text.
    public Search(string text)
    {
        this.text = text;
    }

    // Naive string search method.
    // This method searches for the pattern in the text in a straightforward manner.
    public int NormalSearch(string pattern)
    {
        int M = pattern.Length; // Length of the pattern
        int N = text.Length; // Length of the text

        // Loop through the text
        for (int i = 0; i <= N - M; i++)
        {
            int j;
            // Loop through the pattern
            for (j = 0; j < M; j++)
            {
                // Break if a mismatch is found
                if (text[i + j] != pattern[j])
                    break;
            }
            // If the whole pattern was found, return the start index
            if (j == M)
                return i; // Found at index i
        }

        return -1; // Pattern not found
    }

    // Knuth-Morris-Pratt (KMP) string search method.
    // This method uses a precomputed longest prefix suffix array to improve search efficiency.
    public int KMP(string pattern)
    {
        int M = pattern.Length; // Length of the pattern
        int N = text.Length; // Length of the text
        int[] lps = new int[M]; // Longest prefix suffix array
        int j = 0; // Index for pattern[]

        // Preprocess the pattern (calculate lps[] array)
        ComputeLPSArray(pattern, M, lps);

        int i = 0; // Index for text[]
        while (i < N)
        {
            if (pattern[j] == text[i])
            {
                j++;
                i++;
            }
            if (j == M)
            {
                return i - j; // Found at index i - j
                j = lps[j - 1];
            }
            // Mismatch after j matches
            else if (i < N && pattern[j] != text[i])
            {
                // Do not match lps[0..lps[j-1]] characters,
                // they will match anyway
                if (j != 0)
                    j = lps[j - 1];
                else
                    i = i + 1;
            }
        }

        return -1; // Pattern not found
    }

    // Computes the Longest Prefix Suffix (LPS) array.
    // The LPS array stores the length of the longest prefix which is also a suffix for each substring of the pattern.
    private void ComputeLPSArray(string pattern, int M, int[] lps)
    {
        int length = 0; // Length of the previous longest prefix suffix
        lps[0] = 0; // lps[0] is always 0
        int i = 1;

        // The loop calculates lps[i] for i = 1 to M-1
        while (i < M)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else // (pattern[i] != pattern[length])
            {
                if (length != 0)
                {
                    length = lps[length - 1];
                    // Also, note that we do not increment i here
                }
                else // if (length == 0)
                {
                    lps[i] = length;
                    i++;
                }
            }
        }
    }
}