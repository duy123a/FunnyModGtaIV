using GTA;
using System;
using System.Windows.Forms;

namespace TestScriptCS.Scripts
{
	public class ProstituteMod_EX : Script
	{
		private int idle = 0;

		private int initial = 1;

		private int whoreChance;

		private bool isWhoring;

		private bool isSmoking = false;

		private int payment;

		private Vector3 myLocation;

		private Vehicle myVehicle;

		private Model myModel;

		private Ped currentPed;
		public ProstituteMod_EX()
		{
			while (true)
			{
				myLocation = this.Player.Character.Position;
				Random random = new Random();
				myVehicle = World.GetClosestVehicle(myLocation, 8f);

				this.Player.Character.Voice = "F_Y_HOOKER_03_BH2";

				if ((Game.isKeyPressed(Keys.F7)
						|| Game.isGameKeyPressed(GTA.GameKey.RadarZoom))
					&& (this.Player.Character.isInWater == false)
					&& (this.Player.Character.isInCombat == false)
					&& (isSmoking == false))
				{
					isSmoking = true;
					this.Player.Money -= 2;
					if (this.Player.Character.isSittingInVehicle() == false)
					{
						Game.DisplayText("It is cold outside, I should find a client soon... ");
						this.Player.Character.Animation.Play(new AnimationSet("missray1"), "stand_smoke", 5f);
					}
					else
					{
						if (myVehicle.Model.isCar)
						{
							Game.DisplayText("I hope someone can abuse me... ");
							this.Player.Character.Animation.Play(new AnimationSet("missfaustin8"), "carsmoke_passenger", 5f);
						}
						else if (myVehicle.Model.isBike)
						{
							Game.DisplayText("Maybe I should work in the brothel instead... ");
							this.Player.Character.Animation.Play(new AnimationSet("missroman10"), "smoke_sports_bike", 5f);
						}
					}
					this.Wait(12000);
					this.Player.Character.Health += 10;
					isSmoking = false;
				}

				if ((Game.isKeyPressed(Keys.F1)
					|| (Game.isGameKeyPressed(GTA.GameKey.Sprint)
					&& Game.isGameKeyPressed(GTA.GameKey.RadarZoom)))
				&& initial == 1)
				{
					if (this.Player.Character.isInCombat == false && this.Player.Character.isInWater == false)
					{
						if (this.Player.Character.isSittingInVehicle() == false)
						{
							if (idle == 0)
							{
								this.Player.Character.Animation.Play(new AnimationSet("amb@hooker"), "idle_b", 5f);
								GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", this.Player.Character, "SOLICIT", 1, 1, 2);
								idle = 1;
							}
							else if (idle == 1)
							{
								this.Player.Character.Animation.Play(new AnimationSet("amb@hooker"), "idle_c", 5f);
								GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", this.Player.Character, "SOLICIT", 1, 1, 2);
								idle = 2;
							}
							else if (idle == 2)
							{
								this.Player.Character.Animation.Play(new AnimationSet("amb@hooker"), "idle_a", 5f);
								GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", this.Player.Character, "SOLICIT", 1, 1, 2);
								idle = 0;
							}
						}
						else if (this.Player.Character.isSittingInVehicle())
						{
							currentPed = this.Player.Character.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver);
							if (currentPed != null && currentPed.Gender == Gender.Female)
							{
								Game.DisplayText("Let see if you can clean my pussy clean");
								this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_blowjob_intro", 5f);
								this.Wait(6000);
								this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_blowjob_loop", 5f);
								this.Wait(12000);
								this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_blowjob_outro", 5f);
								this.Wait(2000);
								payment = random.Next(20, 50);
								this.Player.Money += payment;
								this.Wait(5000);
								myVehicle.FreezePosition = true;
								this.Player.Character.LeaveVehicle();
								this.Wait(4000);
								myVehicle.FreezePosition = false;
							}
							else if (currentPed != null && currentPed.Gender == Gender.Male)
							{
								var sexType = random.Next(3);
								if (sexType < 2)
								{
									Game.DisplayText("Put it in your mouth, be a good lil whore");
									this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_blowjob_intro", 5f);
									//currentPed.Animation.Play(new AnimationSet("misscar_sex"), "m_blowjob_intro", 5f);
									this.Wait(6000);
									this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_blowjob_loop", 5f);
									//currentPed.Animation.Play(new AnimationSet("misscar_sex"), "m_blowjob_loop", 5f);
									this.Wait(50);
									if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", this.Player.Character, "misscar_sex", "f_blowjob_loop"))
									{
										this.Wait(12000);
									}
									this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_blowjob_outro", 5f);
									//currentPed.Animation.Play(new AnimationSet("misscar_sex"), "m_blowjob_outro", 5f);
									this.Wait(2000);
									payment = random.Next(20, 50);
									this.Player.Money += payment;
									this.Wait(5000);
									myVehicle.FreezePosition = true;
									this.Player.Character.LeaveVehicle();
									this.Wait(4000);
									myVehicle.FreezePosition = false;
								}
								else
								{
									Game.DisplayText("I'm gonna ruin your pussy");
									this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_sex_intro", 5f);
									//currentPed.Animation.Play(new AnimationSet("misscar_sex"), "m_sex_intro", 5f);
									this.Wait(6000);
									GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", this.Player.Character, "HOOKER_SEX", 1, 1, 2);
									this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_sex_loop_low", 5f);
									//currentPed.Animation.Play(new AnimationSet("misscar_sex"), "m_sex_loop", 5f);
									this.Wait(50);
									if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", this.Player.Character, "misscar_sex", "f_sex_loop_low"))
									{
										this.Wait(10000);
										GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", this.Player.Character, "HOOKER_SEX", 1, 1, 2);
										this.Wait(10000);
									}
									this.Player.Character.Animation.Play(new AnimationSet("misscar_sex"), "f_sex_outro", 5f);
									//currentPed.Animation.Play(new AnimationSet("misscar_sex"), "m_sex_outro", 5f);
									this.Wait(2000);
									payment = random.Next(40, 70);
									this.Player.Money += payment;
									this.Wait(5000);
									myVehicle.FreezePosition = true;
									this.Player.Character.LeaveVehicle();
									this.Wait(4000);
									myVehicle.FreezePosition = false;
								}
							}
						}
					}
				}

				if (this.Player.Character.Animation.isPlaying(new AnimationSet("amb@hooker"), "idle_a")
					|| this.Player.Character.Animation.isPlaying(new AnimationSet("amb@hooker"), "idle_b")
					|| this.Player.Character.Animation.isPlaying(new AnimationSet("amb@hooker"), "idle_c"))
				{
					whoreChance = random.Next(10000);

					if (Game.Exists((GTA.@base.Object)(object)myVehicle) && whoreChance < 100 && myVehicle.isDriveable)
					{
						if (myVehicle.Model.isCar && myVehicle.isSeatFree((VehicleSeat)0) && !myVehicle.isSeatFree((VehicleSeat)(-1)))
						{
							this.Wait(50);
							isWhoring = true;
							myVehicle.FreezePosition = true;
							this.Wait(500);
							Game.DisplayText("Press and hold Action button if you want to whore");
							myVehicle.SoundHorn(1000);
							this.Wait(500);
							myVehicle.SoundHorn(2000);
							this.Wait(5000);
						}
					}

					if (Game.isGameKeyPressed((GameKey)3) && isWhoring)
					{
						if (myVehicle.Model.isCar && myVehicle.isSeatFree((VehicleSeat)0) && !myVehicle.isSeatFree((VehicleSeat)(-1)))
						{
							Game.FadeScreenOut(1000, true);
							this.Player.Character.WarpIntoVehicle(myVehicle, (VehicleSeat)0);
							Game.FadeScreenIn(1000, true);
							Game.DisplayText("Press F1 or combo keys to initiate sex... ");
							isWhoring = false;
						}
					}
				}
				else if (Game.Exists((GTA.@base.Object)(object)myVehicle))
				{
					myVehicle.FreezePosition = false;
					isWhoring = false;
				}
				this.Wait(0);
			}
		}
	}
}
