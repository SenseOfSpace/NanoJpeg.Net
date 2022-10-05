namespace NanoJpeg
{
    public ref struct Span<T>
    {

        public Span(T[] data)
        {
            this.data = data;
            offset = 0;
            length = data.Length;
        }

        public Span(T[] data, int offset)
        {
            this.data = data;
            this.offset = offset;
            length = data.Length - offset;
        }

        public Span(T[] data, int offset, int length)
        {
            this.data = data;
            this.offset = offset;
            this.length = length;
        }

        public Span<T> Slice(int offset)
        {
            int newOffset = this.offset + offset;
            return new Span<T>(data, newOffset, length - newOffset);
        }

        public Span<T> Slice(int offset, int length)
        {
            int newOffset = this.offset + offset;
            return new Span<T>(data, newOffset, length);
        }

        public T this[int index]
        {
            get => data[offset + index];
            set => data[offset + index] = value;
        }

        public T[] data;
        public int offset;
        public int length;

    }
}
