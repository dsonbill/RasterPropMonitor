/*****************************************************************************
 * RasterPropMonitor
 * =================
 * Plugin for Kerbal Space Program
 *
 *  by Mihara (Eugene Medvedev), MOARdV, and other contributors
 * 
 * RasterPropMonitor is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, revision
 * date 29 June 2007, or (at your option) any later version.
 * 
 * RasterPropMonitor is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
 * for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with RasterPropMonitor.  If not, see <http://www.gnu.org/licenses/>.
 ****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace JSI
{
    public static class MapIcons
    {
        public enum OtherIcon
        {
            None,
            PE,
            AP,
            AN,
            DN,
            NODE,
            SHIPATINTERCEPT,
            TGTATINTERCEPT,
            ENTERSOI,
            EXITSOI,
            PLANET,
        }

        public static Rect VesselTypeIcon(VesselType type, OtherIcon icon)
        {
            int x = 0;
            int y = 0;
            const float symbolSpan = 0.2f;
            if (icon != OtherIcon.None)
            {
                switch (icon)
                {
                    case OtherIcon.AP:
                        x = 1;
                        y = 4;
                        break;
                    case OtherIcon.PE:
                        x = 0;
                        y = 4;
                        break;
                    case OtherIcon.AN:
                        x = 2;
                        y = 4;
                        break;
                    case OtherIcon.DN:
                        x = 3;
                        y = 4;
                        break;
                    case OtherIcon.NODE:
                        x = 2;
                        y = 1;
                        break;
                    case OtherIcon.SHIPATINTERCEPT:
                        x = 0;
                        y = 1;
                        break;
                    case OtherIcon.TGTATINTERCEPT:
                        x = 1;
                        y = 1;
                        break;
                    case OtherIcon.ENTERSOI:
                        x = 0;
                        y = 2;
                        break;
                    case OtherIcon.EXITSOI:
                        x = 1;
                        y = 2;
                        break;
                    case OtherIcon.PLANET:
                        // Not sure if it is (2,3) or (3,2) - both are round
                        x = 2;
                        y = 3;
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case VesselType.Base:
                        x = 2;
                        y = 0;
                        break;
                    case VesselType.Debris:
                        x = 1;
                        y = 3;
                        break;
                    case VesselType.EVA:
                        x = 2;
                        y = 2;
                        break;
                    case VesselType.Flag:
                        x = 4;
                        y = 0;
                        break;
                    case VesselType.Lander:
                        x = 3;
                        y = 0;
                        break;
                    case VesselType.Probe:
                        x = 1;
                        y = 0;
                        break;
                    case VesselType.Rover:
                        x = 0;
                        y = 0;
                        break;
                    case VesselType.Ship:
                        x = 0;
                        y = 3;
                        break;
                    case VesselType.Station:
                        x = 3;
                        y = 1;
                        break;
                    case VesselType.Unknown:
                        x = 3;
                        y = 3;
                        break;
                    case VesselType.SpaceObject:
                        x = 4;
                        y = 1;
                        break;
                    default:
                        x = 3;
                        y = 2;
                        break;
                }
            }
            var result = new Rect();
            result.x = symbolSpan * x;
            result.y = symbolSpan * y;
            result.height = result.width = symbolSpan;
            return result;
        }
    }

    public static class GizmoIcons
    {
        public enum IconType
        {
            PROGRADE,
            RETROGRADE,
            MANEUVERPLUS,
            MANEUVERMINUS,
            TARGETPLUS,
            TARGETMINUS,
            NORMALPLUS,
            NORMALMINUS,
            RADIALPLUS,
            RADIALMINUS,
        };

        public static Rect GetIconLocation(IconType type)
        {
            Rect loc = new Rect(0.0f, 0.0f, 1.0f / 3.0f, 1.0f / 3.0f);
            switch (type)
            {
                case IconType.PROGRADE:
                    loc.x = 0.0f / 3.0f;
                    loc.y = 2.0f / 3.0f;
                    break;
                case IconType.RETROGRADE:
                    loc.x = 1.0f / 3.0f;
                    loc.y = 2.0f / 3.0f;
                    break;
                case IconType.MANEUVERPLUS:
                    loc.x = 2.0f / 3.0f;
                    loc.y = 0.0f / 3.0f;
                    break;
                case IconType.MANEUVERMINUS:
                    loc.x = 1.0f / 3.0f;
                    loc.y = 2.0f / 3.0f;
                    break;
                case IconType.TARGETPLUS:
                    loc.x = 2.0f / 3.0f;
                    loc.y = 2.0f / 3.0f;
                    break;
                case IconType.TARGETMINUS:
                    loc.x = 2.0f / 3.0f;
                    loc.y = 1.0f / 3.0f;
                    break;
                case IconType.NORMALPLUS:
                    loc.x = 0.0f / 3.0f;
                    loc.y = 0.0f / 3.0f;
                    break;
                case IconType.NORMALMINUS:
                    loc.x = 1.0f / 3.0f;
                    loc.y = 0.0f / 3.0f;
                    break;
                case IconType.RADIALPLUS:
                    loc.x = 1.0f / 3.0f;
                    loc.y = 1.0f / 3.0f;
                    break;
                case IconType.RADIALMINUS:
                    loc.x = 0.0f / 3.0f;
                    loc.y = 1.0f / 3.0f;
                    break;
            }

            return loc;
        }
    }

    public static class JUtil
    {
        public static readonly string[] VariableListSeparator = { "$&$" };
        public static readonly string[] VariableSeparator = { };
        public static readonly string[] LineSeparator = { Environment.NewLine };
        private static readonly int ClosestApproachRefinementInterval = 16;
        internal static Dictionary<string, Shader> parsedShaders = new Dictionary<string, Shader>();
        internal static Dictionary<string, Font> loadedFonts = new Dictionary<string, Font>();
        internal static Dictionary<string, Color32> globalColors = new Dictionary<string, Color32>();

        internal static GameObject CreateSimplePlane(string name, float vectorSize, int drawingLayer)
        {
            return CreateSimplePlane(name, new Vector2(vectorSize, vectorSize), new Rect(0.0f, 0.0f, 1.0f, 1.0f), drawingLayer);
        }

        internal static GameObject CreateSimplePlane(string name, Vector2 vectorSize, Rect textureCoords, int drawingLayer)
        {
            var mesh = new Mesh();

            var obj = new GameObject(name);
            MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();

            mesh.vertices = new[] 
            {
                new Vector3(-vectorSize.x, -vectorSize.y, 0.0f), 
                new Vector3(vectorSize.x, -vectorSize.y, 0.0f), 
                new Vector3(-vectorSize.x, vectorSize.y, 0.0f),
                new Vector3(vectorSize.x, vectorSize.y, 0.0f)
            };

            mesh.uv = new[] 
            {
                new Vector2(textureCoords.xMin, textureCoords.yMin), 
                new Vector2(textureCoords.xMax, textureCoords.yMin), 
                new Vector2(textureCoords.xMin, textureCoords.yMax),
                new Vector2(textureCoords.xMax, textureCoords.yMax)
            };

            mesh.triangles = new[] 
            {
                1, 0, 2,
                3, 1, 2
            };

            mesh.RecalculateBounds();
            mesh.Optimize();

            meshFilter.mesh = mesh;

            obj.layer = drawingLayer;

            UnityEngine.Object.Destroy(obj.GetComponent<Collider>());

            return obj;
        }

        internal static Shader LoadInternalShader(string shaderName)
        {
            if (!parsedShaders.ContainsKey(shaderName))
            {
                JUtil.LogErrorMessage(null, "Failed to find shader {0}", shaderName);
                return null;
            }
            else
            {
                return parsedShaders[shaderName];
            }
        }

        /// <summary>
        /// Parse a config file color string into a Color32.  The colorString
        /// parameter is a sequnce of R, G, B, A (ranging [0,255]), or it is a
        /// string prefixed with "COLOR_".  In the latter case, we'll look up
        /// the color from config files specified in the parent part's
        /// RasterPropMonitorComputer module, or from a globally-defined color
        /// table.
        /// </summary>
        /// <param name="colorString">The color string to parse.</param>
        /// <param name="part">The part containing the prop that is asking for
        /// the color parsing.</param>
        /// <param name="rpmComp">The rpmComp for the specified part; if null,
        /// ParseColor32 looks up the RPMC module.</param>
        /// <returns>Color32; white if colorString is empty, obnoxious magenta
        /// if an unknown COLOR_ string is provided.</returns>
        internal static Color32 ParseColor32(string colorString, Part part, ref RasterPropMonitorComputer rpmComp)
        {
            if (string.IsNullOrEmpty(colorString))
            {
                return Color.white;
            }

            colorString = colorString.Trim();
            if (colorString.StartsWith("COLOR_"))
            {
                if (part != null)
                {
                    if (rpmComp == null)
                    {
                        rpmComp = RasterPropMonitorComputer.Instantiate(part, false);
                    }

                    if (rpmComp != null)
                    {
                        if (rpmComp.overrideColors.ContainsKey(colorString))
                        {
                            return rpmComp.overrideColors[colorString];
                        }
                    }
                }

                if (globalColors.ContainsKey(colorString))
                {
                    return globalColors[colorString];
                }
                else
                {
                    JUtil.LogErrorMessage(null, "Unrecognized color '{0}' in ParseColor32", colorString);
                    return new Color32(255, 0, 255, 255);
                }
            }
            else
            {
                return ConfigNode.ParseColor32(colorString);
            }
        }

        internal static void ShowHide(bool status, params GameObject[] objects)
        {
            for (int i = 0; i < objects.Length; ++i)
            {
                if (objects[i] != null)
                {
                    objects[i].SetActive(status);
                    Renderer renderer = null;
                    objects[i].GetComponentCached<Renderer>(ref renderer);
                    if (renderer != null)
                    {
                        renderer.enabled = status;
                    }
                }
            }
        }

        public static Vector3d SwizzleXZY(this Vector3d vector)
        {
            return new Vector3d(vector.x, vector.z, vector.y);
        }

        public static void MakeReferencePart(this Part thatPart)
        {
            if (thatPart != null)
            {
                foreach (PartModule thatModule in thatPart.Modules)
                {
                    var thatNode = thatModule as ModuleDockingNode;
                    var thatPod = thatModule as ModuleCommand;
                    var thatClaw = thatModule as ModuleGrappleNode;
                    if (thatNode != null)
                    {
                        thatNode.MakeReferenceTransform();
                        break;
                    }
                    if (thatPod != null)
                    {
                        thatPod.MakeReference();
                        break;
                    }
                    if (thatClaw != null)
                    {
                        thatClaw.MakeReferenceTransform();
                    }
                }
            }
        }
        /* I wonder why this isn't working. 
         * It's like the moment I unseat a kerbal, no matter what else I do,
         * the entire internal goes poof. Although I'm pretty sure it doesn't quite,
         * because the modules keep working and generating errors.
         * What's really going on here, and why the same thing works for Crew Manifest?
        public static void ReseatKerbalInPart(this Kerbal thatKerbal) {
            if (thatKerbal.InPart == null || !JUtil.VesselIsInIVA(thatKerbal.InPart.vessel))
                return;

            InternalModel thatModel = thatKerbal.InPart.internalModel;
            Part thatPart = thatKerbal.InPart;
            int spareSeat = thatModel.GetNextAvailableSeatIndex();
            if (spareSeat >= 0) {
                ProtoCrewMember crew = thatKerbal.protoCrewMember;
                CameraManager.Instance.SetCameraFlight();
                thatPart.internalModel.UnseatKerbal(crew);
                thatPart.internalModel.SitKerbalAt(crew,thatPart.internalModel.seats[spareSeat]);
                thatPart.internalModel.part.vessel.SpawnCrew();
                CameraManager.Instance.SetCameraIVA(thatPart.internalModel.seats[spareSeat].kerbalRef,true);
            }
        }
        */

        /// <summary>
        /// Returns true if the active Kerbal is in the specified part.
        /// </summary>
        /// <param name="thisPart"></param>
        /// <returns></returns>
        public static bool ActiveKerbalIsLocal(this Part thisPart)
        {
            Kerbal thatKerbal = CameraManager.Instance.IVACameraActiveKerbal;
            if (thatKerbal != null)
            {
                return thatKerbal.InPart == thisPart;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the index of the active seat in the current part, or -1 if
        /// there is none.
        /// </summary>
        /// <param name="thisPart"></param>
        /// <returns></returns>
        public static int CurrentActiveSeat(this Part thisPart)
        {
            Kerbal activeKerbal = CameraManager.Instance.IVACameraActiveKerbal;
            if (activeKerbal != null)
            {
                return (activeKerbal.InPart == thisPart) ? activeKerbal.protoCrewMember.seatIdx : -1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Return a reference to the active kerbal in the current part.
        /// </summary>
        /// <param name="thisPart"></param>
        /// <returns></returns>
        public static Kerbal FindCurrentKerbal(this Part thisPart)
        {
            Kerbal activeKerbal = CameraManager.Instance.IVACameraActiveKerbal;
            if (activeKerbal != null)
            {
                return (activeKerbal.InPart == thisPart) ? activeKerbal : null;
            }
            else
            {
                return null;
            }
        }

        public static void HideShowProp(InternalProp thatProp, bool visibility)
        {
            foreach (Renderer thatRenderer in thatProp.FindModelComponents<MeshRenderer>())
            {
                thatRenderer.enabled = visibility;
            }
            foreach (Renderer thatRenderer in thatProp.FindModelComponents<SkinnedMeshRenderer>())
            {
                thatRenderer.enabled = visibility;
            }
        }

        public static Camera GetCameraByName(string name)
        {
            for (int i = 0; i < Camera.allCamerasCount; ++i)
            {
                if (Camera.allCameras[i].name == name)
                {
                    return Camera.allCameras[i];
                }
            }
            return null;
        }

        public static void RemoveAllNodes(PatchedConicSolver solver)
        {
            // patchedConicSolver can be null in early career mode.
            if (solver != null)
            {
                while (solver.maneuverNodes.Count > 0)
                {
                    solver.maneuverNodes.Last().RemoveSelf();
                }
            }
        }

        internal static void DisposeOfGameObjects(GameObject[] objs)
        {
            for (int i = 0; i < objs.Length; ++i)
            {
                if (objs[i] != null)
                {
                    MeshFilter meshFilter = objs[i].GetComponent<MeshFilter>();
                    if (meshFilter != null)
                    {
                        UnityEngine.Object.Destroy(meshFilter.mesh);
                        UnityEngine.Object.Destroy(meshFilter);
                    }
                    UnityEngine.Object.Destroy(objs[i].GetComponent<Renderer>().material);
                    UnityEngine.Object.Destroy(objs[i]);
                }
            }
        }

        internal static bool DoesCameraExist(string name)
        {
            for (int i = 0; i < Camera.allCamerasCount; ++i)
            {
                if (Camera.allCameras[i].name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static Material DrawLineMaterial()
        {
            var lineMaterial = new Material(LoadInternalShader("RPM/FontShader"));
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
            return lineMaterial;
        }

        public static Texture2D GetGizmoTexture()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                // This clever method at getting at the stock texture asset originates in Enhanced Navball.
                ManeuverGizmo maneuverGizmo = MapView.ManeuverNodePrefab.GetComponent<ManeuverGizmo>();
                ManeuverGizmoHandle maneuverGizmoHandle = maneuverGizmo.handleNormal;
                Transform gizmoTransform = maneuverGizmoHandle.flag;
                Renderer gizmoRenderer = gizmoTransform.GetComponent<Renderer>();
                return (Texture2D)gizmoRenderer.sharedMaterial.mainTexture;
            }

            return null;
        }

        public static void AnnoyUser(object caller)
        {
            LogErrorMessage(caller, "INITIALIZATION ERROR, CHECK CONFIGURATION.");
            ScreenMessages.PostScreenMessage(string.Format("{0}: INITIALIZATION ERROR, CHECK CONFIGURATION.", caller.GetType().Name), 120, ScreenMessageStyle.UPPER_CENTER);
        }

        /// <summary>
        /// Utility function to find a JSIFlashModule configured for a particular
        /// refresh rate on a given part, and to create one if it doesn't already
        /// exist.
        /// </summary>
        /// <param name="part">The part where the module will be installed</param>
        /// <param name="flashRate">The flash rate for the module.</param>
        /// <returns></returns>
        public static JSIFlashModule InstallFlashModule(Part part, float flashRate)
        {
            JSIFlashModule[] loadedModules = part.GetComponents<JSIFlashModule>();
            for (int i = 0; i < loadedModules.Length; ++i)
            {
                if (loadedModules[i].flashRate == flashRate)
                {
                    return loadedModules[i];
                }
            }

            JSIFlashModule newModule = part.AddModule("JSIFlashModule") as JSIFlashModule;
            newModule.flashRate = flashRate;

            return newModule;
        }

        /// <summary>
        /// Try to figure out which part on the craft is the current part.
        /// </summary>
        /// <returns></returns>
        public static Part DeduceCurrentPart(Vessel vessel)
        {
            Part currentPart = null;

            if (JUtil.VesselIsInIVA(vessel))
            {
                Kerbal thatKerbal = CameraManager.Instance.IVACameraActiveKerbal;
                if (thatKerbal != null)
                {
                    // This should be a drastically faster way to determine
                    // where we are.  I hope.
                    currentPart = thatKerbal.InPart;
                }

                if (currentPart == null)
                {
                    Transform internalCameraTransform = InternalCamera.Instance.transform;
                    foreach (Part thisPart in InternalModelParts(vessel))
                    {
                        for (int seatIdx = 0; seatIdx < thisPart.internalModel.seats.Count; ++seatIdx)
                        {
                            if (thisPart.internalModel.seats[seatIdx].kerbalRef != null)
                            {
                                if (thisPart.internalModel.seats[seatIdx].kerbalRef.eyeTransform == internalCameraTransform.parent)
                                {
                                    currentPart = thisPart;
                                    break;
                                }
                            }
                        }

                        if (CameraManager.Instance.currentCameraMode == CameraManager.CameraMode.Internal)
                        {
                            Transform[] modelTransforms = thisPart.internalModel.GetComponentsInChildren<Transform>();
                            for (int xformIdx = 0; xformIdx < modelTransforms.Length; ++xformIdx)
                            {
                                if (modelTransforms[xformIdx] == InternalCamera.Instance.transform.parent)
                                {
                                    currentPart = thisPart;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return currentPart;
        }

        /// <summary>
        /// Iterate over the parts of a vessel and return only those that
        /// contain an internal model that has been instantiated.
        /// </summary>
        /// <param name="vessel"></param>
        /// <returns></returns>
        private static IEnumerable<Part> InternalModelParts(Vessel vessel)
        {
            for (int i = 0; i < vessel.parts.Count; ++i)
            {
                if (vessel.parts[i].internalModel != null)
                {
                    yield return vessel.parts[i];
                }
            }
        }

        public static bool RasterPropMonitorShouldUpdate(Vessel thatVessel)
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (IsActiveVessel(thatVessel))
                {
                    return (IsInIVA() || StockOverlayCamIsOn());
                }
                else
                {
                    // TODO: Under what circumstances would I set this to true?
                    // Since the computer module is a VesselModule, it's a per-
                    // craft module, so I think it's safe to update other pods
                    // while StockOverlayCamIsOn is true.
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the vessel is the current vessel, we're in flight,
        /// and we're in IVA.
        /// </summary>
        /// <param name="thatVessel"></param>
        /// <returns></returns>
        public static bool VesselIsInIVA(Vessel thatVessel)
        {
            // Inactive IVAs are renderer.enabled = false, this can and should be used...
            // ... but now it can't because we're doing transparent pods, so we need a more complicated way to find which pod the player is in.
            return HighLogic.LoadedSceneIsFlight && IsActiveVessel(thatVessel) && IsInIVA();
        }

        public static bool StockOverlayCamIsOn()
        {
            Camera stockOverlayCamera = JUtil.GetCameraByName("InternalSpaceOverlay Host");

            return (stockOverlayCamera != null);
        }

        public static bool UserIsInPod(Part thisPart)
        {

            // Just in case, check for whether we're not in flight.
            if (!HighLogic.LoadedSceneIsFlight)
            {
                return false;
            }

            // If we're not in IVA, or the part does not have an instantiated IVA, the user can't be in it.
            if (thisPart.internalModel == null || !VesselIsInIVA(thisPart.vessel))
            {
                return false;
            }

            // Now that we got that out of the way, we know that the user is in SOME pod on our ship. We just don't know which.
            // Let's see if he's controlling a kerbal in our pod.
            if (ActiveKerbalIsLocal(thisPart))
            {
                return true;
            }

            // There still remains an option of InternalCamera which we will now sort out.
            if (CameraManager.Instance.currentCameraMode == CameraManager.CameraMode.Internal)
            {
                // So we're watching through an InternalCamera. Which doesn't record which pod we're in anywhere, like with kerbals.
                // But we know that if the camera's transform parent is somewhere in our pod, it's us.
                // InternalCamera.Instance.transform.parent is the transform the camera is attached to that is on either a prop or the internal itself.
                // The problem is figuring out if it's in our pod, or in an identical other pod.
                // Unfortunately I don't have anything smarter right now than get a list of all transforms in the internal and cycle through it.
                // This is a more annoying computation than looking through every kerbal in a pod (there's only a few of those,
                // but potentially hundreds of transforms) and might not even be working as I expect. It needs testing.
                Transform[] componentTransforms = thisPart.internalModel.GetComponentsInChildren<Transform>();
                foreach (Transform thisTransform in componentTransforms)
                {
                    if (thisTransform == InternalCamera.Instance.transform.parent)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsActiveVessel(Vessel thatVessel)
        {
            return (HighLogic.LoadedSceneIsFlight && thatVessel != null && thatVessel.isActiveVessel);
        }

        public static bool IsInIVA()
        {
            return (CameraManager.Instance != null && (CameraManager.Instance.currentCameraMode == CameraManager.CameraMode.IVA || CameraManager.Instance.currentCameraMode == CameraManager.CameraMode.Internal));
        }

        // LogMessage, but unconditional (logs regardless of debugLoggingEnabled state).
        public static void LogInfo(object caller, string line, params object[] list)
        {
            if (caller != null)
            {
                Debug.Log(String.Format("[" + caller.GetType().Name + "]: " + line, list));
            }
            else
            {
                Debug.Log(String.Format("[RasterPropMonitor]: " + line, list));
            }
        }

        public static void LogMessage(object caller, string line, params object[] list)
        {
            if (RPMGlobals.debugLoggingEnabled)
            {
                string callerName = (caller != null) ? caller.GetType().Name : "RasterPropMonitor";

                if (RPMGlobals.debugShowOnly.Count == 0 || RPMGlobals.debugShowOnly.Contains(callerName))
                {
                    Debug.Log(String.Format("[" + callerName + "]: " + line, list));
                }
            }
        }

        public static void LogErrorMessage(object caller, string line, params object[] list)
        {
            if (caller != null)
            {
                Debug.LogError(String.Format("[" + caller.GetType().Name + "]: " + line, list));
            }
            else
            {
                Debug.LogError(String.Format("[RasterPropMonitor]: " + line, list));
            }
        }

        // Working in a generic to make that a generic function for all numbers is too much work
        public static float DualLerp(Vector2 destRange, Vector2 sourceRange, float value)
        {
            return DualLerp(destRange.x, destRange.y, sourceRange.x, sourceRange.y, value);
        }

        public static float DualLerp(float destMin, float destMax, float sourceMin, float sourceMax, float value)
        {
            if (sourceMin < sourceMax)
            {
                if (value < sourceMin)
                    value = sourceMin;
                else if (value > sourceMax)
                    value = sourceMax;
            }
            else
            {
                if (value < sourceMax)
                    value = sourceMax;
                else if (value > sourceMin)
                    value = sourceMin;
            }
            return (destMax - destMin) * ((value - sourceMin) / (sourceMax - sourceMin)) + destMin;
        }

        public static double DualLerp(double destMin, double destMax, double sourceMin, double sourceMax, double value)
        {
            if (sourceMin < sourceMax)
            {
                if (value < sourceMin)
                    value = sourceMin;
                else if (value > sourceMax)
                    value = sourceMax;
            }
            else
            {
                if (value < sourceMax)
                    value = sourceMax;
                else if (value > sourceMin)
                    value = sourceMin;
            }
            return (destMax - destMin) * ((value - sourceMin) / (sourceMax - sourceMin)) + destMin;
        }

        /// <summary>
        /// Convert a variable to a log10-like value (log10 for values > 1,
        /// pass-through for values [-1, 1], and -log10(abs(value)) for values
        /// values less than -1.  Useful for logarithmic VSI and altitude strips.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double PseudoLog10(double value)
        {
            if (Math.Abs(value) <= 1.0)
            {
                return value;
            }
            return (1.0 + Math.Log10(Math.Abs(value))) * Math.Sign(value);
        }

        /// <summary>
        /// ibid, just using a float
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float PseudoLog10(float value)
        {
            if (Mathf.Abs(value) <= 1.0f)
            {
                return value;
            }
            return (1.0f + Mathf.Log10(Mathf.Abs(value))) * Mathf.Sign(value);
        }

        public static string LoadPageDefinition(string pageDefinition)
        {
            try
            {
                return string.Join(Environment.NewLine, File.ReadAllLines(KSPUtil.ApplicationRootPath + "GameData/" + pageDefinition.EnforceSlashes(), Encoding.UTF8));
            }
            catch
            {
                return pageDefinition.UnMangleConfigText();
            }
        }

        /// <summary>
        /// Translate a Color32 to a color tag [#rrggbbaa].
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToColorTag(Color32 color)
        {
            var result = new StringBuilder();
            result.Append("[");
            result.Append(XKCDColors.ColorTranslator.ToHexA(color));
            result.Append("]");
            return result.ToString();
        }

        public static bool OrbitMakesSense(Vessel thatVessel)
        {
            if (thatVessel == null)
                return false;
            if (thatVessel.situation == Vessel.Situations.FLYING ||
                thatVessel.situation == Vessel.Situations.SUB_ORBITAL ||
                thatVessel.situation == Vessel.Situations.ORBITING ||
                thatVessel.situation == Vessel.Situations.ESCAPING ||
                thatVessel.situation == Vessel.Situations.DOCKED) // Not sure about this last one.
                return true;
            return false;
        }

        public static FXGroup SetupIVASound(InternalProp thatProp, string buttonClickSound, float buttonClickVolume, bool loopState)
        {
            FXGroup audioOutput = null;
            if (!string.IsNullOrEmpty(buttonClickSound.EnforceSlashes()))
            {
                audioOutput = new FXGroup("RPM" + thatProp.propID);
                audioOutput.audio = thatProp.gameObject.AddComponent<AudioSource>();
                audioOutput.audio.clip = GameDatabase.Instance.GetAudioClip(buttonClickSound);
                audioOutput.audio.Stop();
                audioOutput.audio.volume = GameSettings.SHIP_VOLUME * buttonClickVolume;
                audioOutput.audio.rolloffMode = AudioRolloffMode.Logarithmic;
                audioOutput.audio.maxDistance = 10f;
                audioOutput.audio.minDistance = 2f;
                audioOutput.audio.dopplerLevel = 0f;
                audioOutput.audio.panStereo = 0f;
                audioOutput.audio.playOnAwake = false;
                audioOutput.audio.loop = loopState;
                audioOutput.audio.pitch = 1f;
            }
            return audioOutput;
        }

        public static string WordWrap(string text, int maxLineLength)
        {
            var sb = new StringBuilder();
            char[] prc = { ' ', ',', '.', '?', '!', ':', ';', '-' };
            char[] ws = { ' ' };

            foreach (string line in text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                int currentIndex;
                int lastWrap = 0;
                do
                {
                    currentIndex = lastWrap + maxLineLength > line.Length ? line.Length : (line.LastIndexOfAny(prc, Math.Min(line.Length - 1, lastWrap + maxLineLength)) + 1);
                    if (currentIndex <= lastWrap)
                        currentIndex = Math.Min(lastWrap + maxLineLength, line.Length);
                    sb.AppendLine(line.Substring(lastWrap, currentIndex - lastWrap).Trim(ws));
                    lastWrap = currentIndex;
                } while (currentIndex < line.Length);
            }
            return sb.ToString();
        }

        public static Vector3d ProjectPositionOntoSurface(this Vessel vessel)
        {
            Vector3d coM = vessel.CoMD;

            double latitude = vessel.mainBody.GetLatitude(coM);
            double longitude = vessel.mainBody.GetLongitude(coM);

            double groundHeight;
            if (vessel.mainBody.pqsController != null)
            {
                groundHeight = vessel.mainBody.pqsController.GetSurfaceHeight(QuaternionD.AngleAxis(longitude, Vector3d.down) *
                QuaternionD.AngleAxis(latitude, Vector3d.forward) *
                Vector3d.right) - vessel.mainBody.pqsController.radius;
            }
            else
            {
                // No pqsController
                groundHeight = 0.0;
            }

            return vessel.mainBody.GetWorldSurfacePosition(latitude, longitude, groundHeight);
        }

        public static Vector3d ProjectPositionOntoSurface(this ITargetable target, CelestialBody vesselBody)
        {
            Vector3d position = target.GetTransform().position;
            CelestialBody body = null;
            if (target.GetVessel() != null)
            {
                body = target.GetVessel().mainBody;

                if (body != vesselBody)
                {
                    return Vector3d.zero;
                }
            }
            else if (vesselBody != null)
            {
                body = vesselBody;
            }

            if (body == null)
            {
                return Vector3d.zero;
            }

            double latitude = body.GetLatitude(position);
            double longitude = body.GetLongitude(position);
            // handy!
            double groundHeight = FinePrint.Utilities.CelestialUtilities.TerrainAltitude(body, latitude, longitude);

            return body.GetWorldSurfacePosition(latitude, longitude, groundHeight);
        }

        // Some snippets from MechJeb...
        public static double ClampDegrees360(double angle)
        {
            angle = angle % 360.0;
            if (angle < 0)
                return angle + 360.0;
            return angle;
        }
        //keeps angles in the range -180 to 180
        public static double ClampDegrees180(double angle)
        {
            angle = ClampDegrees360(angle);
            if (angle > 180)
                angle -= 360;
            return angle;
        }

        public static double ClampRadiansTwoPi(double angle)
        {
            angle = angle % (2 * Math.PI);
            if (angle < 0)
                return angle + 2 * Math.PI;
            return angle;
        }
        //acosh(x) = log(x + sqrt(x^2 - 1))
        public static double Acosh(double x)
        {
            return Math.Log(x + Math.Sqrt(x * x - 1));
        }

        public static double NormalAngle(Vector3 a, Vector3 b, Vector3 up)
        {
            return SignedAngle(Vector3.Cross(up, a), Vector3.Cross(up, b), up);
        }

        public static double SignedAngle(Vector3 v1, Vector3 v2, Vector3 up)
        {
            return Vector3.Dot(Vector3.Cross(v1, v2), up) < 0 ? -Vector3.Angle(v1, v2) : Vector3.Angle(v1, v2);
        }

        public static Orbit OrbitFromStateVectors(Vector3d pos, Vector3d vel, CelestialBody body, double UT)
        {
            Orbit ret = new Orbit();
            ret.UpdateFromStateVectors((pos - body.position).xzy, vel.xzy, body, UT);
            return ret;
        }

        // Another MechJeb import.
        public static string CurrentBiome(this Vessel thatVessel)
        {
            if (!string.IsNullOrEmpty(thatVessel.landedAt))
            {
                return thatVessel.landedAt;
            }
            try
            {
                string biome = thatVessel.mainBody.BiomeMap.GetAtt(thatVessel.latitude * Math.PI / 180d, thatVessel.longitude * Math.PI / 180d).name;
                switch (thatVessel.situation)
                {
                    //ExperimentSituations.SrfLanded
                    case Vessel.Situations.LANDED:
                    case Vessel.Situations.PRELAUNCH:
                        return thatVessel.mainBody.theName + "'s " + (biome == "" ? "surface" : biome);
                    //ExperimentSituations.SrfSplashed
                    case Vessel.Situations.SPLASHED:
                        return thatVessel.mainBody.theName + "'s " + (biome == "" ? "oceans" : biome);
                    case Vessel.Situations.FLYING:
                        if (thatVessel.altitude < thatVessel.mainBody.scienceValues.flyingAltitudeThreshold)
                        {
                            //ExperimentSituations.FlyingLow
                            return "Flying over " + thatVessel.mainBody.theName + (biome == "" ? "" : "'s " + biome);
                        }
                        //ExperimentSituations.FlyingHigh
                        return "Upper atmosphere of " + thatVessel.mainBody.theName + (biome == "" ? "" : "'s " + biome);
                    default:
                        if (thatVessel.altitude < thatVessel.mainBody.scienceValues.spaceAltitudeThreshold)
                        {
                            //ExperimentSituations.InSpaceLow
                            return "Space just above " + thatVessel.mainBody.theName;
                        }
                        // ExperimentSituations.InSpaceHigh
                        return "Space high over " + thatVessel.mainBody.theName;
                }
            }
            finally { }

            return "Space over " + thatVessel.mainBody.theName;
        }


        // Pseudo-orbit for closest approach to a landed object
        public static Orbit OrbitFromSurfacePos(CelestialBody body, double lat, double lon, double alt, double UT)
        {
            double t0 = Planetarium.GetUniversalTime();
            double angle = body.rotates ? (UT - t0) * 360.0 / body.rotationPeriod : 0;

            double LAN = (lon + body.rotationAngle + angle - 90.0) % 360.0;
            Orbit orbit = new Orbit(lat, 0, body.Radius + alt, LAN, 90.0, 0, UT, body);

            orbit.pos = orbit.getRelativePositionAtT(0);
            if (body.rotates)
                orbit.vel = Vector3d.Cross(body.zUpAngularVelocity, -orbit.pos);
            else
                orbit.vel = orbit.getOrbitalVelocityAtObT(Time.fixedDeltaTime);
            orbit.h = Vector3d.Cross(orbit.pos, orbit.vel);

            orbit.StartUT = t0;
            orbit.EndUT = UT + orbit.period;
            if (body.rotates)
                orbit.period = body.rotationPeriod;
            orbit.patchEndTransition = Orbit.PatchTransitionType.FINAL;
            return orbit;
        }

        public static Orbit ClosestApproachSrfOrbit(Orbit vesselOrbit, Vessel target, out double UT, out double distance)
        {
            return ClosestApproachSrfOrbit(vesselOrbit, target.mainBody, target.latitude, target.longitude, target.altitude, out UT, out distance);
        }

        public static Orbit ClosestApproachSrfOrbit(Orbit vesselOrbit, CelestialBody body, double lat, double lon, double alt, out double UT, out double distance)
        {
            Vector3d pos = body.GetRelSurfacePosition(lat, lon, alt);
            distance = GetClosestApproach(vesselOrbit, body, pos, out UT);
            return OrbitFromSurfacePos(body, lat, lon, alt, UT);
        }

        public static double GetClosestApproach(Orbit vesselOrbit, ITargetable target, out double timeAtClosestApproach)
        {
            if (target == null)
            {
                timeAtClosestApproach = -1.0;
                return -1.0;
            }

            if (target is CelestialBody)
            {
                return GetClosestApproach(vesselOrbit, target as CelestialBody, out timeAtClosestApproach);
            }
            else if (target is ModuleDockingNode)
            {
                return GetClosestApproach(vesselOrbit, (target as ModuleDockingNode).GetVessel().GetOrbit(), out timeAtClosestApproach);
            }
            else if (target is Vessel)
            {
                Vessel targetVessel = target as Vessel;
                if (targetVessel.LandedOrSplashed)
                {
                    double closestApproach;
                    Orbit targetOrbit = JUtil.ClosestApproachSrfOrbit(vesselOrbit, targetVessel, out timeAtClosestApproach, out closestApproach);
                    return closestApproach;
                }
                else
                {
                    return JUtil.GetClosestApproach(vesselOrbit, targetVessel.GetOrbit(), out timeAtClosestApproach);
                }
            }
            else
            {
                LogErrorMessage(target, "Unknown / unsupported target type in GetClosestApproach");
                timeAtClosestApproach = -1.0;
                return -1.0;
            }
        }

        // Closest Approach algorithms based on Protractor mod
        private static double GetClosestApproach(Orbit vesselOrbit, CelestialBody targetCelestial, out double timeAtClosestApproach)
        {
            Orbit closestorbit = GetClosestOrbit(vesselOrbit, targetCelestial);
            if (closestorbit.referenceBody == targetCelestial)
            {
                timeAtClosestApproach = closestorbit.StartUT + closestorbit.timeToPe;
                return closestorbit.PeA;
            }
            if (closestorbit.referenceBody == targetCelestial.referenceBody)
            {
                return MinTargetDistance(closestorbit, targetCelestial.orbit, closestorbit.StartUT, closestorbit.EndUT, out timeAtClosestApproach) - targetCelestial.Radius;
            }
            return MinTargetDistance(closestorbit, targetCelestial.orbit, Planetarium.GetUniversalTime(), Planetarium.GetUniversalTime() + closestorbit.period, out timeAtClosestApproach) - targetCelestial.Radius;
        }

        private static double GetClosestApproach(Orbit vesselOrbit, CelestialBody targetCelestial, Vector3d srfTarget, out double timeAtClosestApproach)
        {
            Orbit closestorbit = GetClosestOrbit(vesselOrbit, targetCelestial);
            if (closestorbit.referenceBody == targetCelestial)
            {
                double t0 = Planetarium.GetUniversalTime();
                Func<double, Vector3d> fn = delegate(double t)
                {
                    double angle = targetCelestial.rotates ? (t - t0) * 360.0 / targetCelestial.rotationPeriod : 0;
                    return targetCelestial.position + QuaternionD.AngleAxis(angle, Vector3d.down) * srfTarget;
                };
                double d = MinTargetDistance(closestorbit, fn, closestorbit.StartUT, closestorbit.EndUT, out timeAtClosestApproach);
                // When just passed over the target, some look ahead may be needed
                if ((timeAtClosestApproach <= closestorbit.StartUT || timeAtClosestApproach >= closestorbit.EndUT) &&
                    closestorbit.eccentricity < 1 && closestorbit.patchEndTransition == Orbit.PatchTransitionType.FINAL)
                {
                    d = MinTargetDistance(closestorbit, fn, closestorbit.EndUT, closestorbit.EndUT + closestorbit.period / 2, out timeAtClosestApproach);
                }
                return d;
            }
            return GetClosestApproach(vesselOrbit, targetCelestial, out timeAtClosestApproach);
        }

        public static double GetClosestApproach(Orbit vesselOrbit, Orbit targetOrbit, out double timeAtClosestApproach)
        {
            Orbit closestorbit = GetClosestOrbit(vesselOrbit, targetOrbit);

            double startTime = Planetarium.GetUniversalTime();
            double endTime;
            if (closestorbit.patchEndTransition != Orbit.PatchTransitionType.FINAL)
            {
                endTime = closestorbit.EndUT;
            }
            else
            {
                endTime = startTime + Math.Max(closestorbit.period, targetOrbit.period);
            }

            return MinTargetDistance(closestorbit, targetOrbit, startTime, endTime, out timeAtClosestApproach);
        }

        // Closest Approach support methods
        private static Orbit GetClosestOrbit(Orbit vesselOrbit, CelestialBody targetCelestial)
        {
            Orbit checkorbit = vesselOrbit;
            int orbitcount = 0;

            while (checkorbit.nextPatch != null && checkorbit.patchEndTransition != Orbit.PatchTransitionType.FINAL && orbitcount < 3)
            {
                checkorbit = checkorbit.nextPatch;
                orbitcount += 1;
                if (checkorbit.referenceBody == targetCelestial)
                {
                    return checkorbit;
                }

            }
            checkorbit = vesselOrbit;
            orbitcount = 0;

            while (checkorbit.nextPatch != null && checkorbit.patchEndTransition != Orbit.PatchTransitionType.FINAL && orbitcount < 3)
            {
                checkorbit = checkorbit.nextPatch;
                orbitcount += 1;
                if (checkorbit.referenceBody == targetCelestial.orbit.referenceBody)
                {
                    return checkorbit;
                }
            }

            return vesselOrbit;
        }

        private static Orbit GetClosestOrbit(Orbit vesselOrbit, Orbit targetOrbit)
        {
            Orbit checkorbit = vesselOrbit;
            int orbitcount = 0;

            while (checkorbit.nextPatch != null && checkorbit.patchEndTransition != Orbit.PatchTransitionType.FINAL && orbitcount < 3)
            {
                checkorbit = checkorbit.nextPatch;
                orbitcount += 1;
                if (checkorbit.referenceBody == targetOrbit.referenceBody)
                {
                    return checkorbit;
                }

            }

            return vesselOrbit;
        }

        private static double MinTargetDistance(Orbit vesselOrbit, Orbit targetOrbit, double startTime, double endTime, out double timeAtClosestApproach)
        {
            return MinTargetDistance(vesselOrbit, t => targetOrbit.getPositionAtUT(t), startTime, endTime, out timeAtClosestApproach);
        }

        private static double MinTargetDistance(Orbit vesselOrbit, Func<double, Vector3d> targetOrbit, double startTime, double endTime, out double timeAtClosestApproach)
        {
            var dist_at_int = new double[ClosestApproachRefinementInterval + 1];
            double step = startTime;
            double dt = (endTime - startTime) / (double)ClosestApproachRefinementInterval;
            for (int i = 0; i <= ClosestApproachRefinementInterval; i++)
            {
                dist_at_int[i] = (targetOrbit(step) - vesselOrbit.getPositionAtUT(step)).magnitude;
                step += dt;
            }
            double mindist = dist_at_int.Min();
            double maxdist = dist_at_int.Max();
            int minindex = Array.IndexOf(dist_at_int, mindist);
            if ((maxdist - mindist) / maxdist >= 0.00001)
            {
                // Don't allow negative times.  Clamp the startTime to the current startTime.
                mindist = MinTargetDistance(vesselOrbit, targetOrbit, startTime + (Math.Max(minindex - 1, 0) * dt), startTime + ((minindex + 1) * dt), out timeAtClosestApproach);
            }
            else
            {
                timeAtClosestApproach = startTime + minindex * dt;
            }

            return mindist;
        }

        // Piling all the extension methods into the same utility class to reduce the number of classes.
        // Because DLL size. Not really important and probably a bad practice, but one function static classes are silly.
        public static float? GetFloat(this string source)
        {
            float result;
            return float.TryParse(source, out result) ? result : (float?)null;
        }

        public static float? GetFloat(this ConfigNode node, string valueName)
        {
            return node.HasValue(valueName) ? node.GetValue(valueName).GetFloat() : (float?)null;
        }

        public static int? GetInt(this string source)
        {
            int result;
            return int.TryParse(source, out result) ? result : (int?)null;
        }

        public static int? GetInt(this ConfigNode node, string valueName)
        {
            return node.HasValue(valueName) ? node.GetValue(valueName).GetInt() : (int?)null;
        }

        public static string EnforceSlashes(this string input)
        {
            return input.Replace('\\', '/');
        }

        public static string UnMangleConfigText(this string input)
        {
            return input.Replace("<=", "{").Replace("=>", "}").Replace("$$$", Environment.NewLine);
        }

        public static string MangleConfigText(this string input)
        {
            return input.Replace("{", "<=").Replace("}", "=>").Replace(Environment.NewLine, "$$$");
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
                return min;
            return val.CompareTo(max) > 0 ? max : val;
        }

        /// <summary>
        /// Method to instantiate one of the IComplexVariable objects on an rpmComp.
        /// </summary>
        /// <param name="node">The config node fetched from RPMGlobals</param>
        /// <param name="rpmComp">The RasterPropMonitorComputer that is hosting the variable</param>
        /// <returns>The complex variable, or null</returns>
        internal static IComplexVariable InstantiateComplexVariable(ConfigNode node, RasterPropMonitorComputer rpmComp)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node was null. how did that happen?");
            }

            switch (node.name)
            {
                case "RPM_CUSTOM_VARIABLE":
                    return new CustomVariable(node, rpmComp);
                case "RPM_MAPPED_VARIABLE":
                    return new MappedVariable(node, rpmComp);
                case "RPM_MATH_VARIABLE":
                    return new MathVariable(node, rpmComp);
                case "RPM_SELECT_VARIABLE":
                    return new SelectVariable(node, rpmComp);
            }

            throw new ArgumentException("Unrecognized complex variable " + node.name);
        }

        /// <summary>
        /// Convert a numeric object to a float where.
        /// </summary>
        /// <param name="thatValue"></param>
        /// <returns></returns>
        public static float MassageToFloat(this object thatValue)
        {
            // RPMC only produces doubles, floats, ints, bools, and strings.
            if (thatValue is float)
                return (float)thatValue;
            if (thatValue is double)
                return (float)(double)thatValue;
            if (thatValue is int)
                return (float)(int)thatValue;
            if (thatValue is bool)
                return (float)(((bool)thatValue).GetHashCode());
            return float.NaN;
        }

        /// <summary>
        /// Convert a numeric object to an integer.
        /// </summary>
        /// <param name="thatValue"></param>
        /// <returns></returns>
        public static int MassageToInt(this object thatValue)
        {
            // RPMC only produces doubles, floats, ints, bools, and strings.
            if (thatValue is int)
                return (int)thatValue;
            if (thatValue is double)
                return (int)(double)thatValue;
            if (thatValue is float)
                return (int)(float)thatValue;
            if (thatValue is bool)
                return ((bool)thatValue).GetHashCode();
            return 0;
        }

        /// <summary>
        /// Convert a numeric object to a double.
        /// </summary>
        /// <param name="thatValue"></param>
        /// <returns></returns>
        public static double MassageToDouble(this object thatValue)
        {
            // RPMC only produces doubles, floats, ints, bools, and strings.
            if (thatValue is double)
                return (double)thatValue;
            if (thatValue is float)
                return (double)(float)thatValue;
            if (thatValue is int)
                return (double)(int)thatValue;
            if (thatValue is bool)
                return (double)(((bool)thatValue).GetHashCode());
            return double.NaN;
        }

        //public static bool ReturnFalse()
        //{
        //    return false;
        //}

        internal static Delegate GetMethod(string packedMethod, InternalProp internalProp, Type delegateType)
        {
            string moduleName, stateMethod;
            string[] tokens = packedMethod.Split(':');
            if (tokens.Length != 2)
            {
                JUtil.LogErrorMessage(internalProp, "Bad format on {0}", packedMethod);
                throw new ArgumentException("stateMethod incorrectly formatted");
            }
            moduleName = tokens[0];
            stateMethod = tokens[1];

            InternalModule thatModule = null;
            foreach (InternalModule potentialModule in internalProp.internalModules)
            {
                if (potentialModule.ClassName == moduleName)
                {
                    thatModule = potentialModule;
                    break;
                }
            }

            if (thatModule == null)
            {
                // The module hasn't been instantiated on this part, so let's do so now.
                // MOARdV TODO: This actually causes an exception, because
                // it's added during InternalProp.OnUpdate.  One thing I could
                // do is add the internal modules when I instantiate RPMC.
                var handlerConfiguration = new ConfigNode("MODULE");
                handlerConfiguration.SetValue("name", moduleName, true);
                thatModule = internalProp.AddModule(handlerConfiguration);
            }
            if (thatModule == null)
            {
                JUtil.LogErrorMessage(internalProp, "Failed finding module {0} for method {1}", moduleName, stateMethod);
                return null;
            }

            Type returnType = delegateType.GetMethod("Invoke").ReturnType;
            Delegate stateCall = null;
            foreach (MethodInfo m in thatModule.GetType().GetMethods())
            {
                if (!string.IsNullOrEmpty(stateMethod) && m.Name == stateMethod && m.ReturnParameter.ParameterType == returnType)
                {
                    stateCall = Delegate.CreateDelegate(delegateType, thatModule, m);
                }
            }

            return stateCall;
        }

        private static List<string> knownFonts = null;
        internal static Font LoadFont(string fontName, int size)
        {
            if (loadedFonts.ContainsKey(fontName))
            {
                return loadedFonts[fontName];
            }
            else if (loadedFonts.ContainsKey(fontName + size.ToString()))
            {
                return loadedFonts[fontName + size.ToString()];
            }

            if (knownFonts == null)
            {
                string[] fn = Font.GetOSInstalledFontNames();
                if (fn != null)
                {
                    knownFonts = fn.ToList<string>();
                }
            }

            if (knownFonts.Contains(fontName))
            {
                Font fontFn = Font.CreateDynamicFontFromOSFont(fontName, size);
                loadedFonts.Add(fontName + size.ToString(), fontFn);
                return fontFn;
            }
            else
            {
                // Fallback
                return LoadFont("Arial", size);
            }
        }
    }

    // This, instead, is a static class on it's own because it needs its private static variables.
    public static class InstallationPathWarning
    {
        private static readonly List<string> warnedList = new List<string>();
        private const string gameData = "GameData";
        private static readonly string[] pathSep = { gameData };

        public static bool Warn(string path = "JSI/RasterPropMonitor/Plugins")
        {
            string assemblyPath = Assembly.GetCallingAssembly().Location;
            string fileName = Path.GetFileName(assemblyPath);
            bool wrongpath = false;
            if (!warnedList.Contains(fileName))
            {
                string installedLocation = Path.GetDirectoryName(assemblyPath).Split(pathSep, StringSplitOptions.None)[1].TrimStart('/').TrimStart('\\').EnforceSlashes();
                if (installedLocation != path)
                {
                    ScreenMessages.PostScreenMessage(string.Format("ERROR: {0} must be in GameData/{1} but it's in GameData/{2}", fileName, path, installedLocation),
                        120, ScreenMessageStyle.UPPER_CENTER);
                    Debug.LogError("RasterPropMonitor components are incorrectly installed. I should stop working and make you fix it, but KSP won't let me.");
                    wrongpath = true;
                }
                warnedList.Add(fileName);
            }
            return !wrongpath;
        }
    }

    // This handy class is also from MechJeb.
    //A simple wrapper around a Dictionary, with the only change being that
    //accessing the value of a nonexistent key returns a default value instead of an error.
    class DefaultableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> d = new Dictionary<TKey, TValue>();
        readonly TValue defaultValue;

        public DefaultableDictionary(TValue defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public TValue this[TKey key]
        {
            get
            {
                return d.ContainsKey(key) ? d[key] : defaultValue;
            }
            set
            {
                if (d.ContainsKey(key))
                    d[key] = value;
                else
                    d.Add(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            d.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return d.ContainsKey(key);
        }

        public ICollection<TKey> Keys { get { return d.Keys; } }

        public bool Remove(TKey key)
        {
            return d.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return d.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values { get { return d.Values; } }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)d).Add(item);
        }

        public void Clear()
        {
            d.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)d).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)d).CopyTo(array, arrayIndex);
        }

        public int Count { get { return d.Count; } }

        public bool IsReadOnly { get { return ((IDictionary<TKey, TValue>)d).IsReadOnly; } }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)d).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return d.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)d).GetEnumerator();
        }
    }

    /// <summary>
    /// The RPMShaderLoader is a run-once class that is executed when KSP
    /// reaches the main menu.  Its purpose is to parse rasterpropmonitor.ksp
    /// and fetch the shaders embedded in there.  Those shaders are stored in
    /// a dictionary in JUtil.  In addition, other config assets are parsed
    /// and stored (primarily values found in the RPMVesselComputer).
    /// </summary>
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RPMShaderLoader : MonoBehaviour
    {
        //private bool reloadInProgress = false;
        KSPAssets.AssetDefinition[] rpmShaders;

        RPMShaderLoader()
        {
            // I don't want this object destroyed on scene change, since the database
            // loader coroutine can take a while to run to completion.  Eventually,
            // I may add smarts so database reloads get handled, too.
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Wake up and ask for all of the shaders in our asset bundle and kick off
        /// the coroutines that look for global RPM config data.
        /// </summary>
        private void Awake()
        {
            if (KSPAssets.Loaders.AssetLoader.Ready == false)
            {
                JUtil.LogErrorMessage(this, "Unable to load shaders - AssetLoader is not ready.");
                return;
            }

            rpmShaders = KSPAssets.Loaders.AssetLoader.GetAssetDefinitionsWithType("JSI/RasterPropMonitor/rasterpropmonitor", typeof(Shader));
            if (rpmShaders == null || rpmShaders.Length == 0)
            {
                JUtil.LogErrorMessage(this, "Unable to load shaders - No shaders found in RPM asset bundle.");
                return;
            }

            if (!GameDatabase.Instance.IsReady())
            {
                JUtil.LogErrorMessage(this, "GameDatabase.IsReady is false");
                throw new Exception("RPMShaderLoader: GameDatabase is not ready.  Unable to continue.");
            }

            ConfigNode rpmSettings = ConfigNode.Load(KSPUtil.ApplicationRootPath + RPMGlobals.configFileName);
            // rpmSettings points at the base node.  I need to step into that node to access my settings.
            if (rpmSettings != null && rpmSettings.CountNodes > 0)
            {
                rpmSettings = rpmSettings.GetNode("RasterPropMonitorSettings");
            }
            else
            {
                rpmSettings = null;
            }

            if (rpmSettings != null)
            {
                bool enableLogging = false;
                if (rpmSettings.TryGetValue("DebugLogging", ref enableLogging))
                {
                    RPMGlobals.debugLoggingEnabled = enableLogging;
                    JUtil.LogInfo(this, "Set debugLoggingEnabled to {0}", enableLogging);
                }
                else
                {
                    RPMGlobals.debugLoggingEnabled = false;
                }

                bool showVariableCallCount = false;
                if (rpmSettings.TryGetValue("ShowCallCount", ref showVariableCallCount))
                {
                    // call count doesn't write anything if enableLogging is false
                    RPMGlobals.debugShowVariableCallCount = showVariableCallCount && RPMGlobals.debugLoggingEnabled;
                }
                else
                {
                    RPMGlobals.debugShowVariableCallCount = false;
                }

                int defaultRefresh = RPMGlobals.defaultRefreshRate;
                if (rpmSettings.TryGetValue("DefaultRefreshRate", ref defaultRefresh))
                {
                    RPMGlobals.defaultRefreshRate = Math.Max(defaultRefresh, 1);
                }

                int minRefresh = RPMGlobals.minimumRefreshRate;
                if (rpmSettings.TryGetValue("MinimumRefreshRate", ref minRefresh))
                {
                    RPMGlobals.minimumRefreshRate = Math.Max(minRefresh, 1);
                }

                bool useNewVariableAnimator = false;
                if (rpmSettings.TryGetValue("UseNewVariableAnimator", ref useNewVariableAnimator))
                {
                    RPMGlobals.useNewVariableAnimator = useNewVariableAnimator;
                }
                else
                {
                    RPMGlobals.useNewVariableAnimator = false;
                }

                RPMGlobals.debugShowOnly.Clear();
                string showOnlyConcat = string.Empty;
                if (rpmSettings.TryGetValue("ShowOnly", ref showOnlyConcat) && !string.IsNullOrEmpty(showOnlyConcat))
                {
                    string[] showOnly = showOnlyConcat.Split('|');
                    for (int i = 0; i < showOnly.Length; ++i)
                    {
                        RPMGlobals.debugShowOnly.Add(showOnly[i].Trim());
                    }
                }
            }

            // HACK: Pass only one of the asset definitions, since LoadAssets
            // behaves badly if we ask it to load more than one.  If that ever
            // gets fixed, I can clean up AssetsLoaded drastically.
            KSPAssets.Loaders.AssetLoader.LoadAssets(AssetsLoaded, rpmShaders[0]);

            //reloadInProgress = true;
            StartCoroutine("LoadRasterPropMonitorValues");

            // Register a callback with ModuleManager so we can get notified
            // of database reloads.
            if (!RegisterWithModuleManager())
            {
                JUtil.LogErrorMessage(this, "Unable to register with ModuleManager for database reloads");
            }
        }

        /// <summary>
        /// Coroutine for loading the various custom variables used for variables.
        /// Yield-returns ever 32 or so variables so it's not as costly in a
        /// given frame.  Also loads all the other various values used by RPM.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadRasterPropMonitorValues()
        {
            var bodies = FlightGlobals.Bodies;
            for (int i = 0; i < bodies.Count; ++i)
            {
                JUtil.LogMessage(this, "CelestialBody {0} is index {1}", bodies[i].bodyName, bodies[i].flightGlobalsIndex);
            }

            RPMGlobals.customVariables.Clear();

            ConfigNode[] nodes = GameDatabase.Instance.GetConfigNodes("RPM_CUSTOM_VARIABLE");
            for (int i = 0; i < nodes.Length; ++i)
            {

                try
                {
                    string varName = nodes[i].GetValue("name");

                    if (!string.IsNullOrEmpty(varName))
                    {
                        string completeVarName = "CUSTOM_" + varName;
                        RPMGlobals.customVariables.Add(completeVarName, nodes[i]);
                        JUtil.LogMessage(this, "I know about {0}", completeVarName);
                    }
                }
                catch
                {

                }

                if ((i & 0x1f) == 0x1f)
                {
                    yield return null;
                }
            }

            // And parse known mapped variables
            nodes = GameDatabase.Instance.GetConfigNodes("RPM_MAPPED_VARIABLE");
            for (int i = 0; i < nodes.Length; ++i)
            {
                try
                {
                    string varName = nodes[i].GetValue("mappedVariable");

                    if (!string.IsNullOrEmpty(varName))
                    {
                        string completeVarName = "MAPPED_" + varName;
                        RPMGlobals.customVariables.Add(completeVarName, nodes[i]);
                        JUtil.LogMessage(this, "I know about {0}", completeVarName);
                    }
                }
                catch
                {

                }
                if ((i & 0x1f) == 0x1f)
                {
                    yield return null;
                }
            }

            // And parse known math variables
            nodes = GameDatabase.Instance.GetConfigNodes("RPM_MATH_VARIABLE");
            for (int i = 0; i < nodes.Length; ++i)
            {
                try
                {
                    string varName = nodes[i].GetValue("name");

                    if (!string.IsNullOrEmpty(varName))
                    {
                        string completeVarName = "MATH_" + varName;
                        RPMGlobals.customVariables.Add(completeVarName, nodes[i]);
                        JUtil.LogMessage(this, "I know about {0}", completeVarName);
                    }
                }
                catch
                {

                }
                if ((i & 0x1f) == 0x1f)
                {
                    yield return null;
                }
            }

            // And parse known select variables
            nodes = GameDatabase.Instance.GetConfigNodes("RPM_SELECT_VARIABLE");
            for (int i = 0; i < nodes.Length; ++i)
            {
                try
                {
                    string varName = nodes[i].GetValue("name");

                    if (!string.IsNullOrEmpty(varName))
                    {
                        string completeVarName = "SELECT_" + varName;
                        RPMGlobals.customVariables.Add(completeVarName, nodes[i]);
                        JUtil.LogMessage(this, "I know about {0}", completeVarName);
                    }
                }
                catch
                {

                }
                if ((i & 0x1f) == 0x1f)
                {
                    yield return null;
                }
            }
            yield return null;

            JUtil.globalColors.Clear();
            nodes = GameDatabase.Instance.GetConfigNodes("RPM_GLOBALCOLORSETUP");
            for (int idx = 0; idx < nodes.Length; ++idx)
            {
                ConfigNode[] colorConfig = nodes[idx].GetNodes("COLORDEFINITION");
                for (int defIdx = 0; defIdx < colorConfig.Length; ++defIdx)
                {
                    if (colorConfig[defIdx].HasValue("name") && colorConfig[defIdx].HasValue("color"))
                    {
                        string name = "COLOR_" + (colorConfig[defIdx].GetValue("name").Trim());
                        Color32 color = ConfigNode.ParseColor32(colorConfig[defIdx].GetValue("color").Trim());
                        if (JUtil.globalColors.ContainsKey(name))
                        {
                            JUtil.globalColors[name] = color;
                        }
                        else
                        {
                            JUtil.globalColors.Add(name, color);
                        }
                        JUtil.LogMessage(this, "I know {0} = {1}", name, color);
                    }
                }
            }

            RPMGlobals.triggeredEvents.Clear();
            nodes = GameDatabase.Instance.GetConfigNodes("RPM_TRIGGERED_EVENT");
            for (int idx = 0; idx < nodes.Length; ++idx)
            {
                string eventName = nodes[idx].GetValue("eventName").Trim();

                try
                {
                    RasterPropMonitorComputer.TriggeredEventTemplate triggeredVar = new RasterPropMonitorComputer.TriggeredEventTemplate(nodes[idx]);

                    if (!string.IsNullOrEmpty(eventName) && triggeredVar != null)
                    {
                        RPMGlobals.triggeredEvents.Add(triggeredVar);
                        JUtil.LogMessage(this, "I know about event {0}", eventName);
                    }
                }
                catch (Exception e)
                {
                    JUtil.LogErrorMessage(this, "Error adding triggered event {0}: {1}", eventName, e);
                }
            }

            RPMGlobals.knownLoadedAssemblies.Clear();
            for (int i = 0; i < AssemblyLoader.loadedAssemblies.Count; ++i)
            {
                string thatName = AssemblyLoader.loadedAssemblies[i].assembly.GetName().Name;
                RPMGlobals.knownLoadedAssemblies.Add(thatName.ToUpper());
                JUtil.LogMessage(this, "I know that {0} ISLOADED_{1}", thatName, thatName.ToUpper());
                if ((i & 0xf) == 0xf)
                {
                    yield return null;
                }
            }

            RPMGlobals.systemNamedResources.Clear();
            foreach (PartResourceDefinition thatResource in PartResourceLibrary.Instance.resourceDefinitions)
            {
                string varname = thatResource.name.ToUpperInvariant().Replace(' ', '-').Replace('_', '-');
                RPMGlobals.systemNamedResources.Add(varname, thatResource.name);
                JUtil.LogMessage(this, "Remembering system resource {1} as SYSR_{0}", varname, thatResource.name);
            }

            //reloadInProgress = false;
            yield return null;
        }

        /// <summary>
        /// Callback that fires once the requested assets have loaded.
        /// </summary>
        /// <param name="loader">Object containing our loaded assets (see comments in this method)</param>
        private void AssetsLoaded(KSPAssets.Loaders.AssetLoader.Loader loader)
        {
            var loadedBundles = KSPAssets.Loaders.AssetLoader.LoadedBundles;
            if (loadedBundles == null)
            {
                JUtil.LogErrorMessage(this, "Unable to find any loaded bundles in AssetLoader");
                return;
            }

            // Iterate over all loadedBundles.  Experimentally, my bundle was
            // the only one in the array, but I expect that to change as other
            // mods use asset bundles (maybe none of the mods I have load this
            // early).
            for (int i = 0; i < loadedBundles.Count; ++i)
            {
                Shader[] shaders = null;
                Font[] fonts = null;
                bool theRightBundle = false;

                try
                {
                    // Try to get a list of all the shaders in the bundle.
                    shaders = loadedBundles[i].LoadAllAssets<Shader>();
                    if (shaders != null)
                    {
                        // Look through all the shaders to see if our named
                        // shader is one of them.  If so, we assume this is
                        // the bundle we want.
                        for (int shaderIdx = 0; shaderIdx < shaders.Length; ++shaderIdx)
                        {
                            if (shaders[shaderIdx].name == rpmShaders[0].name)
                            {
                                theRightBundle = true;
                                break;
                            }
                        }
                    }
                    fonts = loadedBundles[i].LoadAllAssets<Font>();
                }
                catch { }

                if (theRightBundle)
                {
                    // If we found our bundle, set up our parsedShaders
                    // dictionary and bail - our mission is complete.
                    JUtil.LogInfo(this, "Found {0} RPM shaders and {1} fonts.", shaders.Length, fonts.Length);
                    for (int j = 0; j < shaders.Length; ++j)
                    {
                        if (!shaders[j].isSupported)
                        {
                            JUtil.LogErrorMessage(this, "Shader {0} - unsupported in this configuration", shaders[j].name);
                        }
                        JUtil.parsedShaders[shaders[j].name] = shaders[j];
                    }
                    for (int j = 0; j < fonts.Length; ++j)
                    {
                        JUtil.LogInfo(this, "Adding RPM-included font {0} / {1}", fonts[j].name, fonts[j].fontSize);
                        JUtil.loadedFonts[fonts[j].name] = fonts[j];
                    }
                    return;
                }
            }

            JUtil.LogErrorMessage(this, "No RasterPropMonitor shaders were loaded - how did this callback execute?");
        }

        public void PostPatchCallback()
        {
            JUtil.LogMessage(this, "ModuleManager has reloaded - reloading RPM values");
            StartCoroutine("LoadRasterPropMonitorValues");
        }

        private bool RegisterWithModuleManager()
        {
            Type mmPatchLoader = null;
            AssemblyLoader.loadedAssemblies.TypeOperation(t =>
            {
                if (t.FullName == "ModuleManager.MMPatchLoader")
                {
                    mmPatchLoader = t;
                }
            });

            if (mmPatchLoader == null)
            {
                return false;
            }

            MethodInfo addPostPatchCallback = mmPatchLoader.GetMethod("addPostPatchCallback", BindingFlags.Static | BindingFlags.Public);

            if (addPostPatchCallback == null)
            {
                return false;
            }

            try
            {
                var parms = addPostPatchCallback.GetParameters();
                if (parms.Length < 1)
                {
                    return false;
                }

                Delegate callback = Delegate.CreateDelegate(parms[0].ParameterType, this, "PostPatchCallback");

                object[] args = new object[] { callback };

                addPostPatchCallback.Invoke(null, args);
            }
            catch (Exception e)
            {
                JUtil.LogMessage(this, "addPostPatchCallback threw {0}", e);
                return false;
            }

            return true;
        }
    }
}
