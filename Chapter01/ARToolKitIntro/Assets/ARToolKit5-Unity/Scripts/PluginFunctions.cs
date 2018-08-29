/*
 *  PluginFunctions.cs
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
 *  Author(s): Philip Lamb, Julian Looser
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

public static class PluginFunctions
{
	[NonSerialized]
	public static bool inited = false;

	// Delegate type declaration.
	public delegate void LogCallback([MarshalAs(UnmanagedType.LPStr)] string msg);

	// Delegate instance.
	private static LogCallback logCallback = null;
	private static GCHandle logCallbackGCH;

	public static void arwRegisterLogCallback(LogCallback lcb)
	{
		if (lcb != null) {
			logCallback = lcb;
			logCallbackGCH = GCHandle.Alloc(logCallback); // Does not need to be pinned, see http://stackoverflow.com/a/19866119/316487 
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwRegisterLogCallback(logCallback);
		else ARNativePlugin.arwRegisterLogCallback(logCallback);
		if (lcb == null) {
			logCallback = null;
			logCallbackGCH.Free();
		}
	}

	public static void arwSetLogLevel(int logLevel)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetLogLevel(logLevel);
		else ARNativePlugin.arwSetLogLevel(logLevel);
	}

	public static bool arwInitialiseAR(int pattSize = 16, int pattCountMax = 25)
	{
		bool ok;
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwInitialiseARWithOptions(pattSize, pattCountMax);
		else ok = ARNativePlugin.arwInitialiseARWithOptions(pattSize, pattCountMax);
		if (ok) PluginFunctions.inited = true;
		return ok;
	}
	
	public static string arwGetARToolKitVersion()
	{
		StringBuilder sb = new StringBuilder(128);
		bool ok;
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwGetARToolKitVersion(sb, sb.Capacity);
		else ok = ARNativePlugin.arwGetARToolKitVersion(sb, sb.Capacity);
		if (ok) return sb.ToString();
		else return "unknown";
	}

	public static int arwGetError()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetError();
		else return ARNativePlugin.arwGetError();
	}

    public static bool arwShutdownAR()
	{
		bool ok;
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwShutdownAR();
		else ok = ARNativePlugin.arwShutdownAR();
		if (ok) PluginFunctions.inited = false;
		return ok;
	}
	
	public static bool arwStartRunningB(string vconf, byte[] cparaBuff, int cparaBuffLen, float nearPlane, float farPlane)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwStartRunningB(vconf, cparaBuff, cparaBuffLen, nearPlane, farPlane);
		else return ARNativePlugin.arwStartRunningB(vconf, cparaBuff, cparaBuffLen, nearPlane, farPlane);
	}
	
	public static bool arwStartRunningStereoB(string vconfL, byte[] cparaBuffL, int cparaBuffLenL, string vconfR, byte[] cparaBuffR, int cparaBuffLenR, byte[] transL2RBuff, int transL2RBuffLen, float nearPlane, float farPlane)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwStartRunningStereoB(vconfL, cparaBuffL, cparaBuffLenL, vconfR, cparaBuffR, cparaBuffLenR, transL2RBuff, transL2RBuffLen, nearPlane, farPlane);
		else return ARNativePlugin.arwStartRunningStereoB(vconfL, cparaBuffL, cparaBuffLenL, vconfR, cparaBuffR, cparaBuffLenR, transL2RBuff, transL2RBuffLen, nearPlane, farPlane);
	}

	public static bool arwIsRunning()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwIsRunning();
		else return ARNativePlugin.arwIsRunning();
	}

	public static bool arwStopRunning()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwStopRunning();
		else return ARNativePlugin.arwStopRunning();
	}

	public static bool arwGetProjectionMatrix(float[] matrix)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetProjectionMatrix(matrix);
		else return ARNativePlugin.arwGetProjectionMatrix(matrix);
	}

	public static bool arwGetProjectionMatrixStereo(float[] matrixL, float[] matrixR)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetProjectionMatrixStereo(matrixL, matrixR);
		else return ARNativePlugin.arwGetProjectionMatrixStereo(matrixL, matrixR);
	}

	public static bool arwGetVideoParams(out int width, out int height, out int pixelSize, out String pixelFormatString)
	{
		StringBuilder sb = new StringBuilder(128);
		bool ok;
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwGetVideoParams(out width, out height, out pixelSize, sb, sb.Capacity);
		else ok = ARNativePlugin.arwGetVideoParams(out width, out height, out pixelSize, sb, sb.Capacity);
		if (!ok) pixelFormatString = "";
		else pixelFormatString = sb.ToString();
		return ok;
	}

	public static bool arwGetVideoParamsStereo(out int widthL, out int heightL, out int pixelSizeL, out String pixelFormatL, out int widthR, out int heightR, out int pixelSizeR, out String pixelFormatR)
	{
		StringBuilder sbL = new StringBuilder(128);
		StringBuilder sbR = new StringBuilder(128);
		bool ok;
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwGetVideoParamsStereo(out widthL, out heightL, out pixelSizeL, sbL, sbL.Capacity, out widthR, out heightR, out pixelSizeR, sbR, sbR.Capacity);
		else ok = ARNativePlugin.arwGetVideoParamsStereo(out widthL, out heightL, out pixelSizeL, sbL, sbL.Capacity, out widthR, out heightR, out pixelSizeR, sbR, sbR.Capacity);
		if (!ok) {
			pixelFormatL = "";
			pixelFormatR = "";
		} else {
			pixelFormatL = sbL.ToString();
			pixelFormatR = sbR.ToString();
		}
		return ok;
	}

	public static bool arwCapture()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwCapture();
		else return ARNativePlugin.arwCapture();
	}

	public static bool arwUpdateAR()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwUpdateAR();
		else return ARNativePlugin.arwUpdateAR();
	}
	
    public static bool arwUpdateTexture([In, Out]Color[] colors)
	{
		bool ok;
		GCHandle handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
		IntPtr address = handle.AddrOfPinnedObject();
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwUpdateTexture(address);
		else ok = ARNativePlugin.arwUpdateTexture(address);
		handle.Free();
		return ok;
	}

	public static bool arwUpdateTextureStereo([In, Out]Color[] colorsL, [In, Out]Color[] colorsR)
	{
		bool ok;
		GCHandle handle0 = GCHandle.Alloc(colorsL, GCHandleType.Pinned);
		GCHandle handle1 = GCHandle.Alloc(colorsR, GCHandleType.Pinned);
		IntPtr address0 = handle0.AddrOfPinnedObject();
		IntPtr address1 = handle1.AddrOfPinnedObject();
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwUpdateTextureStereo(address0, address1);
		else ok = ARNativePlugin.arwUpdateTextureStereo(address0, address1);
		handle0.Free();
		handle1.Free();
		return ok;
	}
	
	public static bool arwUpdateTexture32([In, Out]Color32[] colors32)
	{
		bool ok;
		GCHandle handle = GCHandle.Alloc(colors32, GCHandleType.Pinned);
		IntPtr address = handle.AddrOfPinnedObject();
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwUpdateTexture32(address);
		else ok = ARNativePlugin.arwUpdateTexture32(address);
		handle.Free();
		return ok;
	}
	
	public static bool arwUpdateTexture32Stereo([In, Out]Color32[] colors32L, [In, Out]Color32[] colors32R)
	{
		bool ok;
		GCHandle handle0 = GCHandle.Alloc(colors32L, GCHandleType.Pinned);
		GCHandle handle1 = GCHandle.Alloc(colors32R, GCHandleType.Pinned);
		IntPtr address0 = handle0.AddrOfPinnedObject();
		IntPtr address1 = handle1.AddrOfPinnedObject();
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwUpdateTexture32Stereo(address0, address1);
		else ok = ARNativePlugin.arwUpdateTexture32Stereo(address0, address1);
		handle0.Free();
		handle1.Free();
		return ok;
	}
	
	public static bool arwUpdateTextureGL(int textureID)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwUpdateTextureGL(textureID);
		else return ARNativePlugin.arwUpdateTextureGL(textureID);
	}
	
	public static bool arwUpdateTextureGLStereo(int textureID_L, int textureID_R)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwUpdateTextureGLStereo(textureID_L, textureID_R);
		else return ARNativePlugin.arwUpdateTextureGLStereo(textureID_L, textureID_R);
	}

	public static void arwSetUnityRenderEventUpdateTextureGLTextureID(int textureID)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetUnityRenderEventUpdateTextureGLTextureID(textureID);
		else ARNativePlugin.arwSetUnityRenderEventUpdateTextureGLTextureID(textureID);
	}

	public static void arwSetUnityRenderEventUpdateTextureGLStereoTextureIDs(int textureID_L, int textureID_R)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetUnityRenderEventUpdateTextureGLStereoTextureIDs(textureID_L, textureID_R);
		else ARNativePlugin.arwSetUnityRenderEventUpdateTextureGLStereoTextureIDs(textureID_L, textureID_R);
	}
	
	public static int arwGetMarkerPatternCount(int markerID)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetMarkerPatternCount(markerID);
		else return ARNativePlugin.arwGetMarkerPatternCount(markerID);
	}

	public static bool arwGetMarkerPatternConfig(int markerID, int patternID, float[] matrix, out float width, out float height, out int imageSizeX, out int imageSizeY)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetMarkerPatternConfig(markerID, patternID, matrix, out width, out height, out imageSizeX, out imageSizeY);
		else return ARNativePlugin.arwGetMarkerPatternConfig(markerID, patternID, matrix, out width, out height, out imageSizeX, out imageSizeY);
	}
	
	public static bool arwGetMarkerPatternImage(int markerID, int patternID, [In, Out]Color[] colors)
	{
		bool ok;
		if (Application.platform == RuntimePlatform.IPhonePlayer) ok = ARNativePluginStatic.arwGetMarkerPatternImage(markerID, patternID, colors);
		else ok = ARNativePlugin.arwGetMarkerPatternImage(markerID, patternID, colors);
		return ok;
	}
	
	public static bool arwGetMarkerOptionBool(int markerID, int option)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetMarkerOptionBool(markerID, option);
		else return ARNativePlugin.arwGetMarkerOptionBool(markerID, option);
	}
	
	public static void arwSetMarkerOptionBool(int markerID, int option, bool value)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetMarkerOptionBool(markerID, option, value);
		else ARNativePlugin.arwSetMarkerOptionBool(markerID, option, value);
	}

	public static int arwGetMarkerOptionInt(int markerID, int option)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetMarkerOptionInt(markerID, option);
		else return ARNativePlugin.arwGetMarkerOptionInt(markerID, option);
	}
	
	public static void arwSetMarkerOptionInt(int markerID, int option, int value)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetMarkerOptionInt(markerID, option, value);
		else ARNativePlugin.arwSetMarkerOptionInt(markerID, option, value);
	}

	public static float arwGetMarkerOptionFloat(int markerID, int option)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetMarkerOptionFloat(markerID, option);
		else return ARNativePlugin.arwGetMarkerOptionFloat(markerID, option);
	}
	
	public static void arwSetMarkerOptionFloat(int markerID, int option, float value)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetMarkerOptionFloat(markerID, option, value);
		else ARNativePlugin.arwSetMarkerOptionFloat(markerID, option, value);
	}

	public static void arwSetVideoDebugMode(bool debug)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetVideoDebugMode(debug);
		else ARNativePlugin.arwSetVideoDebugMode(debug);
	}

	public static bool arwGetVideoDebugMode()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetVideoDebugMode();
		else return ARNativePlugin.arwGetVideoDebugMode();
	}

	public static void arwSetVideoThreshold(int threshold)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetVideoThreshold(threshold);
		else ARNativePlugin.arwSetVideoThreshold(threshold);
	}

	public static int arwGetVideoThreshold()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetVideoThreshold();
		else return ARNativePlugin.arwGetVideoThreshold();
	}

	public static void arwSetVideoThresholdMode(int mode)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetVideoThresholdMode(mode);
		else ARNativePlugin.arwSetVideoThresholdMode(mode);
	}

	public static int arwGetVideoThresholdMode()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetVideoThresholdMode();
		else return ARNativePlugin.arwGetVideoThresholdMode();
	}

	public static void arwSetLabelingMode(int mode)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetLabelingMode(mode);
		else ARNativePlugin.arwSetLabelingMode(mode);
	}

	public static int arwGetLabelingMode()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetLabelingMode();
		else return ARNativePlugin.arwGetLabelingMode();
	}

	public static void arwSetBorderSize(float size)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetBorderSize(size);
		else ARNativePlugin.arwSetBorderSize(size);
	}

	public static float arwGetBorderSize()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetBorderSize();
		else return ARNativePlugin.arwGetBorderSize();
	}

	public static void arwSetPatternDetectionMode(int mode)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetPatternDetectionMode(mode);
		else ARNativePlugin.arwSetPatternDetectionMode(mode);
	}

	public static int arwGetPatternDetectionMode()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetPatternDetectionMode();
		else return ARNativePlugin.arwGetPatternDetectionMode();
	}

	public static void arwSetMatrixCodeType(int type)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetMatrixCodeType(type);
		else ARNativePlugin.arwSetMatrixCodeType(type);
	}

	public static int arwGetMatrixCodeType()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetMatrixCodeType();
		else return ARNativePlugin.arwGetMatrixCodeType();
	}

	public static void arwSetImageProcMode(int mode)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetImageProcMode(mode);
		else ARNativePlugin.arwSetImageProcMode(mode);
	}

	public static int arwGetImageProcMode()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetImageProcMode();
		else return ARNativePlugin.arwGetImageProcMode();
	}
	
	public static void arwSetNFTMultiMode(bool on)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) ARNativePluginStatic.arwSetNFTMultiMode(on);
		else ARNativePlugin.arwSetNFTMultiMode(on);
	}

	public static bool arwGetNFTMultiMode()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwGetNFTMultiMode();
		else return ARNativePlugin.arwGetNFTMultiMode();
	}


	public static int arwAddMarker(string cfg)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwAddMarker(cfg);
		else return ARNativePlugin.arwAddMarker(cfg);
	}
	
	public static bool arwRemoveMarker(int markerID)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwRemoveMarker(markerID);
		else return ARNativePlugin.arwRemoveMarker(markerID);
	}

	public static int arwRemoveAllMarkers()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwRemoveAllMarkers();
		else return ARNativePlugin.arwRemoveAllMarkers();
	}


	public static bool arwQueryMarkerVisibility(int markerID)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwQueryMarkerVisibility(markerID);
		else return ARNativePlugin.arwQueryMarkerVisibility(markerID);
	}

	public static bool arwQueryMarkerTransformation(int markerID, float[] matrix)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwQueryMarkerTransformation(markerID, matrix);
		else return ARNativePlugin.arwQueryMarkerTransformation(markerID, matrix);
	}

	public static bool arwQueryMarkerTransformationStereo(int markerID, float[] matrixL, float[] matrixR)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwQueryMarkerTransformationStereo(markerID, matrixL, matrixR);
		else return ARNativePlugin.arwQueryMarkerTransformationStereo(markerID, matrixL, matrixR);
	}
	
	public static bool arwLoadOpticalParams(string optical_param_name, byte[] optical_param_buff, int optical_param_buffLen, out float fovy_p, out float aspect_p, float[] m, float[] p)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) return ARNativePluginStatic.arwLoadOpticalParams(optical_param_name, optical_param_buff, optical_param_buffLen, out fovy_p, out aspect_p, m, p);
		else return ARNativePlugin.arwLoadOpticalParams(optical_param_name, optical_param_buff, optical_param_buffLen, out fovy_p, out aspect_p, m, p);
	}

}
