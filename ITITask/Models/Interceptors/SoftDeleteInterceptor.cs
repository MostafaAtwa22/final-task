using ITITask.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ITITask.Models.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges (DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
                return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is null || (entry.State is not EntityState.Deleted) || (entry.Entity is not ISoftDeleteable entity))
                    continue;
                entry.State = EntityState.Modified;
                entity.Delete();
            }
            return result;
        }
    }
}
