using System;
using System.Collections.Generic;
using System.Text;

namespace UserRepository
{
    public class StringNormalizer
    {
        public string Normalize(string s)
        {
            return NormalizeUnicode(s)?.ToLowerInvariant();
        }

        //TODO: implement a proper stringprep implementation according to https://tools.ietf.org/html/rfc3454
        private string NormalizeUnicode(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return null;

            var builder = new StringBuilder();

            foreach (var c in s.Normalize(NormalizationForm.FormC))
            {
                builder.Append(MapUnicodeChar(c));
            }

            return builder.ToString();
        }

        private static readonly Dictionary<char, char> CharMap = new Dictionary<char, char>
        {
            {'s', 's'},
            {'ᴛ', 't'},
            {'ᴇ', 'e'},
            {'ᴠ', 'v'},
            {'ᴵ', 'i'},
        };

        private char MapUnicodeChar(in char c)
        {
            if (CharMap.TryGetValue(c, out var mapped))
                return mapped;

            return c;
        }
    }
}