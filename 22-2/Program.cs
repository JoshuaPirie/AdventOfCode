using System;

namespace _22_2 {
    class Program {
        static int Sbosshp = 51;
        static int bossdam = 9;
        static int best = int.MaxValue;
        static int[] spellcost = { 53, 73, 113, 173, 229 };

        static void Main(string[] args) {
            for(int firstspell = 0; firstspell < 5; firstspell++)
                ExecuteRound(Sbosshp, 50 - 1, 500, 0, 0, 0, 0, firstspell);

            Console.WriteLine(best);
            Console.ReadLine();
        }

        static bool ExecuteRound(int bosshp, int playerhp, int mana, int cost, int s_shield, int s_poison, int s_recharge, int spell) {
            int armor;
            // player turn action
            switch(spell) {
                case 0: // magic missile
                    bosshp -= 4;
                    break;
                case 1: // drain
                    bosshp -= 2;
                    playerhp += 2;
                    break;
                case 2: // shield
                    if(s_shield == 0)
                        s_shield = 6;
                    else
                        return false;
                    break;
                case 3: // poison
                    if(s_poison == 0)
                        s_poison = 6;
                    else
                        return false;
                    break;
                case 4: // recharge
                    if(s_recharge == 0)
                        s_recharge = 5;
                    else
                        return false;
                    break;
            }

            mana -= spellcost[spell];
            cost += spellcost[spell];
            if(bosshp <= 0) {
                if(cost < best)
                    best = cost;
                return true;
            }

            // boss turn
            if(s_recharge > 0) {
                mana += 101;
                s_recharge--;
            }
            if(s_poison > 0) {
                bosshp -= 3;
                s_poison--;
            }
            if(s_shield > 0) {
                s_shield--;
                armor = 7;
            }
            else
                armor = 0;
            if(bosshp > 0) {
                if((bossdam - armor) < 1)
                    playerhp--;
                else
                    playerhp -= (bossdam - armor);
            }
            else {
                if(cost < best)
                    best = cost;
                return true;
            }
            playerhp--;  // hard mode bleed effect
            if(playerhp <= 0)
                return false;


            // player turn begin
            if(s_shield > 0)
                s_shield--;
            if(s_recharge > 0) {
                mana += 101;
                s_recharge--;
            }
            if(s_poison > 0) {
                bosshp -= 3;
                s_poison--;
            }
            if(bosshp <= 0) {
                if(cost < best)
                    best = cost;
                return true;
            }
            for(int nextspell = 0; nextspell < 5; nextspell++) {
                if(mana >= spellcost[nextspell])
                    ExecuteRound(bosshp, playerhp, mana, cost, s_shield, s_poison, s_recharge, nextspell);
            }
            return false;
        }
    }
}
