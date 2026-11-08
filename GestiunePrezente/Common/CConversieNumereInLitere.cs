using System.Globalization;

namespace GestiunePrezente.Common
{
    public class CConversieNumereInLitere
    {
        public string PrepNumber(float number)
        {
            if (number == 0)
                return "zero lei";

            if (number >= 10_000_000)
                return "Număr prea mare";

            var parts = number.ToString("0.00", CultureInfo.InvariantCulture).Split('.');
            int intPart = int.Parse(parts[0]);
            int decPart = int.Parse(parts[1]);

            string result = $"{ConversieInLitere(intPart)} lei";

            if (decPart > 0)
            {
                result += $" și {ConversieBani(decPart)} bani";
            }

            return result.Trim();
        }

        private string ConversieInLitere(int number)
        {
            if (number == 0)
                return "zero";

            Span<int> cifre = stackalloc int[9];
            int i = 0, n = number;

            while (n > 0)
            {
                cifre[i++] = n % 10;
                n /= 10;
            }

            string rezultat = "";

            // milioane
            int mil = cifre[6] + cifre[7] * 10;
            if (mil > 0)
                rezultat += mil switch
                {
                    1 => "un milion ",
                    2 => "două milioane ",
                    _ => $"{CreareGrupa(mil)} milioane "
                };

            // mii
            int mii = cifre[3] + cifre[4] * 10 + cifre[5] * 100;
            if (mii > 0)
            {
                rezultat += mii switch
                {
                    1 => "o mie ",
                    2 => "două mii ",
                    _ => $"{CreareGrupa(mii)} mii "
                };
            }

            // restul (sute, zeci, unități)
            int rest = cifre[0] + cifre[1] * 10 + cifre[2] * 100;
            if (rest > 0)
                rezultat += CreareGrupa(rest);

            return rezultat.Trim();
        }

        private static string ConversieBani(int bani)
        {
            bani = Math.Clamp(bani, 0, 99);

            if (bani == 0) return "zero";
            if (bani < 10) return CreareGrupa(bani);
            if (bani < 20) return Numezeci(bani);

            int zeci = bani / 10;
            int unit = bani % 10;
            string rezultat = Zeci(zeci);

            if (unit > 0)
                rezultat += $" și {Unitate(unit)}";

            return rezultat.Trim();
        }

        private static string CreareGrupa(int num)
        {
            if (num == 0) return "";

            if (num < 10) return Unitate(num);
            if (num < 20) return Numezeci(num);
            if (num < 100)
            {
                int zeci = num / 10;
                int unit = num % 10;
                return unit == 0
                    ? Zeci(zeci)
                    : $"{Zeci(zeci)} și {Unitate(unit)}";
            }

            int sute = num / 100;
            int rest = num % 100;

            string prefix = sute switch
            {
                1 => "o sută",
                2 => "două sute",
                _ => $"{Unitate(sute)} sute"
            };

            return rest == 0 ? prefix : $"{prefix} {CreareGrupa(rest)}";
        }

        private static string Unitate(int n) => n switch
        {
            1 => "unu",
            2 => "doi",
            3 => "trei",
            4 => "patru",
            5 => "cinci",
            6 => "șase",
            7 => "șapte",
            8 => "opt",
            9 => "nouă",
            _ => ""
        };

        private static string Numezeci(int n) => n switch
        {
            10 => "zece",
            11 => "unsprezece",
            12 => "doisprezece",
            13 => "treisprezece",
            14 => "paisprezece",
            15 => "cincisprezece",
            16 => "șaisprezece",
            17 => "șaptesprezece",
            18 => "optsprezece",
            19 => "nouăsprezece",
            _ => ""
        };

        private static string Zeci(int n) => n switch
        {
            2 => "douăzeci",
            3 => "treizeci",
            4 => "patruzeci",
            5 => "cincizeci",
            6 => "șaizeci",
            7 => "șaptezeci",
            8 => "optzeci",
            9 => "nouăzeci",
            _ => ""
        };
    }
}
