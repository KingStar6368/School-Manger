using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using School_Manager.Domain.Entities.Catalog.App;

namespace School_Manager.Data.Extensions.Auditing
{
    public class AuditTrail
    {

        public AuditTrail(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public int UserId { get; set; }
        public string? TableName { get; set; }
        public Dictionary<string, object?> KeyValues { get; } = new();
        public Dictionary<string, object?> OldValues { get; } = new();
        public Dictionary<string, object?> NewValues { get; } = new();
        public List<PropertyEntry> TemporaryProperties { get; } = new();
        public TrailType TrailType { get; set; }
        public List<string> ChangedColumns { get; } = new();
        public bool HasTemporaryProperties => TemporaryProperties.Count > 0;

        public Trail ToAuditTrail() =>
            new()
            {
                UserId = UserId,
                Type = TrailType.ToString(),
                TableName = TableName,
                DateTime = DateTime.UtcNow,
                PrimaryKey = Serialize(KeyValues),
                OldValues = OldValues.Count == 0 ? null : Serialize(OldValues),
                NewValues = NewValues.Count == 0 ? null : Serialize(NewValues),
                AffectedColumns = ChangedColumns.Count == 0 ? null : Serialize(ChangedColumns)
            };

        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
    {
        //new StringEnumConverter() { CamelCaseText = true }
        new StringEnumConverter() { NamingStrategy = new CamelCaseNamingStrategy() }
    }
            });
        }

        public string Serialize<T>(T obj, Type type)
        {
            return JsonConvert.SerializeObject(obj, type, new());
        }
    }
}
