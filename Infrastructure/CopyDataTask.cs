using System;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;

namespace Infrastructure;


public class CopyDataTask : Task
{
	public string DataDir { get; set; }
	public string OutputDir { get; set; }
	
	
	/// <summary>
	/// Copies the first level of <c>InputDir</c> to <c>OutputDir</c> and strips all the whitespace from
	/// any files found in the subdirectories of <c>InputDir</c>. Intended for JSON optimization.
	/// </summary>
	/// <returns>Returns true unless an exception is thrown.</returns>
	public override bool Execute()
	{
		CopyJsonData();
		return true;
	}

	private void CopyJsonData()
	{
		DirectoryInfo outputDataDirInfo = new DirectoryInfo(OutputDir);
		if (!outputDataDirInfo.Exists)
			outputDataDirInfo.Create();
		foreach (string dir in Directory.EnumerateDirectories(DataDir))
		{
			DirectoryInfo dataDirInfo = new DirectoryInfo(dir);
			DirectoryInfo outputDirInfo = new DirectoryInfo(Path.Join(OutputDir, dataDirInfo.Name));
			if (!outputDirInfo.Exists)
				outputDirInfo.Create();
			foreach (string inputFilename in Directory.EnumerateFiles(dir))
			{
				string outputFilename = Path.Join(outputDirInfo.FullName, Path.GetFileName(inputFilename));
				// Skip if the output file is newer than the input file. 
				if (File.Exists(outputFilename) &&
					(File.GetCreationTime(outputFilename) > File.GetCreationTime(inputFilename)))
					continue;
				string fileContents;
				
				using (StreamReader reader = new StreamReader(File.OpenRead(inputFilename)))
				{
					fileContents = reader.ReadToEnd();
				}

				using (JsonDocument doc = JsonDocument.Parse(fileContents))
				{
					using (Utf8JsonWriter writer = new Utf8JsonWriter(File.OpenWrite(outputFilename), new JsonWriterOptions() {Indented = false}))
					{
						doc.WriteTo(writer);
					}					
				}
				
				
			}
		}
	}
}