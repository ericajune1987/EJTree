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

        private static IEnumerable<List<int>> GetRemoveKeys()
        {
            List<int> keys = new List<int>();
            keys.Add(1);
            keys.Add(6);
            keys.Add(2);
            keys.Add(3);
            keys.Add(4);
            keys.Add(5);
            return new[] { keys };
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

            foreach (int key in keys)
            {
                Boolean added = keySet.AddKey(key);
                Assert.That(added, Is.True);

                size++;
                Assert.That(keySet.Size(), Is.EqualTo(size));
            }

            keys.Sort();
            int idx = 0;
            foreach (int key in keySet)
            {
                Assert.That(key, Is.EqualTo(keys[idx]));
                idx++;
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

            foreach (int key in keys)
            {
                Boolean removed = keySet.RemoveKey(key);
                Assert.That(removed, Is.True);

                size--;
                Assert.That(keySet.Size(), Is.EqualTo(size));
            }

            Assert.That(size, Is.Zero);
            Assert.That(keySet.Empty(), Is.True);

        }
    }
}