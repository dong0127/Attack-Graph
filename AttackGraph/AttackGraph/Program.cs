using System;
using System.Collections.Generic;

namespace AttackGraph
{
    class Program
    {
       
        
        static void Main(string[] args)
        {
        
            
            Attack attack = new Attack();
            attack.IsAttack();
            Console.WriteLine(attack.Source);
            
            Console.ReadKey();
        }
    }
}
