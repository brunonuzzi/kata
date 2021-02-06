using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata.RangeExtraction
{
    public static class Range
    {
        public static void Test()
        {
            Console.WriteLine(Extract(new[] { -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20 }));
            Console.WriteLine("End");
        }
        public static string Extract(int[] args)
        {
            string finalString = "";
            int? previousValue = null;
            int? previousPosition = null;
        
            Array.Resize(ref args, args.Length+1);
            args[args.Length-1] = args[args.Length - 2];

            List<List<int>> arrayGroups = new List<List<int>>();
            List<int> group = new List<int>();

            for (int i = 0; i < args.Length; i++)
            {
                if ((previousValue != null && (previousValue + 1) != args[i])) 
                {
                    group.Add(args[(int)previousPosition]);
                    arrayGroups.Add(group);
                    group = new List<int>();
                    
                }

                if ((previousValue != null && (previousValue + 1) == args[i]))
                {
                    group.Add(args[(int)previousPosition]);
                }

                previousValue = args[i];
                previousPosition = i;
            }

            
            foreach(var item in arrayGroups)
            {
                if (item.Count >= 3)
                {
                    finalString += item.Min() + "-" + item.Max() + ",";
                }
                else
                {
                    finalString += string.Join(",", item.Select(x => x.ToString()).ToArray()) + ",";
                }
            }

            return finalString.Substring(0, finalString.Length-1);  //TODO
        }
    }
}
