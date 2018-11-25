using Kent.Libary.Configurations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Kent.Libary.Utilities
{
    public static class StringUtilities
    {
        #region Web

        public static string RemoveTags(this string html)
        {
            if (String.IsNullOrEmpty(html))
            {
                return String.Empty;
            }

            var result = new char[html.Length];

            var cursor = 0;
            var inside = false;
            foreach (var current in html)
            {
                switch (current)
                {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (!inside)
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        public static string StripHtml(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return string.Empty;
            return Regex.Replace(inputString, @"<(.|\n)*?>", string.Empty);
        }

        public static Dictionary<string, string> ToDictionary(this string s)
        {
            var dictionary = new Dictionary<string, string>();
            var keyValuePairs = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in keyValuePairs)
            {
                var values = pair.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length != 2)
                    continue;

                var key = values[0];
                var value = values[1];

                dictionary[key.ToLower()] = value;
            }
            return dictionary;
        }

        public static RouteValueDictionary ToRouteValueDictionary(this string s)
        {
            var dictionary = s.ToDictionary();

            var routeDictionary = new RouteValueDictionary();

            foreach (var routeValue in dictionary)
            {
                var key = routeValue.Key;
                routeDictionary.Add(key.EndsWith("-")
                                    ? key.Substring(0, key.Length - 1)
                                    : key, routeValue.Value);
            }

            return routeDictionary;
        }

        #endregion

        #region Common

        //public static string Pluralize(this string name, bool toLowercase = false)
        //{
        //    if (string.IsNullOrEmpty(name)) return string.Empty;
        //    var pluralizationService = PluralizationService.CreateService(
        //        CultureInfo.CurrentCulture);
        //    return toLowercase ? pluralizationService.Pluralize(name).ToLower() : pluralizationService.Pluralize(name);
        //}

        //public static string Singularize(this string name, bool toLowercase = false)
        //{
        //    if (string.IsNullOrEmpty(name)) return string.Empty;
        //    var pluralizationService = PluralizationService.CreateService(
        //        CultureInfo.CurrentCulture);
        //    return toLowercase ? pluralizationService.Singularize(name).ToLower() : pluralizationService.Singularize(name);
        //}

        public static string ToLowercaseNamingConvention(this string s, bool toLowercase = false)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            if (toLowercase)
            {
                return r.Replace(s, " ").ToLower();
            }
            return r.Replace(s, " ");
        }

        public static string CamelFriendly(this string camel)
        {
            if (String.IsNullOrWhiteSpace(camel))
                return "";

            var sb = new StringBuilder(camel);

            for (var i = camel.Length - 1; i > 0; i--)
            {
                var current = sb[i];
                if ('A' <= current && current <= 'Z')
                {
                    sb.Insert(i, ' ');
                }
            }

            return sb.ToString();
        }

        public static string FormatWith(this string s, params object[] parms)
        {
            return String.Format(s, parms);
        }

        public static string GetBasePathFromCurrentPath(string folderNameToGet, string currentPath, string baseFolderName)
        {
            if (String.IsNullOrEmpty(currentPath))
            {
                return null;
            }
            string basePath = null;
            var viewsPartIndex = currentPath.LastIndexOf(@"\" + baseFolderName, StringComparison.OrdinalIgnoreCase);
            if (viewsPartIndex >= 0)
            {
                basePath = currentPath.Substring(0, viewsPartIndex + 1) + folderNameToGet;
            }

            return basePath;
        }

        public static string SafeSubstring(this string input, int length)
        {
            if (length > input.Length)
            {
                return input;
            }

            var endPosition = input.IndexOf(" ", length, StringComparison.Ordinal);
            if (endPosition < 0) endPosition = input.Length;

            return length >= input.Length ? input : input.Substring(0, endPosition) + "...";
        }

        public static string RemoveDiacritics(this string name)
        {
            var stFormD = name.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var t in from t in stFormD
                              let uc = CharUnicodeInfo.GetUnicodeCategory(t)
                              where uc != UnicodeCategory.NonSpacingMark
                              select t)
            {
                sb.Append(t);
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string Slugify(this string input)
        {
            var disallowed = new Regex(@"[/:?#\[\]@!$&'()*+,.;=\s\""\<\>\\\|%]+");

            var cleanedSlug = disallowed.Replace(input, "-").Trim('-', '.');

            var slug = Regex.Replace(cleanedSlug, @"\-{2,}", "-");

            if (slug.Length > 1000)
                slug = slug.Substring(0, 1000).Trim('-', '.');

            slug = slug.RemoveDiacritics();
            return slug;
        }

        public static string RemoveDoubleSpace(this string input)
        {
            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }
            return input;
        }

        public static string RemoveString(this string input, string remove)
        {
            if (string.IsNullOrEmpty(remove)) return input;

            while (input.Contains(remove))
            {
                input = input.Replace(remove, string.Empty);
            }
            return input;
        }

        /// <summary>
        /// Convert a dictionary to aggregated string format like key=value;key2=value2;...;keyn=valuen
        /// </summary>
        /// <param name="dictionary">The input dictionary</param>
        /// <returns>Aggregated string in format key=value;key2=value2;...;keyn=valuen</returns>
        public static string ToAggregatedString(this Dictionary<string, string> dictionary)
        {
            var str = dictionary.Keys.Aggregate("", (current, dictionaryKey) => current + (dictionaryKey + "=" + dictionary[dictionaryKey] + ";"));
            return str;
        }

        public static string ReplaceOnce(this string template, string placeholder, string replacement)
        {
            var loc = template.IndexOf(placeholder, StringComparison.Ordinal);
            if (loc < 0)
            {
                return template;
            }
            return new StringBuilder(template.Substring(0, loc))
                .Append(replacement)
                .Append(template.Substring(loc + placeholder.Length))
                .ToString();
        }

        public static string LastPart(this string input, char separator)
        {
            var pos = input.LastIndexOf(separator);
            if (pos < 0)
            {
                return input;
            }
            if (pos == input.Length - 1)
            {
                return "";
            }
            return input.Substring(pos + 1);
        }


        #endregion

        #region Encode / Decode

        public static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// Decode string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Decode(this string input)
        {
            return HttpUtility.HtmlDecode(input);
        }

        #endregion

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        #region File & Folder

        /// <summary>
        /// Generate unique avatar file name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="extension"> </param>
        /// <returns></returns>
        public static string GenerateAvatarFileName(this int id, string extension)
        {
            return string.Format("{0}_{1}.{2}", id, DateTime.UtcNow.ToString("yyyyMMddhhmmss"), extension);
        }

        /// <summary>
        /// Convert path to string
        /// This must match with jquery script
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToIdString(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;

            var invalidUrlCharacter = new Regex(@"[^a-z|^_|^\d|^\u4e00-\u9fa5|^/]+",
                                                                          RegexOptions.Compiled | RegexOptions.Singleline |
                                                                          RegexOptions.IgnoreCase);

            return invalidUrlCharacter.Replace(path.Replace("/", "").Replace("_", "").Replace("\\", ""), "");
        }

        /// <summary>
        /// Convert path to string
        /// </summary>
        /// <param name="path"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ToIdStringByHash(this string path, string prefix = "")
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;

            var hashCode = path.GetHashCode() > 0 ? path.GetHashCode() : path.GetHashCode() * -1;
            if (string.IsNullOrEmpty(prefix))
            {
                return hashCode.ToString(CultureInfo.InvariantCulture);
            }
            return string.Format("{0}_{1}", prefix, hashCode);
        }

        #endregion

        #region Get Match String

        /// <summary>
        /// Get matching string from list of search items
        /// Generate the final string base on input string with higher match on top
        /// </summary>
        /// <param name="input"></param>
        /// <param name="searchItems"></param>
        /// <returns></returns>
        public static string GetMatchingContent(this string input, List<string> searchItems, bool caseSensitive)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var contents = new List<MatchingString>();
            foreach (var s in searchItems)
            {
                string item = caseSensitive ? s : s.ToLower();
                if (string.IsNullOrWhiteSpace(item)) continue;
                var position = -1;
                do
                {
                    position = input.IndexOf(item, position + 1, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
                    if (position < 0) break;
                    var startString = input.Substring(0, position);
                    var startStringPosition = startString.LastIndexOf(".", caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
                    if (startStringPosition < 0) startStringPosition = 0;
                    var endStringPosition = input.IndexOf(".", position, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
                    if (endStringPosition < 0)
                    {
                        endStringPosition = input.Length - 1;
                    }
                    var stringResult = input.Substring(startStringPosition, endStringPosition - startStringPosition).Trim();
                    var content = contents.SingleOrDefault(c => c.Content.Equals(stringResult));
                    if (content != null)
                    {
                        content.Value = content.Value + 1;
                    }
                    else
                    {
                        content = new MatchingString
                        {
                            Content = stringResult,
                            Value = 1
                        };
                        contents.Add(content);
                    }
                } while (position > 0);
            }
            contents = contents.Distinct().OrderByDescending(c => c.Value).ToList();
            if (contents.Count == 0)
            {
                return input.SafeSubstring(300).Trim();
            }
            if (contents.Count > 3)
            {
                contents = contents.Take(3).ToList();
            }
            return string.Join("...", contents.Select(c => c.Content)) + "...";
        }
        #endregion

        #region Hierarchy

        /// <summary>
        /// Get level from hierarchy
        /// </summary>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        public static int GetHierarchyLevel(this string hierarchy)
        {
            if (string.IsNullOrEmpty(hierarchy))
                return 0;
            return hierarchy.Count(h => h.Equals(KentConfiguration.IdSeparator)) - 1;
        }

        #endregion

        public static void GenerateName(this string fullName, out string firstName, out string lastName)
        {
            firstName = string.Empty;
            lastName = string.Empty;
            if (string.IsNullOrEmpty(fullName))
                return;
            var nameParts = fullName.Split(' ');
            // Full name more than 1 part
            if (nameParts.Count() >= 2)
            {
                lastName = nameParts.Last();
                firstName = string.Join(" ", nameParts.Take(nameParts.Count() - 1));
            }
            // Only 1 part
            else
            {
                firstName = fullName;
            }
        }

        /// <summary>
        /// Get first name and last name from keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public static string SeparateKeyword(this string keyword, out string firstName, out string lastName)
        {

            firstName = string.Empty;
            lastName = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
                var arrKeyword = keyword.Split(' ');

                if (arrKeyword.Length >= 2)
                {
                    firstName = arrKeyword[0];
                    lastName = arrKeyword[1];
                }
            }
            return keyword;
        }

        public static string GenerateFullName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(firstName))
            {
                return lastName;
            }

            return string.Format("{0} {1}", firstName, lastName);
        }

        /// <summary>
        /// Generate unique string
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueString()
        {
            var now = DateTime.UtcNow.ToString("U");
            return PasswordUtilities.Base64Encode(now);
        }

        #region Format

        public static string[] ParseExact(this string data, string format)
        {
            return ParseExact(data, format, false);
        }

        public static string[] ParseExact(this string data, string format, bool ignoreCase)
        {
            string[] values;

            if (TryParseExact(data, format, out values, ignoreCase))
                return values;
            throw new ArgumentException("Format not compatible with value.");
        }

        public static bool TryExtract(this string data, string format, out string[] values)
        {
            return TryParseExact(data, format, out values, false);
        }

        public static bool TryParseExact(this string data, string format, out string[] values, bool ignoreCase)
        {
            int tokenCount;
            format = Regex.Escape(format).Replace("\\{", "{");

            for (tokenCount = 0; ; tokenCount++)
            {
                string token = string.Format("{{{0}}}", tokenCount);
                if (!format.Contains(token)) break;
                format = format.Replace(token,
                    string.Format("(?'group{0}'.*)", tokenCount));
            }

            RegexOptions options =
                ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;

            Match match = new Regex(format, options).Match(data);

            if (tokenCount != (match.Groups.Count - 1))
            {
                values = new string[] { };
                return false;
            }

            values = new string[tokenCount];
            for (int index = 0; index < tokenCount; index++)
                values[index] = match.Groups[string.Format("group{0}", index)].Value;

            return true;
        }
        #endregion

        #region Compress / Decompress

        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string Compress(this string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer).Replace("+", "-").Replace("/", "_");
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string Decompress(this string compressedText)
        {
            compressedText = compressedText.Replace("-", "+").Replace("_", "/");
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        #endregion

        /// <summary>
        /// Split string to list string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<T> Split<T>(this string input, string separator)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new List<T>();
            }

            return input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim().ToType<T>()).ToList();
        }

        /// <summary>
        /// Split string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <param name="splitOptions"></param>
        /// <returns></returns>
        public static string[] Split(this string input, string separator, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            return input.Split(new[] { separator }, splitOptions);
        }

        public static List<int> SplitToInts(string input, string separator, bool removeDuplicated = false)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(input))
                return result;
            var parts = input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                int dump;
                if (int.TryParse(part, out dump))
                {
                    result.Add(dump);
                }
            }
            return removeDuplicated ? result : result.Distinct().ToList();
        }
    }

    public class StringLengthComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var result = -(x.Length.CompareTo(y.Length));
            if (result == 0)
            {
                result = String.Compare(x, y, StringComparison.Ordinal);
            }
            return result;
        }
    }

    public class MatchingString
    {
        public int Value { get; set; }

        public string Content { get; set; }
    }
}
