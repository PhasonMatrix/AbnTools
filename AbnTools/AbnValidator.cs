using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbnTools
{
    public static class AbnValidator
    {
        private static Dictionary<int, int> positionWeights = new Dictionary<int, int>()
        {
            {0, 10 },
            {1, 1 },
            {2, 3 },
            {3, 5 },
            {4, 7 },
            {5, 9 },
            {6, 11 },
            {7, 13 },
            {8, 15 },
            {9, 17 },
            {10, 19 },
        };


        /// <summary>
        /// Checks that an Australian Busness Number (ABN) is valid, according 
        /// to the Australian Business Register's check algorithm
        /// See: https://abr.business.gov.au/Help/AbnFormat
        /// </summary>
        /// <param name="abn">ABN as string</param>
        /// <returns>True if the ABN is valid, otherwse returns false.</returns>
        public static bool Validate(string abn)
        {
            // remove any spaces
            abn = abn.Replace(" ", "");

            // ABN's must be 11 numerals
            if (!Regex.IsMatch(abn, @"[0-9]{11}")) { return false; }

            // split into 11 integers
            var digits = abn.Select(d => int.Parse(d.ToString())).ToList();

            digits[0] = digits[0] - 1; // subtract 1 from first digit

            var weightedDigits = new List<int>();
            // multiple each digit by the prescribed weighting factor
            for (int i = 0; i < digits.Count; i++)
            {
                weightedDigits.Add(digits[i] * positionWeights[i]);
            }
            int sum = weightedDigits.Sum();

            if (sum % 89 == 0) // abn is Validate if sum-of-weighted-digits mod 89 is zero
            {
                return true;
            }

            return false;
        }
    }
}
