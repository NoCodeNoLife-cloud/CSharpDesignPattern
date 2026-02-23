namespace Strategy.Strategies
{
    /// <summary>
    /// Bubble sort implementation
    /// Simple comparison-based sorting algorithm
    /// </summary>
    public class BubbleSortStrategy<T> : ISortingStrategy<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> data)
        {
            if (data == null || data.Count <= 1)
                return new List<T>(data ?? new List<T>());

            var result = new List<T>(data);
            int n = result.Count;
            
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (result[j].CompareTo(result[j + 1]) > 0)
                    {
                        // Swap elements
                        (result[j], result[j + 1]) = (result[j + 1], result[j]);
                        swapped = true;
                    }
                }
                
                // If no swapping occurred, array is sorted
                if (!swapped)
                    break;
            }
            
            return result;
        }

        public string GetName()
        {
            return "Bubble Sort";
        }

        public string GetTimeComplexity()
        {
            return "O(n²) average/worst case, O(n) best case";
        }
    }
}