namespace AA.CommoditiesDashboard.Contracts
{
    public class KeyValuePairDto
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public override string ToString() => $"{Key}-{Value}";
        public override int GetHashCode() =>
            ToString().GetHashCode();
        public override bool Equals(object obj) =>
            obj is KeyValuePairDto dto && dto.Key == Key && dto.Value == Value;
    }
}