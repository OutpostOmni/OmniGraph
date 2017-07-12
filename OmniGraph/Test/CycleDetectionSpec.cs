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

namespace OmniGraph.Tests {
    [TestFixture]
    public class CycleDetectionSpec {
        [Test]
        public void HandlesIncompleteCubeCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0},
                new int[] {1, 0, 0},
                new int[] {1, 0, 0},
                new int[] {1, 1, 0},
                new int[] {0, 0, 0}
            };

            var d = new CycleDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Cycles.Count, 0);
        }

        [Test]
        public void ReadsCubeCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0},
                new int[] {1, 1, 1},
                new int[] {1, 0, 1},
                new int[] {1, 1, 1},
                new int[] {0, 0, 0}
            };

            var d = new CycleDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Cycles.Count, 1);
            Assert.AreEqual(d.Cycles[0].Vertices.Count, 4);
            Assert.AreEqual(d.Cycles[0].TriangleSample.Centroid, new Point(2, 1));

            Assert.AreEqual(d.Cycles[0].Points.Count, 9);
            Assert.AreEqual(d.Cycles[0].Points[0], new Point(1, 0));
            Assert.AreEqual(d.Cycles[0].Points[4], new Point(3, 2));
            Assert.AreEqual(d.Cycles[0].Points[8], new Point(1, 0));
        }

        [Test]
        public void ReadsCubeWithStrandsCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0, 0, 0},
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 1, 0, 0},
                new int[] {1, 1, 1, 0, 0},
                new int[] {0, 0, 0, 0, 0}
            };

            var d = new CycleDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Cycles.Count, 1);
            Assert.AreEqual(d.Cycles[0].TriangleSample.Centroid, new Point(2, 1));

            Assert.AreEqual(d.Cycles[0].Points.Count, 9);
            Assert.AreEqual(d.Cycles[0].Points[0], new Point(1, 0));
            Assert.AreEqual(d.Cycles[0].Points[4], new Point(3, 2));
            Assert.AreEqual(d.Cycles[0].Points[8], new Point(1, 0));
        }


        [Test]
        public void ReadsJoinedCubesCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0, 0, 0},
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 1, 0, 1},
                new int[] {1, 1, 1, 1, 1},
                new int[] {0, 0, 0, 0, 0}
            };

            var d = new CycleDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Cycles.Count, 3);

            Assert.AreEqual(d.Cycles[0].Points.Count, 9);
            Assert.AreEqual(d.Cycles[0].Points[0], new Point(3, 2));
            Assert.AreEqual(d.Cycles[0].Points[4], new Point(1, 4));
            Assert.AreEqual(d.Cycles[0].Points[8], new Point(3, 2));
            Assert.AreEqual(d.Cycles[0].TriangleSample.Centroid, new Point(2, 3));

            Assert.AreEqual(d.Cycles[1].Points.Count, 13);
            Assert.AreEqual(d.Cycles[1].Points[0], new Point(1, 0));
            Assert.AreEqual(d.Cycles[1].Points[6], new Point(3, 4));
            Assert.AreEqual(d.Cycles[1].Points[12], new Point(1, 0));
            Assert.AreEqual(d.Cycles[1].TriangleSample.Centroid, new Point(2, 1));

            Assert.AreEqual(d.Cycles[2].Points.Count, 9);
            Assert.AreEqual(d.Cycles[2].Points[0], new Point(1, 0));
            Assert.AreEqual(d.Cycles[2].Points[4], new Point(3, 2));
            Assert.AreEqual(d.Cycles[2].Points[8], new Point(1, 0));
            Assert.AreEqual(d.Cycles[2].TriangleSample.Centroid, new Point(2, 1));
        }

        [Test]
        public void ReadsUnequalJoinedCubesCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0, 0, 0},
                new int[] {1, 1, 1, 0, 0},
                new int[] {1, 0, 1, 1, 1},
                new int[] {1, 0, 1, 0, 1},
                new int[] {1, 1, 1, 1, 1},
                new int[] {0, 0, 0, 0, 0}
            };

            var d = new CycleDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Cycles.Count, 3);

            Assert.AreEqual(d.Cycles[0].Points.Count, 9);
            Assert.AreEqual(d.Cycles[0].Points[0], new Point(4, 2));
            Assert.AreEqual(d.Cycles[0].Points[4], new Point(2, 4));
            Assert.AreEqual(d.Cycles[0].Points[8], new Point(4, 2));
            Assert.AreEqual(d.Cycles[0].TriangleSample.Centroid, new Point(3, 3));

            Assert.AreEqual(d.Cycles[1].Points.Count, 15);
            Assert.AreEqual(d.Cycles[1].Points[0], new Point(1, 0));
            Assert.AreEqual(d.Cycles[1].Points[6], new Point(4, 3));
            Assert.AreEqual(d.Cycles[1].Points[14], new Point(1, 0));
            Assert.AreEqual(d.Cycles[1].TriangleSample.Centroid, new Point(3, 1));

            Assert.AreEqual(d.Cycles[2].Points.Count, 11);
            Assert.AreEqual(d.Cycles[2].Points[0], new Point(1, 0));
            Assert.AreEqual(d.Cycles[2].Points[4], new Point(4, 1));
            Assert.AreEqual(d.Cycles[2].Points[10], new Point(1, 0));
            Assert.AreEqual(d.Cycles[2].TriangleSample.Centroid, new Point(3, 1));
        }

        [Test]
        public void ReadsVerticalJoinedCubesCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0, 0, 0},
                new int[] {0, 0, 0, 0, 0},
                new int[] {0, 0, 1, 1, 1},
                new int[] {0, 0, 1, 0, 1},
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1}
            };

            var d = new CycleDetection(new Point(4, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            foreach (var cycle in d.Cycles) {
                Console.WriteLine("final cycle: " + cycle);
            }

            Assert.AreEqual(d.Cycles.Count, 3);

            Assert.AreEqual(d.Cycles[0].Points.Count, 9);
            Assert.AreEqual(d.Cycles[0].Points[0], new Point(4, 4));
            Assert.AreEqual(d.Cycles[0].Points[4], new Point(2, 2));
            Assert.AreEqual(d.Cycles[0].Points[8], new Point(4, 4));
            Assert.AreEqual(d.Cycles[0].TriangleSample.Centroid, new Point(3, 3));

            Assert.AreEqual(d.Cycles[1].Points.Count, 19);
            Assert.AreEqual(d.Cycles[1].Points[0], new Point(4, 0));
            Assert.AreEqual(d.Cycles[1].Points[9], new Point(5, 4));
            Assert.AreEqual(d.Cycles[1].Points[18], new Point(4, 0));
            Assert.AreEqual(d.Cycles[1].TriangleSample.Centroid, new Point(6, 1));
        }
    }
}
