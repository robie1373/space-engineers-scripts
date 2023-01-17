//
// XXX Drill-Platform//_Customize.cs XXX
//

// EDIT THESE VARIABLES

public const string VERSION = "Template v0.1";
public const UpdateFrequency FREQ = UpdateFrequency.Update100;

public const string yPistonGroup = "yPistons";
public const string zPistonGroup = "zPistons";
//
// XXX Drill-Platform//_Main_v0.1.cs XXX
//

//// GLOBAL VARIABLES
// for transfering data between Runtime events

//// Program()
// for variable initialization, setup, etc.
public Program() {
  Runtime.UpdateFrequency = FREQ; // set from _Customize
  Initialize();
} // Program()

public void Initialize() {
  // run each Program__...() submethods here
} // Initialize()

//// Save()
// called when the Programmable Block shuts down
// use this method to save state to the storage field
public void Save() {
} // Save()

//// Main()
// called when the Programmable Block is "Run",
// or automatically by UpdateFrequency
public void Main(string arg, UpdateType source) {
  // NOTE: multiple trigger sources can roll in on the same tick
  // test each trigger individually, not with if() else if () blocks

  if((FREQ) != 0) { // TODO: can != 0 be dropped? // had to delete source check due to compile error
    // run each Main__...() submethod here
  }
  Main__WriteDiagnostics();
  Program__GetPistons();
} // Main()

public void Program__GetPistons() {
//  IMyBlockGroup xgroup = GridTerminalSystem.GetBlockGroupWithName(xPistonGroup);
//    if (group == null)
//      {
//        Echo("X Group not found");
//        return;
//      }
//      delete__me(xgroup);

  IMyBlockGroup ygroup = GridTerminalSystem.GetBlockGroupWithName(yPistonGroup);
    if (ygroup == null)
    {
      Echo("Y Group not found");
      return;
    }
    delete__me(ygroup);

  IMyBlockGroup zgroup = GridTerminalSystem.GetBlockGroupWithName(zPistonGroup);
    if (zgroup == null)
    {
      Echo("Z Group not found");
      return;
    }
    delete__me(zgroup);

}

public void delete__me(IMyBlockGroup group) {

Echo($"{group.Name}:");
List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
group.GetBlocks(blocks);
foreach (var block in blocks)
{
    Echo($"- {block.CustomName}");
}
}
//
// XXX Drill-Platform//_Template_v0.1.cs XXX
//

// XXX START (DO NOT EDIT) XXX
// This portion of the file should not be edited
// Rather, the Template should be revised and re-released with a new version
// Submit a pull-request for edits to the Template

//
// CLASS DEFINITIONS
//
public class Dict<TKey, TValue> : Dictionary<TKey, TValue> {}
public class Dict<TKey1, TKey2, TValue> : Dictionary<TKey1, Dictionary<TKey2, TValue>> {}

public const int PROGRAMMABLE_BLOCK_SCREEN_SURFACE_NUM   = 0; // TODO unverified
public const int PROGRAMMABLE_BLOCK_KEYBOARD_SURFACE_NUM = 1; // TODO unverified
public const float FONT_SIZE_REGULAR = 0.50f;
public const string HR_NO_NL = "====================================", // TODO = or - instead?
HR       = "\n" + HR_NO_NL + "\n",
FONT = "Monospace";
//
// HELPER METHODS
// these are shared across many scripts
// if there is a bug, report it or make a PR on github
// TODO github link
//

private void Program__Programmable_Block_Display() {
  IMyTextSurface display = Me.GetSurface(PROGRAMMABLE_BLOCK_SCREEN_SURFACE_NUM);
  string timeString = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss tt");

  // write out to display
  display.ContentType = ContentType.TEXT_AND_IMAGE;
  display.WriteText($"\nCurrent Program: {VERSION}\n", false);
  display.WriteText($"\nLast Compiled: {timeString}\n", true);
} // Program__Programmable_Block_Display()

private void Main__WriteDiagnostics() {
  double LastRunTimeNs = Runtime.LastRunTimeMs * 1000;

  // write out to the programmable block's internal console
  Echo($"Last run: {LastRunTimeNs.ToString("n0")}ns");
  Echo($"Instruction limit: ");
  Echo($"{Runtime.CurrentInstructionCount}/{Runtime.MaxInstructionCount}");
} // WriteDiagnostics()

private string Match(string input, string pattern, string errMsg) {
  System.Text.RegularExpressions.Match match;
  match = System.Text.RegularExpressions.Regex.Match(input, pattern);
  if(match.Success) {
    return match.Value;
  } else {
    Echo(errMsg);
    return (string)(null);
  }
} // Match()

public List<Type> GetBlocksOfTypeWithNames<Type>(List<Type> blocks,
                                                        params string[] strings)
                                                        where Type : class, IMyTerminalBlock {
  // create an empty list of blocks of a specific type
  List<Type> blks = new List<Type>();
  // populate that list with all those blocks
  GridTerminalSystem.GetBlocksOfType(blks);
  // filter that list by the strings provided
  return FilterBlocksOfTypeWithNames(blks, strings);
}

public List<Type> FilterBlocksOfTypeWithNames<Type>(List<Type> blocks,
                                                            params string[] strings)
                                                            where Type : class, IMyTerminalBlock {
  // find the subset of blocks which match all strings provided
  return blocks.FindAll(block => strings.All(str => block.CustomName.Contains(str)));
}

public List<IMyTerminalBlock> GetBlocksOfNames(params string[] strings) {
  // create an empty list of blocks of all types
  List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
  // populate that list with all blocks on the grid
  GridTerminalSystem.GetBlocks(blocks);
  // filter that list by the strings provided
  return FilterBlocksOfTypeWithNames(blocks, strings);
}

public string StripClassesFromName(IMyTerminalBlock blk) {
  string name = blk.CustomName;
  if(name.Contains('.')) {
    name = name.Substring(0, name.IndexOf('.')).Trim();
  }
  return name;
} // StripClassesFromName()

// XXX END (DO NOT EDIT) XXX

