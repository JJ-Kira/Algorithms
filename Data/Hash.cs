using System.Text;

namespace Algorithms.Data
{
    // Generic Hash class that can handle different types of data (e.g., int, double, string)
    internal class Hash<T>
    {
        private const int prime = 250766; // A prime number used in the hash function for strings

        private int m; // The initial size of the hash table
        private int n; // The number of elements currently in the hash table

        // TODO: change to LinkedList
        public List<T>[] hash; // The hash table, an array of lists to handle collisions

        double alfa = (Math.Sqrt(5) - 1) / 2; // Constant used in the hash function for integers
        public double ratio; // Load factor of the hash table, used to decide when to resize

        // Constructor to initialize the hash table
        public Hash()
        {
            m = 10; // Initial size of the hash table
            n = 0;  // Initially, the table is empty
            hash = new List<T>[m]; // Create the hash table with 'm' buckets
            for (int i = 0; i < m; i++)
                hash[i] = new List<T>(); // Initialize each bucket as an empty list
        }

        // Method to print the current state of the hash table
        public void PrintHashTable()
        {
            Console.WriteLine($"Array elements: {n} / {m}");

            for (int i = 0; i < hash.Length; i++)
            {
                Console.Write($"{i}: ");
                foreach (T t in hash[i])
                    Console.Write($"{t} ");
                Console.WriteLine();
            }
        }

        // Hash function for strings
        private int HashString(string val, int m)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(val); // Convert string to byte array
            ulong hashValue = 0;
            ulong pow = 1;
            ulong mod = (ulong)m;

            // Calculate hash value using a polynomial rolling hash function
            for (int i = 0; i < val.Length; i++)
            {
                hashValue = (uint)(hashValue + byteArray[i] * pow);
                pow *= prime;
            }

            hashValue = (hashValue % mod);
            return (int)hashValue;
        }

        // Hash function for integers
        private int HashInt(int val)
        {
            // Use multiplication method for hashing integers
            uint hashValue = (uint)Math.Floor(m * ((val * alfa) % 1)); //modulo
            return (int)(hashValue % m);
        }

        // Hash function for long
        private int HashLong(long val)
        {
            // Use the same method as for integers
            return HashInt((int)val);
        }

        // Hash function for doubles
        public int HashDouble(double a)
        {
            int hashValue;
            hashValue = (int)(a * 19); // Simple multiplication hash function
            hashValue = HashInt(hashValue) % m; // Reuse the integer hash function
            return hashValue;
        }

        // Hash function for char*
        private int HashCharPointer(char[] val)
        {
            // Calculate the hash based on the ASCII values of characters
            ulong hashValue = 0;
            ulong pow = 1;
            ulong mod = (ulong)m;

            for (int i = 0; i < val.Length; i++)
            {
                hashValue = (uint)(hashValue + val[i] * pow);
                pow *= prime;
            }

            hashValue = (hashValue % mod);
            return (int)hashValue;
        }

        // Hash function for a custom structure
        private int HashCustomStructure(CustomStructure val) //option to set user's own hashing function
        {
            // For simplicity, let's assume the structure has two fields of type int
            return HashInt(val.Field1 + val.Field2);
        }

        // Method to insert an array of values into the hash table
        public void HashInsert(T[] value)
        {
            for (int i = 0; i < value.Length; i++)
                HashInsert(value[i]);
        }

        // Check if the hash table needs to be resized based on the load factor
        private bool CheckToResize()
        {
            if ((ratio > 0.9 || ratio < 0.3) & n > 10)
                return true;
            else
                return false;
        }

        // Method to perform the actual hashing and optionally add the value to the hash table
        private int PerformHash(T value, bool option)
        {
            Type type = typeof(T);
            int ind;

            // Hashing based on the type of the value
            if (type == typeof(int))
            {
                ind = HashInt(Convert.ToInt32(value));
                if (option) hash[ind].Add(value);
                if (hash[ind].Contains(value)) return ind;
            }
            if (type == typeof(double))
            {
                ind = HashDouble(Convert.ToDouble(value));
                if (option) hash[ind].Add(value);
                if (hash[ind].Contains(value)) return ind;
            }
            if (type == typeof(string))
            {
                ind = HashString(value.ToString(), m);
                if (option) hash[ind].Add(value);
                if (hash[ind].Contains(value)) return ind;
            }
            if (type == typeof(long))
            {
                ind = HashLong(Convert.ToInt64(value));

                if (option)
                    hash[ind].Add(value);

                if (hash[ind].Contains(value))
                    return ind;
            }
            if (type == typeof(char[]))
            {
                ind = HashCharPointer(value as char[]);

                if (option)
                    hash[ind].Add(value);

                if (hash[ind].Contains(value))
                    return ind;
            }
            if (type == typeof(CustomStructure))
            {
                ind = HashCustomStructure((CustomStructure)(object)value);

                if (option)
                    hash[ind].Add(value);

                if (hash[ind].Contains(value))
                    return ind;
            }
            return -1;
        }

        // Method to resize the hash table when necessary
        private void ResizeTable()
        {
            List<T>[] hashTemp = new List<T>[m]; // Temporary hash table for resizing

            for (int i = 0; i < m; i++)
                hashTemp[i] = new List<T>(hash[i]);

            // Resize logic
            if (ratio > 0.9) // If the table is too full, double its size
            {
                m *= 2;
                hash = new List<T>[m];
                for (int i = 0; i < m; i++)
                    hash[i] = new List<T>();

                // Rehash all elements in the new table
                for (int i = 0; i < hashTemp.Length; i++)
                    for (int j = 0; j < hashTemp[i].Count; j++)
                    {
                        PerformHash(hashTemp[i][j], true);
                    }
            }
            else if (ratio < 0.3) // If the table is too empty, halve its size
            {
                m /= 2;
                hash = new List<T>[m];
                for (int i = 0; i < m; i++)
                    hash[i] = new List<T>();

                // Rehash all elements in the new table
                for (int i = 0; i < hashTemp.Length; i++)
                    for (int j = 0; j < hashTemp[i].Count; j++)
                    {
                        PerformHash(hashTemp[i][j], true);
                    }
            }
        }

        // Method to insert a single value into the hash table
        public void HashInsert(T value)
        {
            ratio = n / (double)m; // Update the load factor

            if (!CheckToResize())
            {
                n++;
                PerformHash(value, true);
            }
            else
            {
                ResizeTable();
                n++;
                PerformHash(value, true);
            }
        }

        // Method to delete a value from the hash table
        public void HashDelete(T value)
        {
            n--;
            int ind = HashSearch(value);

            if (hash[ind].Contains(value))
                hash[ind].Remove(value);
        }

        // Method to search for a value in the hash table and return its index
        public int HashSearch(T value)
        {
            return PerformHash(value, false);
        }
    }

    // Example of a custom structure
    public struct CustomStructure
    {
        public int Field1;
        public int Field2;
    }
}
