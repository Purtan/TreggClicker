using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Serialization;
using UnityEngine;
using Action = System.Action;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Assets.Scripts.Util {

    public class Util {
        private static Util instance;
        private Random random;
        private static readonly int[] digits = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private static Util Instance => instance ?? (instance = new Util());

        public Util() {
            random = new Random();
        }

        public static IEnumerator Delay(float seconds, Action callback) {
            yield return new WaitForSeconds(seconds);
            callback();
        }

        public static int Lerp(int min, int max, float amount) {
            return (int)Math.Ceiling((max - min) * amount) + min;
        }

        public static string FormatNumber(BigInteger n) {
            if(n < 1000) {
                return n.ToString();
            }
            if(n < 10000) {
                return String.Format("{0:#,.##}K", n - 5);
            }
            if(n < 100000) {
                return String.Format("{0:#,.#}K", n - 50);
            }
            if(n < 1000000) {
                return String.Format("{0:#,.}K", n - 500);
            }
            if(n < 10000000) {
                return String.Format("{0:#,,.##}M", n - 5000);
            }
            if(n < 100000000) {
                return String.Format("{0:#,,.#}M", n - 50000);
            }
            if(n < 1000000000) {
                return String.Format("{0:#,,.}M", n - 500000);
            }
            return String.Format("{0:#,,,.##}B", n - 5000000);
        }

        public static float Lerp(float min, float max, float amount) {
            return (max - min) * amount + min;
        }

        public static float Random() {
            return (float)Instance.random.NextDouble();
        }

        public static byte RandomByte() {
            return Convert.ToByte(Instance.random.Next() % 255);
        }

        public static int RandomRange(int min, int max) {
            return Instance.random.Next(min, max + 1);
        }

        // CREDIT: http://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way
        public static long RandomRange(long min, long max) {
            var buf = new byte[8];
            Instance.random.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        public static float RandomRange(float min, float max) {
            return (max - min) * (float)Instance.random.NextDouble() + min;
        }

        public static T RandomElement<T>(T[] array) {
            if(array == null)
                return default(T);

            if(!array.Any())
                return default(T);

            return array[Instance.random.Next(0, array.Length)];
        }

        public static T RandomElement<T>(IEnumerable<T> list) {
            return RandomElement(list.ToArray());
        }

        public static T RandomElement<T>(Array array) {
            if(array == null)
                return default(T);

            if(array.Length == 0)
                return default(T);

            return (T)array.GetValue(Instance.random.Next(0, array.Length));
        }

        // Credit: https://stackoverflow.com/questions/3132126/how-do-i-select-a-random-value-from-an-enumeration
        public static T RandomEnum<T>() {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(Instance.random.Next(values.Length));
        }

        public static bool FlipCoin() {
            return Instance.random.Next(0, 2) == 1;
        }

        public static bool Chance(float chance) {
            return Random() <= chance;
        }

        public static T Choice<T>(params T[] list) {
            return RandomElement(list);
        }

        public static void SetSeed(int seed) {
            Instance.random = new Random(seed);
        }

        public static void SetRandomSeed() {
            Instance.random = new Random();
        }

        public static string GetTimeDeltaDescription(double delta) {
            return String.Format("{0} day{1} ago", delta, delta > 1 ? "s" : "");
        }

        //		public static void PopulateFromJSONObject(object obj, string path)
        //		{
        //			try
        //			{
        //				JsonSerializer.CreateDefault(new JsonSerializerSettings())
        //					.Populate(JObject.Parse(File.ReadAllText(path)).CreateReader(), obj);
        //			}
        //			catch (JsonReaderException e)
        //			{
        //				throw new Exception("Error while parsing JSON file " + path + ": " + e.Message);
        //			}
        //			catch (JsonException e)
        //			{
        //				throw new Exception(e.Message);
        //			}
        //		}
        //
        //		public static void PopulateArrayFromJSON(object obj, string json)
        //		{
        //			try
        //			{
        //				JsonSerializer.CreateDefault(new JsonSerializerSettings()).Populate(JArray.Parse(json).CreateReader(), obj);
        //			}
        //			catch (JsonReaderException e)
        //			{
        //				throw new Exception("Error while parsing JSON " + json + ": " + e.Message);
        //			}
        //		}

        //		public static void PopulateArrayFromJSONPath(object obj, string path)
        //		{
        //			PopulateArrayFromJSON(obj, File.ReadAllText(path));
        //		}

        public static string Capitalize(string str) {
            return Char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string CapitalizeWords(string sentance) {
            var spaceWords = sentance.Split(' ');
            var result = String.Join(" ", spaceWords.Select(w => (w != "of" && w != "and") ? Capitalize(w) : w).ToArray());
            var dashWords = result.Split('-');
            result = String.Join("-", dashWords.Select(w => (w != "of" && w != "and") ? Capitalize(w) : w).ToArray());

            return result;
        }

        // Credit: https://stackoverflow.com/questions/3824059/linq-list-to-sentence-format-insert-commas-and
        public static string ToCommaList<T>(List<T> source, System.Func<T, string> stringSelector) {
            var count = source.Count;

            System.Func<int, string> prefixSelector = x =>
                x == 0 ? "" :
                x == count - 1 ? " and " :
                ", ";

            var sb = new StringBuilder();

            for(var i = 0; i < count; i++) {
                sb.Append(prefixSelector(i));
                sb.Append(stringSelector(source[i]));
            }

            var result = sb.ToString();
            return result;
        }

        public static int RandomDigit() {
            return RandomElement(digits);
        }

        // Credit: http://stackoverflow.com/questions/10390356/c-sharp-serializing-deserializing-with-memory-stream

        public static MemoryStream SerializeToStream(object o) {
            var stream = new MemoryStream();
            var dcs = new DataContractSerializer(o.GetType());
            //			var xdw = XmlDictionaryWriter.CreateTextWriter(stream);
            dcs.WriteObject(stream, o);
            //			xdw.Flush();

            return stream;
        }

        //		public static object DeserializeFromStream(MemoryStream stream)

        //		{

        //			IFormatter formatter = new BinaryFormatter();

        //			stream.Seek(0, SeekOrigin.Begin);

        //			object o = formatter.Deserialize(stream);

        //			return o;

        //		}

        public static string FormatLargeNumber(long number) {
            string result = "";
            if(number < 1000000)
                result = String.Format("{0:N}", number);
            else if(number < 1000000000)
                result = String.Format("{0:N}M", number / 1000000f);
            else if(number < 1000000000000)
                result = String.Format("{0:N}B", number / 1000000000f);
            else if(number < 1000000000000000)
                result = String.Format("{0:N}T", number / 1000000000000f);

            result = result.Replace(".00", "");

            return result;
        }

        //		public static string GetSecureHash(string str)

        //		{

        //			// Generate the hash, with an automatic 32 byte salt

        //			var rfc2898DeriveBytes = new Rfc2898DeriveBytes(str, 32) {IterationCount = 10000};

        //			var hash = rfc2898DeriveBytes.GetBytes(20);

        //			var salt = rfc2898DeriveBytes.Salt;

        //			

        //			// Return the salt and the hash

        //			return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);

        //		}


        // Credit: https://cmatskas.com/-net-password-hashing-using-pbkdf2/

        public class PasswordHash {
            public const int SaltByteSize = 32;
            public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
            public const int Pbkdf2Iterations = 1000;
            public const int IterationIndex = 0;
            public const int SaltIndex = 1;
            public const int Pbkdf2Index = 2;

            public static string GenerateHash(string password) {
                var cryptoProvider = new RNGCryptoServiceProvider();
                byte[] salt = new byte[SaltByteSize];
                cryptoProvider.GetBytes(salt);

                var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
                return Pbkdf2Iterations + ":" +
                       Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }

            private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes) {
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        public static string RandomizeCapitalization(string message) {
            // ll oo pp qq
            // ^
            var originalMessage = message.ToCharArray();
            var result = message.ToCharArray();
            var lastChar = ' ';
            for(var i = 0; i < result.Length; ++i) {
                if(char.ToLower(originalMessage[i]) == char.ToLower(lastChar)) {
                    result[i] = char.IsUpper(lastChar) ? char.ToLower(lastChar) : char.ToUpper(lastChar);
                } else {
                    result[i] = FlipCoin() ? char.ToUpper(result[i]) : char.ToLower(result[i]);
                }

                lastChar = result[i];
            }

            return new string(result);
        }

        public static string PluralizeIfNeeded(long number, string str) {
            return number == 1 ? str : $"{str}s";
        }
    }

    // Credit: http://stackoverflow.com/questions/2683442/where-can-i-find-the-clamp-function-in-net
    public static class MathExtensionMethods {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T> {
            if(val.CompareTo(min) < 0) return min;
            return val.CompareTo(max) > 0 ? max : val;
        }
    }

    public static class StringExtensionMethods {
        // Credit: http://stackoverflow.com/questions/8809354/replace-first-occurrence-of-pattern-in-a-string
        public static string ReplaceFirst(this string text, string search, string replace) {
            var pos = text.IndexOf(search);
            if(pos < 0) {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static BigInteger ToBigInteger(this string str) {
            return BigInteger.Parse(str);
        }

        public static string ToString(this BigInteger number) {
            return number.ToString();
        }

        // Credit: http://stackoverflow.com/questions/9367119/replacing-a-char-at-a-given-index-in-string
        public static string ReplaceAt(this string input, int index, char newChar) {
            if(input == null) {
                throw new ArgumentNullException("input");
            }
            var chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }

    //	class WritablePropertiesOnlyResolver : DefaultContractResolver
    //	{
    //		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    //		{
    //			var props = base.CreateProperties(type, memberSerialization);
    //			return props.Where(p => p.Writable).ToList();
    //		}
    //	}

    public static class EnumerableExtension {
        public static T PickRandom<T>(this IEnumerable<T> source) {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count) {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }

    public static class TransformExtension {
        public static void ClearChildren(this Transform transform) {
            foreach(Transform child in transform) {
                Object.Destroy(child.gameObject);
            }
        }

        public static void SetChildrenActive(this Transform transform, bool active) {
            foreach(Transform child in transform) {
                child.gameObject.SetActive(active);
            }
        }
    }
}
