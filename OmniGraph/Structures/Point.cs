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
using System.Text;

namespace OmniGraph.Structures {
    // Represents a 2D grid coordinate.
    public class Point : IEquatable<Point> {
        public readonly int x;
        public readonly int y;

        public static Point operator +(Point a, Point b) {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b) {
            return new Point(a.x - b.x, a.y - b.y);
        }

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public double Distance(Point end) {
            return Math.Pow(x - end.x, 2) + Math.Pow(y - end.y, 2);
        }

        public bool Equals(Point p) {
            return p.x == x && p.y == y;
        }

        public Point Inverse() {
            return new Point(x * -1, y * -1);
        }

        public override int GetHashCode() {
            int hash = 13;
            hash = (hash * 7) + x;
            hash = (hash * 7) + y;

            return hash;
        }

        public override string ToString() {
            var result = new StringBuilder();

            result.Append("{");
            result.Append(x);
            result.Append(",");
            result.Append(y);
            result.Append("}");

            return result.ToString();
        }
    }
}
