/// <summary>
/// Fuse Fixer by Jason RT Bond
/// version 1.0, January 20, 2016
///
/// Quick and dirty utility to correct textures and material for character models
/// created in Fuse, rigged via Mixamo.com and downloaded in the "FBX for Unity" format.
/// 
/// Seems to work with:
/// 	• Adobe Fuse CC Preview (2015.1.0)
/// 	• Mixamo.com export (FBX for Unity) as of Jan. 2016.
/// 	• Unity 5.3.1
/// 
/// Instructions:
/// 	1) With this script in your project, select your .fbx(s) in the project view, 
/// and then in the menu select "Assets"->"Fix Fuse Model(s)".
/// 	2) Wait a nice long time :)
/// 	3) Use the generated prefab (sits next to the model in the assets) in your scenes.
/// 
/// Notes:
/// 	• Should work with both packed and unpacked texture options.
/// 	• Should work after updating models as well (prefab should be updated but not overwritten).
/// 	• Absolutely no warranty provided :)
/// 	• Likely to break when the exact format of the source models inevitably changes.
/// </summary>

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

public class FuseFixer
{

	[MenuItem ("Assets/Fix Fuse Model(s)")]
	private static void FixFuseModel ()
	{
		_totalAssets = UnityEditor.Selection.assetGUIDs.Length;
		_curAsset = 0;
		foreach (string s in UnityEditor.Selection.assetGUIDs) {
			string assetPath = AssetDatabase.GUIDToAssetPath (s);

			if (assetPath.IndexOf (".fbx") == -1) {
				continue;
			} else {
				try {
					FixFuseModelAtPath (assetPath);
				}
				catch (Exception r) {
					EditorUtility.ClearProgressBar ();
					throw r;
				}
			}

			_curAsset++;
		}
	}

	// Basic stuff for giving some progress feedback.
	private static int _totalAssets;
	private static int _curAsset;
	private static void UpdateProgressBar(string msg, float progThisAsset)
	{
		EditorUtility.DisplayProgressBar ("Fixing Fuse Model (" + (_curAsset+1) + " of " + _totalAssets + ")", msg, (float)_curAsset / (_totalAssets) + progThisAsset / _totalAssets);
	}

	/// <summary>
	/// Takes a path to an .fbx file and attempts to "fix" it up.
	/// </summary>
	private static void FixFuseModelAtPath (string assetPath)
	{
		// Only operate on FBX files
		if (assetPath.IndexOf (".fbx") == -1) {
			return;
		}

		UpdateProgressBar ("Managing prefab.", 0f);

		string assetPathRoot = assetPath.Substring (0, assetPath.LastIndexOf (".fbx"));

		// Find or create the prefab version.
		string assetPathPrefab = assetPathRoot + ".prefab";
		GameObject prefabObj = AssetDatabase.LoadAssetAtPath<GameObject> (assetPathPrefab);
		if (prefabObj == null)
		{
			// Create and instance of the model, make a prefab out of it, then remove the instance. Yikes.
			Debug.Log ("Creating prefab: " + assetPathPrefab);
			GameObject instanceObj = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> (assetPath));
			prefabObj = PrefabUtility.CreatePrefab(assetPathPrefab, instanceObj, ReplacePrefabOptions.Default);
			GameObject.DestroyImmediate (instanceObj);
		}

		string textureFolder = assetPathRoot + ".fbm";
		if (!AssetDatabase.IsValidFolder (textureFolder))
			return;

		List<string> textureAssets = new List<string> ();
		textureAssets.AddRange (AssetDatabase.FindAssets ("", new string[]{ textureFolder }));


		// Scan for diffuse textures.
		for (int i = 0; i < textureAssets.Count; i++) {

			UpdateProgressBar ("Combining textures.", 0.1f + (float)i / textureAssets.Count * 0.75f);

			string pathRGB = AssetDatabase.GUIDToAssetPath (textureAssets [i]);

			if (pathRGB.EndsWith ("Diffuse.png")) {
				MakeTextureCombo (pathRGB, "Diffuse.png", "Opacity.png", "DiffuseOpacity.png");
			}
			if (pathRGB.EndsWith ("Specular.png")) {
				MakeTextureCombo (pathRGB, "Specular.png", "Gloss.png", "SpecularGloss.png");
			}
		}

