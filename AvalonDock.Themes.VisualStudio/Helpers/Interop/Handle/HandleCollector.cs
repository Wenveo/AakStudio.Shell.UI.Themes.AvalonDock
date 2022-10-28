// reference from https://referencesource.microsoft.com/#WindowsBase/Shared/MS/Win32/HandleCollector.cs,d0f99220d8e1b708

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace AvalonDock.Themes.VisualStudio.Helpers.Interop.Handle
{
    internal static class HandleCollector
    {
        private static HandleType[]? _handleTypes;
        private static int _handleTypeCount;

        private static readonly object HandleMutex = new();

        internal static IntPtr Add(IntPtr handle, int type)
        {
            _handleTypes?[type - 1].Add();
            return handle;
        }

        [SecuritySafeCritical]
        internal static SafeHandle Add(SafeHandle handle, int type)
        {
            _handleTypes?[type - 1].Add();
            return handle;
        }

        internal static void Add(int type)
        {
            _handleTypes?[type - 1].Add();
        }

        internal static int RegisterType(string _, int expense, int initialThreshold)
        {
            lock (HandleMutex)
            {
                if (_handleTypeCount == 0 || _handleTypes is not null && _handleTypeCount == _handleTypes.Length)
                {
                    var newTypes = new HandleType[_handleTypeCount + 10];
                    if (_handleTypes is not null) Array.Copy(_handleTypes, 0, newTypes, 0, _handleTypeCount);
                    _handleTypes = newTypes;
                }

                if (_handleTypes is not null) _handleTypes[_handleTypeCount++] = new HandleType(expense, initialThreshold);
                return _handleTypeCount;
            }
        }

        internal static IntPtr Remove(IntPtr handle, int type)
        {
            _handleTypes?[type - 1].Remove();
            return handle;
        }

        [SecuritySafeCritical]
        internal static SafeHandle Remove(SafeHandle handle, int type)
        {
            _handleTypes?[type - 1].Remove();
            return handle;
        }

        internal static void Remove(int type)
        {
            _handleTypes?[type - 1].Remove();
        }

        private class HandleType
        {
            private readonly int _deltaPercent;
            private readonly int _initialThreshHold;
            private int _handleCount;
            private int _threshHold;

            internal HandleType(int expense, int initialThreshHold)
            {
                _initialThreshHold = initialThreshHold;
                _threshHold = initialThreshHold;
                _deltaPercent = 100 - expense;
            }

            internal void Add()
            {
                lock (this)
                {
                    _handleCount++;
                    var performCollect = NeedCollection();

                    if (!performCollect) return;
                }

                GC.Collect();

                var sleep = (100 - _deltaPercent) / 4;
                Thread.Sleep(sleep);
            }

            private bool NeedCollection()
            {
                if (_handleCount > _threshHold)
                {
                    _threshHold = _handleCount + _handleCount * _deltaPercent / 100;
                    return true;
                }

                var oldThreshHold = 100 * _threshHold / (100 + _deltaPercent);
                if (oldThreshHold >= _initialThreshHold && _handleCount < (int)(oldThreshHold * .9F))
                    _threshHold = oldThreshHold;

                return false;
            }

            internal void Remove()
            {
                lock (this)
                {
                    _handleCount--;

                    _handleCount = Math.Max(0, _handleCount);
                }
            }
        }
    }
}