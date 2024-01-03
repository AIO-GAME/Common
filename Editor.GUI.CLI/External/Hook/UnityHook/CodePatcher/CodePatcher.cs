using System;
using DotNetDetour;

namespace MonoHook
{
    public unsafe abstract class CodePatcher
    {
        public bool isValid { get; protected set; }

        protected void* _pTarget, _pReplace, _pProxy;
        protected int _jmpCodeSize;
        protected byte[] _targetHeaderBackup;

        public CodePatcher(IntPtr target, IntPtr replace, IntPtr proxy, int jmpCodeSize)
        {
            _pTarget = target.ToPointer();
            _pReplace = replace.ToPointer();
            _pProxy = proxy.ToPointer();
            _jmpCodeSize = jmpCodeSize;
        }

        public void ApplyPatch()
        {
            BackupHeader();
            EnableAddrModifiable();
            PatchTargetMethod();
            PatchProxyMethod();
            FlushICache();
        }

        public void RemovePatch()
        {
            if (_targetHeaderBackup == null)
                return;

            EnableAddrModifiable();
            RestoreHeader();
            FlushICache();
        }

        protected void BackupHeader()
        {
            if (_targetHeaderBackup != null)
                return;

            uint requireSize = LDasm.SizeofMinNumByte(_pTarget, _jmpCodeSize);
            _targetHeaderBackup = new byte[requireSize];

            fixed (void* ptr = _targetHeaderBackup)
                HookUtils.MemCpy(ptr, _pTarget, _targetHeaderBackup.Length);
        }

        protected void RestoreHeader()
        {
            if (_targetHeaderBackup == null)
                return;

            HookUtils.MemCpy_Jit(_pTarget, _targetHeaderBackup);
        }

        protected void PatchTargetMethod()
        {
            byte[] buff = GenJmpCode(_pTarget, _pReplace);
            HookUtils.MemCpy_Jit(_pTarget, buff);
        }

        protected void PatchProxyMethod()
        {
            if (_pProxy == null)
                return;

            // copy target's code to proxy
            HookUtils.MemCpy_Jit(_pProxy, _targetHeaderBackup);

            // jmp to target's new position
            long jmpFrom = (long)_pProxy + _targetHeaderBackup.Length;
            long jmpTo = (long)_pTarget + _targetHeaderBackup.Length;

            byte[] buff = GenJmpCode((void*)jmpFrom, (void*)jmpTo);
            HookUtils.MemCpy_Jit((void*)jmpFrom, buff);
        }

        protected void FlushICache()
        {
            HookUtils.FlushICache(_pTarget, _targetHeaderBackup.Length);
            HookUtils.FlushICache(_pProxy, _targetHeaderBackup.Length * 2);
        }

        protected abstract byte[] GenJmpCode(void* jmpFrom, void* jmpTo);

#if ENABLE_HOOK_DEBUG
        protected string PrintAddrs()
        {
            if (IntPtr.Size == 4)
                return $"target:0x{(uint)_pTarget:x}, replace:0x{(uint)_pReplace:x}, proxy:0x{(uint)_pProxy:x}";
            else
                return $"target:0x{(ulong)_pTarget:x}, replace:0x{(ulong)_pReplace:x}, proxy:0x{(ulong)_pProxy:x}";
        }
#endif

        private void EnableAddrModifiable()
        {
            HookUtils.SetAddrFlagsToRWX(new IntPtr(_pTarget), _targetHeaderBackup.Length);
            HookUtils.SetAddrFlagsToRWX(new IntPtr(_pProxy), _targetHeaderBackup.Length + _jmpCodeSize);
        }
    }
}