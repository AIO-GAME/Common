/*
Copyright (c) 2020 Omar Duarte
Unauthorized copying of this file, via any medium is strictly prohibited.
Writen by Omar Duarte, 2020.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using UnityEngine;

namespace PluginMaster
{
    public static class RandomUtils
    {
        [Serializable]
        public class Range
        {
            [SerializeField] private float _min = -1f;
            [SerializeField] private float _max = 1f;

            public Range() { }
            public Range(Range other) => (_min, _max) = (other._min, other.max);
            public Range(float min, float max) => (_min, _max) = (min, max);

            public float min
            {
                get => _min;
                set
                {
                    if (_min == value) return;
                    _min = value;
                    if (_min > _max)
                    {
                        _max = _min;
                    }
                }
            }
            public float max
            {
                get => _max;
                set
                {
                    if (_max == value) return;
                    _max = value;
                    if (_max < _min)
                    {
                        _min = _max;
                    }
                }
            }

            public override int GetHashCode()
            {
                int hashCode = -1605643878;
                hashCode = hashCode * -1521134295 + _min.GetHashCode();
                hashCode = hashCode * -1521134295 + _max.GetHashCode();
                return hashCode;
            }
            public override bool Equals(object obj) => obj is Range range && _min == range._min;
            public static bool operator ==(Range value1, Range value2) => Equals(value1, value2);
            public static bool operator !=(Range value1, Range value2) => !Equals(value1, value2);
            
            public float randomValue => UnityEngine.Random.Range(min, max);

        }

        [Serializable]
        public class Range3
        {
            public Range x = new Range(0,0);
            public Range y = new Range(0, 0);
            public Range z = new Range(0, 0);

            public Range3(Vector3 min, Vector3 max)
            {
                x = new Range(min.x, max.x);
                y = new Range(min.y, max.y);
                z = new Range(min.z, max.z);
            }

            public Range3(Range3 other)
            {
                x = new Range(other.x);
                y = new Range(other.y);
                z = new Range(other.z);
            }
            public Vector3 min
            {
                get => new Vector3(x.min, y.min, z.min); 
                set
                {
                    x.min = value.x;
                    y.min = value.y;
                    z.min = value.z;
                }
            }

            public Vector3 max
            {
                get => new Vector3(x.max, y.max, z.max);
                set
                {
                    x.max = value.x;
                    y.max = value.y;
                    z.max = value.z;
                }
            }

            public override int GetHashCode()
            {
                int hashCode = 373119288;
                hashCode = hashCode * -1521134295 + x.GetHashCode();
                hashCode = hashCode * -1521134295 + y.GetHashCode();
                hashCode = hashCode * -1521134295 + z.GetHashCode();
                return hashCode;
            }
            public override bool Equals(object obj) => obj is Range3 range3 && x == range3.x && y == range3.y && z == range3.z;
            public static bool operator ==(Range3 value1, Range3 value2) => Equals(value1, value2);
            public static bool operator !=(Range3 value1, Range3 value2) => !Equals(value1, value2);

            public Vector3 randomVector => new Vector3(x.randomValue, y.randomValue, z.randomValue);

            
        }
    }
}
