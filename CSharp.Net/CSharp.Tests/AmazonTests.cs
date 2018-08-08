using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class AmazonTests
    {
        static string FindNumber(int[] arr, int k) 
        {
        
            return arr.Skip(1)
                .Take(arr.First())
                .Contains(k)
                ? "YES"
                : "NO";

        }
        
        [Test]
        public void CanFindNumber1()
        {
            var input = new[] {5, 1, 2, 3, 4, 5};
            
            Assert.AreEqual("YES", FindNumber(input, 1) );
        }
        
        [Test]
        public void CanFindNumber5()
        {                        
            var input = new[] {3, 2, 3, 1};            
            Assert.AreEqual("NO", FindNumber(input, 5) );
        }

        [Test]
        public void RealTest()
        {
           

        }
        
        int[] getMinimumDifference(string[] a, string[] b) {
            var res = new List<int>();
            var dictA = new Dictionary<char, int>();
            var dictB = new Dictionary<char, int>();                
            for (int len = 0; len < a.Length; len++) {
                var left = a[len];
                var right = b[len];            
                if (left.Length != right.Length)
                    res.Add(-1);
                else{
                    for (int lLen = 0; lLen < left.Length; lLen++)
                        if (dictA.ContainsKey(left[lLen]))
                            dictA[left[lLen]]++;
                        else dictA.Add(left[lLen], 1);
                    for (int rLen = 0; rLen < right.Length; rLen++)
                        if (dictB.ContainsKey(right[rLen]))
                            dictB[right[rLen]]++;
                        else dictB.Add(right[rLen], 1);
                    var count = 0;
                    foreach(var kvp in dictA)
                    {                    
                        if (dictB.ContainsKey(kvp.Key)){
                            count += System.Math.Abs(kvp.Value - dictB[kvp.Key]);
                        }
                        else count += kvp.Value;                    
                    }
                    res.Add(count);
                
                
                }
            }
            return res.ToArray();

        }
        
    }
}