using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace BuyTicketFor12306.SafeWebBrowserExt
{
    class SafeWebBrowser : WebBrowser
    {
        public delegate void DocHostShowUIShowMessageEventHandler(object sender, ExtendedBrowserMessageEventArgs e);
        public event DocHostShowUIShowMessageEventHandler WBDocHostShowUIShowMessage = null;

        class ExtendedWebBrowserSite : WebBrowserSite, NativeMethods.IOleCommandTarget, UnsafeNativeMethods.IDocHostShowUI
        {
            /// <summary>
            /// Creates a new instance of the <see cref="ExtendedWebBrowserSite"/> class
            /// </summary>
            /// <param name="host">The <see cref="ExtendedWebBrowser"/> hosting the browser</param>
            public ExtendedWebBrowserSite(SafeWebBrowser host)
                : base(host)
            {
                _host = host;
            }

            private SafeWebBrowser _host;
            private SafeWebBrowser Host
            {
                get
                {
                    return _host;
                }
            }

            #region IOleCommandTarget Members

            int NativeMethods.IOleCommandTarget.QueryStatus(ref Guid pguidCmdGroup, int cCmds, NativeMethods.OLECMD prgCmds, IntPtr pCmdText)
            {
                return NativeMethods.S_FALSE;
            }

            int NativeMethods.IOleCommandTarget.Exec(ref Guid pguidCmdGroup, int nCmdID, int nCmdexecopt, object[] pvaIn, ref int pvaOut)
            {
                int hResult = NativeMethods.S_OK;
                if (pguidCmdGroup == null)
                    return hResult;
                // Check for invalid pointers (or get a NullReferenceException on a value type???)
                //if (NativeMethods.CGID_DocHostCommandHandler.Equals(pguidCmdGroup))
                {
                    switch (nCmdID)
                    {
                        case (int)NativeMethods.OLECMDID.OLECMDID_SHOWSCRIPTERROR:
                            // Hide the dialog
                            pvaOut = NativeMethods.VARIANT_TRUE;
                            break;
                        default:
                            hResult = NativeMethods.OLECMDERR_E_NOTSUPPORTED;
                            break;
                    }
                }
                return hResult;
            }
            #endregion

            public int ShowMessage(IntPtr hwnd, string lpstrText, string lpstrCaption, uint dwType, string lpstrHelpFile, uint dwHelpContext, ref int lpResult)
            {
                int iRet = 1;  //Hresults.S_FALSE
                if (Host.WBDocHostShowUIShowMessage != null)
                {
                    ExtendedBrowserMessageEventArgs args = new ExtendedBrowserMessageEventArgs(hwnd, lpstrText, lpstrCaption, dwType, lpstrHelpFile, dwHelpContext);
                    Host.WBDocHostShowUIShowMessage(this, args);
                    if (args.Cancel == true)
                    {

                        iRet = 0;//Hresults.S_OK;  
                        lpResult = args.pResult;
                    }
                }
                return iRet;
            }

            public int ShowHelp(IntPtr hwnd, string pszHelpFile, uint uCommand, uint dwData, UnsafeNativeMethods.tagPOINT ptMouse, object pDispatchObjectHit)
            {
                return unchecked((int)0x80004001);//Hresults.E_NOTIMPL
            }
        }

        public class UnsafeNativeMethods
        {
            #region IDocHostShowUI
            [StructLayout(LayoutKind.Explicit, Pack = 4)]
            public struct __MIDL_IWinTypes_0009
            {
                // Fields 
                [FieldOffset(0)]
                public int hInproc;
                [FieldOffset(0)]
                public int hRemote;
            }
            [StructLayout(LayoutKind.Sequential, Pack = 4)]
            public struct _RemotableHandle
            {
                public int fContext;
                public __MIDL_IWinTypes_0009 u;
            }
            [StructLayout(LayoutKind.Sequential, Pack = 4)]
            public struct tagPOINT
            {
                public int x;
                public int y;
            }
            [ComImport, ComVisible(true)]
            [Guid("C4D244B0-D43E-11CF-893B-00AA00BDCE1A")]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IDocHostShowUI
            {
                [return: MarshalAs(UnmanagedType.I4)]
                [PreserveSig]
                int ShowMessage(
                    IntPtr hwnd,
                    [MarshalAs(UnmanagedType.LPWStr)] string lpstrText,
                    [MarshalAs(UnmanagedType.LPWStr)] string lpstrCaption,
                    [MarshalAs(UnmanagedType.U4)] uint dwType,
                    [MarshalAs(UnmanagedType.LPWStr)] string lpstrHelpFile,
                    [MarshalAs(UnmanagedType.U4)] uint dwHelpContext,
                    [In, Out] ref int lpResult);
                [return: MarshalAs(UnmanagedType.I4)]
                [PreserveSig]
                int ShowHelp(
                    IntPtr hwnd,
                    [MarshalAs(UnmanagedType.LPWStr)] string pszHelpFile,
                    [MarshalAs(UnmanagedType.U4)] uint uCommand,
                    [MarshalAs(UnmanagedType.U4)] uint dwData,
                    [In, MarshalAs(UnmanagedType.Struct)] tagPOINT ptMouse,
                    [Out, MarshalAs(UnmanagedType.IDispatch)] object pDispatchObjectHit);
            }
            #endregion
        }

        #region ExtendedBrowserMessageEventArgs
        public class ExtendedBrowserMessageEventArgs : CancelEventArgs
        {
            private int _plResult;
            public int pResult
            {
                get { return _plResult; }
            }
            public System.Windows.Forms.DialogResult DlgResult
            {
                set { _plResult = (int)value; }
            }
            private IntPtr _hwnd;
            public IntPtr hwnd
            {
                get { return _hwnd; }
            }
            private string _lpstrText;
            public string Text
            {
                get { return _lpstrText; }
            }
            private string _lpstrCaption;
            public string Caption
            {
                get { return _lpstrCaption; }
            }
            private uint _dwType;
            public System.Windows.Forms.MessageBoxButtons DlgButtons
            {
                get
                {
                    switch (_dwType & (int)MsgBoxButton.MASK)
                    {
                        case (int)MsgBoxButton.MB_OKCANCEL:
                            return System.Windows.Forms.MessageBoxButtons.OKCancel;
                        case (int)MsgBoxButton.MB_ABORTRETRYIGNORE:
                            return System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore;
                        case (int)MsgBoxButton.MB_YESNOCANCEL:
                            return System.Windows.Forms.MessageBoxButtons.YesNoCancel;
                        case (int)MsgBoxButton.MB_YESNO:
                            return System.Windows.Forms.MessageBoxButtons.YesNo;
                        case (int)MsgBoxButton.MB_RETRYCANCEL:
                            return System.Windows.Forms.MessageBoxButtons.RetryCancel;
                        case (int)MsgBoxButton.MB_OK:
                        default:
                            return System.Windows.Forms.MessageBoxButtons.OK;
                    }
                }
            }
            public System.Windows.Forms.MessageBoxIcon DlgIcon
            {
                get
                {
                    switch (_dwType & (int)MsgBoxIcon.MASK)
                    {
                        case (int)MsgBoxIcon.MB_ICONHAND:
                            return System.Windows.Forms.MessageBoxIcon.Hand;
                        case (int)MsgBoxIcon.MB_ICONQUESTION:
                            return System.Windows.Forms.MessageBoxIcon.Question;
                        case (int)MsgBoxIcon.MB_ICONEXCLAMATION:
                            return System.Windows.Forms.MessageBoxIcon.Exclamation;
                        case (int)MsgBoxIcon.MB_ICONASTERISK:
                            return System.Windows.Forms.MessageBoxIcon.Asterisk;
                        case (int)MsgBoxIcon.MB_ICONNONE:
                        case (int)MsgBoxIcon.MB_USERICON:
                        default:
                            return System.Windows.Forms.MessageBoxIcon.None;
                    }
                }
            }
            public System.Windows.Forms.MessageBoxDefaultButton DlgDefaultButtons
            {
                get
                {
                    switch (_dwType & (int)MsgBoxDefButton.MASK)
                    {
                        case (int)MsgBoxDefButton.MB_DEFBUTTON2:
                            return System.Windows.Forms.MessageBoxDefaultButton.Button2;
                        case (int)MsgBoxDefButton.MB_DEFBUTTON3:
                            return System.Windows.Forms.MessageBoxDefaultButton.Button3;
                        case (int)MsgBoxDefButton.MB_DEFBUTTON1:
                        default:
                            return System.Windows.Forms.MessageBoxDefaultButton.Button1;
                    }
                }
            }
            public System.Windows.Forms.MessageBoxOptions DlgOptions
            {
                get
                {
                    switch (_dwType & (int)MsgBoxOption.MASK)
                    {
                        case (int)MsgBoxOption.MB_DEFAULT_DESKTOP_ONLY:
                            return System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly;
                        case (int)MsgBoxOption.MB_RIGHT:
                            return System.Windows.Forms.MessageBoxOptions.RightAlign;
                        case (int)MsgBoxOption.MB_RTLREADING:
                            return System.Windows.Forms.MessageBoxOptions.RtlReading;
                        case (int)MsgBoxOption.MB_SERVICE_NOTIFICATION1:
                        case (int)MsgBoxOption.MB_SERVICE_NOTIFICATION2:
                            return System.Windows.Forms.MessageBoxOptions.ServiceNotification;
                        default:
                            return 0;
                    }
                }
            }
            public bool displayHelpButton
            {
                get { return (_dwType & (int)MsgBoxHelpButton.MASK) == (int)MsgBoxHelpButton.MB_HELP; }
            }
            private string _lpstrHelpFile;
            public string lpstrHelpFile
            {
                get { return _lpstrHelpFile; }
            }
            private uint _dwHelpContext;
            public uint dwHelpContext
            {
                get { return _dwHelpContext; }
            }
            public ExtendedBrowserMessageEventArgs(IntPtr Hwnd, string LpstrText, string LpstrCaption, uint DwType, string LpstrHelpFile, uint DwHelpContext)
                : base()
            {
                this._hwnd = Hwnd;
                _lpstrText = LpstrText;
                _lpstrCaption = LpstrCaption;
                _dwType = DwType;
                _lpstrHelpFile = LpstrHelpFile;
                _dwHelpContext = DwHelpContext;
                switch (DlgButtons)
                {
                    case System.Windows.Forms.MessageBoxButtons.OKCancel:
                    case System.Windows.Forms.MessageBoxButtons.RetryCancel:
                    case System.Windows.Forms.MessageBoxButtons.YesNoCancel:
                        DlgResult = System.Windows.Forms.DialogResult.Cancel;
                        break;
                    case System.Windows.Forms.MessageBoxButtons.YesNo:
                        DlgResult = System.Windows.Forms.DialogResult.No;
                        break;
                    case System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore:
                        DlgResult = System.Windows.Forms.DialogResult.Abort;
                        break;
                    case System.Windows.Forms.MessageBoxButtons.OK:
                    default:
                        DlgResult = System.Windows.Forms.DialogResult.OK;
                        break;
                }
            }
        }
        public enum MsgBoxButton
        {
            MASK = 0x0000000F,
            MB_OK = 0x00000000,
            MB_OKCANCEL = 0x00000001,
            MB_ABORTRETRYIGNORE = 0x00000002,
            MB_YESNOCANCEL = 0x00000003,
            MB_YESNO = 0x00000004,
            MB_RETRYCANCEL = 0x00000005
        }
        public enum MsgBoxIcon
        {
            MASK = 0x000000F0,
            MB_ICONNONE = 0x00000000,
            MB_ICONHAND = 0x00000010,
            MB_ICONSTOP = 0x00000010,
            MB_ICONERROR = 0x00000010,
            MB_ICONQUESTION = 0x00000020,
            MB_ICONEXCLAMATION = 0x00000030,
            MB_ICONWARNING = 0x00000030,
            MB_ICONASTERISK = 0x00000040,
            MB_ICONINFORMATION = 0x00000040,
            MB_USERICON = 0x00000080
        }
        public enum MsgBoxDefButton
        {
            MASK = 0x00000F00,
            MB_DEFBUTTON1 = 0x00000000,
            MB_DEFBUTTON2 = 0x00000100,
            MB_DEFBUTTON3 = 0x00000200
        }
        public enum MsgBoxOption
        {
            MASK = 0x003F0000,
            MB_DEFAULT_DESKTOP_ONLY = 0x00020000,
            MB_RIGHT = 0x00080000,
            MB_RTLREADING = 0x00100000,
            MB_SERVICE_NOTIFICATION1 = 0x00200000,
            MB_SERVICE_NOTIFICATION2 = 0x00040000
        }
        public enum MsgBoxHelpButton
        {
            MASK = 0x00004000,
            MB_HELP = 0x00004000
        }
        #endregion

        //override

        protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
        {
            return new ExtendedWebBrowserSite(this);
        }
    }
}
