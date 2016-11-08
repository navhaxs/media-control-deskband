//	This file is a part of the Command Prompt Explorer Bar project.
// 
//	Copyright Pavel Zolnikov, 2002
//
//			declarations of some COM interfaces and structues

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BandObjectLib
{

	abstract class ExplorerGUIDs
	{
		public static readonly Guid IID_IWebBrowserApp = new Guid("{0002DF05-0000-0000-C000-000000000046}");
		public static readonly Guid IID_IUnknown =		 new Guid("{00000000-0000-0000-C000-000000000046}");
	}



	[Flags]
	public enum DBIM : uint
	{
		MINSIZE   =0x0001,
		MAXSIZE   =0x0002,
		INTEGRAL  =0x0004,
		ACTUAL    =0x0008,
		TITLE     =0x0010,
		MODEFLAGS =0x0020,
		BKCOLOR   =0x0040
	}


	[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
	public struct DESKBANDINFO
	{
		public UInt32		dwMask;
		public Point	    ptMinSize;
		public Point		ptMaxSize;
		public Point		ptIntegral;
		public Point		ptActual;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=255)]
		public String		wszTitle;
		public DBIM			dwModeFlags;
		public Int32		crBkgnd;

        /// <summary>
        /// The view mode of the band object. This is one of the following values.
        /// </summary>
        [Flags]
        public enum DBIF : uint
        {
            /// <summary>
            /// Band object is displayed in a horizontal band.
            /// </summary>
            DBIF_VIEWMODE_NORMAL = 0x0000,

            /// <summary>
            /// Band object is displayed in a vertical band.
            /// </summary>
            DBIF_VIEWMODE_VERTICAL = 0x0001,

            /// <summary>
            /// Band object is displayed in a floating band.
            /// </summary>
            DBIF_VIEWMODE_FLOATING = 0x0002,

            /// <summary>
            /// Band object is displayed in a transparent band.
            /// </summary>
            DBIF_VIEWMODE_TRANSPARENT = 0x0004
        }

    };

	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")] 
	public interface IObjectWithSite
	{
		void SetSite([In ,MarshalAs(UnmanagedType.IUnknown)] Object pUnkSite);
		void GetSite(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out Object ppvSite);
	}

    /// <summary>
    /// The IOleWindow interface provides methods that allow an application to obtain the handle to the various windows that participate in in-place activation, and also to enter and exit context-sensitive help mode.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000114-0000-0000-C000-000000000046")]
    public interface IOleWindow
    {
        /// <summary>
        /// Retrieves a handle to one of the windows participating in in-place activation (frame, document, parent, or in-place object window).
        /// </summary>
        /// <param name="phwnd">A pointer to a variable that receives the window handle.</param>
        /// <returns>This method returns S_OK on success. </returns>
        [PreserveSig]
        int GetWindow(out IntPtr phwnd);

        /// <summary>
        /// Determines whether context-sensitive help mode should be entered during an in-place activation session.
        /// </summary>
        /// <param name="fEnterMode">TRUE if help mode should be entered; FALSE if it should be exited.</param>
        /// <returns>This method returns S_OK if the help mode was entered or exited successfully, depending on the value passed in fEnterMode.</returns>
        [PreserveSig]
        int ContextSensitiveHelp(bool fEnterMode);
    };

    [ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("012dd920-7b26-11d0-8ca9-00a0c92dbfe8")] 
	public interface IDockingWindow
	{
        int GetWindow(out System.IntPtr phwnd);
        int ContextSensitiveHelp([In] bool fEnterMode);

        int ShowDW([In] bool fShow);
        int CloseDW([In] UInt32  dwReserved);
        int ResizeBorderDW(
			IntPtr prcBorder,
			[In, MarshalAs(UnmanagedType.IUnknown)] Object punkToolbarSite,
			bool fReserved);
	}

























    /// <summary>
    /// Gets information about a band object.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("EB0FE172-1A3A-11D0-89B3-00A0C90A90AC")]
    public interface IDeskBand : IDockingWindow
    {
        #region IOleWindow Overrides

        [PreserveSig]
        new int GetWindow(out IntPtr phwnd);

        [PreserveSig]
        new int ContextSensitiveHelp(bool fEnterMode);

        #endregion

        #region Overrides of IDockingWindow

        [PreserveSig]
        new int ShowDW(bool bShow);

        [PreserveSig]
        new int CloseDW(UInt32 dwReserved);

        [PreserveSig]
        new int ResizeBorderDW(RECT rcBorder, IntPtr punkToolbarSite, bool fReserved);

        #endregion

        /// <summary>
        /// Gets state information for a band object.
        /// </summary>
        /// <param name="dwBandID">The identifier of the band, assigned by the container. The band object can retain this value if it is required.</param>
        /// <param name="dwViewMode">The view mode of the band object. One of the following values: DBIF_VIEWMODE_NORMAL, DBIF_VIEWMODE_VERTICAL, DBIF_VIEWMODE_FLOATING, DBIF_VIEWMODE_TRANSPARENT.</param>
        /// <param name="pdbi">The pdbi.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetBandInfo(UInt32 dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi);
    }

    /// <summary>
    /// Exposes methods to enable and query translucency effects in a deskband object.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79D16DE4-ABEE-4021-8D9D-9169B261D657")]
    public interface IDeskBand2 : IDeskBand
    {
        #region IOleWindow Overrides

        [PreserveSig]
        new int GetWindow(out IntPtr phwnd);

        [PreserveSig]
        new int ContextSensitiveHelp(bool fEnterMode);

        #endregion

        #region Overrides of IDockingWindow

        [PreserveSig]
        new int ShowDW(bool bShow);

        [PreserveSig]
        new int CloseDW(UInt32 dwReserved);

        [PreserveSig]
        new int ResizeBorderDW(RECT rcBorder, IntPtr punkToolbarSite, bool fReserved);

        #endregion

        #region IDeskBand Overrides

        [PreserveSig]
        new int GetBandInfo(UInt32 dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi);

        #endregion

        /// <summary>
        /// Indicates the deskband's ability to be displayed as translucent.
        /// </summary>
        /// <param name="pfCanRenderComposited">When this method returns, contains a BOOL indicating ability.</param>
        /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [PreserveSig]
        int CanRenderComposited(out bool pfCanRenderComposited);

        /// <summary>
        /// Sets the composition state.
        /// </summary>
        /// <param name="fCompositionEnabled">TRUE to enable the composition state; otherwise, FALSE.</param>
        /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [PreserveSig]
        int SetCompositionState(bool fCompositionEnabled);

        /// <summary>
        /// Gets the composition state.
        /// </summary>
        /// <param name="pfCompositionEnabled">When this method returns, contains a BOOL that indicates state.</param>
        /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [PreserveSig]
        int GetCompositionState(out bool pfCompositionEnabled);
    }

    [ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010c-0000-0000-C000-000000000046")]
	public interface IPersist
	{
		void GetClassID(out Guid pClassID);
	}

	
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000109-0000-0000-C000-000000000046")]
	public interface IPersistStream
	{
		void GetClassID(out Guid pClassID);

		void IsDirty ();

		void Load ([In, MarshalAs(UnmanagedType.Interface)] Object pStm);

		void Save ([In, MarshalAs(UnmanagedType.Interface)] Object pStm,
			[In] bool fClearDirty);

		void GetSizeMax ([Out] out UInt64 pcbSize);
	}


	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
	public interface _IServiceProvider
	{
		void QueryService(
			ref Guid guid, 
			ref Guid riid, 
			[MarshalAs(UnmanagedType.Interface)] out Object Obj);
	}


	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("68284faa-6a48-11d0-8c78-00c04fd918b4")]
	public interface IInputObject
	{
		void UIActivateIO(Int32 fActivate, ref MSG  msg);
		
		[PreserveSig]
		//[return:MarshalAs(UnmanagedType.Error)]
		Int32 HasFocusIO();

		[PreserveSig]
		Int32 TranslateAcceleratorIO(ref MSG msg);
	}

	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("f1db8392-7331-11d0-8c99-00a0c92dbfe8")]
	public interface IInputObjectSite
	{
		[PreserveSig]
		Int32 OnFocusChangeIS( [MarshalAs(UnmanagedType.IUnknown)] Object punkObj, Int32 fSetFocus);
	}

	public struct POINT
	{
		public Int32		x;
		public Int32		y;
	}

	public struct MSG 
	{
		public IntPtr		hwnd;
		public UInt32		message;
		public UInt32		wParam;
		public Int32		lParam;
		public UInt32		time;
		public POINT		pt;
	}

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }


        public int left, top, right, bottom;

        public int Width()
        {
            return right - left;
        }

        public int Height()
        {
            return bottom - top;
        }

        public void Offset(int x, int y)
        {
            left += x;
            right += x;
            top += y;
            bottom += y;
        }

        public void Set(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public bool IsEmpty()
        {
            return Width() == 0 && Height() == 0;
        }
}

}