		string materialFolder = assetPath.Substring (0, assetPath.LastIndexOf ("/")) + "/Materials";
		if (!AssetDatabase.IsValidFolder (materialFolder))
			return;

		List<string> materialAssetsGUIDs = new List<string> ();
		List<string> materialAssetsPaths = new List<string> ();
		materialAssetsGUIDs.AddRange (AssetDatabase.FindAssets ("", new string[]{ materialFolder }));
		for (int i = 0; i < materialAssetsGUIDs.Count; i++) {
			materialAssetsPaths.Add(AssetDatabase.GUIDToAssetPath (materialAssetsGUIDs [i]));
		}

		// Scan once to ensure eyeball and eyelash materials exist for each body material.
		for (int i = 0; i < materialAssetsPaths.Count; i++) {

			UpdateProgressBar ("Assuring material instances.", 0.75f + (float)i / materialAssetsPaths.Count * 0.05f);

			// Eyeballs and eyelashes are initially set up with either under the Body or Packed0 materials.
			int indEyeSuffix = materialAssetsPaths [i].LastIndexOf ("_Body_Diffuse.mat");
			if (indEyeSuffix == -1)
				indEyeSuffix = materialAssetsPaths [i].LastIndexOf ("_Packed0_Diffuse.mat");

			if (indEyeSuffix != -1) {
				string pathRoot = materialAssetsPaths [i].Substring (0, indEyeSuffix);
				string eyeballsPath = pathRoot + "_Eyeballs.mat";
				string eyelashesPath = pathRoot + "_Eyelashes.mat";

				// Create these extra materials if they don't exist.
				if (!materialAssetsPaths.Contains (eyeballsPath)) {
					if (AssetDatabase.CopyAsset (materialAssetsPaths [i], eyeballsPath)) {
						AssetDatabase.Refresh ();
						Debug.Log ("Creating " + eyeballsPath);
						materialAssetsGUIDs.Add (AssetDatabase.AssetPathToGUID (eyeballsPath));
						materialAssetsPaths.Add (eyeballsPath);
					} else
						Debug.LogError ("Failed to copy " + materialAssetsPaths [i]);
				}
				if (!materialAssetsPaths.Contains (eyelashesPath)) {
					if (AssetDatabase.CopyAsset (materialAssetsPaths [i], eyelashesPath)) {
						AssetDatabase.Refresh ();
						Debug.Log ("Creating " + eyelashesPath);
						materialAssetsGUIDs.Add (AssetDatabase.AssetPathToGUID (eyelashesPath));
						materialAssetsPaths.Add (eyelashesPath);
					} else
						Debug.LogError ("Failed to copy " + materialAssetsPaths [i]);
				}
			}

			// Also, for packed textures, we find hair under Packed1.
			int indHairSuffix = materialAssetsPaths [i].LastIndexOf ("_Packed1_Diffuse.mat");
			if (indHairSuffix != -1) {
				string pathRoot = materialAssetsPaths [i].Substring (0, indHairSuffix);
				string hairPath = pathRoot + "_Hair_Diffuse.mat";

				// Create these extra materials if they don't exist.
				if (!materialAssetsPaths.Contains (hairPath)) {
					if (AssetDatabase.CopyAsset (materialAssetsPaths [i], hairPath)) {
						AssetDatabase.Refresh ();
						Debug.Log ("Creating " + hairPath);
						materialAssetsGUIDs.Add (AssetDatabase.AssetPathToGUID (hairPath));
						materialAssetsPaths.Add (hairPath);
					} else
						Debug.LogError ("Failed to copy " + materialAssetsPaths [i]);
				}
			}
		}

