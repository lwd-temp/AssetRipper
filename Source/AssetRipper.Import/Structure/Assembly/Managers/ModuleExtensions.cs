﻿using AsmResolver.DotNet;

namespace AssetRipper.Import.Structure.Assembly.Managers;

internal static class ModuleExtensions
{
	public static TypeDefinition? GetType(this ModuleDefinition module, string @namespace, string name)
	{
		IList<TypeDefinition> types = module.TopLevelTypes;
		foreach (TypeDefinition type in types)
		{
			if ((type.Namespace ?? "") == @namespace && type.Name == name)
			{
				return type;
			}
		}

		return null;
	}

	public static void SetResolver(this ModuleDefinition module, IAssemblyResolver assemblyResolver)
	{
		module.MetadataResolver = new DefaultMetadataResolver(assemblyResolver);
	}

	public static void InitializeResolvers(this ModuleDefinition assembly, BaseManager assemblyManager)
	{
		assembly.SetResolver(assemblyManager.AssemblyResolver);
	}
}
