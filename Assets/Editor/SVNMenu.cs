using UnityEngine;
using System.IO;
using System.Collections;
using UnityEditor;

public class SVNMenu 
{

	[MenuItem( "SVN/Update Assets")]
	
	static void svnUpdateAssets ()
	{
		tortoiseProc("update", "Assets");
	}
	
	[MenuItem( "SVN/Commit Assets")]
	
	static void svnCommitAssets ()
	{
		tortoiseProc("commit", "Assets");
	}
	
	[MenuItem( "SVN/Revert Assets")]
	
	static void svnRevertAssets ()
	{
		tortoiseProc("revert", "Assets");
	}
	
	static void tortoiseProc( string command, string file )
	{
		string path = Path.Combine(getProjectPath(), file );
		string url = Path.Combine(getSVNUrl(), file );
		
		string args = string.Format ("/command:{0} /path:\"{1}\"", command, path, url);
		System.Diagnostics.Process.Start ("TortoiseProc", args);
	}
	
	static string getProjectPath ()
	{
		string path = Path.GetFullPath(".");
		return path;
	}
	
	static string getSVNUrl ()
	{
        return "https://sandbr/svn/GalaxyLife";
	}
}
