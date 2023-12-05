using System.Diagnostics;

namespace AdventOfCode2023.Utility
{
#if !DEBUG
    [DebuggerStepThrough]
#endif
    public struct MemoryLineEnumerator
    {
        private ReadOnlyMemory<char> _remaining;
        private ReadOnlyMemory<char> _current;
        private bool _isEnumeratorActive;

        internal MemoryLineEnumerator(ReadOnlyMemory<char> buffer)
        {
            _remaining = buffer;
            _current = default;
            _isEnumeratorActive = true;
        }

        public readonly ReadOnlyMemory<char> Current => _current;
        public readonly MemoryLineEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (!_isEnumeratorActive)
                return false; // EOF previously reached or enumerator was never initialized

            var remaining = _remaining;

            int idx = remaining.Span.IndexOfAny("\r\n");

            if ((uint)idx < (uint)remaining.Length)
            {
                int stride = 1;

                if (remaining.Span[idx] == '\r' && (uint)(idx + 1) < (uint)remaining.Length && remaining.Span[idx + 1] == '\n')
                    stride = 2;

                _current = remaining[..idx];
                _remaining = remaining[(idx + stride)..];
            }
            else
            {
                // We've reached EOF, but we still need to return 'true' for this final
                // iteration so that the caller can query the Current property once more.

                _current = remaining;
                _remaining = default;
                _isEnumeratorActive = false;
            }

            return true;
        }
    }
}
