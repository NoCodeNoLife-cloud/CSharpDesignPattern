namespace Strategy.Strategies
{
    /// <summary>
    /// Merge sort implementation
    /// Stable divide-and-conquer sorting algorithm
    /// </summary>
    public class MergeSortStrategy<T> : ISortingStrategy<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> data)
        {
            if (data == null || data.Count <= 1)
                return new List<T>(data ?? new List<T>());

            var result = new List<T>(data);
            MergeSort(result, 0, result.Count - 1);
            return result;
        }

        private void MergeSort(List<T> arr, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
        }

        private void Merge(List<T> arr, int left, int mid, int right)
        {
            int leftSize = mid - left + 1;
            int rightSize = right - mid;

            var leftArray = new T[leftSize];
            var rightArray = new T[rightSize];

            // Copy data to temporary arrays
            for (int i = 0; i < leftSize; i++)
                leftArray[i] = arr[left + i];
            for (int j = 0; j < rightSize; j++)
                rightArray[j] = arr[mid + 1 + j];

            // Merge the temporary arrays
            int leftIndex = 0, rightIndex = 0, mergeIndex = left;
            while (leftIndex < leftSize && rightIndex < rightSize)
            {
                if (leftArray[leftIndex].CompareTo(rightArray[rightIndex]) <= 0)
                {
                    arr[mergeIndex] = leftArray[leftIndex];
                    leftIndex++;
                }
                else
                {
                    arr[mergeIndex] = rightArray[rightIndex];
                    rightIndex++;
                }
                mergeIndex++;
            }

            // Copy remaining elements
            while (leftIndex < leftSize)
            {
                arr[mergeIndex] = leftArray[leftIndex];
                leftIndex++;
                mergeIndex++;
            }

            while (rightIndex < rightSize)
            {
                arr[mergeIndex] = rightArray[rightIndex];
                rightIndex++;
                mergeIndex++;
            }
        }

        public string GetName()
        {
            return "Merge Sort";
        }

        public string GetTimeComplexity()
        {
            return "O(n log n) guaranteed - stable sorting";
        }
    }
}