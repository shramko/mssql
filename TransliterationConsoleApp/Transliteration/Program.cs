using System;
using System.Collections;
using System.Collections.Generic;

namespace Transliteration
{
    internal class TransBox : IComparable<TransBox>
    {
        public int GroupId;
        public string TransString;

        public int CompareTo(TransBox other)
        {
            return other.GroupId.CompareTo(this.GroupId);
        }
    }

    internal class Program
    {

        private static List<TransBox> _buffer;
        private static void Main(string[] args)
        {
            _buffer = new List<TransBox>();
            var input = Console.ReadLine();

            char[] chArray = input.ToCharArray();
            foreach (char c in chArray)
            {
                Trans(c);
            }
            Console.ReadLine();

            _buffer.Sort();

            int lastGroupId;
            List<TransBox> lastGroup = GetLastGroup(out lastGroupId);

            bool isAllUpper = IsAllUpper(chArray);

            ArrayList list = new ArrayList();

            foreach (var box in lastGroup)
            {
                if (isAllUpper)
                    box.TransString = box.TransString.ToUpper();
                list.Add(box);
            }

            foreach (var box in list)
            {
                Console.WriteLine(isAllUpper ? (box as TransBox).TransString.ToUpper() : (box as TransBox).TransString);
            }


            Console.ReadLine();

        }

        private static bool IsAllUpper(char[] chArray)
        {
            foreach (char c in chArray)
            {
                if (Char.IsLower(c))
                {
                    return false;
                }
            }
            return true;
        }

        private static void Trans(char ch)
        {
            if (_buffer.Count <= 0) _buffer.Add(new TransBox() {GroupId = 0, TransString = ""});
            int lastGroupId;
            List<TransBox> lastGroup = GetLastGroup(out lastGroupId);

            foreach (var b in lastGroup)
            {
                bool isUpper = Char.IsUpper(ch);
                char ch1 = ch.ToString().ToLower().ToCharArray()[0];
                switch (ch1)
                {
                    case 'а':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "А" : "a")
                        });
                        break;
                    case 'б':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "B" : "b")
                        });
                        break;
                    case 'в':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "V" : "v")
                        });
                        break;
                    case 'г':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "G" : "g")
                        });
                        break;
                    case 'д':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "D" : "d")
                        });
                        break;
                    case 'е':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "E" : "e")
                        });
                        break;
                    case 'ё':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "E" : "e")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Yo" : "yo")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Io" : "io")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Jo" : "jo")
                        });
                        break;
                    }
                    case 'ж':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Zh" : "zh")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "J" : "j")
                        });
                        break;
                    }
                    case 'з':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Z" : "z")
                        });
                        break;
                    case 'и':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "I" : "i")
                        });
                        break;
                    case 'й':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Y" : "y")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "I" : "i")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "J" : "j")
                        });
                        break;
                    }
                    case 'к':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "K" : "k")
                        });
                        break;
                    case 'л':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "L" : "l")
                        });
                        break;
                    case 'м':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "M" : "m")
                        });
                        break;
                    case 'н':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "N" : "n")
                        });
                        break;
                    case 'о':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "O" : "o")
                        });
                        break;
                    case 'п':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "P" : "p")
                        });
                        break;
                    case 'р':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "R" : "r")
                        });
                        break;
                    case 'с':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "S" : "s")
                        });
                        break;
                    case 'т':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "T" : "t")
                        });
                        break;
                    case 'у':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "U" : "u")
                        });
                        break;
                    case 'ф':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "F" : "f")
                        });
                        break;
                    case 'х':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "H" : "h")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Kh" : "kh")
                        });
                        break;
                    }
                    case 'ц':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "S" : "s")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Ts" : "ts")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Tc" : "tc")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Cz" : "cz")
                        });
                        break;
                    }
                    case 'ч':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Ch" : "ch")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "C" : "c")
                        });
                        break;
                    }
                    case 'ш':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Sh" : "sh")
                        });
                        break;
                    case 'щ':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Shch" : "shch")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Sch" : "sch")
                        });
                        break;
                    }
                    case 'ъ':
                    {
                        _buffer.Add(new TransBox() {GroupId = lastGroupId + 1, TransString = b.TransString + "'"});
                        _buffer.Add(new TransBox() {GroupId = lastGroupId + 1, TransString = b.TransString + ""});
                        break;
                    }
                    case 'ы':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Y" : "y")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "I" : "i")
                        });
                        break;
                    }
                    case 'ь':
                    {
                        _buffer.Add(new TransBox() {GroupId = lastGroupId + 1, TransString = b.TransString + "'"});
                        _buffer.Add(new TransBox() {GroupId = lastGroupId + 1, TransString = b.TransString + ""});
                        break;
                    }
                    case 'э':
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "E" : "e")
                        });
                        break;
                    case 'ю':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Yu" : "yu")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Iu" : "iu")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Ju" : "ju")
                        });
                        break;
                    }
                    case 'я':
                    {
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Ya" : "ya")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Ia" : "ia")
                        });
                        _buffer.Add(new TransBox()
                        {
                            GroupId = lastGroupId + 1,
                            TransString = b.TransString + (isUpper ? "Ja" : "ja")
                        });
                        break;
                    }
                    default:
                        _buffer.Add(new TransBox() {GroupId = lastGroupId + 1, TransString = b.TransString + ch});
                        break;
                }
            }
        }

        private static List<TransBox> GetLastGroup(out int lastGroupId)
        {
            _buffer.Sort();
            lastGroupId = _buffer[0].GroupId;

            List<TransBox> lastGroup = new List<TransBox>();
            foreach (TransBox transBox in _buffer)
            {
                if (transBox.GroupId == lastGroupId)
                    lastGroup.Add(transBox);
            }
            return lastGroup;
        }
    }
}

