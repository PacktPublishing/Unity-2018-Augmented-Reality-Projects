/*
 *  ARNativePlugin.cs
 *  ARToolKit for Unity
 *
 *  This file is part of ARToolKit for Unity.
 *
 *  ARToolKit for Unity is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  ARToolKit for Unity is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with ARToolKit for Unity.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  As a special exception, the copyright holders of this library give you
 *  permission to link this library with independent modules to produce an
 *  executable, regardless of the license terms of these independent modules, and to
 *  copy and distribute the resulting executable under terms of your choice,
 *  provided that you also meet, for each linked independent module, the terms and
 *  conditions of the license of that module. An independent module is a module
 *  which is neither derived from nor based on this library. If you modify this
 *  library, you may extend this exception to your version of the library, but you
 *  are not obligated to do so. If you do not wish to do so, delete this exception
 *  statement from your version.
 *
 *  Copyright 2015 Daqri, LLC.
 *  Copyright 2010-2015 ARToolworks, Inc.
 *
 *  Author(s): Philip Lamb
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

public static class ARNativePlugin
{
	// The name of the external library containing the native functions
	private const string LIBRARY_NAME = "ARWrapper";

	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwRegisterLogCallback(PluginFunctions.LogCallback callback);

	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetLogLevel(int logLevel);

	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwInitialiseAR();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwInitialiseARWithOptions(int pattSize, int pattCountMax);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetARToolKitVersion([MarshalAs(UnmanagedType.LPStr)]StringBuilder buffer, int length);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetError();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwShutdownAR();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwStartRunningB(string vconf, byte[] cparaBuff, int cparaBuffLen, float nearPlane, float farPlane);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwStartRunningStereoB(string vconfL, byte[] cparaBuffL, int cparaBuffLenL, string vconfR, byte[] cparaBuffR, int cparaBuffLenR, byte[] transL2RBuff, int transL2RBuffLen, float nearPlane, float farPlane);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwIsRunning();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwStopRunning();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetProjectionMatrix([MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] matrix);

	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetProjectionMatrixStereo([MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] matrixL, [MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] matrixR);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetVideoParams(out int width, out int height, out int pixelSize, [MarshalAs(UnmanagedType.LPStr)]StringBuilder pixelFormatBuffer, int pixelFormatBufferLen);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetVideoParamsStereo(out int widthL, out int heightL, out int pixelSizeL, [MarshalAs(UnmanagedType.LPStr)]StringBuilder pixelFormatBufferL, int pixelFormatBufferLenL, out int widthR, out int heightR, out int pixelSizeR, [MarshalAs(UnmanagedType.LPStr)]StringBuilder pixelFormatBufferR, int pixelFormatBufferLenR);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwCapture();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwUpdateAR();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	//public static extern bool arwUpdateTexture([In, Out]Color[] colors);
	public static extern bool arwUpdateTexture(IntPtr colors);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	//public static extern bool arwUpdateTextureStereo([In, Out]Color[] colorsL, [In, Out]Color[] colorsR);
	public static extern bool arwUpdateTextureStereo(IntPtr colorsL, IntPtr colorsR);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	//public static extern bool arwUpdateTexture32([In, Out]Color32[] colors32);
	public static extern bool arwUpdateTexture32(IntPtr colors32);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	//public static extern bool arwUpdateTexture32Stereo([In, Out]Color32[] colors32L, [In, Out]Color32[] colors32R);
	public static extern bool arwUpdateTexture32Stereo(IntPtr colors32L, IntPtr colors32R);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwUpdateTextureGL(int textureID);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwUpdateTextureGLStereo(int textureID_L, int textureID_R);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetUnityRenderEventUpdateTextureGLTextureID(int textureID);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetUnityRenderEventUpdateTextureGLStereoTextureIDs(int textureID_L, int textureID_R);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetMarkerPatternCount(int markerID);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetMarkerPatternConfig(int markerID, int patternID, [MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] matrix, out float width, out float height, out int imageSizeX, out int imageSizeY);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetMarkerPatternImage(int markerID, int patternID, [In, Out]Color[] colors);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetMarkerOptionBool(int markerID, int option);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetMarkerOptionBool(int markerID, int option, bool value);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetMarkerOptionInt(int markerID, int option);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetMarkerOptionInt(int markerID, int option, int value);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern float arwGetMarkerOptionFloat(int markerID, int option);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetMarkerOptionFloat(int markerID, int option, float value);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetVideoDebugMode(bool debug);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetVideoDebugMode();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetVideoThreshold(int threshold);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetVideoThreshold();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetVideoThresholdMode(int mode);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetVideoThresholdMode();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetLabelingMode(int mode);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetLabelingMode();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetBorderSize(float mode);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern float arwGetBorderSize();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetPatternDetectionMode(int mode);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetPatternDetectionMode();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetMatrixCodeType(int type);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetMatrixCodeType();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetImageProcMode(int mode);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwGetImageProcMode();
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern void arwSetNFTMultiMode(bool on);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwGetNFTMultiMode();
	

	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern int arwAddMarker(string cfg);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwRemoveMarker(int markerID);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	public static extern int arwRemoveAllMarkers();
	
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwQueryMarkerVisibility(int markerID);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwQueryMarkerTransformation(int markerID, [MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] matrix);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwQueryMarkerTransformationStereo(int markerID, [MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] matrixL, [MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] matrixR);
	
	[DllImport(LIBRARY_NAME, CallingConvention=CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	[return: MarshalAsAttribute(UnmanagedType.I1)]
	public static extern bool arwLoadOpticalParams(string optical_param_name, byte[] optical_param_buff, int optical_param_buffLen, out float fovy_p, out float aspect_p, [MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] m, [MarshalAs(UnmanagedType.LPArray, SizeConst=16)] float[] p);
	
}

