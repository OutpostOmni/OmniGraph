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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OmniGraph {
    // Detects cycles/shapes in a 2D grid.
    public class CycleDetection {
        // Cache found cycles
        List<Cycle> cycles = new List<Cycle>();

        // Provide public readonly access to our cycle list
        public ReadOnlyCollection<Cycle> Cycles {
            get { return new ReadOnlyCollection<Cycle>(cycles); }
        }

        // Steps/slopes that determine how we iterate grid points
        public Point[] Steps = new Point[] {
            new Point(1, 0),
            new Point(0, 1),
            new Point(-1, 0),
            new Point(0, -1)
        };

        // Cache our starting position
        Point origin;

        // Cache the validation function
        Func<Point, bool> validator;

        // Initializes a new instance of the <see cref="T:OmniGraph.CycleDetection"/> class.
        public CycleDetection(Point origin, Func<Point, bool> validator) {
            this.origin = origin;
            this.validator = validator;

            this.Scan();
        }

        // Activate a new scan.
        public void Scan() {
            cycles.Clear();

            if (validator(origin)) {
                Scan(new List<Point>(), origin);
            }
        }

        // Add a cycle to our final list.
        // This ensures the cycle doesn't already exist (compares points, ignoring order).
        void AddCycle(Cycle cycle) {
            // Cycles have reached some existing point in the trail, but not necessarily
            // the exact starting point. To filter out "strands" we find the index of
            // the actual starting point and skip points that came before it
            var index = cycle.Points.IndexOf(cycle.Points[cycle.Points.Count - 1]);

            // Make a new object with only the points forming the exact cycle
            // If the end point is the actual starting point, this has no effect.
            cycle = new Cycle(cycle.Points.Skip(index).ToList());

            // Add unless duplicate
            if (!cycles.Contains(cycle)) {
                cycles.Add(cycle);
            }
        }

        // Scan a new point and follow any valid new trails.
        void Scan(List<Point> trail, Point start) {
            // Cycle completed?
            if (trail.Contains(start)) {
                // Add this position as the end point
                trail.Add(start);

                // Add the finished cycle
                AddCycle(new Cycle(trail));

                return;
            }

            trail.Add(start);

            // Look for neighbors
            foreach (var step in Steps) {
                var neighbor = start + step;

                // Make sure the neighbor isn't the last point we were on... that'd be an infinite loop
                if (trail.Count >= 2 && neighbor.Equals(trail[trail.Count - 2])) {
                    continue;
                }
   
                // If neighbor is new and matches
                if (validator(neighbor)) {
                    // Continue the trail with the neighbor
                    Scan(new List<Point>(trail), neighbor);
                }
            }
        }
    }
}
