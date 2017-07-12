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
    public class FloodFillSpec {
        [Test]
        public void FillsSimpleArea() {
            var map = new int[][] {
                new int[] {0, 0, 0, 0, 0},
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1},
                new int[] {0, 0, 0, 0, 0}
            };

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

            var totalFilled = f.Fill(new Point(2, 1));

            Assert.AreEqual(totalFilled, 9);

            for (var x = 2; x < 5; x++) {
                for (var y = 1; y < 4; y++) {
                    Assert.AreEqual(map[x][y], 2);
                } 
            }
        }

        [Test]
        public void FillsIrregularArea() {
            var map = new int[][] {
                new int[] {0, 0, 1, 1, 1},
                new int[] {1, 1, 0, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1},
                new int[] {0, 0, 0, 0, 0}
            };

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

            var totalFilled = f.Fill(new Point(2, 1));

            Assert.AreEqual(totalFilled, 11);

            Assert.AreEqual(map[1][2], 2);
            Assert.AreEqual(map[1][3], 2);
        }
    }
}
