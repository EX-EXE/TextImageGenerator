using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextImageGenerator.App
{
    internal class ArgumentParser
    {
        private string[] keys = Array.Empty<string>();
        private Dictionary<string, string> keyValues = new Dictionary<string, string>();

        public void Init(string cmd, params string[] keys)
        {
            this.keys = keys;
            keyValues.Clear();

            var keyIndexes = new List<(string key, int index)>();
            foreach (var key in keys)
            {
                var keyIndex = cmd.IndexOf(key, StringComparison.OrdinalIgnoreCase);
                if (0 <= keyIndex)
                {
                    keyIndexes.Add((key, keyIndex));
                }
            }
            var sortKeyIndexes = keyIndexes.OrderBy(x => x.index).ToArray();
            var result = new Dictionary<string, string>();
            foreach (var (index, keyIndex) in sortKeyIndexes.Select((x, i) => (i, x)))
            {
                var startIndex = keyIndex.index + keyIndex.key.Length;
                var lastIndex = (index != sortKeyIndexes.Length - 1) ? sortKeyIndexes[index + 1].index : cmd.Length;
                keyValues[keyIndex.key] = cmd.Substring(startIndex, lastIndex - startIndex).Trim();
            }
        }

        public void OutputParameter()
        {
            Console.WriteLine("----- Parameter -----");
            foreach(var key in keyValues.Keys.OrderBy(x => x))
            {
                Console.WriteLine($"{key} {keyValues[key].Trim()}");
            }
        }

        public bool ContainsKey(ReadOnlySpan<char> key)
        {
            return keyValues.ContainsKey(key.ToString());
        }

        public bool TryGetParam(ReadOnlySpan<char> key, out int param)
        {
            if (keyValues.ContainsKey(key.ToString()))
            {
                return int.TryParse(key, out param);
            }
            param = default;
            return false;
        }

        public bool TryGetString(ReadOnlySpan<char> key, out string result)
        {
            if (keyValues.TryGetValue(key.ToString(), out var value))
            {
                result = value.Trim();
                return true;
            }
            result = string.Empty;
            return false;
        }
        public bool TryGetStrings(ReadOnlySpan<char> key, out string[] result, char separator = ',')
        {
            if (keyValues.TryGetValue(key.ToString(), out var value))
            {
                result = value.Split(separator).Select(x => x.Trim()).ToArray();
                return true;
            }
            result = Array.Empty<string>();
            return false;
        }

        public bool TryGetBool(ReadOnlySpan<char> key, out bool param)
        {
            if (TryGetString(key, out var value))
            {
                param = value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("1", StringComparison.OrdinalIgnoreCase);
                return true;
            }
            param = false;
            return false;
        }
        public bool TryGetBools(ReadOnlySpan<char> key, out bool[] param, char separator = ',')
        {
            param = Array.Empty<bool>();
            if (TryGetStrings(key, out var values, separator))
            {
                var result = new List<bool>(values.Length);
                foreach (var value in values)
                {
                    result.Add(value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("1", StringComparison.OrdinalIgnoreCase));
                }
                param = result.ToArray();
                return true;
            }
            return false;
        }

        public bool TryGetNumber<T>(ReadOnlySpan<char> key, out T param) where T : INumber<T>
        {
            if (TryGetString(key, out var value))
            {
                if (T.TryParse(value, null, out var parse))
                {
                    param = parse;
                    return true;
                }
            }
            param = T.Zero;
            return false;
        }
        public bool TryGetNumbers<T>(ReadOnlySpan<char> key, out T[] param, char separator = ',') where T : INumber<T>
        {
            param = Array.Empty<T>();
            if (TryGetStrings(key, out var values, separator))
            {
                var result = new List<T>(values.Length);
                foreach (var value in values)
                {
                    if (T.TryParse(value, null, out var parseValue))
                    {
                        result.Add(parseValue);
                    }
                    else
                    {
                        return false;
                    }
                }
                param = result.ToArray();
                return true;
            }
            return false;
        }

        public bool TryGetInstance<T>(ReadOnlySpan<char> key, out T? instance, Func<string, T> ParseFunc)
        {
            var value = string.Empty;
            if (TryGetString(key, out value))
            {
                try
                {
                    instance = ParseFunc(value);
                    return true;
                }
                catch (Exception)
                {
                    // Through
                }
            }
            instance = default;
            return false;
        }
        public bool TryGetInstances<T>(ReadOnlySpan<char> key, out T[] instances, Func<string, T> ParseFunc, char separator = ',')
        {
            instances = Array.Empty<T>();
            if (TryGetStrings(key, out var values, separator))
            {
                var result = new List<T>(values.Length);
                foreach (var value in values)
                {
                    try
                    {
                        result.Add(ParseFunc(value));
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                instances = result.ToArray();
                return true;
            }
            return false;
        }
    }
}
