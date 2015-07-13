using System.Runtime.InteropServices;

namespace th.Adminibot
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ScrollInfo
    {
        public uint cbSize;
        public uint fMask;
        public int nMin;
        public int nMax;
        public uint nPage;
        public int nPos;
        public int nTrackPos;
        
    }
}
