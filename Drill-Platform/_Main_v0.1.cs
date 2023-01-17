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

  if((source & FREQ) != 0) { // TODO: can != 0 be dropped?
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
    if (group == null)
    {
      Echo("Y Group not found");
      return;
    }
    delete__me(ygroup);

  IMyBlockGroup zgroup = GridTerminalSystem.GetBlockGroupWithName(zPistonGroup);
    if (group == null)
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