namespace Strategy.Strategies
{
    /// <summary>
    /// Quick sort implementation
    /// Divide-and-conquer sorting algorithm
    /// </summary>
    public class QuickSortStrategy<T> : ISortingStrategy<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> data)
        {
            if (data == null || data.Count <= 1)
                return new List<T>(data ?? new List<T>());

            var result = new List<T>(data);
            QuickSort(result, 0, result.Count - 1);
            return result;
        }

        private void QuickSort(List<T> arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);
                QuickSort(arr, low, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, high);
            }
        }

        private int Partition(List<T> arr, int low, int high)
        {
            T pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }

            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return i + 1;
        }

        public string GetName()
        {
            return "Quick Sort";
        }

        public string GetTimeComplexity()
        {
            return "O(n log n) average/best case, O(n²) worst case";
        }
    }
}