		// Scan again and set up properties.
		for (int i = 0; i < materialAssetsGUIDs.Count; i++) {

			UpdateProgressBar ("Configuring materials.", 0.8f + (float)i / materialAssetsGUIDs.Count * 0.1f);

			string materialPath = AssetDatabase.GUIDToAssetPath (materialAssetsGUIDs [i]);

			Material material = AssetDatabase.LoadAssetAtPath<Material> (materialPath);

			// Set shader type.
			/*if (material.name.EndsWith ("_Eyeballs")) {
				material.shader = Shader.Find ("Toon/Lit Outline");

				// Hard-coded values that seem "okay" for eyeballs...
				//material.SetFloat ("_Metallic", 0.02f);
				//material.SetFloat ("_Glossiness", 0.8f);
				
			}
			else*/
			material.shader = Shader.Find ("Toon/Lit Outline");
			material.SetFloat ("_Outline width", 0.002f);

			Texture t = material.GetTexture ("_MainTex");
			string tPath = AssetDatabase.GetAssetPath (t);

			// Replace either assigned diffuse or diffuse/opacity. (Because maybe spec/gloss not set up yet?)
			int suffixInd = tPath.LastIndexOf ("_Diffuse.png");
			if (suffixInd == -1)
				suffixInd = tPath.LastIndexOf ("_DiffuseOpacity.png");

			if (suffixInd != -1) {
				string tPathRoot = tPath.Substring (0, suffixInd);

				Texture2D dO = AssetDatabase.LoadAssetAtPath<Texture2D> (tPathRoot + "_DiffuseOpacity.png");
				Texture2D sG = AssetDatabase.LoadAssetAtPath<Texture2D> (tPathRoot + "_SpecularGloss.png");

				if (dO != null) {
					material.SetTexture ("_MainTex", dO);

					// Assume all opaque except hair and eyelashes.
					if (material.name.EndsWith("_Eyelashes") 
						|| material.name.EndsWith("_Hair_Diffuse")
						|| material.name.EndsWith("_Eyewear_Diffuse")
						|| material.name.EndsWith("_Mask_Diffuse"))
					{
						material.SetFloat ("_Mode", 2);
						material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
						material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
						material.SetInt ("_ZWrite", 0);
						material.DisableKeyword ("_ALPHATEST_ON");
						material.EnableKeyword ("_ALPHABLEND_ON");
						material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
						material.renderQueue = 3000;
					} else {
						material.SetFloat ("_Mode", 0);
						material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
						material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
						material.SetInt ("_ZWrite", 1);
						material.DisableKeyword ("_ALPHATEST_ON");
						material.DisableKeyword ("_ALPHABLEND_ON");
						material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
					}
				}
				if (sG != null)
					material.SetTexture ("_SpecGlossMap", sG);
			}
		}

		// Finally, check that the renderers are assigned to the correct materials.

		SkinnedMeshRenderer[] mr = prefabObj.GetComponentsInChildren<SkinnedMeshRenderer> ();
		for (int i = 0; i < mr.Length; i++) {

			UpdateProgressBar ("Setting materials to renderers.", 0.9f + (float)i / materialAssetsGUIDs.Count * 0.1f);

			if (mr [i].name == "default")
				SetUpEyeballs (mr [i]);

			else if (mr [i].name == "Eyelashes")
				SetUpEyelashes (mr [i]);
			
			else if (mr [i].name == "Hair")
				SetUpHair (mr [i]);
		}

