using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GTA;

namespace TestScriptCS
{
   public class Smoking : Script
   {
       GTA.Object cig_plyer = null;
       float angle = 0.0f;
       float x = 0.0f;
       float y = 0.0f;
       float z = 0.0f;
       int cig_num = 20;
       int flg_smoking = 0;
       int a = 0;
       Random cRandom = new System.Random();
       public Smoking()
      {
          Wait(2000);
          this.Tick += new EventHandler(this.Smoking_Tick);
          cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
          Wait(1000);
          while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
          {
              Wait(0);
          }
          cig_plyer.Delete();
          //Player.Character.Health = 5;
      }

      private void Smoking_Tick(object sender, EventArgs e)
      {
          Smoking_RQ();
          Smoking_radar();
          SitDown();
          if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@burgercart", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@ffood_server", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@hotdogcart", "buy_hotdog_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@nutcart", "buy_nuts_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "customer_juice")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "player_drink"))
          {
              cig_num = cig_num + 20;
              while (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@burgercart", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@ffood_server", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@hotdogcart", "buy_hotdog_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@nutcart", "buy_nuts_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "customer_juice")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "player_drink"))
              {
                  Game.DisplayText("Cigarette X " + cig_num);
                  Wait(500);
              }
          }
          if (Game.isGameKeyPressed(GTA.GameKey.Sprint) && Game.isGameKeyPressed(GTA.GameKey.RadarZoom) && (cig_num >= 1)
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb_down")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb_run")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_idle")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_idle_ambient")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_getoff_top")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_geton")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb_down_run")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_geton_topb")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_geton_top")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_slide")
              && !GTA.Native.Function.Call<bool>("IS_PLAYER_CLIMBING", Player) && !GTA.Native.Function.Call<bool>("IS_CHAR_SWIMMING", Player.Character)
              && !GTA.Native.Function.Call<bool>("IS_CHAR_IN_AIR", Player.Character) && !GTA.Native.Function.Call<bool>("IS_CHAR_GETTING_UP", Player.Character)
              //&& !GTA.Native.Function.Call<bool>("IS_CHAR_IN_WATER", Player.Character) 
              && !GTA.Native.Function.Call<bool>("IS_CHAR_SITTING_IDLE", Player.Character)
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "upset_in_bed_idle")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "lie_on_bed_l")
              && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "lie_on_bed_r")
              )
          {
              cig_num -= 1;
              Game.DisplayText("Cigarette X " + cig_num);
              if (!GTA.Native.Function.Call<bool>("IS_CHAR_ON_ANY_BIKE", Player.Character))
              {
                  GTA.Native.Function.Call("TASK_PLAY_ANIM_SECONDARY_UPPER_BODY", Player.Character, "create_spliff", "amb@smoking_spliff", 2.0f, 0, 0, 0, 0, -2);
              }
              else
              {
                  GTA.Native.Function.Call("TASK_PLAY_ANIM_UPPER_BODY", Player.Character, "create_spliff", "amb@smoking_spliff", 2.0f, 0, 0, 0, 0, -2);
              }
              Wait(1200);
               cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
               while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
               {
                   Wait(0);
               }
              GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
              GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
              Game.DisplayText("Cigarette X " + cig_num + " START SMOKING");
              Wait(800);
              Game.DisplayText("Cigarette X " + cig_num + " START SMOKING");
              Wait(1000);
              Game.DisplayText("Cigarette X " + cig_num + " START SMOKING");
              Wait(500);
              cig_plyer.Delete();
              Wait(50);
              cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
              while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
              {
                  Wait(0);
              }
              int episodes = (int)Game.CurrentEpisode;
              if (episodes == 1)
              {
                  GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1205, -0.119f, 0.145f, -0.01f, 90.1f, 0.0f, 0.0f, 0);
              }
              else if (episodes == 2)
              {
                  GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1205, -0.121f, 0.145f, -0.01f, 90.1f, 0.0f, 0.0f, 0);
              }
              else
              {
                  GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1205, -0.125f, 0.145f, -0.01f, 90.1f, 0.0f, 0.0f, 0);
              }
              GTA.Native.Function.Call("START_PTFX_ON_OBJ", "ambient_cig_smoke", cig_plyer, 0.125f, -0.02f, 0.01f, 0.0f, 0.0f, 0.0f, 1.1f);
              GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
              Player.Character.Health += 10;
              flg_smoking = 4000; //800
              while (flg_smoking >= 1)
              {
                  Wait(50);
                  if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "facials@m_hi", "gest_angry_loop"))
                  {
                      GTA.Native.Function.Call("TASK_PLAY_ANIM_FACIAL", Player.Character, "gest_angry_loop", "facials@m_hi", 1.1f, 1, 0, 0, 0, -2);
                  }
                  flg_smoking -= 1;
                  Smoking_RQ();
                  Smoking_radar_loop();
                  SitDown();
                  if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@burgercart", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@ffood_server", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@hotdogcart", "buy_hotdog_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@nutcart", "buy_nuts_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "customer_juice")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "player_drink"))
                  {
                      cig_plyer.Delete();
                      flg_smoking = 0;
                      Game.DisplayText("Cigarette X " + cig_num + " END SMOKING");
                  }

                  if (Game.isGameKeyPressed(GTA.GameKey.Sprint) && Game.isGameKeyPressed(GTA.GameKey.RadarZoom))
                  {
                      flg_smoking = 0;
                  }
                  if (GTA.Native.Function.Call<bool>("IS_CHAR_SWIMMING", Player.Character) // || GTA.Native.Function.Call<bool>("IS_CHAR_IN_WATER", Player.Character)
                  || GTA.Native.Function.Call<bool>("IS_CHAR_SITTING_IDLE", Player.Character)
                  || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "upset_in_bed_idle")
                  || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "lie_on_bed_l")
                  || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "lie_on_bed_r"))
                  {
                      flg_smoking = 0;
                  }
                  if (!Game.isGameKeyPressed(GTA.GameKey.Sprint) && Game.isGameKeyPressed(GTA.GameKey.RadarZoom))
                  {
                      Wait(1000);
                      if (!Game.isGameKeyPressed(GTA.GameKey.Sprint) && Game.isGameKeyPressed(GTA.GameKey.RadarZoom))
                      {
                          cig_plyer.Delete();
                          Wait(50);
                          cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
                          while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                          {
                              Wait(0);
                          }
                          GTA.Native.Function.Call("START_PTFX_ON_OBJ", "ambient_cig_smoke", cig_plyer, 0.125f, -0.02f, 0.01f, 0.0f, 0.0f, 0.0f, 1.1f);
                          GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                          GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
                          GTA.Native.Function.Call("TASK_PLAY_ANIM_UPPER_BODY", Player.Character, "stand_smoke", "amb@smk_scn_idles", 1.0f, 1, 0, 0, 0, -2);
                          Wait(1000);
                          while (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@smk_scn_idles", "stand_smoke"))
                          {
                              Wait(50);
                              flg_smoking -= 2;
                              Player.Character.Health += 1;
                              if (Game.isGameKeyPressed(GTA.GameKey.Sprint) || Game.isGameKeyPressed(GTA.GameKey.RadarZoom)
                                  || flg_smoking < 1 && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@smk_scn_idles", "stand_smoke"))
                              {
                                  GTA.Native.Function.Call("TASK_PLAY_ANIM_UPPER_BODY", Player.Character, "create_spliff", "amb@smoking_spliff", 0.0f, 0, 0, 0, 0, 1);
                                  cig_plyer.Delete();
                                  Wait(50);
                                  cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
                                  while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                                  {
                                      Wait(0);
                                  }
                                  if (episodes == 1)
                                  {
                                      GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1205, -0.119f, 0.145f, -0.01f, 90.1f, 0.0f, 0.0f, 0);
                                  }
                                  else if (episodes == 2)
                                  {
                                      GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1205, -0.121f, 0.145f, -0.01f, 90.1f, 0.0f, 0.0f, 0);
                                  }
                                  else
                                  {
                                      GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1205, -0.125f, 0.145f, -0.01f, 90.1f, 0.0f, 0.0f, 0);
                                  }
                                  GTA.Native.Function.Call("START_PTFX_ON_OBJ", "ambient_cig_smoke", cig_plyer, 0.125f, -0.02f, 0.01f, 0.0f, 0.0f, 0.0f, 1.1f);
                                  GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
                                  Wait(450);
                              }
                          }
                      }
                  }
                  int a = cRandom.Next(80);
                  if (a == 1)
                  {
                      Smoking_health();
                  }
              }
              Game.DisplayText("Cigarette X " + cig_num + " END SMOKING");
              if (GTA.Native.Function.Call<bool>("IS_PLAYER_CLIMBING", Player) || GTA.Native.Function.Call<bool>("IS_CHAR_SWIMMING", Player.Character)
                  || GTA.Native.Function.Call<bool>("IS_CHAR_IN_AIR", Player.Character) || GTA.Native.Function.Call<bool>("IS_CHAR_GETTING_UP", Player.Character)
                  //|| GTA.Native.Function.Call<bool>("IS_CHAR_IN_WATER", Player.Character) 
                  || GTA.Native.Function.Call<bool>("IS_CHAR_SITTING_IDLE", Player.Character)
                  || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "upset_in_bed_idle")
                  || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "lie_on_bed_l")
                  || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@savegame", "lie_on_bed_r")
                  || GTA.Native.Function.Call<bool>("IS_CHAR_SHOOTING", Player.Character) || GTA.Native.Function.Call<bool>("IS_CHAR_IN_MELEE_COMBAT", Player.Character)
                  || Game.isGameKeyPressed(GTA.GameKey.Aim) || Game.isGameKeyPressed(GTA.GameKey.Reload) || Game.isGameKeyPressed(GTA.GameKey.Attack)
                      ||GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@burgercart", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@ffood_server", "buy_burger_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@hotdogcart", "buy_hotdog_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@nutcart", "buy_nuts_plyr")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "customer_juice")
                      || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@kiosk", "player_drink")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb_down")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb_run")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_idle")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_idle_ambient")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_getoff_top")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_geton")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_climb_down_run")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_geton_topb")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_geton_top")
              || GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "climb_std", "ladder_slide")
                  || (flg_smoking == -99)
                  )
              {
                  cig_plyer.Delete();
              }
              else
              {
                  if (!GTA.Native.Function.Call<bool>("IS_CHAR_ON_ANY_BIKE", Player.Character))
                  {
                      GTA.Native.Function.Call("TASK_PLAY_ANIM_SECONDARY_UPPER_BODY", Player.Character, "smoke_stub_out", "amb@nightclub_ext", 1.0f, 0, 0, 0, 0, 2000);
                  }
                  else
                  {
                      GTA.Native.Function.Call("TASK_PLAY_ANIM_UPPER_BODY", Player.Character, "smoke_stub_out", "amb@nightclub_ext", 1.0f, 0, 0, 0, 0, 2000);
                  }
                  Wait(500);
                  cig_plyer.Delete();
                  Wait(50);
                  cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
                  while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                  {
                      Wait(0);
                  }
                  GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
                  GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                  Wait(1000);
                  GTA.Native.Function.Call("DETACH_OBJECT", cig_plyer, Player.Character);
                  Game.DisplayText("Cigarette X " + cig_num + " END SMOKING");
              }
              Wait(1000);
              Game.DisplayText("Cigarette X " + cig_num + " END SMOKING");
              flg_smoking = 0;
          }
      }


      private void Smoking_radar()
      {
          if (!Game.isGameKeyPressed(GTA.GameKey.Sprint) && Game.isGameKeyPressed(GTA.GameKey.RadarZoom))
          {
              Game.DisplayText("Cigarette X " + cig_num);
              Wait(800);
          }
      }

      private void Smoking_radar_loop()
      {
          if (!Game.isGameKeyPressed(GTA.GameKey.Sprint) && Game.isGameKeyPressed(GTA.GameKey.RadarZoom))
          {
              Game.DisplayText("Cigarette X " + cig_num + " SMOKING");
              Wait(800);
          }
      }

      private void Smoking_health()
      {
          GTA.Native.Function.Call("TRIGGER_PTFX_ON_PED_BONE", "ped_smoke_exhale", Player.Character, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1205, 1.1f);
          Player.Character.Health += 5;
      }

      private void Smoking_RQ()
      {
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@nightclub_ext"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@nightclub_ext");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@smk_scn_idles"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@smk_scn_idles");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@smoking_spliff"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@smoking_spliff");
              Wait(100);
          }

          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@cafe_eat_idles"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@cafe_eat_idles");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@cafe_idles"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@cafe_idles");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@bnch_read_idl"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@bnch_read_idl");
              Wait(100);
          }

          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@bnch_eat_idl"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@bnch_eat_idl");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@cafe_smk_create"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@cafe_smk_create");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@bnch_smk_idl"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@bnch_smk_idl");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@smoking"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@smoking");
              Wait(100);
          }
          while (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@bottle_idle"))
          {
              GTA.Native.Function.Call("REQUEST_ANIMS", "amb@bottle_idle");
              Wait(100);
          }

          //while (!GTA.Native.Function.Call<bool>("HAS_MODEL_LOADED", "cj_dinner_plate_3b"))
          //{
              //GTA.Native.Function.Call("REQUEST_MODEL", "cj_dinner_plate_3b");
              //Wait(100);
          //}
          //while (!GTA.Native.Function.Call<bool>("HAS_MODEL_LOADED", "amb_burg"))
          //{
              //GTA.Native.Function.Call("REQUEST_MODEL", "amb_burg");
              //Wait(100);
          //}
          //while (!GTA.Native.Function.Call<bool>("HAS_MODEL_LOADED", "amb_chocbar"))
          //{
              //GTA.Native.Function.Call("REQUEST_MODEL", "amb_chocbar");
              //Wait(100);
          //}
      }

      private void SitDown()
      {
          if (Game.isGameKeyPressed(GTA.GameKey.Action) && Game.isGameKeyPressed(GTA.GameKey.EnterCar)
              && !GTA.Native.Function.Call<bool>("IS_CHAR_SITTING_IDLE", Player.Character)
              && GTA.Native.Function.Call<bool>("IS_PLAYER_CONTROL_ON", Player) && !GTA.Native.Function.Call<bool>("IS_CHAR_IN_ANY_CAR", Player.Character))
          {
              //super_starA
              if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 23.756f, 976.016f, 14.65f, 6.1f, 6.1f, 2.1f, 0))
              {
                  GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 24.430f, 978.297f, 14.65f, 2.5f);
                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                  GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 24.430f, 978.297f, 14.65f, 225.1f, -2);
                  Wait(3000);
                  Check_SitDown();
              }
              //super_starA
              else
              if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 23.429f, 987.156f, 14.65f, 6.1f, 6.1f, 2.1f, 0))
              {
                  GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 22.592f, 985.100f, 14.65f, 2.5f);
                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                  GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 22.592f, 985.100f, 14.65f, 45.1f, -2);
                  Wait(3000);
                  Check_SitDown();
              }
              //super_starB
              else
              if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -239.667f, 43.939f, 14.711f, 6.1f, 6.1f, 2.1f, 0))
              {
                  GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -236.562f, 42.957f, 14.711f, 2.5f);
                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                  GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -236.579f, 43.554f, 14.211f);
                  GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -237.162f, 42.857f, 14.711f, 135.1f, -2);
                  Wait(3000);
                  Check_SitDown();
              }
              //super_starB
              else
              if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -228.402f, 43.856f, 14.711f, 6.1f, 6.1f, 2.1f, 0))
              {
                  GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -230.202f, 44.999f, 14.711f, 2.5f);
                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                  GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                  GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -230.202f, 44.999f, 14.711f, 315.1f, -2);
                  Wait(3000);
                  Check_SitDown();
              }
              //resutoran
              else
              if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 888.520f, -485.065f, 15.88f, 4.1f, 4.1f, 2.1f, 0))
              {
                  GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 886.641f, -485.749f, 15.98f, 2.5f);
                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                  //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                  GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 886.641f, -485.749f, 16.08f, 180.1f, -2);
                  Wait(3000);
                  Check_SitDown();
              }
              //burgA
              else
                  if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 1647.039f, 226.126f, 25.267f, 6.1f, 6.1f, 2.1f, 0))
                  {
                      GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 1645.971f, 222.967f, 25.267f, 2.5f);
                      GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                      //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                      GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 1645.971f, 222.967f, 25.267f, 90.1f, -2);
                      Wait(3000);
                      Check_SitDown();
                  }
                  //burgB
                  else
                      if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 1104.180f, 1591.196f, 16.912f, 6.1f, 6.1f, 2.1f, 0))
                      {
                          GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 1099.004f, 1587.674f, 16.912f, 2.5f);
                          GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                          //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                          GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 1099.066f, 1587.568f, 16.912f, 225.1f, -2);
                          Wait(3000);
                          Check_SitDown();
                      }
                      //burgC
                      else
                          if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 445.078f, 1511.381f, 16.320f, 6.1f, 6.1f, 2.1f, 0))
                          {
                              GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 441.191f, 1513.011f, 16.320f, 2.5f);
                              GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                              //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                              GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 441.191f, 1513.011f, 16.320f, 135.1f, -2);
                              Wait(3000);
                              Check_SitDown();
                          }
                          //burgD
                          else
                              if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -436.219f, 1194.207f, 13.052f, 6.1f, 6.1f, 2.1f, 0))
                              {
                                  GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -434.775f, 1197.405f, 13.052f, 2.5f);
                                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                  //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                                  GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -434.775f, 1197.405f, 13.052f, 270.1f, -2);
                                  Wait(3000);
                                  Check_SitDown();
                              }
                              //burgE
                              else
                                  if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -180.196f, 287.730f, 14.825f, 6.1f, 6.1f, 2.1f, 0))
                                  {
                                      GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -183.729f, 291.758f, 14.825f, 2.5f);
                                      GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                      //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                                      GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -183.729f, 291.758f, 14.825f, 180.1f, -2);
                                      Wait(3000);
                                      Check_SitDown();
                                  }
                                  //burgF
                                  else
                                      if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -617.436f, 141.752f, 4.816f, 6.1f, 6.1f, 2.1f, 0))
                                      {
                                          GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -623.866f, 142.933f, 4.816f, 2.5f);
                                          GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                          //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                                          GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -623.866f, 142.933f, 4.816f, 180.1f, -2);
                                          Wait(3000);
                                          Check_SitDown();
                                      }
                                      //burgG
                                      else
                                          if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -1006.139f, 1618.770f, 24.318f, 6.1f, 6.1f, 2.1f, 0))
                                          {
                                              GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -1010.343f, 1615.255f, 24.318f, 2.5f);
                                              GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                              //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                                              GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -1010.343f, 1615.255f, 24.318f, 270.1f, -2);
                                              Wait(3000);
                                              Check_SitDown();
                                          }
                                          //bellA
                                          else
                                              if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 1181.926f, 369.076f, 25.108f, 7.1f, 7.1f, 2.1f, 0))
                                              {
                                                  GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 1183.474f, 368.315f, 25.108f, 2.5f);
                                                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                                  //GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -230.402f, 44.999f, 14.711f);
                                                  GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 1183.474f, 368.315f, 25.108f, 270.1f, -2);
                                                  Wait(3000);
                                                  Check_SitDown();
                                              }
                                              //bellB
                                              else
                                                  if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -129.894f, 65.048f, 14.808f, 7.1f, 7.1f, 2.1f, 0))
                                                  {
                                                      GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -129.512f, 66.839f, 14.808f, 2.5f);
                                                      GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                                      GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -129.212f, 66.839f, 14.808f);
                                                      GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -129.212f, 66.839f, 14.808f, 0.1f, -2);
                                                      Wait(3000);
                                                      Check_SitDown();
                                                  }
                                                  //barA
                                                  else
                                                      if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 1142.474f, 737.866f, 35.519f, 4.1f, 4.1f, 2.1f, 0))
                                                      {
                                                          GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", 1141.553f, 736.801f, 35.519f, 2.5f);
                                                          GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                                          GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, 1141.553f, 737.101f, 35.519f);
                                                          GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, 1141.553f, 736.801f, 35.519f, 270.1f, -2);
                                                          Wait(3000);
                                                          Check_SitDown();
                                                      }
                                                      //barB
                                                      else
                                                          if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -433.071f, 454.102f, 10.399f, 4.1f, 4.1f, 2.1f, 0))
                                                          {
                                                              GTA.Native.Function.Call("CLEAR_AREA_OF_CHARS", -432.364f, 455.013f, 10.399f, 2.5f);
                                                              GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                                              GTA.Native.Function.Call("SET_CHAR_COORDINATES", Player.Character, -432.364f, 454.513f, 10.399f);
                                                              GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 2, -432.364f, 455.013f, 10.399f, 90.1f, -2);
                                                              Wait(3000);
                                                              Check_SitDown();
                                                          }
                                                          else
                                                          {
                                                              GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                                                              Wait(50);
                                                              GTA.Native.Pointer x = new GTA.Native.Pointer(typeof(float));
                                                              GTA.Native.Pointer y = new GTA.Native.Pointer(typeof(float));
                                                              GTA.Native.Pointer z = new GTA.Native.Pointer(typeof(float));
                                                              GTA.Native.Function.Call("GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS", Player.Character, 0.0f, 0.6f, 0.0f, x, y, z);
                                                              z += 0.05f;
                                                              GTA.Native.Pointer angle = new GTA.Native.Pointer(typeof(float));
                                                              GTA.Native.Function.Call("GET_CHAR_HEADING", Player.Character, angle);
                                                              angle -= 180.0f;
                                                              GTA.Native.Function.Call("TASK_SIT_DOWN_ON_SEAT", Player.Character, 0, 1, x.ToInputParameter(), y.ToInputParameter(), z.ToInputParameter(), angle.ToInputParameter(), -2);
                                                              Wait(1000);
                                                              Check_SitDown();
                                                          }
              while (GTA.Native.Function.Call<bool>("IS_CHAR_SITTING_IDLE", Player.Character))
              {
                  Wait(50);
                  Smoking_RQ();
                  Smoking_radar();
                  a = cRandom.Next(50);
                  if (a == 0)
                  {
                      Player.Character.Health += 1;
                  }
                  if (Game.isGameKeyPressed(GTA.GameKey.Action))
                  {
                      GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);                      
                  }
                  //cig
                  if (Game.isGameKeyPressed(GTA.GameKey.Jump) && (cig_num >= 1)
                      && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@cafe_smk_create", "smoke_create")
                      && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@bnch_smk_idl", "sit_idle_b"))
                  {
                      Detach_Check();
                      flg_smoking = -11;
                      cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
                      while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                      {
                          Wait(0);
                      }
                      GTA.Native.Function.Call("START_PTFX_ON_OBJ", "ambient_cig_smoke", cig_plyer, 0.125f, -0.02f, 0.01f, 0.0f, 0.0f, 0.0f, 1.1f);
                      GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                      GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);                      
                      if (GTA.Native.Function.Call<bool>("IS_INTERIOR_SCENE"))
                      {
                          GTA.Native.Function.Call("CHANGE_CHAR_SIT_IDLE_ANIM", Player.Character, "amb@cafe_smk_create", "smoke_create", 1);
                          Wait(1000);
                      }
                      else
                      {
                          GTA.Native.Function.Call("CHANGE_CHAR_SIT_IDLE_ANIM", Player.Character, "amb@bnch_smk_idl", "sit_idle_b", 1);
                          Wait(1000);
                      }
                      cig_num -= 1;
                      Game.DisplayText("Cigarette X " + cig_num);
                      Wait(1000);
                      Game.DisplayText("Cigarette X " + cig_num);
                  }
                  if (Game.isGameKeyPressed(GTA.GameKey.Jump) && (cig_num <= 0))
                  {
                      Game.DisplayText("Cigarette X " + cig_num);
                      Wait(1000);
                  }
                  //eat
                  if (Game.isGameKeyPressed(GTA.GameKey.EnterCar)
                      && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@cafe_eat_idles", "sit_eat")
                      && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@bnch_eat_idl", "sit_idle_a"))
                  {                      
                      Detach_Check();
                      flg_smoking = -22;
                      if (GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 24.430f, 978.297f, 14.65f, 2.1f, 2.1f, 2.1f, 0)
                          || GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 22.592f, 985.100f, 14.65f, 2.1f, 2.1f, 2.1f, 0)
                          || GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -237.162f, 42.857f, 14.711f, 2.1f, 2.1f, 2.1f, 0)
                          || GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -230.202f, 44.999f, 14.65f, 2.1f, 2.1f, 2.1f, 0)
                          || GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, 1141.553f, 736.801f, 35.519f, 2.1f, 2.1f, 2.1f, 0)
                          || GTA.Native.Function.Call<bool>("LOCATE_CHAR_ANY_MEANS_3D", Player.Character, -432.364f, 455.013f, 10.399f, 2.1f, 2.1f, 2.1f, 0))
                      {
                          GTA.Native.Function.Call("CHANGE_CHAR_SIT_IDLE_ANIM", Player.Character, "amb@cafe_eat_idles", "sit_eat", 1);
                          cig_plyer = World.CreateObject("cj_dinner_plate_3b", Player.Character.Position.Around(99.0F));
                          while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                          {
                              Wait(0);
                          }
                          GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0);
                          Wait(50);
                          GTA.Native.Function.Call("DETACH_OBJECT", cig_plyer, Player.Character);
                          GTA.Native.Pointer x = new GTA.Native.Pointer(typeof(float));
                          GTA.Native.Pointer y = new GTA.Native.Pointer(typeof(float));
                          GTA.Native.Pointer z = new GTA.Native.Pointer(typeof(float));
                          GTA.Native.Function.Call("GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS", Player.Character, 0.0f, 0.45f, 0.5f, x, y, z);
                          GTA.Native.Function.Call("GET_GROUND_Z_FOR_3D_COORD", x.ToInputParameter(), y.ToInputParameter(), z.ToInputParameter(), z);
                          GTA.Native.Function.Call("SET_OBJECT_COORDINATES", cig_plyer, x.ToInputParameter(), y.ToInputParameter(), z.ToInputParameter());
                          GTA.Native.Pointer angle = new GTA.Native.Pointer(typeof(float));
                          GTA.Native.Function.Call("GET_CHAR_HEADING", Player.Character, angle);
                          GTA.Native.Function.Call("SET_OBJECT_HEADING", cig_plyer, angle.ToInputParameter());
                          Wait(1000);
                      }                          
                      else
                      {
                          GTA.Native.Function.Call("CHANGE_CHAR_SIT_IDLE_ANIM", Player.Character, "amb@bnch_eat_idl", "sit_idle_a", 1);
                          if (GTA.Native.Function.Call<bool>("IS_INTERIOR_SCENE"))
                          {
                              cig_plyer = World.CreateObject("amb_burg", Player.Character.Position.Around(99.0F));
                              while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                              {
                                  Wait(0);
                              }
                              GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, -0.05f, 0.0f, 0.035f, 0.0f, 0.0f, 0.0f, 0);
                              GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer); 
                              Wait(1000);
                          }
                          else
                          {
                              cig_plyer = World.CreateObject("amb_chocbar", Player.Character.Position.Around(99.0F));
                              while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                              {
                                  Wait(0);
                              }
                              GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0);
                              GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer); 
                              Wait(1000);
                          }
                      }
                  }
                  //drink
                  if (Game.isGameKeyPressed(GTA.GameKey.Reload)
                      && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@cafe_idles", "sit_drink_b"))
                  {
                      Detach_Check();
                      flg_smoking = -33;
                      GTA.Native.Function.Call("CHANGE_CHAR_SIT_IDLE_ANIM", Player.Character, "amb@cafe_idles", "sit_drink_b", 1);
                      a = cRandom.Next(3);

                      if (a == 0)
                      {
                          cig_plyer = World.CreateObject("amb_coffee", Player.Character.Position.Around(99.0F));
                          while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                          {
                              Wait(0);
                          }
                          GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0);
                          GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer); 
                          Wait(1000);
                      }
                      else
                      {
                          if (a == 1)
                          {
                              cig_plyer = World.CreateObject("bm_whiskeybottle", Player.Character.Position.Around(99.0F));
                              while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                              {
                                  Wait(0);
                              }
                              GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.1f, 0.06f, -0.23f, 0.0f, 0.0f, 0.0f, 0);
                              GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer); 
                              Wait(1000);
                          }
                          else
                          {
                              if (a == 2)
                              {
                                  cig_plyer = World.CreateObject("bm_wineglass", Player.Character.Position.Around(99.0F));
                                  while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                                  {
                                      Wait(0);
                                  }
                                  GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.1f, 0.02f, 0.035f, 0.0f, 0.0f, 0.0f, 0);
                                  GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer); 
                                  Wait(1000);
                              }
                          }
                      }
                  }
                  //book
                  if (Game.isGameKeyPressed(GTA.GameKey.Sprint)
                      && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "amb@bnch_read_idl", "turn_page"))
                  {
                      Detach_Check();
                      flg_smoking = -44;
                      GTA.Native.Function.Call("CHANGE_CHAR_SIT_IDLE_ANIM", Player.Character, "amb@bnch_read_idl", "turn_page", 1);
                      cig_plyer = World.CreateObject("amb_bookopen", Player.Character.Position.Around(99.0F));
                      while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                      {
                          Wait(0);
                      }
                      GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0);
                      GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer); 
                      Wait(1000);
                  }
              }
              if (GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
              {
                  GTA.Native.Function.Call("DETACH_OBJECT", cig_plyer, Player.Character);
                  GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
              }
              Detach_Check();
              flg_smoking = -99;
          }
          
      }

      private void Detach_Check()
      {
          if (GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
          {
              if (flg_smoking == -11)
              {
                  cig_plyer.Delete();
                  cig_plyer = World.CreateObject("bm_char_fag_f", Player.Character.Position.Around(99.0F));
                  while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", cig_plyer))
                  {
                      Wait(0);
                  }
                  GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", cig_plyer, Player.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                  Wait(200);
                  GTA.Native.Function.Call("DETACH_OBJECT", cig_plyer, Player.Character);
                  GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
              }
              else
              {
                  if (flg_smoking == -22)
                  {
                      cig_plyer.Delete();
                  }
                  else
                  {
                      if (flg_smoking == -33)
                      {
                          GTA.Native.Function.Call("DETACH_OBJECT", cig_plyer, Player.Character);
                          GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
                      }
                      else
                      {
                          if (flg_smoking == -44)
                          {
                              GTA.Native.Function.Call("DETACH_OBJECT", cig_plyer, Player.Character);
                              GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", cig_plyer);
                          }
                          else
                          {
                              if (flg_smoking != 0)
                              {
                                  cig_plyer.Delete();
                              }
                          }
                      }
                  }
              }
          }
      }

      private void Check_SitDown()
      {
          flg_smoking = 130;
          while (!GTA.Native.Function.Call<bool>("IS_CHAR_SITTING_IDLE", Player.Character) && (flg_smoking >= -6))
          {
              Wait(50);
              flg_smoking -= 1;
              if (flg_smoking <= 0)
              {
                  GTA.Native.Function.Call("CLEAR_CHAR_TASKS", Player.Character);
                  Wait(500);
              }
              if (Game.isGameKeyPressed(GTA.GameKey.Action) || Game.isGameKeyPressed(GTA.GameKey.EnterCar) || Game.isGameKeyPressed(GTA.GameKey.MoveRight)
                  || Game.isGameKeyPressed(GTA.GameKey.MoveLeft) || Game.isGameKeyPressed(GTA.GameKey.MoveBackward)
                  || Game.isGameKeyPressed(GTA.GameKey.MoveForward) || Game.isGameKeyPressed(GTA.GameKey.Sprint))
              {
                  flg_smoking -= 5;
              }
          }
      }

   }

}