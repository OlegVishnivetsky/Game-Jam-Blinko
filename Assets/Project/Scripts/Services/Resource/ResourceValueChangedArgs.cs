namespace Services.Resource
{
    public struct ResourceValueChangedArgs
    {
        public readonly string ID;
        public readonly float Value;

        public ResourceValueChangedArgs(string iD, float value)
        {
            ID = iD;
            Value = value;
        }
    }
}
