using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	internal static class ScreenshotManager
	{
		public static void CaptureScreenshot(NodeProcess process, NodeProcess.ScreenshotMode screenshotMode, Action<Texture2D> onComplete, float allNodesScaleFactor, bool useProgressBar)
		{
			process.editor.Focus();
			switch (screenshotMode)
			{
			case NodeProcess.ScreenshotMode.VisibleArea:
				DeEditorCoroutines.StartCoroutine(CaptureScreenshot_VisibleArea(process, onComplete, useProgressBar));
				break;
			case NodeProcess.ScreenshotMode.AllNodes:
				DeEditorCoroutines.StartCoroutine(CaptureScreenshot_AllNodes(process, onComplete, allNodesScaleFactor, useProgressBar));
				break;
			}
		}

		private static IEnumerator CaptureScreenshot_VisibleArea(NodeProcess process, Action<Texture2D> onComplete, bool useProgressBar)
		{
			if (useProgressBar)
			{
				EditorUtility.DisplayProgressBar("Screenshot", "Capturing...", 0.5f);
			}
			process.editor.Repaint();
			yield return DeEditorCoroutines.WaitForSeconds(0.001f);
			process.editor.Focus();
			yield return DeEditorCoroutines.WaitForSeconds(0.001f);
			Rect source = process.position.ResetXY();
			Texture2D texture2D = new Texture2D((int)source.width, (int)source.height, TextureFormat.RGB24, mipChain: false);
			texture2D.ReadPixels(source, 0, 0, recalculateMipMaps: false);
			texture2D.Apply();
			if (useProgressBar)
			{
				EditorUtility.ClearProgressBar();
			}
			onComplete(texture2D);
		}

		private static IEnumerator CaptureScreenshot_AllNodes(NodeProcess process, Action<Texture2D> onComplete, float scaleFactor, bool useProgressBar)
		{
			bool hasMinimap = process.options.showMinimap;
			float orGuiScale = process.guiScale;
			Vector2 orShift = process.areaShift;
			Rect orEditorPosition = process.editor.position;
			process.guiScale = scaleFactor;
			if (useProgressBar)
			{
				EditorUtility.DisplayProgressBar("Screenshot", "Capturing...", 0.3f);
			}
			process.options.showMinimap = false;
			process.editor.position = new Rect(0f, 0f, 4096f, 4096f);
			process.editor.Repaint();
			yield return DeEditorCoroutines.WaitForSeconds(0.001f);
			Rect fullNodesArea2 = process.EvaluateFullNodesArea();
			fullNodesArea2 = fullNodesArea2.Shift(-20f, -20f, 40f, 40f);
			fullNodesArea2.x -= process.areaShift.x;
			fullNodesArea2.y -= process.areaShift.y;
			fullNodesArea2.width *= scaleFactor;
			fullNodesArea2.height *= scaleFactor;
			Rect rect = process.position.ResetXY();
			int cols = Mathf.CeilToInt(fullNodesArea2.width / rect.width);
			int rows = Mathf.CeilToInt(fullNodesArea2.height / rect.height);
			int blockW = (int)rect.width;
			int blockH = (int)rect.height;
			float cropValOffset = 2f / scaleFactor;
			Texture2D tx = new Texture2D((int)fullNodesArea2.width - 2 * cols, (int)fullNodesArea2.height - 2 * rows, TextureFormat.RGB24, mipChain: false);
			int y = tx.height;
			int r = 0;
			while (r < rows)
			{
				bool shiftTxY = true;
				int num;
				for (int c = 0; c < cols; c = num)
				{
					int w = blockW;
					int h = blockH;
					if (c == cols - 1)
					{
						w = blockW - (blockW * (c + 1) - (int)fullNodesArea2.width);
					}
					if (r == rows - 1)
					{
						h = blockH - (blockH * (r + 1) - (int)fullNodesArea2.height);
					}
					if (shiftTxY)
					{
						shiftTxY = false;
						y -= h - 2;
					}
					Vector2 areaShift = new Vector2(0f - fullNodesArea2.x - (float)blockW / scaleFactor * (float)c - cropValOffset, 0f - fullNodesArea2.y - (float)blockH / scaleFactor * (float)r - cropValOffset);
					process.SetAreaShift(areaShift);
					Texture2D subTx = new Texture2D(w, h, TextureFormat.RGB24, mipChain: false);
					yield return DeEditorCoroutines.WaitForSeconds(0.001f);
					process.editor.Focus();
					yield return DeEditorCoroutines.WaitForSeconds(0.001f);
					subTx.ReadPixels(new Rect(0f, blockH - h, w, h), 0, 0, recalculateMipMaps: false);
					subTx.Apply();
					Color[] pixels = subTx.GetPixels(1, 1, w - 2, h - 2);
					tx.SetPixels((blockW - 2) * c, y, w - 2, h - 2, pixels);
					num = c + 1;
				}
				num = r + 1;
				r = num;
			}
			tx.Apply(updateMipmaps: false);
			if (hasMinimap)
			{
				process.options.showMinimap = hasMinimap;
			}
			process.guiScale = orGuiScale;
			process.editor.position = orEditorPosition;
			process.SetAreaShift(orShift);
			if (useProgressBar)
			{
				EditorUtility.ClearProgressBar();
			}
			onComplete(tx);
		}
	}
}
