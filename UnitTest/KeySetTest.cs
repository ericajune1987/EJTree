using EJTree;
using NuGet.Frameworks;
using System.ComponentModel;
using System.Net.WebSockets;

namespace UnitTest
{
    [TestFixture]
    public class KeySetTest
    {
        private IKeySet<int> keySet;

        private static IEnumerable<List<int>> GetAddKeys()
        {
            List<int> keys = new List<int>();
            keys.Add(1);
            keys.Add(2);
            keys.Add(5);
            keys.Add(6);
            keys.Add(4);
            keys.Add(3);
            return new[] { keys };
        }

        private static List<List<int>> AddResults()
        {
            List<int> res1 = new List<int> { 1 };
            List<int> res2 = new List<int> { 1, 2 };
            List<int> res3 = new List<int> { 1, 2, 5 };
            List<int> res4 = new List<int> { 1, 2, 5, 6 };
            List<int> res5 = new List<int> { 1, 2, 4, 5, 6 };
            List<int> res6 = new List<int> { 1, 2, 3, 4, 5, 6 };

            List<List<int>> ret = new List<List<int>> { res1, res2, res3, res4, res5, res6 };

            return ret;
        }

        private static IEnumerable<List<int>> GetRemoveKeys()
        {
            List<int> keys = new List<int>();
            keys.Add(1);
            keys.Add(6);
            keys.Add(2);
            keys.Add(3);
            keys.Add(5);
            keys.Add(4);
            return new[] { keys };
        }

        private static List<List<int>> RemoveResults()
        {
            List<int> res1 = new List<int> { 2, 3, 4, 5, 6 };
            List<int> res2 = new List<int> { 2, 3, 4, 5 };
            List<int> res3 = new List<int> { 3, 4, 5};
            List<int> res4 = new List<int> { 4, 5 };
            List<int> res5 = new List<int> { 4 };

            List<List<int>> ret = new List<List<int>> { res1, res2, res3, res4, res5 };

            return ret;
        }

        [SetUp]
        public void Setup()
        {
            keySet = new KeySet<int>();
        }

        [Test, TestCaseSource(nameof(GetAddKeys))]
        public void AddKeyTest(List<int> keys)
        {
            int size = 0;
            Assert.That(keySet.Size(), Is.EqualTo(size));

            var addResults = AddResults();

            int it = 0;
            foreach (int key in keys)
            {
                Boolean added = keySet.AddKey(key);
                Assert.That(added, Is.True);

                List<int> addSet = addResults[it];

                int idx = 0;
                foreach(int k in keySet)
                {
                    Assert.That(k, Is.EqualTo(addSet[idx]));
                    idx++;
                } 
                    
                it++;
                size++;
                Assert.That(keySet.Size(), Is.EqualTo(size));
            }
        }

        [Test, TestCaseSource(nameof(GetRemoveKeys))]
        public void RemoveKeyTest(List<int> keys)
        {
            foreach (var l in GetAddKeys())
                foreach (var k in l)
                    keySet.AddKey(k);

            int size = keys.Count;
            Assert.That(keySet.Size(), Is.EqualTo(size));

            var removeResults = RemoveResults();

            int it = 0;
            foreach (int key in keys)
            {
                Boolean removed = keySet.RemoveKey(key);
                Assert.That(removed, Is.True);

                if (it < removeResults.Count)
                {
                    List<int> remSet = removeResults[it];

                    int idx = 0;
                    foreach(int k in keySet)
                    {
                        Assert.That(k, Is.EqualTo(remSet[idx]));;
                        idx++;
                    }
                }

                it++;
                size--;
                Assert.That(keySet.Size(), Is.EqualTo(size));
            }

            Assert.That(size, Is.Zero);
            Assert.That(keySet.Empty(), Is.True);

        }
    }
}