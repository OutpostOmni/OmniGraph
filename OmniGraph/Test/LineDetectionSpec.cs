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
    public class LineDetectionSpec {
        [Test]
        public void ReadsVerticalLinesCorrectly() {
            var map = new int[][] {
                new int[] {0},
                new int[] {1},
                new int[] {1},
                new int[] {1},
                new int[] {0},
            };

            var d = new LineDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Lines.Length, 1);
            Assert.AreEqual(d.Lines[0].Start.x, 1);
            Assert.AreEqual(d.Lines[0].Start.y, 0);
            Assert.AreEqual(d.Lines[0].End.x, 3);
            Assert.AreEqual(d.Lines[0].End.y, 0);
            Assert.AreEqual(d.Lines[0].IsOrphan, true);
        }

        [Test]
        public void ReadsHorizontalLinesCorrectly() {
            var map = new int[][] {
                new int[] {0, 1, 1, 1, 0}
            };

            var d = new LineDetection(new Point(0, 1), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Lines.Length, 1);
            Assert.AreEqual(d.Lines[0].Start.x, 0);
            Assert.AreEqual(d.Lines[0].Start.y, 1);
            Assert.AreEqual(d.Lines[0].End.x, 0);
            Assert.AreEqual(d.Lines[0].End.y, 3);
            Assert.AreEqual(d.Lines[0].IsOrphan, true);
        }

        [Test]
        public void ReadsLeftAngleLinesCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0},
                new int[] {1, 0, 0},
                new int[] {1, 0, 0},
                new int[] {1, 1, 1},
                new int[] {0, 0, 0}
            };

            var d = new LineDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Lines.Length, 2);

            Assert.AreEqual(d.Lines[0].Start.x, 1);
            Assert.AreEqual(d.Lines[0].Start.y, 0);
            Assert.AreEqual(d.Lines[0].End.x, 3);
            Assert.AreEqual(d.Lines[0].End.y, 0);
            Assert.AreEqual(d.Lines[0].IsOrphan, true);

            Assert.AreEqual(d.Lines[1].Start.x, 3);
            Assert.AreEqual(d.Lines[1].Start.y, 0);
            Assert.AreEqual(d.Lines[1].End.x, 3);
            Assert.AreEqual(d.Lines[1].End.y, 2);
            Assert.AreEqual(d.Lines[1].IsOrphan, true);
        }

        public void ReadsULinesCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0},
                new int[] {1, 0, 1},
                new int[] {1, 0, 1},
                new int[] {1, 1, 1},
                new int[] {0, 0, 0}
            };

            var d = new LineDetection(new Point(1, 0), (p) => {
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Lines.Length, 3);

            Assert.AreEqual(d.Lines[0].Start.x, 1);
            Assert.AreEqual(d.Lines[0].Start.y, 0);
            Assert.AreEqual(d.Lines[0].End.x, 3);
            Assert.AreEqual(d.Lines[0].End.y, 0);
            Assert.AreEqual(d.Lines[0].IsOrphan, true);

            Assert.AreEqual(d.Lines[1].Start.x, 3);
            Assert.AreEqual(d.Lines[1].Start.y, 0);
            Assert.AreEqual(d.Lines[1].End.x, 3);
            Assert.AreEqual(d.Lines[1].End.y, 2);
            Assert.AreEqual(d.Lines[1].IsOrphan, false);

            Assert.AreEqual(d.Lines[2].Start.x, 3);
            Assert.AreEqual(d.Lines[2].Start.y, 2);
            Assert.AreEqual(d.Lines[2].End.x, 1);
            Assert.AreEqual(d.Lines[2].End.y, 2);
            Assert.AreEqual(d.Lines[2].IsOrphan, true);
        }

        [Test]
        public void ReadsCubeLinesCorrectly() {
            var map = new int[][] {
                new int[] {0, 0, 0},
                new int[] {1, 1, 1},
                new int[] {1, 0, 1},
                new int[] {1, 1, 1},
                new int[] {0, 0, 0}
            };

            var d = new LineDetection(new Point(1, 0), (p) => {
                // Ensure coordinate is valid in our map
                if (p.x >= 0 && p.x < map.Length && p.y >= 0 && p.y < map[p.x].Length) {
                    return map[p.x][p.y] == 1;
                }

                return false;
            });

            Assert.AreEqual(d.Lines.Length, 4);

            Assert.AreEqual(d.Lines[0].Start.x, 1);
            Assert.AreEqual(d.Lines[0].Start.y, 0);
            Assert.AreEqual(d.Lines[0].End.x, 3);
            Assert.AreEqual(d.Lines[0].End.y, 0);
            Assert.AreEqual(d.Lines[0].IsOrphan, false);

            Assert.AreEqual(d.Lines[1].Start.x, 3);
            Assert.AreEqual(d.Lines[1].Start.y, 0);
            Assert.AreEqual(d.Lines[1].End.x, 3);
            Assert.AreEqual(d.Lines[1].End.y, 2);
            Assert.AreEqual(d.Lines[1].IsOrphan, false);

            Assert.AreEqual(d.Lines[1].Start.x, 3);
            Assert.AreEqual(d.Lines[2].Start.y, 2);
            Assert.AreEqual(d.Lines[2].End.x, 1);
            Assert.AreEqual(d.Lines[2].End.y, 2);
            Assert.AreEqual(d.Lines[2].IsOrphan, false);

            Assert.AreEqual(d.Lines[3].Start.x, 1);
            Assert.AreEqual(d.Lines[3].Start.y, 2);
            Assert.AreEqual(d.Lines[3].End.x, 1);
            Assert.AreEqual(d.Lines[3].End.y, 0);
            Assert.AreEqual(d.Lines[3].IsOrphan, false);
        }
    }
}
