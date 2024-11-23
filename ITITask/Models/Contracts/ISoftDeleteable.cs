namespace ITITask.Models.Contracts
{
    public interface ISoftDeleteable
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateOfDelete { get; set; }  

        public void Delete()
        {
            IsDeleted = true;
            DateOfDelete = DateTime.Now;
        }

        public void UndoDelete()
        {
            IsDeleted = false;
            DateOfDelete = null;
        }
    }
}
