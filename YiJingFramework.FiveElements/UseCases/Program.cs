using System;
using YiJingFramework.FiveElements;

namespace UseCases
{
    class Program
    {
        static void Main(string[] args)
        {
            #region to get or convert elements
            FiveElement wood = FiveElement.Wood;

            _ = FiveElement.TryParse(" metal \t\n", out FiveElement metal);
            _ = FiveElement.TryParse(" 木 \t\n", out _);
            // case-insensitive and allows white spaces preceding and trailing.

            FiveElement fire = (FiveElement)6;
            // we don't suggest this, unless you are (de)serializing something.
            // ...
            // -2: 金 Metal
            // -1: 水 Water
            // 0: 木 Wood *
            // 1: 火 Fire *
            // 2: 土 Earth *
            // 3: 金 Metal *
            // 4: 水 Water *
            // 5: 木 Wood
            // 6: 火 Fire
            // ...

            Console.WriteLine($"{fire.ToString()} {(int)fire} OVERCOMES {metal} {(int)metal}!");
            Console.WriteLine($"{fire.ToString("C")}克{metal:C}");
            Console.WriteLine();
            // Output: Fire 1 OVERCOMES Metal 3!
            // 火克金
            #endregion

            #region to use the relationship between elements
            FiveElement water = wood.GetElement(FiveElementsRelationship.GeneratingMe);
            // water generates wood.
            Console.WriteLine($"fire to {water}: {water.GetRelationship(fire)}");
            // water overcomes fire
            // Output: fire to Water: OvercameByMe
            #endregion
        }
    }
}
