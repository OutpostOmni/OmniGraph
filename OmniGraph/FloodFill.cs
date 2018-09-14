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
using OmniGraph.Structures;
using System.Collections.Generic;
using System;

namespace OmniGraph {
    // A flood fill implementation for 2d grids
    public class FloodFill {
        // Queue points to process
        Queue<Point> points = new Queue<Point>();

        HashSet<Point> visited = new HashSet<Point>();

        static Point North = new Point(0, 1);
        static Point South = new Point(0, -1);
        static Point East = new Point(1, 0);
        static Point West = new Point(-1, 0);

        // Cache the validator
        Func<Point, bool> validator;

        public FloodFill(Func<Point, bool> validator) {
            this.validator = validator;
        }

        // Begin flood fill using the given starting point
        public int Fill(Point start) {
            var totalFilled = 0;

            // If the point is valid, queue for processing
            Check(start);

            while (points.Count > 0) {
                var point = points.Dequeue();

                totalFilled++;

                Check(point + North);
                Check(point + South);
                Check(point + East);
                Check(point + West);
            }

            return totalFilled;
        }

        // Checks a given point and queues for further processing
        void Check(Point p) {
            // If we have not visited this point and it validates, queue it
            if (!visited.Contains(p) && validator(p)) {
                points.Enqueue(p);
            }

            visited.Add(p);
        }
    }
}
