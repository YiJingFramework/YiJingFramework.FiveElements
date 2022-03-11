using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace YiJingFramework.FiveElements
{
    /// <summary>
    /// 五行。
    /// An element of the five elements.
    /// </summary>
    public struct FiveElement : IComparable<FiveElement>, IEquatable<FiveElement>, IFormattable
    {
        private readonly int int32Value;
        private FiveElement(int int32ValueNotSmallerThanZero)
        {
            Debug.Assert(int32ValueNotSmallerThanZero >= 0);
            this.int32Value = int32ValueNotSmallerThanZero % 5;
        }

        #region creating
        /// <summary>
        /// 木。
        /// Wood.
        /// </summary>
        public static FiveElement Wood => default; // => new FiveElement(0);
        /// <summary>
        /// 火。
        /// Fire.
        /// </summary>
        public static FiveElement Fire => new FiveElement(1);
        /// <summary>
        /// 土。
        /// Earth.
        /// </summary>
        public static FiveElement Earth => new FiveElement(2);
        /// <summary>
        /// 金。
        /// Metal.
        /// </summary>
        public static FiveElement Metal => new FiveElement(3);
        /// <summary>
        /// 水。
        /// Water.
        /// </summary>
        public static FiveElement Water => new FiveElement(4);
        #endregion

        #region converting
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.int32Value switch {
                0 => "Wood",
                1 => "Fire",
                2 => "Earth",
                3 => "Metal",
                _ => "Water" // 4 => "Water"
            };
        }

        /// <summary>
        /// 按照指定格式转换为字符串。
        /// Convert to a string with the given format.
        /// </summary>
        /// <param name="format">
        /// 要使用的格式。
        /// <c>"G"</c> 表示英文； <c>"C"</c> 表示中文。
        /// <c>"G"</c> represents English; and <c>"C"</c> represents Chinese.
        /// </param>
        /// <param name="formatProvider">
        /// 不会使用此参数。
        /// This parameter will won't be used.
        /// </param>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        /// <exception cref="FormatException">
        /// 给出的格式化字符串不受支持。
        /// The given format is not supported.
        /// </exception>
        public string ToString(string? format, IFormatProvider? formatProvider = null)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";

            return format.ToUpperInvariant() switch {
                "G" => this.ToString(),
                "C" => this.int32Value switch {
                    0 => "木",
                    1 => "火",
                    2 => "土",
                    3 => "金",
                    _ => "水" // 4 => "水"
                },
                _ => throw new FormatException($"The format string \"{format}\" is not supported.")
            };
        }

        /// <summary>
        /// 从字符串转换。
        /// Convert from a string.
        /// </summary>
        /// <param name="s">
        /// 字符串。
        /// The string.
        /// </param>
        /// <param name="result">
        /// 结果。
        /// The result.
        /// </param>
        /// <returns>
        /// 一个指示转换成功与否的值。
        /// A value indicates whether it has been successfully converted or not.
        /// </returns>
        public static bool TryParse(
            [NotNullWhen(true)] string? s,
            [MaybeNullWhen(false)] out FiveElement result)
        {
            switch (s?.Trim()?.ToLower())
            {
                case "wood":
                case "木":
                    result = Wood;
                    return true;
                case "fire":
                case "火":
                    result = Fire;
                    return true;
                case "earth":
                case "土":
                    result = Earth;
                    return true;
                case "metal":
                case "金":
                    result = Metal;
                    return true;
                case "water":
                case "水":
                    result = Water;
                    return true;
                default:
                    result = default;
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fiveElement"></param>
        public static explicit operator int(FiveElement fiveElement)
            => fiveElement.int32Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator FiveElement(int value)
            => new FiveElement(value % 5 + 5);
        #endregion

        #region comparing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(FiveElement other)
        {
            return this.int32Value.CompareTo(other.int32Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FiveElement other)
        {
            return this.int32Value.Equals(other.int32Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is not FiveElement other)
                return false;
            return this.int32Value.Equals(other.int32Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.int32Value.GetHashCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(FiveElement left, FiveElement right)
            => left.int32Value == right.int32Value;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(FiveElement left, FiveElement right)
            => left.int32Value != right.int32Value;
        #endregion

        #region generating and overcoming
        /// <summary>
        /// 获取与另一五行之间的关系。
        /// Get the relationship with another element.
        /// </summary>
        /// <param name="another">
        /// 另一五行。
        /// The another element.
        /// </param>
        /// <returns>
        /// 关系。
        /// The relationship.
        /// </returns>
        public FiveElementsRelationship GetRelationship(FiveElement another)
        {
            return (FiveElementsRelationship)((another.int32Value - this.int32Value + 5) % 5);
        }
        /// <summary>
        /// 通过五行关系获取另一五行。
        /// Get another element with the relationship.
        /// </summary>
        /// <param name="relation">
        /// 关系。
        /// The relationship.
        /// </param>
        /// <returns>
        /// 另一五行。
        /// The another element.
        /// </returns>
        public FiveElement GetElement(FiveElementsRelationship relation)
        {
            var relationInt = ((int)relation % 5 + 5) % 5;
            return new FiveElement(this.int32Value + relationInt);
        }
        #endregion
    }
}
