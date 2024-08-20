public static class Solution88
{
    public static void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        Array.Copy(nums2, 0, nums1, m, n);

        for (var i = 1; i < nums1.Length; i++)
        {
            var temp = nums1[i];

            var location = Math.Abs(Array.BinarySearch(nums1, 0, i, temp) + 1);

            Array.Copy(nums1, location, nums1, location + 1, i - location);

            nums1[location] = temp;
        }
    }
}