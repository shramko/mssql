using System;

namespace CLR.TransliterationCombinations
{
    public class TransBox:IComparable<TransBox>
    {
        public int GroupId;
        public string TransString;

        public int CompareTo(TransBox other)
        {
            return other.GroupId.CompareTo(this.GroupId);
        }
    }
}
