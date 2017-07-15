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

namespace OmniGraph.Test {
    [TestFixture]
    public class CombinedSpec {
        [Test]
        public void CycleAndFill() {
            var map = new int[][] {
                new int[] {0, 0, 0, 0, 0},
                new int[] {0, 0, 0, 0, 0},
                new int[] {0, 0, 1, 1, 1},
                new int[] {0, 0, 1, 0, 1},
                new int[] {1, 1, 1, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1}
            };

            // Detect shape
            var d = new CycleDetection(new Point(4, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Cycles.Count, 1);

            // Find the centroid of the triangle
            var centroid = d.Cycles[0].TriangleSample.Centroid;
            Assert.AreEqual(centroid, new Point(6, 1));

            // Flood fill the interior of the structure
            var f = new FloodFill((p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    var matches = map[p.x][p.y] == 0;

                    if (matches) {
                        map[p.x][p.y] = 2;
                    }

                    return matches;
                }

                return false;
            });

            var totalFilled = f.Fill(centroid);

            Assert.AreEqual(totalFilled, 8);
        }
    }
}
