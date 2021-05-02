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
            // case-insensitive and allows white spaces preceding and trailing.

            FiveElement fire = (FiveElement)6;
            // we don't suggest this, unless you are (de)serializing something.
            // ...
            // -2: Metal
            // -1: Water
            // 0: Wood *
            // 1: Fire *
            // 2: Earth *
            // 3: Metal *
            // 4: Water *
            // 5: Wood
            // 6: Fire
            // ...

            Console.WriteLine($"{fire.ToString()}{(int)fire} Melts {metal}{(int)metal}!");
            Console.WriteLine();
            // Output: Fire1 Melts Metal3!
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
