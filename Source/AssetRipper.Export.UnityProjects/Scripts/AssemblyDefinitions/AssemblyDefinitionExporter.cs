﻿using AsmResolver.DotNet;
using System.Text.Json;

namespace AssetRipper.Export.UnityProjects.Scripts.AssemblyDefinitions
{
	public static class AssemblyDefinitionExporter
	{
		public static void Export(AssemblyDefinitionDetails details)
		{
			string assetPath = Path.Combine(details.OutputFolder, $"{details.AssemblyName}.asmdef");

			AssemblyDefinitionAsset asset = new AssemblyDefinitionAsset(details.AssemblyName);
			if (details.Assembly is not null)
			{
				foreach (AssemblyReference reference in details.Assembly.AssemblyReferences)
				{
					if (reference.Name is null || ReferenceAssemblies.IsReferenceAssembly(reference.Name))
					{
						continue;
					}

					asset.References.Add(reference.Name);
				}
			}

			string assetData = JsonSerializer.Serialize(asset, AssemblyDefinitionSerializerContext.Default.AssemblyDefinitionAsset);
			File.WriteAllText(assetPath, assetData);
		}
	}
}
