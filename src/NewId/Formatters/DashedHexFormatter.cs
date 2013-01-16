﻿namespace NewId.Formatters
{
    public class DashedHexFormatter :
        INewIdFormatter
    {
        readonly char _prefix;
        readonly char _suffix;
        readonly int _alpha;
        int _length;

        public DashedHexFormatter(char prefix = '\0', char suffix = '\0', bool upperCase = false)
        {
            if (prefix == '\0' || suffix == '\0')
                _length = 36;
            else
            {
                _prefix = prefix;
                _suffix = suffix;
                _length = 38;
            }

            _alpha = upperCase ? 'A' : 'a';
        }

        public string Format(int[] bytes)
        {
            var result = new char[_length];

            int i = 0;
            int offset = 0;
            if (_prefix != '\0')
                result[offset++] = _prefix;
            for (; i < 4; i++)
            {
                int value = bytes[i];
                result[offset++] = HexToChar(value >> 4, _alpha);
                result[offset++] = HexToChar(value, _alpha);
            }
            result[offset++] = '-';
            for (; i < 6; i++)
            {
                int value = bytes[i];
                result[offset++] = HexToChar(value >> 4, _alpha);
                result[offset++] = HexToChar(value, _alpha);
            }
            result[offset++] = '-';
            for (; i < 8; i++)
            {
                int value = bytes[i];
                result[offset++] = HexToChar(value >> 4, _alpha);
                result[offset++] = HexToChar(value, _alpha);
            }
            result[offset++] = '-';
            for (; i < 10; i++)
            {
                int value = bytes[i];
                result[offset++] = HexToChar(value >> 4, _alpha);
                result[offset++] = HexToChar(value, _alpha);
            }
            result[offset++] = '-';
            for (; i < 16; i++)
            {
                int value = bytes[i];
                result[offset++] = HexToChar(value >> 4, _alpha);
                result[offset++] = HexToChar(value, _alpha);
            }
            if (_suffix != '\0')
                result[offset++] = _suffix;

            return new string(result, 0, _length);
        }

        static char HexToChar(int value, int alpha)
        {
            value = value & 0xf;
            return (char)((value > 9) ? value - 10 + alpha : value + 0x30);
        }
    }
}