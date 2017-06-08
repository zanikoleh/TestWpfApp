using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace TestWpfApp
{
    public class NaturalTextResolver
    {
        public string Text { get; set; }
        public Dictionary<string, int> Substrings { get; set; }
        public double K { get { return Substrings.Count; } }
        public double T { get { return Text.Length; } }
        public double V { get { return T == 0 ? 0 : K / T; } }
        public string[,] SubstringsGridData
        {
            get
            {
                var result = new string[2, Substrings.Count];
                int i = 0;
                foreach(var str in Substrings)
                {
                    result[0, i] = str.Key;
                    result[1, i] = str.Value.ToString();
                    i++;
                }

                return result;
            }
        }

        public NaturalTextResolver()
        {
            Text = string.Empty;
            Substrings = new Dictionary<string, int>();
        }

        public NaturalTextResolver(string inputData): this(inputData, new Dictionary<string, int>())
        {

        }

        public NaturalTextResolver(string inputData, Dictionary<string, int> resultOfSubstrings)
        {
            Text = inputData;
            Substrings = resultOfSubstrings;
            Calculate(Text, resultOfSubstrings);
        }

        public override string ToString()
        {
            return string.Format("V = {0}", V);
        }

        private void Calculate(string inputData, Dictionary<string, int> resultOfSubstrings)
        {
            var inputLength = inputData.Length;

            for (int i = 0; i < inputLength; i++)
            {
                string currentSubstring = inputData[i].ToString();

                for (int j = i; j < inputLength; j++)
                {
                    string remainingInputData = inputData.Substring(j + 1);
                    if (remainingInputData.Contains(currentSubstring) && IsNotEqualToLastCharacterValue(currentSubstring, inputData))
                    {
                        currentSubstring += inputData[j + 1];
                    }
                    else
                    {
                        if (currentSubstring.Length > 1)
                        {
                            currentSubstring = currentSubstring.Remove(currentSubstring.Length - 1);
                            if (resultOfSubstrings.ContainsKey(currentSubstring))
                            {
                                //resultOfSubstrings[currentSubstring]++;
                            }
                            else
                            {
                                var count = Regex.Matches(inputData, Regex.Escape(currentSubstring)).Count;
                                //var count = inputData.Select((c, f) => inputData.Substring(f)).Count(sub => sub.StartsWith(currentSubstring));
                                resultOfSubstrings.Add(currentSubstring, count);
                            }
                        }
                        break;
                    }
                }
            }
        }

        //Не зрозуміло з опису чому не враховуються x на початку і в кінці. Скоріш за все це через умову - except for the last character.
        private static bool IsNotEqualToLastCharacterValue(string currentSubstring, string inputData)
        {
            return !currentSubstring.Equals(inputData[inputData.Length - 1].ToString());
        }
    }
}