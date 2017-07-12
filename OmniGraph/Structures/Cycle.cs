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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OmniGraph.Structures {
    // Represents a complete "cycle" or "shape" of 2d points.
    public sealed class Cycle : IEquatable<Cycle> {
        // A readonly collection of points contained in this cycle.
        public readonly ReadOnlyCollection<Point> Points;

        // Cache vertices
        List<Point> vertices = new List<Point>();

        // Get a list of all vertices (join position of two line segments)
        public List<Point> Vertices {
            get {
                if (vertices.Count == 0) {
                    for (var i = 0; i < Points.Count - 1; i++) {
                        var p = Points[i];
                        Point prev = Points[i == 0 ? Points.Count - 1 : i - 1];
                        Point next = Points[i == Points.Count - 1 ? 0 : i + 1];
     
                        var diffP = p - prev;
                        var diffN = next - p;

                        if (!diffP.Equals(diffN)) {
                            vertices.Add(p);
                        }
                    }
                }

                return vertices;
            }
        }

        // Build a triangle from three vertices
        public Triangle TriangleSample {
            get {
                var v = Vertices.Take(3).ToList();

                return new Triangle(v[0], v[1], v[2]);
            }
        }

        // Instantiate with a list of points
        public Cycle(IList<Point> points) {
            this.Points = new ReadOnlyCollection<Point>(points);
        }

        // Compare to another cycle. They're equal when
        // all points match, regardless of order
        public bool Equals(Cycle c) {
            foreach (var p in Points) {
                if (!c.Points.Contains(p)) {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode() {
            int hash = 17;
            foreach (var p in Points) {
                hash += p.GetHashCode();
            }

            return hash;
        }

        public override string ToString() {
            var strs = new List<string>();

            foreach (var p in Points) {
                strs.Add(p.ToString());
            }

            return string.Join(" -> ", strs.ToArray());
        }
    }
}