		EditorUtility.ClearProgressBar ();

	}

	static void SetUpEyeballs (SkinnedMeshRenderer r)
	{
		string curMatPath = AssetDatabase.GetAssetPath (r.sharedMaterial);

		int suffixIndex = curMatPath.LastIndexOf ("_Body_Diffuse.mat");
		if (suffixIndex == -1)
			suffixIndex = curMatPath.LastIndexOf ("_Packed0_Diffuse.mat");
		if (suffixIndex != -1) {
			string newMatPath = curMatPath.Substring (0, suffixIndex) + "_Eyeballs.mat";
			Material newMat = AssetDatabase.LoadAssetAtPath<Material> (newMatPath);

			if (newMat != null)
				r.sharedMaterial = newMat;
		}
	}

	static void SetUpEyelashes (SkinnedMeshRenderer r)
	{
		string curMatPath = AssetDatabase.GetAssetPath (r.sharedMaterial);

		int suffixIndex = curMatPath.LastIndexOf ("_Body_Diffuse.mat");
		if (suffixIndex == -1)
			suffixIndex = curMatPath.LastIndexOf ("_Packed0_Diffuse.mat");
		if (suffixIndex != -1) {
			string newMatPath = curMatPath.Substring (0, suffixIndex) + "_Eyelashes.mat";
			Material newMat = AssetDatabase.LoadAssetAtPath<Material> (newMatPath);

			if (newMat != null)
				r.sharedMaterial = newMat;
		}

	}
	static void SetUpHair (SkinnedMeshRenderer r)
	{
		string curMatPath = AssetDatabase.GetAssetPath (r.sharedMaterial);

		int suffixIndex = curMatPath.LastIndexOf ("_Packed1_Diffuse.mat");
		if (suffixIndex != -1) {
			string newMatPath = curMatPath.Substring (0, suffixIndex) + "_Hair_Diffuse.mat";
			Material newMat = AssetDatabase.LoadAssetAtPath<Material> (newMatPath);

			if (newMat != null)
				r.sharedMaterial = newMat;
		}

	}

	static void MakeTextureCombo (string pathRGB, string suffixRGB, string suffixA, string suffixCombo)
	{

		string pathBase = pathRGB.Substring (0, pathRGB.LastIndexOf (suffixRGB));
		string pathA = pathBase + suffixA;
		string pathCombo = pathBase + suffixCombo;

		Texture2D textureRGB = AssetDatabase.LoadAssetAtPath<Texture2D> (pathRGB);
		Texture2D textureA = AssetDatabase.LoadAssetAtPath<Texture2D> (pathA);
		Texture2D textureCombo = AssetDatabase.LoadAssetAtPath<Texture2D> (pathCombo);

		if (textureRGB != null && textureA != null) {

			if (textureCombo == null) {
				if (AssetDatabase.CopyAsset (pathRGB, pathCombo)) {
					AssetDatabase.Refresh ();
					textureCombo = AssetDatabase.LoadAssetAtPath<Texture2D> (pathCombo);
					Debug.Log ("Creating " + pathCombo);
				} else {
					Debug.LogError ("Failed to copy " + pathRGB);
				}
			} 

			// Make them readable!
			TextureImporter tImporterRGB = AssetImporter.GetAtPath (pathRGB) as TextureImporter;
			tImporterRGB.textureType = TextureImporterType.Advanced;
			tImporterRGB.isReadable = true;
			AssetDatabase.ImportAsset (pathRGB);
			TextureImporter tImporterA = AssetImporter.GetAtPath (pathA) as TextureImporter;
			tImporterA.textureType = TextureImporterType.Advanced;
			tImporterA.isReadable = true;
			AssetDatabase.ImportAsset (pathA);
			TextureImporter tImporterCombo = AssetImporter.GetAtPath (pathCombo) as TextureImporter;
			tImporterCombo.textureType = TextureImporterType.Advanced;
			tImporterCombo.isReadable = true;
			tImporterCombo.textureFormat = TextureImporterFormat.RGBA32;
			//					tImporterCombo.textureType = TextureImporterType. 
			tImporterCombo.alphaIsTransparency = true;
			AssetDatabase.ImportAsset (pathCombo);

			AssetDatabase.Refresh ();

			// Copy over data.

			Color32[] dataRGB = textureRGB.GetPixels32 ();
			Color32[] dataA = textureA.GetPixels32 ();

			if (dataRGB.Length != dataA.Length) {
				Debug.LogWarning ("Cannot combine textures of unequal size: " + pathRGB + " and " + pathA);
				return;
			}

			bool hasTransparency = false;

			Color32[] dataCombo = new Color32[dataRGB.Length];
			for (int h = 0; h < dataCombo.Length; h++) {
				dataCombo [h] = new Color32 (
					dataRGB [h].r, 
					dataRGB [h].g, 
					dataRGB [h].b, 
					dataA [h].r);
				hasTransparency |= (dataA [h].r != 255); // Have transparency if A not always saturated.
			}

			// Set the data for the new texture.
			textureCombo.SetPixels32 (dataCombo);
			textureCombo.alphaIsTransparency = true;
			textureCombo.Apply ();

			// Clean up texture import settings.
			tImporterRGB.isReadable = false;
			tImporterRGB.textureType = TextureImporterType.Image;
			AssetDatabase.ImportAsset (pathRGB);

			tImporterA.isReadable = false;
			tImporterA.textureType = TextureImporterType.Image;
			AssetDatabase.ImportAsset (pathA);

			File.WriteAllBytes (pathCombo, textureCombo.EncodeToPNG ());

			tImporterCombo.isReadable = false;
			tImporterCombo.textureType = TextureImporterType.Image;
			tImporterCombo.alphaIsTransparency = hasTransparency;
			tImporterCombo.textureFormat = TextureImporterFormat.AutomaticCompressed;
			AssetDatabase.ImportAsset (pathCombo);


			AssetDatabase.Refresh ();

		} else
			Debug.Log ("Found diffuse but no matching opacity: " + pathA);
	}
}

#endif