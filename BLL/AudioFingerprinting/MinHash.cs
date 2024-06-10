namespace BLL
{
    public class MinHash
    {
        private int numHashFunctions;
        private int[][] hashFunctions;

        public MinHash(int numHashFunctions = 25 )
        {
            this.numHashFunctions = numHashFunctions;
            hashFunctions = new int[numHashFunctions][];
            GenerateHashFunctions();
        }

        private void GenerateHashFunctions()
        {
            Random random = new Random(500);
            for (int i = 0; i < numHashFunctions; i++)
            {
                hashFunctions[i] = new int[2];
                hashFunctions[i][0] = random.Next();
                hashFunctions[i][1] = random.Next();
            }
        }

        private int Hash(int x, int a, int b)
        {
            return (a * x + b) % int.MaxValue;
        }

        public int[] ComputeMinHashSignature(bool[] set)
        {
            int[] signature = new int[numHashFunctions];
            for (int i = 0; i < numHashFunctions; i++)
            {
                int minHash = int.MaxValue;
                for (int index = 0; index < set.Length; index++)
                {
                    if (set[index])
                    {
                        int hashValue = Hash(index, hashFunctions[i][0], hashFunctions[i][1]);
                        if (hashValue < minHash)
                        {
                            minHash = hashValue;
                        }
                    }
                }
                signature[i] = minHash;
            }
            return signature;
        }

        public static double ComputeJaccardSimilarity(int[] sig1, int[] sig2)
        {
            if (sig1.Length != sig2.Length)
            {
                throw new ArgumentException("Signatures must have the same length");
            }

            int count = 0;
            for (int i = 0; i < sig1.Length; i++)
            {
                if (sig1[i] == sig2[i])
                {
                    count++;
                }
            }

            return (double)count / sig1.Length;
        }
    }
}
