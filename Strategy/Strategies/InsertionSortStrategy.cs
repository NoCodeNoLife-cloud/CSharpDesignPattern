namespace Strategy.Strategies
{
    /// <summary>
    /// Insertion sort implementation
    /// Efficient for small datasets and nearly sorted data
    /// </summary>
    public class InsertionSortStrategy<T> : ISortingStrategy<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> data)
        {
            if (data == null || data.Count <= 1)
                return new List<T>(data ?? new List<T>());

            var result = new List<T>(data);
            
            for (int i = 1; i < result.Count; i++)
            {
                T key = result[i];
                int j = i - 1;

                // Move elements greater than key one position ahead
                while (j >= 0 && result[j].CompareTo(key) > 0)
                {
                    result[j + 1] = result[j];
                    j--;
                }
                
                result[j + 1] = key;
            }
            
            return result;
        }

        public string GetName()
        {
            return "Insertion Sort";
        }

        public string GetTimeComplexity()
        {
            return "O(n²) average/worst case, O(n) best case - adaptive";
        }
    }
}