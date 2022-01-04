// Autogenerated by FFIDJI

using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Security;

using char16 = System.Char;
using int8 = System.SByte;
using uint8 = System.Byte;
using int16 = System.Int16;
using uint16 = System.UInt16;
using int32 = System.Int32;
using uint32 = System.UInt32;
using int64 = System.Int64;
using uint64 = System.UInt64;
using float16 = System.Half;
using float32 = System.Single;
using float64 = System.Double;

namespace FFIDJI
{ 
    public static class InterfaceStructs
    { 
        public const string LIBRARY_NAME = "MyNativeLibrary.dll";

        private readonly struct Arr<T>
        { 
            public readonly IntPtr ptr;
            public readonly int size;
            public Arr(IntPtr ptr, int size)
            { 
                this.ptr = ptr;
                this.size = size;
            } 
        } 

        private unsafe static T[] CopyArray<T>(IntPtr ptr, int size) where T : unmanaged
        { 
            int length = size * sizeof(T);
            T[] array = new T[size];
            void* u_src = ptr.ToPointer();
            fixed (T* u_dst = &array[0])
            { 
                Unsafe.CopyBlock(u_dst, u_src, (uint)length);
            } 
            return array;
        } 

        private static T[] Convert<T>(Arr<T> arr) where T : unmanaged
        { 
            return CopyArray<T>(arr.ptr, arr.size);
        } 

        private static T Convert<T>(T obj) where T : unmanaged
        { 
            return obj;
        } 

        private unsafe static Arr<T> Convert<T>(T[] array) where T : unmanaged
        { 
            return Convert(new ReadOnlySpan<T>(array));
        } 

        private unsafe static Arr<T> Convert<T>(ReadOnlySpan<T> array) where T : unmanaged
        { 
            int length = array.Length * sizeof(T);
            IntPtr ptr = Alloc(length);
            void* u_dst = ptr.ToPointer();
            fixed (T* u_src = &array[0])
            { 
                Unsafe.CopyBlock(u_dst, u_src, (uint)length);
            } 
            return new Arr<T>(ptr, array.Length);
        } 

        [SuppressUnmanagedCodeSecurity]
        [DllImport(LIBRARY_NAME, EntryPoint = "Alloc_FFI")]
        private static extern IntPtr Alloc(int length);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(LIBRARY_NAME, EntryPoint = "Free_FFI")]
        private static extern void Free(IntPtr ptr, int length);

        private static unsafe void Free<T>(Arr<T> input) where T : unmanaged
        { 
            Free(input.ptr, input.size * sizeof(T));
        } 

        [StructLayout(LayoutKind.Sequential)]
        public struct StructA
        { 
            public int8 valueA;
            public int16 valueB;
            public int32 valueC;
            public int64 valueD;
            public StructB structB;
        } 

        [StructLayout(LayoutKind.Sequential)]
        public struct StructB
        { 
            public float16 valueA;
            public float32 valueB;
            public float64 valueC;
            public StructC structC;
        } 

        [StructLayout(LayoutKind.Sequential)]
        public struct StructC
        { 
            public uint8 valueA;
            public uint16 valueB;
            public uint32 valueC;
            public uint64 valueD;
        } 

        [StructLayout(LayoutKind.Sequential)]
        private struct string_FFI
        { 
            public Arr<char16> utf16_char;
        } 

        private static unsafe void Free(string_FFI input)
        { 
            Free(input.utf16_char.ptr, input.utf16_char.size * sizeof(char16));
        } 

        private static string Convert(string_FFI data_FFI)
        { 
            unsafe
            { 
                return new string((char*)data_FFI.utf16_char.ptr);
            } 
        } 

        private static string_FFI Convert(string data)
        { 
            return new string_FFI
            { 
                utf16_char = Convert(data.AsSpan())
            };
        } 

        private unsafe static string[] Convert(Arr<string_FFI> arr)
        { 
            var array_ffi = CopyArray<string_FFI>(arr.ptr, arr.size);
            var array = new string[arr.size];
            for (int i = 0; i < arr.size; ++i) array[i] = Convert(array_ffi[i]);
            return array;
        } 

        private unsafe static Arr<string_FFI> Convert(string[] array)
        { 
            int length = array.Length * sizeof(string_FFI);
            IntPtr ptr = Alloc(length);
            string_FFI* u_dst = (string_FFI*)ptr.ToPointer();
            for (int i = 0; i < length; ++i) u_dst[i] = Convert(array[i]);
            return new Arr<string_FFI>(ptr, length);
        } 

        [SuppressUnmanagedCodeSecurity]
        [DllImport(LIBRARY_NAME, EntryPoint = "Passthrough")]
        private extern static StructA Passthrough_FFI(StructA structIn);

        public static StructA Passthrough(StructA structIn)
        { 
            var structIn_ffi = Convert(structIn);
            var result_ffi = Passthrough_FFI(structIn_ffi);
            var result = Convert(result_ffi);
            return result;
        } 
    } 
} 
