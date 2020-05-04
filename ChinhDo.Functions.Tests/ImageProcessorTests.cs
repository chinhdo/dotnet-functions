using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace ChinhDo.Functions.Tests
{
    [TestClass]
    public class ImageProcessorTests
    {
        private ImageProcessor target1;
        private ImageProcessor target2;

        public ImageProcessorTests()
        {
            target1 = new ImageProcessor();
            target2 = new ImageProcessor(true);
        }

        [TestMethod]
        public void CanDetectNonBlank1()
        {
            Assert.IsFalse(target1.IsBlank("images/rcTruck.jpg"));
            Assert.IsFalse(target2.IsBlank("images/rcTruck.jpg"));
        }

        [TestMethod]
        public void CanDetectNonBlank2()
        {
            Assert.IsFalse(target1.IsBlank("images/lexus.jpg"));
            Assert.IsFalse(target2.IsBlank("images/lexus.jpg"));
        }

        [TestMethod]
        public void CanDetectNonBlank3()
        {
            Assert.IsFalse(target1.IsBlank("images/canvas.jpg"));
            Assert.IsFalse(target2.IsBlank("images/canvas.jpg"));
        }

        [TestMethod]
        public void CanDetectNonBlank4()
        {
            Assert.IsFalse(target1.IsBlank("images/girl1.jpg"));
            Assert.IsFalse(target2.IsBlank("images/girl1.jpg"));
        }

        [TestMethod]
        public void CanDetectNonBlank5()
        {
            Assert.IsFalse(target1.IsBlank("images/pattern.jpg"));
            Assert.IsFalse(target2.IsBlank("images/pattern.jpg"));
        }

        [TestMethod]
        public void CanDetectBlank1()
        {
            Assert.IsTrue(target1.IsBlank("images/blank1.jpg"));
            Assert.IsTrue(target2.IsBlank("images/blank1.jpg"));
        }

        [TestMethod]
        public void CanDetectBlank2()
        {
            Assert.IsTrue(target1.IsBlank("images/blank2.jpg"));
            Assert.IsTrue(target2.IsBlank("images/blank2.jpg"));
        }

        [TestMethod]
        public void CanDetectBlank3()
        {
            Assert.IsTrue(target1.IsBlank("images/blank3.jpg"));
            Assert.IsTrue(target2.IsBlank("images/blank3.jpg"));
        }

        [TestMethod]
        public void CanDetectBlank4()
        {
            Assert.IsTrue(target1.IsBlank("images/blank4.jpg"));
            Assert.IsTrue(target2.IsBlank("images/blank4.jpg"));
        }

        [TestMethod]
        public void CanDetectBlank5()
        {
            Assert.IsTrue(target1.IsBlank("images/girl2.jpg"));
            Assert.IsTrue(target2.IsBlank("images/girl2.jpg"));
        }

        [TestMethod]
        public void Scratch()
        {
            string[] files = Directory.GetFiles("images", "*.jpg", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Console.WriteLine(string.Format("imsg={0} stdDev={1}", file, target1.StdDev(file)));
            }
        }
    }
}
