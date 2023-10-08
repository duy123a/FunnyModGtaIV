using System;
using System.Windows.Forms;
using GTA;
using System.Collections.Generic;

namespace InstantCoffee {


   // ### Press F12 button to have an 'instant coffee' with the nearest female ###


   public class InstantCoffee : Script {
 
      public InstantCoffee() {
         this.Tick += new EventHandler(this.ScriptCommunicationExample1_Tick);
 Wait(0);
      }
  private void req()
        {

            GTA.Native.Function.Call("REQUEST_ANIMS", "misscar_sex");

            while (!GTA.Native.Function.Call<bool>("HAS_ANIMS_LOADED", "misscar_sex"))
            {
             GTA.Native.Function.Call("Request_Anims", "misscar_sex");
                Wait(10);
            }
        }

      private void ScriptCommunicationExample1_Tick(object sender, EventArgs e)
      {
          {
 Wait(0);



              if ((isKeyPressed(Settings.GetValueKey("Key", Keys.F12))))//(isKeyPressed(Keys.MButton))||
              {
GTA.Native.Function.Call("Request_Anims", "misscar_sex");

if(!Player.Character.isSittingInVehicle()&&Player.WantedLevel==0){

                      GTA.Ped[] pedArray = GTA.World.GetPeds(Player.Character.Position, 1.7F, 2);
                
                      foreach (GTA.Ped Pede in pedArray)
                      {
                          if (!Pede.isAliveAndWell||Pede.isSittingInVehicle() || Pede == Player.Character)  // || Pede.isAliveAndWell || Pede.isRagdoll || Pede.isAliveAndWell) || Pede.isSittingInVehicle() (Pede.GetBonePosition(Bone.Pelvis).DistanceTo(Pede.Position.ToGround()) >= 4.55F)
                          {
                              continue;
                          }
Group gr;
 gr = Player.Group;

if(Player.Character.isAlive&&!GTA.Native.Function.Call<bool>("IS_CHAR_DUCKING", Player.Character, 1)&& !Player.Character.isRagdoll&&!Player.Character.isInCombat)// &&Player.Character.Weapons.CurrentType!=0
{
GTA.Native.Function.Call("SET_CHAR_AS_MISSION_CHAR",Pede, 1);
GTA.Native.Function.Call("SET_EVERYONE_IGNORE_PLAYER", Player,1);
Pede.RelationshipGroup = RelationshipGroup.Player;
Pede.PreventRagdoll = true;
Pede.Task.ClearAllImmediately();
Pede.DuckWhenAimedAtByGroupMember = false;
         Pede.ChangeRelationship(RelationshipGroup.Player, Relationship.Companion);
if(!Pede.isInGroup){gr.AddMember(Pede);}
Wait(400);
Pede.Task.ClearAllImmediately();
GTA.Native.Function.Call("SET_CHAR_COLLISION", Player.Character, 0);
GTA.Native.Function.Call("WARP_CHAR_FROM_CAR_TO_COORD",Player.Character,Player.Character.Position.X, Player.Character.Position.Y, Player.Character.Position.Z-2.12f);
Player.Character.FreezePosition=true;
Pede.Task.ClearAllImmediately();
Vector3 sx=Player.Character.GetOffsetPosition(new Vector3(-0.05f,0.25f,-0.65f));
Pede.FreezePosition=true;
Wait(0);
GTA.Native.Function.Call("WARP_CHAR_FROM_CAR_TO_COORD",Pede,sx.X, sx.Y, sx.Z);
Pede.Task.ClearAllImmediately();
GTA.Native.Function.Call("SET_CHAR_COLLISION", Pede, 0);
Pede.Task.ClearAllImmediately();
Pede.Heading = Player.Character.Heading;
GTA.Native.Function.Call("TASK_PLAY_ANIM", Player.Character, "m_sex_loop", "misscar_sex", 4.00, 1, 1, 1, 0, -1);
GTA.Native.Function.Call("TASK_PLAY_ANIM", Pede, "f_sex_loop", "misscar_sex", 4.00, 1, 1, 1, 0, -1);
Wait(50);
if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "misscar_sex", "m_sex_loop")&&GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Pede, "misscar_sex", "f_sex_loop") ){
Wait(20500);
if(Pede.Exists()){
if(Pede.isInGroup&&!Game.isGameKeyPressed(GTA.GameKey.Action)){gr.RemoveMember(Pede);}
Pede.PreventRagdoll = false;

Pede.FreezePosition=false;
GTA.Native.Function.Call("SET_CHAR_COLLISION", Pede, 1);

Pede.Euphoria.BeingShot.Start(12000);GTA.Native.Function.Call("SET_CHAR_AS_MISSION_CHAR",Pede, 0);
Pede.NoLongerNeeded();
}
Player.Character.FreezePosition=false;
GTA.Native.Function.Call("SET_CHAR_COLLISION", Player.Character, 1);GTA.Native.Function.Call("SET_EVERYONE_IGNORE_PLAYER", Player,0);
Player.Character.Euphoria.BeingShot.Start(10000);
}else{Pede.PreventRagdoll = false;
Pede.FreezePosition=false;
GTA.Native.Function.Call("SET_CHAR_COLLISION", Pede, 1);
Pede.NoLongerNeeded();Player.Character.FreezePosition=false;Player.Character.Task.ClearAllImmediately();GTA.Native.Function.Call("SET_CHAR_AS_MISSION_CHAR",Pede, 0);
GTA.Native.Function.Call("SET_CHAR_COLLISION", Player.Character, 1);GTA.Native.Function.Call("SET_EVERYONE_IGNORE_PLAYER", Player,0);Pede.Task.ClearAllImmediately();}
}}
}}}}
}

  
}