public class Solution {
    public bool IsMatch(string s, string p) {
        // Initialize the DP table
        bool[] dpCurrent = new bool[p.Length + 1];
        bool[] dpPrevious = new bool[p.Length + 1];

        // Base case: empty string and empty pattern are a match
        dpPrevious[0] = true;

        // Handle patterns like a*, a*b*, a*b*c* etc.
        for (int patternIndex = 1; patternIndex <= p.Length; patternIndex++)
        {
            if (p[patternIndex - 1] == '*')
            {
                dpPrevious[patternIndex] = dpPrevious[patternIndex - 2];
            }
        }

        // Fill the DP table
        for (int textIndex = 1; textIndex <= s.Length; textIndex++)
        {
            dpCurrent[0] = false; // Empty pattern cannot match non-empty text
            for (int patternIndex = 1; patternIndex <= p.Length; patternIndex++)
            {
                if (p[patternIndex - 1] == '.' || p[patternIndex - 1] == s[textIndex - 1])
                {
                    dpCurrent[patternIndex] = dpPrevious[patternIndex - 1];
                }
                else if (p[patternIndex - 1] == '*')
                {
                    dpCurrent[patternIndex] = dpCurrent[patternIndex - 2] ||
                        (p[patternIndex - 2] == '.' || p[patternIndex - 2] == s[textIndex - 1]) && dpPrevious[patternIndex];
                }
                else
                {
                    dpCurrent[patternIndex] = false;
                }
            }
            // Swap current and previous rows
            var temp = dpPrevious;
            dpPrevious = dpCurrent;
            dpCurrent = temp;
        }

        return dpPrevious[p.Length];
    }
}
