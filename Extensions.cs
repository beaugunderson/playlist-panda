﻿namespace PlaylistPanda
{
    public static class Extensions
    {
        /// <summary>
        /// Get the string slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends
            if (end < 0)
            {
                end = source.Length - start - end - 1;
            }

            int len = end - start;

            // Return new array
            T[] res = new T[len];

            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }

            return res;
        }
    }
}