namespace Notes_API.Settings
{
    public interface IMongoDBSettings
    {
        string OwnerCollectionName { get; set; }
        string NoteCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
