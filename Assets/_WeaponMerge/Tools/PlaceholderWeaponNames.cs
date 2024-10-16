using System;
using System.Collections.Generic;

namespace _WeaponMerge.Tools
{
    public static class PlaceholderWeaponNames
    {
        private static readonly List<string> PistolNames = new List<string>
        {
            "Desert Eagle",
            "Glock 17",
            "Beretta 92FS",
            "Colt M1911",
            "SIG Sauer P226",
            "Walther P99",
            "Smith & Wesson M&P",
            "CZ 75",
            "FN Five-seveN",
            "Heckler & Koch USP",
            "Ruger SR9",
            "Taurus PT92",
            "Springfield XD",
            "Browning Hi-Power",
            "Steyr M9-A1",
            "Kahr CW9",
            "Kel-Tec PF-9",
            "Kimber Custom II",
            "Magnum Research Baby Eagle",
            "FNX-45",
            "Beretta PX4 Storm",
            "Walther PPQ",
            "Glock 19",
            "Colt Python",
            "SIG Sauer P320",
            "Smith & Wesson 686",
            "Ruger LCP",
            "Taurus Judge",
            "Springfield Armory Hellcat",
            "Browning 1911-380",
            "Steyr L9-A1",
            "Kahr PM9",
            "Kel-Tec P-11",
            "Kimber Micro 9",
            "Magnum Research Desert Eagle L5",
            "FN 509",
            "Beretta APX",
            "Walther PPK",
            "Glock 26",
            "Colt Anaconda",
            "SIG Sauer P365",
            "Smith & Wesson Bodyguard",
            "Ruger Security-9",
            "Taurus G2C",
            "Springfield Armory XD-S",
            "Browning Buck Mark",
            "Steyr S9-A1",
            "Kahr K9",
            "Kel-Tec PMR-30",
            "Kimber Ultra Carry II"
        };

        public static string GetRandomPistolName()
        {
            var random = new Random();
            int index = random.Next(PistolNames.Count);
            return PistolNames[index];
        }
    }
}