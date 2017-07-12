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
using System.Collections.Generic;
using System.Text;

namespace OmniGraph.Structures {
    // Represents a stright line in a 2D grid.
    public class Line {
        // The starting point of this line
        public readonly Point Start;

        // The ending point
        public readonly Point End;

        // The step/slope used to obtain points for the line
        public readonly Point Step;

        // Orphaned lines have one point that does not belong to any other line
        public bool IsOrphan = false;

        // The distance between the start and end points
        public double Distance {
            get {
                return Start.Distance(End);
            }
        }

        // List all points on this line, including start/end
        public IEnumerable<Point> Points {
            get {
                var pointer = Start;

                while (!pointer.Equals(End + Step)) {
                    yield return pointer;

                    pointer += Step;
                }
            }
        }

        public Line(Point start, Point end, Point step) {
            this.Start = start;
            this.End = end;
            this.Step = step;
        }

        public bool ContainsPoint(Point point) {
            return Start.x * point.y + point.x * End.y + End.x * Start.y - Start.x * End.y - point.x * Start.y - End.x * point.y == 0;
        }

        public override string ToString() {
            var result = new StringBuilder();

            result.Append("{");
            result.Append(Start.x);
            result.Append(",");
            result.Append(Start.y);
            result.Append("} -> {");
            result.Append(End.x);
            result.Append(",");
            result.Append(End.y);
            result.Append("} step: {");
            result.Append(Step.x);
            result.Append(",");
            result.Append(Step.y);
            result.Append("}");

            return result.ToString();
        }
    }
}
