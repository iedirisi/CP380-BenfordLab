using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BenfordLab
{
    public class BenfordData
    {
        public int Digit { get; set; }
        public int Count { get; set; }

        public BenfordData() { }
    }

    public class Benford
    {
       
        public static IEnumerable<BenfordData> D2 { get; private set;}

        public static BenfordData[] calculateBenford(string csvFilePath)
        {
            // load the data
            var data = File.ReadAllLines(csvFilePath)
                .Skip(1) // For header
                .Select(s => Regex.Match(s, @"^(.*?),(.*?)$"))
                .Select(data => new
                {
                    Country = data.Groups[1].Value,
                    Population = int.Parse(data.Groups[2].Value)
                });

            // manipulate the data!
            //
            // Select() with:
            //   - Country
            //   - Digit (using: FirstDigit.getFirstDigit() )
            // 
            // Then:
            //   - you need to count how many of *each digit* there are
            //
            // Lastly:
            //   - transform (select) the data so that you have a list of
            //     BenfordData objects
            //
            int z=0;
            foreach(var s in data)
            { 
                z++;
            }
            int[] arr=new int[z];
            int j=0;
            foreach(var s in data)
            { 
               arr[j]=FirstDigit.getFirstDigit(s.Population);
                j++;
            }
            List<BenfordData> D = new List<BenfordData>();
            for(int i=1; i<0; i++)
            { 
                int a1=0;
                for(int a=0;a<arr.Length;a++)
                { 
                    if(i== arr[a])
                    { 
                        a1++;
                    }
                }
                D.Add(new BenfordData
                { 
                    Digit =i,
                    Count = a1
                });
                D.Concat(D);
            }
            var m = D;

            return m.ToArray();
        }
    }
}
