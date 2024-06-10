using System.Collections;

namespace AudioFingerprinting_ver_1
{
    public class CompactMFCC
    {
        private List<float[]> mfcc;
        private BitArray stringArray;
        public CompactMFCC(List<float[]> mfcc)
        {
            this.mfcc = mfcc;

            int stringLength = (mfcc.Count - 1) * (mfcc[0].Length - 1);
            stringArray = new BitArray(stringLength);
        }
        public CompactMFCC()
        {
            this.mfcc = null;
            this.stringArray = null;
        }

        public BitArray ComputeCompactMFCC()
        {
            if (mfcc == null)
                throw new ArgumentNullException("Mfcc is null");

            for (int i = 1; i < mfcc.Count; i++)
            {
                for (int j = 0; j < mfcc[i].Length - 1; j++)
                {
                    float result = (mfcc[i][j] - mfcc[i][j + 1]) - (mfcc[i - 1][j] - mfcc[i - 1][j + 1]);
                    if (result > 0)
                        stringArray[(mfcc[i].Length - 1) * (i - 1) + j] = true;
                    else
                        stringArray[(mfcc[i].Length - 1) * (i - 1) + j] = false;
                }
            }

            return stringArray;
        }

        public void WriteCompactMFCCToFile(string filename)
        {
            byte[] resultArray = new byte[stringArray.Count / 8 + 1];
            BitArray rightOrderBits = new BitArray(stringArray.Cast<bool>().Reverse().ToArray());
            rightOrderBits.CopyTo(resultArray, 0);
            File.WriteAllBytes(filename, resultArray);
        }

        public BitArray ReadCompactMFCCFromFile(string filename)
        {
            byte[] data = File.ReadAllBytes(filename);
            BitArray reversedBits = new BitArray(data);
            BitArray rightOrderBits = new BitArray(reversedBits.Cast<bool>().Reverse().ToArray());

            return rightOrderBits;
        }
    }
}
