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
using System;

namespace OmniGraph.Test {
    [TestFixture]
    public class SpatialIndexSpec {
        [Test]
        public void AddsAndRemovesSingleEntry() {
            var index = new SpatialIndex<MockGameObject>();

            var p = new Point(0, 0);
            var obj = new MockGameObject(p);
            index.Insert(p, obj);

            // Validate total buckets
            Assert.AreEqual(index.Keys.Count, 1);

            // Validate bucket count
            var bucket = index.Get(p);
            Assert.AreEqual(bucket.Count, 1);

            index.Remove(p, obj);

            // Validate total buckets
            Assert.AreEqual(index.Keys.Count, 0);
        }

        [Test]
        public void AddsMultipleAndRemovesSingleEntry() {
            var index = new SpatialIndex<MockGameObject>();

            var p1 = new Point(0, 0);
            var obj = new MockGameObject(p1);
            index.Insert(p1, obj);

            var p2 = new Point(-55, 654);
            index.Insert(p2, new MockGameObject(p2));

            // Validate total buckets
            Assert.AreEqual(index.Keys.Count, 2);

            // Validate bucket count
            var bucket = index.Get(p1);
            Assert.AreEqual(bucket.Count, 1);

            index.Remove(p1, obj);

            // Validate total buckets
            Assert.AreEqual(index.Keys.Count, 1);
        }
    }
}
