namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    ///     Transaction object helps maintain transaction depth counts
    /// </summary>
    public class Transaction : ITransaction
    {
        private IDatabase _db;

        public Transaction(IDatabase db)
        {
            _db = db;
            _db.BeginTransaction();
        }

        public void Complete()
        {
            _db.CompleteTransaction();
            _db = null;
        }

        public void Dispose()
        {
            _db?.AbortTransaction();
        }
    }
}