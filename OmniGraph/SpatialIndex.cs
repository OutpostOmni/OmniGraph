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
using System.Linq;

namespace OmniGraph {
    // A super-simple 2d spatial index.
    public sealed class SpatialIndex<T> {
        Dictionary<int, List<T>> index = new Dictionary<int, List<T>>();

        /// <summary>
        /// Get a list of all current keys.
        /// </summary>
        /// <value>The keys.</value>
        public List<int> Keys {
            get {
                return index.Keys.ToList();
            }
        }

        /// <summary>
        /// Cell size.
        /// </summary>
        public readonly int CellSize = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OmniGraph.SpatialIndex"/> class.
        /// </summary>
        public SpatialIndex() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OmniGraph.SpatialIndex"/> class.
        /// </summary>
        /// <param name="CellSize">Cell size.</param>
        public SpatialIndex(int CellSize) {
            this.CellSize = CellSize;
        }

        /// <summary>
        /// Get the bucket associated with a given vec. Null if invalid.
        /// </summary>
        /// <returns>The object list.</returns>
        /// <param name="point">Point.</param>
        public List<T> Get(Point point) {
            var hash = Hash(point);

            // Attempt to get an existing list
            List<T> list;
            index.TryGetValue(hash, out list);

            return list;
        }

        /// <summary>
        /// Insert an object with the specific vec.
        /// </summary>
        /// <param name="point">Point.</param>
        /// <param name="obj">Object.</param>
        public void Insert(Point point, T obj) {
            var hash = Hash(point);

            // Cache our list
            List<T> list;

            // Attempt to get an existing list
            if (!index.TryGetValue(hash, out list)) {
                // If we fail, create a new list and store it
                list = new List<T>();
                index.Add(hash, list);
            }

            list.Add(obj);
        }

        /// <summary>
        /// Move the specified oldVec, newVec and obj.
        /// </summary>
        /// <returns>Whether the old object was successfully removed.</returns>
        /// <param name="oldP">Old point.</param>
        /// <param name="newP">New point.</param>
        /// <param name="obj">Object.</param>
        public bool Move(Point oldP, Point newP, T obj) {
            // Insert in the correct bucket
            Insert(newP, obj);

            // Remove from the old
            return Remove(oldP, obj);
        }

        /// <summary>
        /// Removes an object from the index.
        /// </summary>
        /// <returns>The remove.</returns>
        /// <param name="point">Point.</param>
        /// <param name="obj">Object.</param>
        public bool Remove(Point point, T obj) {
            var removed = false;
            var hash = Hash(point);

            // Attempt to get an existing list
            List<T> list;
            if (index.TryGetValue(hash, out list)) {
                // Remove the object
                removed = list.Remove(obj);

                // Clean up list
                if (list.Count == 0) {
                    index.Remove(hash);
                }
            }

            return removed;
        }

        int FastFloor(float val) {
            return (int) val;
        }
 
        int Hash(Point point) {
            // losely based on http://www.beosil.com/download/CollisionDetectionHashing_VMV03.pdf
            var x = FastFloor(point.x / CellSize) * 73856093;
            var y = FastFloor(point.y / CellSize) * 19349663;
            return x ^ y;
        }
    }
}
