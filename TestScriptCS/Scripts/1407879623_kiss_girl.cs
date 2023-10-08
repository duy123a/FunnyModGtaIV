using System;
using System.Windows.Forms;
using GTA;
using System.Collections.Generic;


namespace AdultMod
{
    public class AdultOnly : Script
    {
        Random Whoknow;
        Camera JAV;
        Ped JpActor, JpActress;
        Vector3 TokyoHot;
        float MinimusDis, RomeoDis;
        bool KissOnFoot, InAction, IsCarSmall;
        byte JavScene = 0, ActionInCar = 0, MultiStage = 0;
        GTA.Object LoveDiamond = null;
        GTA.Model ModelDiamond = 0x09C35AC6, CarModel;
        GTA.Native.Pointer NativeValue = 0.0;
        public AdultOnly()
        {
            Whoknow = new Random();
            JAV = new Camera();
            JAV.FOV = 50;
            Interval = 100;
            GTA.Game.Console.Print("ADULT MOD V1.0: Hold K then press Enter for help");
            if (Exists(Player.Character))
            {
                JpActor = Player.Character;
                this.KeyDown += new GTA.KeyEventHandler(EighteenPlus);
            }
            else
            {
                GTA.Game.Console.Print("GET PLAYER ADDRESS ...");
                KissOnFoot = true;
                Tick += GetPlayerCharacTer;
            }
        }
        void GetPlayerCharacTer(object sender, EventArgs e)
        {
            GTA.Game.DisplayText("LOADING ...");
            if (KissOnFoot && Exists(Player.Character))
            {
                JpActor = Player.Character;
                KissOnFoot = false;
                this.KeyDown += new GTA.KeyEventHandler(EighteenPlus);
                Tick -= GetPlayerCharacTer;
            }
            else
                Wait(100);
        }
        Model SuperCarList(int i)
        {
            switch (i)
            {
                case 0: return -1041692462;
                case 1: return -682211828;
                case 2: return 1063483177;
                case 3: return 108773431;
                case 4: return 723973206;
                case 5: return -2119578145;
                case 6: return -1097828879;
                case 7: return 418536135;
                case 8: return -2124201592;
                case 9: return 1830407356;
                case 10: return -227741703;
                case 11: return -449022887;
                case 12: return 1264386590;
                case 13: return -1685021548;
                case 14: return 1349725314;
                case 15: return 1923400478;
                case 16: return 1723137093;
                case 17: return -295689028;
                case 18: return 1821991593;
                case 19: return -1896659641;
                case 20: return 1534326199;
                case 21: return -825837129;
                case 22: return -1758379524;
                case 23: return -583281407;
                case 24: return -498054846;
                case 25: return 2006667053;
                default: return 0;
            }
        }
        void WaitToKiss(object sender, EventArgs e)
        {
            if (KissOnFoot)
            {
                if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@dating"))
                {
                    GTA.Native.Function.Call("REQUEST_ANIMS", "amb@dating");
                }
                else
                {
                    if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "amb@dating", "player_kiss") && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "amb@dating", "girl_hug"))
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "amb@dating", "player_kiss", NativeValue);
                    else
                    {
                        GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "player_kiss", "amb@dating", 4.0f, 0, 0, 0, 0, -1);
                        if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "girl_hug", "amb@dating", 4.0f, 0, 0, 0, 0, -1);
                    }
                    if (NativeValue >= 1.0)
                    {
                        if (Exists(JpActress)) RomeoDis = JpActress.Heading;
                        if (Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_PED_ATTACHED_TO_OBJECT", JpActress, LoveDiamond))
                            GTA.Native.Function.Call("DETACH_PED", JpActress, 1);
                        if (Exists(JpActress)) GTA.Native.Function.Call("SET_CHAR_HEADING", JpActress, RomeoDis);
                        NativeValue = 0.0;
                        InAction = false;
                        JavScene = 0;
                        if (JAV.isActive) JAV.Deactivate();
                        Tick -= WaitToKiss;
                    }
                }
            }
            else
            {
                Tick -= WaitToKiss;
                if (Exists(JpActress)) RomeoDis = JpActress.Heading;
                if (Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_PED_ATTACHED_TO_OBJECT", JpActress, LoveDiamond))
                    GTA.Native.Function.Call("DETACH_PED", JpActress, 1);
                if (Exists(JpActress)) GTA.Native.Function.Call("SET_CHAR_HEADING", JpActress, RomeoDis);
                NativeValue = 0.0;
                InAction = false;
                JavScene = 0;
                if (JAV.isActive) JAV.Deactivate();
            }
        }
        void KissInCar(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@dating"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "amb@dating");
            }
            else
            {
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "amb@dating", "car_kiss_ds") && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "amb@dating", "car_kiss_ps"))
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "amb@dating", "car_kiss_ds", NativeValue);
                else
                {
                    GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "car_kiss_ds", "amb@dating", 4.0f, 0, 0, 0, 0, -1);
                    if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "car_kiss_ps", "amb@dating", 4.0f, 0, 0, 0, 0, -1);
                }
                if (NativeValue >= 1.0)
                {
                    NativeValue = 0.0;
                    InAction = false;
                    JavScene = 0;
                    Tick -= KissInCar;
                }
            }
        }
        void KissInCarLoop(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "amb@dating"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "amb@dating");
            }
            else
            {
                if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "amb@dating", "car_kiss_ds") || (Exists(JpActress) && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "amb@dating", "car_kiss_ps")))
                {
                    GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "car_kiss_ds", "amb@dating", 4.0f, 1, 0, 0, 0, -1);
                    if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "car_kiss_ps", "amb@dating", 4.0f, 1, 0, 0, 0, -1);
                }
                if (!Exists(JpActress))
                {
                    GTA.Native.Function.Call("CLEAR_CHAR_TASKS", JpActor);
                    if (Exists(JpActress)) GTA.Native.Function.Call("CLEAR_CHAR_TASKS", JpActress);
                    NativeValue = 0.0;
                    InAction = false;
                    JavScene = 0;
                    Tick -= KissInCarLoop;
                }
            }
        }
        void MakeBabyLowStyle(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "misscar_sex"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "misscar_sex");
            }
            else
            {
                switch (MultiStage)
                {
                    case 0:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_sex_intro_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_sex_intro_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_sex_intro_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_sex_intro_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_sex_intro_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 1;
                        }
                        break;
                    case 1:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_sex_loop_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_sex_loop_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_sex_loop_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_sex_loop_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_sex_loop_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 2;
                        }
                        break;
                    case 2:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_sex_outro_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_sex_outro_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_sex_outro_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_sex_outro_low", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_sex_outro_low", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 0;
                            InAction = false;
                            JavScene = 0;
                            Tick -= MakeBabyLowStyle;
                        }
                        break;
                }
            }
        }
        void HandJob_Low_Tick(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "misscar_sex"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "misscar_sex");
            }
            else
            {
                switch (MultiStage)
                {
                    case 0:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_handjob_intro_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_handjob_intro_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_handjob_intro_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_handjob_intro_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_handjob_intro_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 1;
                        }
                        break;
                    case 1:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_handjob_loop_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_handjob_loop_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_handjob_loop_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_handjob_loop_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_handjob_loop_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 2;
                        }
                        break;
                    case 2:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_handjob_outro_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_handjob_outro_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_handjob_outro_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_handjob_outro_low", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_handjob_outro_low", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 0;
                            InAction = false;
                            JavScene = 0;
                            Tick -= HandJob_Low_Tick;
                        }
                        break;
                }
            }
        }
        void BlowJob_low_Tick(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "misscar_sex"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "misscar_sex");
            }
            else
            {
                switch (MultiStage)
                {
                    case 0:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_blowjob_intro_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_blowjob_intro_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_blowjob_intro_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_blowjob_intro_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_blowjob_intro_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 1;
                        }
                        break;
                    case 1:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_blowjob_loop_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_blowjob_loop_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_blowjob_loop_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_blowjob_loop_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_blowjob_loop_low", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 2;
                        }
                        break;
                    case 2:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_blowjob_outro_low")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_blowjob_outro_low"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_blowjob_outro_low", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_blowjob_outro_low", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_blowjob_outro_low", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 0;
                            InAction = false;
                            JavScene = 0;
                            Tick -= BlowJob_low_Tick;
                        }
                        break;
                }
            }
        }
        void MakeBaby(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "misscar_sex"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "misscar_sex");
            }
            else
            {
                switch (MultiStage)
                {
                    case 0:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_sex_intro")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_sex_intro"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_sex_intro", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_sex_intro", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_sex_intro", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 1;
                        }
                        break;
                    case 1:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_sex_loop")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_sex_loop"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_sex_loop", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_sex_loop", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_sex_loop", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 2;
                        }
                        break;
                    case 2:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_sex_outro")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_sex_outro"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_sex_outro", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_sex_outro", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_sex_outro", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 0;
                            InAction = false;
                            JavScene = 0;
                            Tick -= MakeBaby;
                        }
                        break;
                }
            }
        }
        void Hand_Job_Tick(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "misscar_sex"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "misscar_sex");
            }
            else
            {
                switch (MultiStage)
                {
                    case 0:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_handjob_intro")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_handjob_intro"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_handjob_intro", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_handjob_intro", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_handjob_intro", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 1;
                        }
                        break;
                    case 1:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_handjob_loop")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_handjob_loop"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_handjob_loop", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_handjob_loop", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_handjob_loop", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 2;
                        }
                        break;
                    case 2:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_handjob_outro")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_handjob_outro"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_handjob_outro", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_handjob_outro", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_handjob_outro", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 0;
                            InAction = false;
                            JavScene = 0;
                            Tick -= Hand_Job_Tick;
                        }
                        break;
                }
            }
        }
        void Blow_Job_Tick(object sender, EventArgs e)
        {
            if (!GTA.Native.Function.Call<bool>("HAVE_ANIMS_LOADED", "misscar_sex"))
            {
                GTA.Native.Function.Call("REQUEST_ANIMS", "misscar_sex");
            }
            else
            {
                switch (MultiStage)
                {
                    case 0:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_blowjob_intro")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_blowjob_intro"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_blowjob_intro", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_blowjob_intro", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_blowjob_intro", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 1;
                        }
                        break;
                    case 1:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_blowjob_loop")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_blowjob_loop"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_blowjob_loop", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_blowjob_loop", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_blowjob_loop", "misscar_sex", 4.0f, 0, 0, 0, 1, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 2;
                        }
                        break;
                    case 2:
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActor, "misscar_sex", "m_blowjob_outro")
                            && Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", JpActress, "misscar_sex", "f_blowjob_outro"))
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", JpActor, "misscar_sex", "m_blowjob_outro", NativeValue);
                        else
                        {
                            GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActor, "m_blowjob_outro", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                            if (Exists(JpActress)) GTA.Native.Function.Call("TASK_PLAY_ANIM", JpActress, "f_blowjob_outro", "misscar_sex", 4.0f, 0, 0, 0, 0, -1);
                        }
                        if (NativeValue >= 1.0)
                        {
                            NativeValue = 0.0;
                            MultiStage = 0;
                            InAction = false;
                            JavScene = 0;
                            Tick -= Blow_Job_Tick;
                        }
                        break;
                }
            }
        }
        public void EighteenPlus(object sender, GTA.KeyEventArgs e)
        {
            if (InAction)
            {
                switch (JavScene)
                {
                    case 1:
                        switch (e.Key)
                        {
                            case Keys.C:
                                if (KissOnFoot && InAction && JAV.isActive)
                                {
                                    JAV.Position = JpActor.Position.Around(3.0f) + Vector3.WorldUp * (float)(Whoknow.NextDouble() + 1.0);
                                    JAV.LookAt(JpActor.Position);
                                }
                                break;
                            case Keys.Enter:
                                Tick -= WaitToKiss;
                                if (Exists(JpActress)) RomeoDis = JpActress.Heading;
                                if (Exists(JpActress) && GTA.Native.Function.Call<bool>("IS_PED_ATTACHED_TO_OBJECT", JpActress, LoveDiamond))
                                    GTA.Native.Function.Call("DETACH_PED", JpActress, 1);
                                if (Exists(JpActress)) GTA.Native.Function.Call("SET_CHAR_HEADING", JpActress, RomeoDis);
                                NativeValue = 0.0;
                                InAction = false;
                                JavScene = 0;
                                if (JAV.isActive) JAV.Deactivate();
                                break;
                            default: break;
                        }
                        break;
                    case 2:
                        switch (e.Key)
                        {
                            case Keys.Enter:
                                switch (ActionInCar)
                                {
                                    case 0:
                                        Tick -= KissInCar;
                                        break;
                                    case 1:
                                        Tick -= KissInCarLoop;
                                        break;
                                    case 2:
                                        MultiStage = 0;
                                        if (IsCarSmall)
                                            Tick -= MakeBabyLowStyle;
                                        else
                                            Tick -= MakeBaby;
                                        break;
                                    case 3:
                                        MultiStage = 0;
                                        if (IsCarSmall)
                                            Tick -= BlowJob_low_Tick;
                                        else
                                            Tick -= Blow_Job_Tick;
                                        break;
                                    case 4:
                                        MultiStage = 0;
                                        if (IsCarSmall)
                                            Tick -= HandJob_Low_Tick;
                                        else
                                            Tick -= Hand_Job_Tick;
                                        break;
                                    default: break;
                                }
                                GTA.Native.Function.Call("CLEAR_CHAR_TASKS", JpActor);
                                if (Exists(JpActress)) GTA.Native.Function.Call("CLEAR_CHAR_TASKS", JpActress);
                                NativeValue = 0.0;
                                InAction = false;
                                JavScene = 0;
                                break;
                            default: break;
                        }
                        break;
                    default: break;
                }
            }
            else
                if (GTA.Game.isKeyPressed(Keys.K))
                    switch (e.Key)
                    {
                        case Keys.Enter:
                            GTA.Native.Function.Call("PLAY_SOUND_FROM_PED", -1, "PLAYER_WHISTLE", JpActor);
                            if (!GTA.Game.Console.isActive)
                                GTA.Game.Console.Open();
                            GTA.Game.Console.Print("\n\n----------ADULT MOD'S CONTROL----------\n Hold K then press L to choose ACTION IN CAR MODE\n----------\n Hold K then press Y to toggle Kissing On Foot\n----------\n Hold K then press I to do adult's action\n----------\n**WHEN ADULT's ACTION IS IN PROGRESS**\n----------\n Press C to change the camera's angle\n----------\n Press Enter to stop the Adult's action");
                            break;
                        case Keys.L:
                            ActionInCar++;
                            ActionInCar %= 5;
                            switch (ActionInCar)
                            {
                                case 0: GTA.Game.DisplayText("Regular Kiss in car"); break;
                                case 1: GTA.Game.DisplayText("Can't stop Kissing in car"); break;
                                case 2: GTA.Game.DisplayText("Make baby in car"); break;
                                case 3: GTA.Game.DisplayText("BlowJob in car"); break;
                                case 4: GTA.Game.DisplayText("HandJob in car"); break;
                                default: break;
                            }
                            break;
                        case Keys.Y:
                            if (!KissOnFoot)
                            {
                                if (LoveDiamond == null)
                                    if (GTA.Native.Function.Call<bool>("HAS_MODEL_LOADED", ModelDiamond))
                                    {
                                        LoveDiamond = World.CreateObject(ModelDiamond, JpActor.Position.Around(5.0F));
                                        GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", LoveDiamond, JpActor, 0x1A1, 0.03f, 0.91f, 0.01f, 0.2f, -1.6f, 3.018f, 0);
                                    }
                                    else
                                    {
                                        GTA.Native.Function.Call("REQUEST_MODEL", ModelDiamond);
                                    }
                                if (LoveDiamond != null)
                                {
                                    GTA.Native.Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", "~y~KISSING ON FOOT ENABLE", 4000, 1);                                    
                                    if (Exists(LoveDiamond)) LoveDiamond.Visible = false;
                                    KissOnFoot = true;
                                }
                                else
                                {
                                    GTA.Game.DisplayText("Try Again");
                                }
                            }
                            else
                            {
                                if (Exists(LoveDiamond)) LoveDiamond.Delete();
                                LoveDiamond = null;
                                KissOnFoot = false;
                                GTA.Native.Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", "~p~KISSING ON FOOT DISABLE", 4000, 1);                                
                            }
                            break;
                        case Keys.I:
                            if (!GTA.Native.Function.Call<bool>("IS_CHAR_IN_ANY_CAR", JpActor))
                            {
                                if (KissOnFoot)
                                {
                                    if (GTA.Native.Function.Call<bool>("IS_OBJECT_ATTACHED", LoveDiamond))
                                    {
                                        TokyoHot = JpActor.Position;
                                        MinimusDis = 15.0f;
                                        foreach (Ped WhoIsIt in GTA.World.GetPeds(TokyoHot, MinimusDis))
                                        {
                                            if (WhoIsIt.Exists() && !GTA.Native.Function.Call<bool>("IS_CHAR_MALE", WhoIsIt) && !GTA.Native.Function.Call<bool>("IS_CHAR_IN_ANY_CAR", WhoIsIt) && !GTA.Native.Function.Call<bool>("IS_CHAR_INJURED", WhoIsIt) && WhoIsIt != JpActor)
                                            {
                                                RomeoDis = WhoIsIt.Position.DistanceTo(TokyoHot);
                                                if (RomeoDis < MinimusDis)
                                                {
                                                    JpActress = WhoIsIt;
                                                    MinimusDis = RomeoDis;
                                                }
                                            }
                                        }
                                        if (Exists(JpActress))
                                        {
                                            JAV.Position = JpActor.Position.Around(3.0f) + Vector3.WorldUp;
                                            JAV.LookAt(JpActor.Position);
                                            JAV.Activate();
                                            GTA.Native.Function.Call("ATTACH_PED_TO_OBJECT", JpActress, LoveDiamond, 0, 0, 0, 0, 0, 0, 0, 0);
                                            NativeValue = 0.0;
                                            InAction = true;
                                            JavScene = 1;
                                            Tick += WaitToKiss;
                                        }
                                        else
                                        {
                                            GTA.Game.DisplayText("There is no girl around");
                                        }
                                    }
                                    else
                                    {
                                        if (Exists(LoveDiamond)) LoveDiamond.Delete();
                                        LoveDiamond = null;
                                        KissOnFoot = false;
                                        GTA.Game.DisplayText("PLEASE ENABLE KISS AGAIN!");
                                    }
                                }
                                else
                                {
                                    GTA.Game.DisplayText("YOU HAVE TO ENABLE KISSING ON FOOT FIRST!");
                                }
                            }
                            else
                            {
                                if (Exists(JpActor.CurrentVehicle) && !JpActor.CurrentVehicle.Model.isBike)
                                {
                                    if (JpActor.CurrentVehicle.isSeatFree(VehicleSeat.RightFront))
                                    {
                                        TokyoHot = JpActor.Position;
                                        MinimusDis = 15.0f;
                                        foreach (Ped WhoIsIt in GTA.World.GetPeds(TokyoHot, MinimusDis))
                                        {
                                            if (WhoIsIt.Exists() && !GTA.Native.Function.Call<bool>("IS_CHAR_MALE", WhoIsIt) && !GTA.Native.Function.Call<bool>("IS_CHAR_IN_ANY_CAR", WhoIsIt) && !GTA.Native.Function.Call<bool>("IS_CHAR_INJURED", WhoIsIt) && WhoIsIt != JpActor)
                                            {
                                                RomeoDis = WhoIsIt.Position.DistanceTo(TokyoHot);
                                                if (RomeoDis < MinimusDis)
                                                {
                                                    JpActress = WhoIsIt;
                                                    MinimusDis = RomeoDis;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!GTA.Native.Function.Call<bool>("IS_CHAR_MALE", JpActor.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightFront)))
                                            JpActress = JpActor.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightFront);

                                    }
                                    if (Exists(JpActress))
                                    {
                                        if (Exists(JpActor.CurrentVehicle))
                                        {
                                            JpActress.WarpIntoVehicle(JpActor.CurrentVehicle, VehicleSeat.RightFront);
                                            NativeValue = 0.0;
                                            InAction = true;
                                            JavScene = 2;
                                            switch (ActionInCar)
                                            {
                                                case 0:
                                                    Tick += KissInCar;
                                                    break;
                                                case 1:
                                                    Tick += KissInCarLoop;
                                                    break;
                                                case 2:
                                                    MultiStage = 0;
                                                    IsCarSmall = false;
                                                    if (Exists(JpActor.CurrentVehicle))
                                                        CarModel = JpActor.CurrentVehicle.Model;
                                                    for (int i = 0; i < 26; i++)
                                                    {
                                                        if (CarModel == SuperCarList(i))
                                                        {
                                                            IsCarSmall = true;
                                                            break;
                                                        }
                                                    }
                                                    if (IsCarSmall)
                                                    {
                                                        GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", JpActor, "HOOKER_CAR_SMALL", 1, 1, 2);
                                                        Tick += MakeBabyLowStyle;
                                                    }
                                                    else
                                                    {
                                                        GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", JpActor, "HOOKER_EXPENSIVE", 1, 1, 2);
                                                        Tick += MakeBaby;
                                                    }
                                                    break;
                                                case 3:
                                                    MultiStage = 0;
                                                    IsCarSmall = false;
                                                    if (Exists(JpActor.CurrentVehicle))
                                                        CarModel = JpActor.CurrentVehicle.Model;
                                                    for (int i = 0; i < 26; i++)
                                                    {
                                                        if (CarModel == SuperCarList(i))
                                                        {
                                                            IsCarSmall = true;
                                                            break;
                                                        }
                                                    }
                                                    if (IsCarSmall)
                                                    {
                                                        GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", JpActor, "HOOKER_CAR_SMALL", 1, 1, 2);
                                                        Tick += BlowJob_low_Tick;
                                                    }
                                                    else
                                                    {
                                                        GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", JpActor, "HOOKER_MIDRANGE", 1, 1, 2);
                                                        Tick += Blow_Job_Tick;
                                                    }
                                                    break;
                                                case 4:
                                                    MultiStage = 0;
                                                    IsCarSmall = false;
                                                    if (Exists(JpActor.CurrentVehicle))
                                                        CarModel = JpActor.CurrentVehicle.Model;
                                                    for (int i = 0; i < 26; i++)
                                                    {
                                                        if (CarModel == SuperCarList(i))
                                                        {
                                                            IsCarSmall = true;
                                                            break;
                                                        }
                                                    }
                                                    if (IsCarSmall)
                                                    {
                                                        GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", JpActor, "HOOKER_CAR_SMALL", 1, 1, 2);
                                                        Tick += HandJob_Low_Tick;
                                                    }
                                                    else
                                                    {
                                                        GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", JpActor, "HOOKER_CHEAP", 1, 1, 2);
                                                        Tick += Hand_Job_Tick;
                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            GTA.Game.DisplayText("Not in car");
                                        }
                                    }
                                    else
                                    {
                                        GTA.Game.DisplayText("There is no girl around");
                                    }
                                }
                                else
                                {
                                    GTA.Game.DisplayText("NO ACTION ON BIKE AVAILABLE!");
                                }
                            }
                            break;
                        default: break;
                    }
        }
    }
}