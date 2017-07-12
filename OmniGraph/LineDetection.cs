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

namespace OmniGraph {
    // Detects individual straight "lines" in a 2D grid.
    public class LineDetection {
        List<Line> lines = new List<Line>();
        public Line[] Lines {
            get { return lines.ToArray(); }
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

        // Initializes a new instance
        public LineDetection(Point origin, Func<Point, bool> validator) {
            this.origin = origin;
            this.validator = validator;

            FindLines(origin);

            for (var i = 0; i < lines.Count; i++) {
                var line = lines[i];

                // Mark all orphaned lines so we can ignore them
                if (IsPointOrphaned(line.Start) || IsPointOrphaned(line.End)) {
                    line.IsOrphan = true;
                }
            }
        }

        // Find all lines originating at start
        void FindLines(Point start) {
            foreach (var step in Steps) {
                FindLine(start, step);
            }
        }

        // Find any line belonging to start, and start new lines for valid neighbors
        void FindLine(Point start, Point step) {
            var end = start;
            var point = start;

            if (LineMatchedByStep(start, step)) {
                return;
            }

            HashSet<LineCandidate> lineCandidates = new HashSet<LineCandidate>();

            while (validator(point)) {
                var line = new Line(start, point, step);

                // Look for valid neighbors *not* on this line
                // and start a new line
                foreach (var neighborStep in Steps) {
                    var neighbor = new Point(point.x + neighborStep.x, point.y + neighborStep.y);

                    // If point is on any existing line, it can't be a candidate
                    if (IsPointOnMatchedLine(neighbor)) {
                        continue;
                    }
                    // If neighbor is on our current line
                    else if (line.ContainsPoint(neighbor)) {
                        continue;
                    }
                    // If neighbor matches...
                    else if (validator(neighbor)) {
                        lineCandidates.Add(new LineCandidate(point, neighborStep, line));
                    }
                }

                // Cache this point in case we can't go further
                end = point;

                // Increment the step to the next point
                point = new Point(point.x + step.x, point.y + step.y);
            }

            // If we have a complete line, add to our list
            if (end != start) {
                var line = new Line(start, end, step);
                lines.Add(line);
            }

            // Attempt to detect additional lines if we found any candidates
            foreach (var lineCandidate in lineCandidates) {
                FindLine(lineCandidate.Start, lineCandidate.Step);
            }
        }

        // Check if a point is on any existing line
        bool IsPointOnMatchedLine(Point point) {
            foreach (var line in lines) {
                if (line.ContainsPoint(point)) {
                    return true;
                }
            }

            return false;
        }

        // @todo we may need to just generate the full line and throw it away if the whole line matches
        // because this may prove faulty
        bool LineMatchedByStep(Point point, Point step) {
            foreach (var line in lines) {
                var inverseStep = new Point(line.Step.x * -1, line.Step.y * -1);

                if (line.ContainsPoint(point) && (line.Step.Equals(step) || inverseStep.Equals(step))) {
                    return true;
                }
            }

            return false;
        }

        bool IsPointOrphaned(Point point) {
            var count = 0;

            foreach (var line in lines) {
                if (line.ContainsPoint(point)) {
                    count++;
                }

                // More than one is satisfactory
                if (count >= 2) {
                    break;
                }
            }

            return count < 2;
        }
    }
}
