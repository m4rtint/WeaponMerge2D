using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers.Data
{
    public class WeaponDataProvider : MonoBehaviour
    {
        [Title("Image Icon")]
        [SerializeField] private Sprite[] _weaponIcons;
        [Space]
        [Title("Audio Clips")]
        [SerializeField] private AudioClip[] _pistolAudioClips;

        private static WeaponDataProvider _instance;
        public static WeaponDataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<WeaponDataProvider>();
                    if (_instance == null)
                    {
                        PanicHelper.CheckAndPanicIfNull(_instance);
                    }
                }
                return _instance;
            }
        }
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            
            PanicHelper.CheckAndPanicIfNullOrEmpty(_weaponIcons);
        }
        
        private readonly string[] _pistolNames = new string[]
        {
            "Desert Eagle",
            "Glock 17",
            "Beretta 92FS",
            "Colt 1911",
            "Smith & Wesson M&P",
            "Sig Sauer P226",
            "Walther PPK",
            "FN Five-seveN",
            "CZ 75",
            "Heckler & Koch USP",
            "Ruger SR9",
            "Springfield XD",
            "Taurus PT92",
            "Browning Hi-Power",
            "Kimber Custom II",
            "Steyr M9-A1",
            "Kel-Tec PF-9",
            "Kahr CW9",
            "Makarov PM",
            "Tokarev TT-33",
            "Luger P08",
            "Mauser C96",
            "Webley Mk VI",
            "Nagant M1895",
            "Rex Zero 1",
            "Canik TP9",
            "Hudson H9",
            "IWI Jericho 941",
            "Tanfoglio Witness",
            "Arsenal Firearms Strike One",
            "Bersa Thunder 9",
            "Caracal F",
            "Grand Power K100",
            "Honor Defense Honor Guard",
            "Lionheart LH9",
            "Magnum Research Baby Eagle",
            "Nighthawk Custom GRP",
            "Para Ordnance P14-45",
            "Rock Island Armory 1911",
            "SAR 9",
            "STI Staccato",
            "Taurus G2C",
            "Wilson Combat EDC X9",
            "Zastava CZ999",
            "Arcus 98DA",
            "Baikal MP-446 Viking",
            "Chiappa Rhino",
            "Detonics CombatMaster",
            "EAA Witness Elite Match",
            "FAMAE FN-750",
            "Beretta PX4 Storm",
            "Glock 19",
            "Colt Python",
            "Smith & Wesson 686",
            "Sig Sauer P320",
            "Walther PPQ",
            "FNX-45",
            "CZ P-10 C",
            "Heckler & Koch VP9",
            "Ruger LCP",
            "Springfield Hellcat",
            "Taurus Judge",
            "Browning BDA",
            "Kimber Micro 9",
            "Steyr L9-A1",
            "Kel-Tec P-11",
            "Kahr PM9",
            "Makarov IJ-70",
            "Tokarev M57",
            "Luger Artillery",
            "Mauser HSc",
            "Webley Fosbery",
            "Nagant M1910",
            "Rex Alpha",
            "Canik TP9 Elite",
            "Hudson H9A",
            "IWI Masada",
            "Tanfoglio Stock II",
            "Arsenal Firearms AF2011",
            "Bersa Thunder 380",
            "Caracal C",
            "Grand Power P1",
            "Honor Defense FIST",
            "Lionheart Regulus",
            "Magnum Research Desert Eagle L5",
            "Nighthawk Custom Predator",
            "Para Ordnance Black Ops",
            "Rock Island Armory TAC Ultra",
            "SAR K2P",
            "STI DVC 3-Gun",
            "Taurus 856",
            "Wilson Combat CQB",
            "Zastava M70",
            "Arcus 94",
            "Baikal MP-443 Grach",
            "Chiappa Rhino 60DS",
            "Detonics Scoremaster",
            "EAA Windicator",
            "FAMAE SAF",
            "Beretta 84FS Cheetah",
            "Glock 21",
            "Colt Anaconda",
            "Smith & Wesson 500",
            "Sig Sauer P365",
            "Walther P99",
            "FN Herstal FNP-45",
            "CZ 97B",
            "Heckler & Koch P30",
            "Ruger GP100",
            "Springfield XDM",
            "Taurus Raging Bull",
            "Browning Buck Mark",
            "Kimber Pro Carry II",
            "Steyr S9-A1",
            "Kel-Tec PMR-30",
            "Kahr K9",
            "Makarov PMM",
            "Tokarev TT-30",
            "Luger Navy",
            "Mauser M712",
            "Webley RIC",
            "Nagant M1895 Officer",
            "Rex Zero 2",
            "Canik TP9 SFX",
            "Hudson H9B",
            "IWI Uzi Pro",
            "Tanfoglio Force",
            "Arsenal Firearms Strike B",
            "Bersa BP9CC",
            "Caracal SC",
            "Grand Power X-Calibur",
            "Honor Defense Long Slide",
            "Lionheart LH9N",
            "Magnum Research BFR",
            "Nighthawk Custom AAC",
            "Para Ordnance Expert",
            "Rock Island Armory GI Standard",
            "SAR ST9",
            "STI Edge",
            "Taurus Spectrum",
            "Wilson Combat Sentinel",
            "Zastava CZ999 Compact",
            "Arcus 98DAC",
            "Baikal MP-448 Skyph",
            "Chiappa Rhino 40DS",
            "Detonics Streetmaster",
            "EAA Witness P",
            "FAMAE FN-750 Compact",
            "Beretta 92X",
            "Glock 34",
            "Colt King Cobra",
            "Smith & Wesson 629",
            "Sig Sauer P229",
            "Walther CCP",
            "FN Herstal FNX-9",
            "CZ P-07",
            "Heckler & Koch P2000",
            "Ruger LCR",
            "Springfield XDS",
            "Taurus 1911",
            "Browning 1911-380",
            "Kimber Ultra Carry II",
            "Steyr M40-A1",
            "Kel-Tec Sub-2000",
            "Kahr P380",
            "Makarov PB",
            "Tokarev TT-34",
            "Luger Carbine",
            "Mauser C96 Broomhandle",
            "Webley Mk IV",
            "Nagant M1895 Target",
            "Rex Zero 3",
            "Canik TP9 Elite Combat",
            "Hudson H9C",
            "IWI Jericho 941 F",
            "Tanfoglio Limited Custom",
            "Arsenal Firearms AF1 Strike One",
            "Bersa Thunder Pro",
            "Caracal Enhanced F",
            "Grand Power P11",
            "Honor Defense Sub-Compact",
            "Lionheart LH9 MKII",
            "Magnum Research Desert Eagle XIX",
            "Nighthawk Custom T4",
            "Para Ordnance P13-45",
            "Rock Island Armory M206",
            "SAR B6P",
            "STI Marauder",
            "Taurus PT111 G2",
            "Wilson Combat X-TAC",
            "Zastava M88",
            "Arcus 98DA Compact",
            "Baikal MP-446 Viking Compact",
            "Chiappa Rhino 20DS",
            "Detonics MTX",
            "EAA Witness Hunter",
            "FAMAE FN-750 Tactical",
            "Beretta APX",
            "Glock 43",
            "Colt Cobra",
            "Smith & Wesson 686 Plus",
            "Sig Sauer P238",
            "Walther PPS",
            "FN Herstal FNS-9",
            "CZ P-09",
            "Heckler & Koch P7",
            "Ruger SP101",
            "Springfield XDM Elite",
            "Taurus 85",
            "Browning 1911-22",
            "Kimber Custom TLE II",
            "Steyr M357-A1",
            "Kel-Tec RFB",
            "Kahr T9",
            "Makarov PMG",
            "Tokarev TT-33A",
            "Luger P04",
            "Mauser C96 Red 9",
            "Webley Mk V",
            "Nagant M1895 Officer Model",
            "Rex Zero 4",
            "Canik TP9 SF",
            "Hudson H9D",
            "IWI Jericho 941 R",
            "Tanfoglio Gold Custom",
            "Arsenal Firearms AF2011-A1",
            "Bersa Thunder 22",
            "Caracal Enhanced C",
            "Grand Power P40",
            "Honor Defense Compact",
            "Lionheart LH9C",
            "Magnum Research Desert Eagle L6",
            "Nighthawk Custom GRP Recon",
            "Para Ordnance P10-45",
            "Rock Island Armory M1911-A1",
            "SAR K2",
            "STI DVC Limited",
            "Taurus 605",
            "Wilson Combat EDC X9L",
            "Zastava CZ999 Scorpion",
            "Arcus 98DA Tactical",
            "Baikal MP-446 Viking Tactical",
            "Chiappa Rhino 50DS",
            "Detonics CombatMaster MK VI",
            "EAA Witness Match",
            "FAMAE FN-750 Match"
        };

        public Sprite GetWeaponIcon(int hashCode)
        {
            var index = hashCode % _weaponIcons.Length;
            return _weaponIcons[index];
        }
        
        public string GetWeaponName(int hashCode)
        {
            var index = hashCode % _pistolNames.Length;
            return _pistolNames[index];
        }

        public AudioClip GetWeaponAudio(int hashCode)
        {
            var index = hashCode % _pistolAudioClips.Length;
            return _pistolAudioClips[index];
        }
    }
}
