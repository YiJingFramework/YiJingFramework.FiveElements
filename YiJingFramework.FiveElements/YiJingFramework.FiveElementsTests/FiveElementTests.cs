using Microsoft.VisualStudio.TestTools.UnitTesting;
using YiJingFramework.FiveElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.FiveElements.Tests
{
    [TestClass()]
    public class FiveElementTests
    {
        [TestMethod()]
        public void ConvertingTest()
        {
            Assert.AreEqual(0, (int)FiveElement.Wood);
            Assert.AreEqual(1, (int)FiveElement.Fire);
            Assert.AreEqual(2, (int)FiveElement.Earth);
            Assert.AreEqual(3, (int)FiveElement.Metal);
            Assert.AreEqual(4, (int)FiveElement.Water);

            Assert.AreEqual("Wood", FiveElement.Wood.ToString());
            Assert.AreEqual("Fire", FiveElement.Fire.ToString());
            Assert.AreEqual("Earth", FiveElement.Earth.ToString());
            Assert.AreEqual("Metal", FiveElement.Metal.ToString());
            Assert.AreEqual("Water", FiveElement.Water.ToString());
            Assert.AreEqual("Wood", FiveElement.Wood.ToString("G"));
            Assert.AreEqual("Fire", FiveElement.Fire.ToString(null));
            Assert.AreEqual("木", FiveElement.Wood.ToString("C"));
            Assert.AreEqual("火", FiveElement.Fire.ToString("C"));
            Assert.AreEqual("土", FiveElement.Earth.ToString("C"));
            Assert.AreEqual("金", FiveElement.Metal.ToString("C"));
            Assert.AreEqual("水", FiveElement.Water.ToString("C"));

            Assert.IsTrue(FiveElement.TryParse("Wood", out FiveElement r));
            Assert.AreEqual(FiveElement.Wood, r);
            Assert.IsTrue(FiveElement.TryParse("\t\t  Wood\r\n", out r));
            Assert.AreEqual(FiveElement.Wood, r);
            Assert.IsTrue(FiveElement.TryParse("Fire", out r));
            Assert.AreEqual(FiveElement.Fire, r);
            Assert.IsTrue(FiveElement.TryParse("\t   \t  fire\r\n", out r));
            Assert.AreEqual(FiveElement.Fire, r);
            Assert.IsTrue(FiveElement.TryParse("earth", out r));
            Assert.AreEqual(FiveElement.Earth, r);
            Assert.IsTrue(FiveElement.TryParse("Metal", out r));
            Assert.AreEqual(FiveElement.Metal, r);
            Assert.IsTrue(FiveElement.TryParse("  water ", out r));
            Assert.AreEqual(FiveElement.Water, r);
            Assert.IsFalse(FiveElement.TryParse("false", out _));
            Assert.IsFalse(FiveElement.TryParse(null, out _));

            var elements = new FiveElement[] {
                FiveElement.Wood,
                FiveElement.Fire,
                FiveElement.Earth,
                FiveElement.Metal,
                FiveElement.Water
            };
            for (int i = -1000, j = 0; i < 1000; i++)
            {
                Assert.AreEqual(elements[j], (FiveElement)i);
                j++;
                if (j == 5)
                    j = 0;
            }
        }

        [TestMethod()]
        public void ComparingTest()
        {
            Random r = new Random();
            for (int i = 0; i < 20000; i++)
            {
                var fir = (r.Next(-10000, 9999) % 5 + 5) % 5;
                var sec = (r.Next(-10000, 9999) % 5 + 5) % 5;
                var firF = (FiveElement)fir;
                var secF = (FiveElement)sec;
                if (fir == sec)
                {
                    Assert.AreEqual(0, firF.CompareTo(secF));
                    Assert.AreEqual(0, secF.CompareTo(firF));
                    Assert.AreEqual(true, firF.Equals(secF));
                    Assert.AreEqual(true, secF.Equals(firF));
                    Assert.AreEqual(true, firF.Equals((object)secF));
                    Assert.AreEqual(true, secF.Equals((object)firF));
                    Assert.AreEqual(firF.GetHashCode(), secF.GetHashCode());
                    Assert.AreEqual(true, firF == secF);
                    Assert.AreEqual(true, secF == firF);
                    Assert.AreEqual(false, firF != secF);
                    Assert.AreEqual(false, secF != firF);
                }

                else if (fir < sec)
                {
                    Assert.AreEqual(-1, firF.CompareTo(secF));
                    Assert.AreEqual(1, secF.CompareTo(firF));
                    Assert.AreEqual(false, firF.Equals(secF));
                    Assert.AreEqual(false, secF.Equals(firF));
                    Assert.AreEqual(false, firF.Equals((object)secF));
                    Assert.AreEqual(false, secF.Equals((object)firF));
                    Assert.AreNotEqual(firF.GetHashCode(), secF.GetHashCode());
                    Assert.AreEqual(false, firF == secF);
                    Assert.AreEqual(false, secF == firF);
                    Assert.AreEqual(true, firF != secF);
                    Assert.AreEqual(true, secF != firF);
                }

                else // fir > sec
                {
                    Assert.AreEqual(1, firF.CompareTo(secF));
                    Assert.AreEqual(-1, secF.CompareTo(firF));
                    Assert.AreEqual(false, firF.Equals(secF));
                    Assert.AreEqual(false, secF.Equals(firF));
                    Assert.AreEqual(false, firF.Equals((object)secF));
                    Assert.AreEqual(false, secF.Equals((object)firF));
                    Assert.AreNotEqual(firF.GetHashCode(), secF.GetHashCode());
                    Assert.AreEqual(false, firF == secF);
                    Assert.AreEqual(false, secF == firF);
                    Assert.AreEqual(true, firF != secF);
                    Assert.AreEqual(true, secF != firF);
                }
                Assert.AreEqual(false, firF.Equals(null));
                Assert.AreEqual(false, secF.Equals(new object()));
            }
        }
        [TestMethod()]
        public void GeneratingAndOvercomingTest()
        {
            Assert.AreEqual(FiveElementsRelationship.GeneratedByMe,
                FiveElement.Wood.GetRelationship(FiveElement.Fire));
            Assert.AreEqual(FiveElementsRelationship.GeneratedByMe,
                FiveElement.Fire.GetRelationship(FiveElement.Earth));
            Assert.AreEqual(FiveElementsRelationship.GeneratedByMe,
                FiveElement.Earth.GetRelationship(FiveElement.Metal));
            Assert.AreEqual(FiveElementsRelationship.GeneratedByMe,
                FiveElement.Metal.GetRelationship(FiveElement.Water));
            Assert.AreEqual(FiveElementsRelationship.GeneratedByMe,
                FiveElement.Water.GetRelationship(FiveElement.Wood));

            Assert.AreEqual(FiveElementsRelationship.GeneratingMe,
                FiveElement.Wood.GetRelationship(FiveElement.Water));
            Assert.AreEqual(FiveElementsRelationship.GeneratingMe,
                FiveElement.Water.GetRelationship(FiveElement.Metal));
            Assert.AreEqual(FiveElementsRelationship.GeneratingMe,
                FiveElement.Metal.GetRelationship(FiveElement.Earth));
            Assert.AreEqual(FiveElementsRelationship.GeneratingMe,
                FiveElement.Earth.GetRelationship(FiveElement.Fire));
            Assert.AreEqual(FiveElementsRelationship.GeneratingMe,
                FiveElement.Fire.GetRelationship(FiveElement.Wood));

            for (int i = 0; i < 5; i++)
            {
                var woodP = (FiveElement)i;
                var fireP = woodP.GetElement(FiveElementsRelationship.GeneratedByMe);
                var earthP = fireP.GetElement(FiveElementsRelationship.GeneratedByMe);
                var metalP = earthP.GetElement(FiveElementsRelationship.GeneratedByMe);
                var waterP = metalP.GetElement(FiveElementsRelationship.GeneratedByMe);

                Assert.AreEqual(FiveElementsRelationship.GeneratedByMe,
                    woodP.GetRelationship(fireP));
                Assert.AreEqual(FiveElementsRelationship.OvercameByMe,
                    woodP.GetRelationship(earthP));
                Assert.AreEqual(FiveElementsRelationship.OvercomingMe,
                    woodP.GetRelationship(metalP));
                Assert.AreEqual(FiveElementsRelationship.GeneratingMe,
                    woodP.GetRelationship(waterP));

                Assert.AreEqual(fireP,
                    woodP.GetElement(FiveElementsRelationship.GeneratedByMe));
                Assert.AreEqual(earthP,
                    woodP.GetElement(FiveElementsRelationship.OvercameByMe));
                Assert.AreEqual(metalP,
                    woodP.GetElement(FiveElementsRelationship.OvercomingMe));
                Assert.AreEqual(waterP,
                    woodP.GetElement(FiveElementsRelationship.GeneratingMe));
            }
        }
    }
}