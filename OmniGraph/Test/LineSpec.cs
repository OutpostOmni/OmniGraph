/**
 * This file is part of OmniGraph, licensed under the MIT License (MIT).
 *
 * Copyright (c) 2017 Helion3 http://helion3.com/
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using NUnit.Framework;
using OmniGraph.Structures;

namespace OmniGraph.Tests {
    [TestFixture]
    public class LineSpec {
        [Test]
        public void VerticalLineContainsPoint() {
            var start = new Point(0, 0);
            var end = new Point(0, 6);

            var line = new Line(start, end, new Point(0, 0));

            Assert.AreEqual(line.ContainsPoint(start), true);
            Assert.AreEqual(line.ContainsPoint(end), true);

            // Point between the start/end
            Assert.AreEqual(line.ContainsPoint(new Point(0, 5)), true);

            // Point outside the start/end
            Assert.AreEqual(line.ContainsPoint(new Point(0, 7)), true);

            Assert.AreEqual(line.ContainsPoint(new Point(1, 1)), false);
        }

        [Test]
        public void HorizontalLineContainsPoint() {
            var start = new Point(0, 0);
            var end = new Point(6, 0);

            var line = new Line(start, end, new Point(0, 0));

            Assert.AreEqual(line.ContainsPoint(start), true);
            Assert.AreEqual(line.ContainsPoint(end), true);

            // Point between the start/end
            Assert.AreEqual(line.ContainsPoint(new Point(5, 0)), true);

            // Point outside the start/end
            Assert.AreEqual(line.ContainsPoint(new Point(7, 0)), true);

            Assert.AreEqual(line.ContainsPoint(new Point(1, 1)), false);
        }
    }
}
